using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace HapticPatterns.Samples
{
    /// <summary>
    /// Visualises haptics on a material, used in the trailer to convey vibration
    /// </summary>
    [RequireComponent(typeof(XRDirectInteractor))]
    public class HandHapticVisualiser : MonoBehaviour
    {
        public Renderer rendererToVisualize;
        [ColorUsage(false, true)]public Color vibrationColor;
        public float vibrateColorFalloffSpeed;

        private Color _originalColor;
        private float _currentColorBlend01;
        private XRDirectInteractor _directInteractor;
        
        // Update is called once per frame
        void Update()
        {
            //If current vibration is stronger than blend value, update blend value
            _currentColorBlend01 = Mathf.Max(_currentColorBlend01, VibrationStrengthThisFrame());

            //Slowly return blend value to 0, based on speed
            _currentColorBlend01 = Mathf.Clamp01(_currentColorBlend01 - vibrateColorFalloffSpeed * Time.deltaTime);

            //Blend colors based upon blend value and apply to renderer
            Color blendedColor = Color.Lerp(_originalColor, vibrationColor, _currentColorBlend01);
            rendererToVisualize.material.color = blendedColor;
        }

        private float VibrationStrengthThisFrame()
        {
            HapticPatternManager.DeviceFinalVibrationStrength.TryGetValue(_directInteractor, out var strength);

            return strength;
        }
        
        void Awake()
        {
            _originalColor = rendererToVisualize.material.color;
            _directInteractor = GetComponent<XRDirectInteractor>();
        }
    }
}
