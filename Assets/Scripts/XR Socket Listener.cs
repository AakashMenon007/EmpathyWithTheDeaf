using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketDeafnessController : MonoBehaviour
{
    [Header("Deafness Simulation Script")]
    public DeafnessSimulatorListener deafnessSimulator; // Reference to the DeafnessSimulatorListener script

    [Header("Socket Interactors")]
    public XRSocketInteractor socket1; // First XR Socket Interactor
    public XRSocketInteractor socket2; // Second XR Socket Interactor
    public string requiredObjectTag = "DeafnessStopper"; // Tag for required objects

    void Update()
    {
        if (deafnessSimulator != null && AreSocketsFilled())
        {
            deafnessSimulator.startSimulation = false; // Turn off the deafness simulation
            Debug.Log("Deafness Simulation Stopped via Socket Interaction.");
        }
    }

    private bool AreSocketsFilled()
    {
        bool socket1Filled = socket1.hasSelection && socket1.GetOldestInteractableSelected().transform.CompareTag(requiredObjectTag);
        bool socket2Filled = socket2.hasSelection && socket2.GetOldestInteractableSelected().transform.CompareTag(requiredObjectTag);

        return socket1Filled && socket2Filled;
    }
}
