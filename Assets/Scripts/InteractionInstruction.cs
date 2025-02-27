using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionInstruction : MonoBehaviour
{
    public GameObject instructionCanvasKettle; // Assign the Canvas in Inspector for the kettle
    public GameObject instructionCanvasPhone; // Assign the Canvas in Inspector for the phone


    private void Start()
    {
        if (instructionCanvasKettle != null && instructionCanvasPhone != null)
        {
            instructionCanvasKettle.SetActive(false); // Ensure the canvas starts hidden
            instructionCanvasPhone.SetActive(false); // Ensure the canvas starts hidden
        }
    }

    public void ShowInstruction()
    {
        if (instructionCanvasKettle != null)
        {
            instructionCanvasKettle.SetActive(true);
        }
        if (instructionCanvasPhone != null)
        {
            instructionCanvasPhone.SetActive(true);
        }
    }

    public void HideInstruction()
    {
        if (instructionCanvasKettle != null)
        {
            instructionCanvasKettle.SetActive(false);
        }
        if (instructionCanvasPhone != null)
        {
            instructionCanvasPhone.SetActive(false);
        }
    }
}
