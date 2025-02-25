using RootMotion.FinalIK;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ReadyPlayerMe.XR
{
    public class HeightCalibrator : MonoBehaviour
    {
        private const float MAX_ALLOWED_HEIGHT = 2.2f;
        private const float MIN_ALLOWED_HEIGHT = 1.35f;
        [SerializeField] private InputActionProperty trackedHeadPosition; // Input action to track head position
        [SerializeField] private VRIK vrik; // Reference to the VRIK component
        [SerializeField] private float defaultAvatarHeight = 1.8f; // Default avatar height

        private float lastCalibratedHeight;
        private float scale;

        private void Start()
        {
            if (vrik == null)
            {
                Debug.LogError("VRIK component is not assigned. Please assign it in the inspector.");
                return;
            }

            lastCalibratedHeight = defaultAvatarHeight;
        }

        public void CalibrateHeight()
        {
            if (vrik == null)
            {
                Debug.LogError("VRIK component is not assigned.");
                return;
            }

            var headPosition = trackedHeadPosition.action.ReadValue<Vector3>();
            lastCalibratedHeight = Mathf.Clamp(headPosition.y, MIN_ALLOWED_HEIGHT, MAX_ALLOWED_HEIGHT);

            CalibrateBody();
        }

        public void CalibrateBody()
        {
            scale = lastCalibratedHeight / defaultAvatarHeight;

            if (vrik.references.root != null)
            {
                vrik.references.root.localScale = new Vector3(scale, scale, scale);
            }

            CalibrateHead();
            CalibrateHands();
        }

        private void CalibrateHead()
        {
            const float scaleDivisionConstant = 2f;

            if (vrik.references.head != null)
            {
                var headScale = 1f + (1f - scale) / scaleDivisionConstant;
                vrik.references.head.localScale = new Vector3(headScale, headScale, headScale);
            }
        }

        private void CalibrateHands()
        {
            if (vrik.references.leftHand != null)
            {
                ScaleBoneToOne(vrik.references.leftHand);
            }

            if (vrik.references.rightHand != null)
            {
                ScaleBoneToOne(vrik.references.rightHand);
            }
        }

        private void ScaleBoneToOne(Transform hand)
        {
            hand.localScale = Vector3.one;
            var lossyScale = hand.lossyScale;

            hand.localScale = new Vector3(1f / lossyScale.x,
                1f / lossyScale.y, 1f / lossyScale.z);
        }
    }
}
