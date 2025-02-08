using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace HapticPatterns
{
    public class HapticPatternManager
    {
        public static readonly Dictionary<XRBaseInputInteractor, float> DeviceFinalVibrationStrength = new (); 
        
        private static List<Vibration> _scheduledVibrations = new ();
        struct Vibration
        {
            public XRBaseInputInteractor Device { get; set; }
            public float Strength { get; set; }
        }
        
        private static List<ScheduledPattern> _scheduledPatterns = new ();
        struct ScheduledPattern
        {
            public XRBaseInputInteractor Device { get; set; }
            public HapticPattern Pattern { get; set; }
            public float StartTime { get; set; }
            public float StrengthMultiplier { get; set; }
        }
        
        private static bool _initialized;
        
        /// <summary>
        /// Substitute for Update() as this is a static method, called every frame once initialized
        /// </summary>
        private static async void Heartbeat()
        {
            _initialized = true;
            
            //Executes every frame
            while (true)
            {
                //Wait until next frame
                await Task.Delay(1);

                SumAllVibrationsThisFrame();
                foreach (XRBaseInputInteractor device in DeviceFinalVibrationStrength.Keys)
                    device.SendHapticImpulse(DeviceFinalVibrationStrength[device], Time.deltaTime);

            }
        }

        private static void SumAllVibrationsThisFrame()
        {
            //Clear last frames vibration strengths
            DeviceFinalVibrationStrength.Clear();
            
            //Convert Sheduled patterns to schedule vibrations
            SchedulePatternVibrationsThisFrame();
            
            //Sum the total vibrational strength for every device
            foreach (var vibration in _scheduledVibrations)
            {
                DeviceFinalVibrationStrength.TryGetValue(vibration.Device, out var existingStrength);
                DeviceFinalVibrationStrength[vibration.Device] = Mathf.Clamp01(existingStrength + vibration.Strength);
            }
            
            _scheduledVibrations.Clear();
        }

        private static void SchedulePatternVibrationsThisFrame()
        {
            //We have to clone the list, since we are modifying it as we iterate
            var scheduledPatternsClone = _scheduledPatterns.ToArray();
            foreach (ScheduledPattern scheduledPattern in scheduledPatternsClone)
            {
                float patternTime = Time.time - scheduledPattern.StartTime;
                
                //If the pattern has played to the end, remove it
                if (patternTime > scheduledPattern.Pattern.GetDuration())
                {
                    _scheduledPatterns.Remove(scheduledPattern);
                    return;
                }

                float vibrationsSinceLastFrame = scheduledPattern.Pattern.GetPeakInInterval(new Interval(patternTime - Time.deltaTime, patternTime));
                ScheduleVibration(scheduledPattern.Device, vibrationsSinceLastFrame * scheduledPattern.StrengthMultiplier);//TODO test
            }
        }

        public static void ScheduleVibration(XRBaseInputInteractor device, float strength01)
        {
            if (!_initialized) Initialize();
            
            _scheduledVibrations.Add(new Vibration
            {
                Device = device, 
                Strength = strength01
            });
        }

        public static void SchedulePattern(XRBaseInputInteractor device, HapticPattern pattern, float strengthMultiplier)
        {
            if (!_initialized) Initialize();
            
            _scheduledPatterns.Add(new ScheduledPattern
            {
                Device = device, 
                Pattern = pattern,
                StartTime = Time.time,
                StrengthMultiplier = strengthMultiplier
            });
        }
        
        private static void Initialize()
        {
            if (_initialized) { Debug.LogWarning("HapticPatternManager was initialized twice, not supposed to happen!"); return; }
            
            //Start Heartbeat loop
            Heartbeat();
        }
    }
}
