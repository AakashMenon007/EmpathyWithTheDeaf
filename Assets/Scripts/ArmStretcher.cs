using System;
using RootMotion.FinalIK;
using UnityEngine;

namespace ReadyPlayerMe.XR
{
    public class ArmStretcher : MonoBehaviour
    {
        [SerializeField] private float differenceMultiplier = 1.8f;

        [SerializeField] private HandTransformInputAction leftHand; // Reference for the left hand
        [SerializeField] private HandTransformInputAction rightHand; // Reference for the right hand

        [SerializeField] private VRIK vrik; // Reference to the VRIK component

        private IKSolverVR solver;

        private void Start()
        {
            if (vrik == null)
            {
                Debug.LogError("VRIK component is not assigned. Please assign it in the inspector.");
                return;
            }

            solver = vrik.solver;

            if (solver != null)
            {
                solver.OnPostUpdate += OnPostUpdate;
            }
            else
            {
                Debug.LogError("Solver is not found on the VRIK component.");
            }
        }

        private void OnDestroy()
        {
            if (solver != null)
            {
                solver.OnPostUpdate -= OnPostUpdate;
            }
        }

        private void OnPostUpdate()
        {
            StretchArm(leftHand, solver.leftArm);
            StretchArm(rightHand, solver.rightArm);
        }

        private void StretchArm(HandTransformInputAction hand, IKSolverVR.Arm arm)
        {
            if (hand.handTransform == null || hand.trueHandPosition == null)
            {
                Debug.LogWarning("HandTransformInputAction is missing references.");
                return;
            }

            var difference = Vector3.Distance(hand.handTransform.position, hand.trueHandPosition.position);
            arm.armLengthMlp = difference > 0.001f ? 1 + difference * differenceMultiplier : 1;
        }
    }

    [Serializable]
    public struct HandTransformInputAction
    {
        public Transform handTransform; // The transform of the hand
        public Transform trueHandPosition; // The true position of the hand
    }
}
