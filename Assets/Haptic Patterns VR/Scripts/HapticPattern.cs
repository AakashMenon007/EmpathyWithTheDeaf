using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace HapticPatterns
{
    [CreateAssetMenu(fileName = "Vibration Pattern", menuName = "XR/Haptic Pattern")]
    public class HapticPattern : ScriptableObject
    {
        [HideInInspector] public AnimationCurve hapticCurve = AnimationCurve.Constant(0, 1f, 1);
        [Space] [Space] [Space] 
        [Range(0, 1)] public float secondaryHandStrengthMultiplier = 1f;

        private float _gradualPlaybackLastValue;
        
        #region Playback
        /// <summary>
        /// Plays the haptic curve  over time on a specified XR Device
        /// </summary>
        /// <param name="device">Specified XR Device</param>
        /// <param name="strengthMultiplier">OPTIONAL strength factor for the vibrations</param>
        public void PlayOverTime(XRBaseInputInteractor device, float strengthMultiplier = 1)
        {
            HapticPatternManager.SchedulePattern(device, this, strengthMultiplier);
        }

        /// <summary>
        /// Recommended to be called every frame inside of Update(). Used to gradually play
        /// a haptic curve with at a specific point in time.
        /// </summary>
        /// <param name="device">The device to play haptic on</param>
        /// <param name="timelinePoint">Current time of haptic this frame, recommended to use values between 0 and 1</param>
        /// <param name="strengthMultiplier">OPTIONAL strength factor for the vibrations</param>
        public void PlayGradually(XRBaseInputInteractor device, float timelinePoint, float strengthMultiplier = 1)
        {
            Interval interval = new Interval(_gradualPlaybackLastValue, timelinePoint);
            float intervalVibrationStrength = GetPeakInInterval(interval);
            intervalVibrationStrength *= strengthMultiplier;
            
            HapticPatternManager.ScheduleVibration(device, intervalVibrationStrength);
            _gradualPlaybackLastValue = timelinePoint;
        }
        #endregion
        
        #region Interactable Playback Wrappers
        /// <summary>
        /// Plays the haptic curve over time on all devices holding the specified
        /// XR Interactable or related interactables (Children and Parents).
        /// Vibration strength on the secondary hand can be altered in the Editor.
        /// </summary>
        /// <param name="heldObject">The interactable that will play vibrations on holding hands</param>
        public void PlayOverTime(XRBaseInteractable heldObject)
        {
            //Primary hand haptic
            XRBaseInputInteractor primaryHand = AttemptToFindPrimaryHand(heldObject);
            if (primaryHand)
                PlayOverTime(primaryHand);

            //Attempt to play haptic on Secondary hand, if enabled
            if (secondaryHandStrengthMultiplier < .01f) return;
            
            XRBaseInputInteractor secondaryHand = AttemptToFindSecondaryHand(heldObject);
            if(secondaryHand)
                PlayOverTime(secondaryHand, secondaryHandStrengthMultiplier);
        }

        /// <summary>
        /// Recommended to be called every frame inside of Update(). Used to gradually play
        /// a haptic curve with at a specific point in time. 
        /// </summary>
        /// <param name="heldObject">The interactable that will play vibrations on holding hands</param>
        /// <param name="timelinePoint">Current time of haptic this frame, recommended to use values between 0 and 1</param>
        public void PlayGradually(XRBaseInteractable  heldObject, float timelinePoint)//TODO implement optional strength Multiplier
        {
            //Primary hand haptic
            XRBaseInputInteractor primaryHand = AttemptToFindPrimaryHand(heldObject);
            if (primaryHand)
                PlayGradually(primaryHand, timelinePoint);
            
            //Attempt to play haptic on Secondary hand, if enabled
            if (secondaryHandStrengthMultiplier < .01f) return;
            
            XRBaseInputInteractor secondaryHand = AttemptToFindSecondaryHand(heldObject);
            if(secondaryHand)
                PlayGradually(secondaryHand, timelinePoint, secondaryHandStrengthMultiplier);
        }
        #endregion
        
        /// <summary>
        /// Will get the strongest vibration peak in haptic curve interval on a specified device.
        /// </summary>
        /// <param name="device">Device to play vibrations on</param>
        /// <param name="interval">Interval to play (start & end point)</param>
        /// <param name="playbackDuration">How long to play the vibration (in seconds)</param>
        /// <param name="strengthMultiplier">Optional vibration strength factor</param>
        public float GetPeakInInterval(Interval interval)//TODO do we need Interval struct
        {
            //Divide interval into x pieces and try to find peaks
            const int peakCheckIntervalDivisions = 20;
            float highestValueInInterval = 0;
            for (int i = 0; i < peakCheckIntervalDivisions; i++)
            {
                float peakDivisionTime = interval.start + (interval.GetDuration() / peakCheckIntervalDivisions) * i;
                float divisionValue = hapticCurve.Evaluate(peakDivisionTime);

                if (divisionValue > highestValueInInterval)
                    highestValueInInterval = divisionValue;
            }
            
            return highestValueInInterval;
        }
        
        /// <summary>
        /// Get the total duration of the haptic curve
        /// </summary>
        /// <returns>Haptic curve duration (in seconds)</returns>
        public float GetDuration()
        {
            if (hapticCurve.keys.Length == 0)
                return 0;
            
            return hapticCurve.keys[^1].time;
        }
        
        #region Hand Retrieval
        private XRBaseInputInteractor AttemptToFindPrimaryHand(XRBaseInteractable heldObject)
        {
            //Return if object isn't held
            if (heldObject.interactorsSelecting.Count == 0)
                return null;

            XRBaseInputInteractor primaryHand = heldObject.interactorsSelecting[0] as XRBaseInputInteractor;
                    
            //Cast was not valid, return
            if (primaryHand == null) return null;

            return primaryHand;
        }

        private XRBaseInputInteractor AttemptToFindSecondaryHand(XRBaseInteractable heldObject)
        {
            //Try to find secondary hand on same interactable
            if(heldObject.interactorsSelecting.Count == 2)
            {
                XRBaseInputInteractor secondaryHand = (XRBaseInputInteractor)heldObject.interactorsSelecting[1];

                if (secondaryHand != null)
                    return secondaryHand;
            }

            //Try to find secondary hand on related (parents and children's) interactables
            List<XRBaseInteractable> relatedInteractables = new List<XRBaseInteractable>();
            //Add interactables in children
            relatedInteractables.AddRange(heldObject.transform.GetComponentsInChildren<XRBaseInteractable>());
            //Add interactables in parents
            relatedInteractables.AddRange(heldObject.transform.GetComponentsInParent<XRBaseInteractable>());

            foreach (XRBaseInteractable relatedInteractable in relatedInteractables)
            {
                //Skip iterating interactable for primary hand
                if(relatedInteractable == heldObject)
                    continue;
                
                if(relatedInteractable.interactorsSelecting.Count > 0)
                {
                    XRBaseInputInteractor secondaryHand = relatedInteractable.interactorsSelecting[0] as XRBaseInputInteractor;
                    
                    //Cast was not valid, continue
                    if (secondaryHand == null) continue;
                     
                    return secondaryHand;
                }
            }

            return null;
        }
        #endregion
    }
    
    public struct Interval
    {
        public float start;
        public float end;

        public Interval(float start, float end)
        {
            this.start = start;
            this.end = end;
        }

        public float GetDuration()
        {
            return end - start;
        }
    }
}