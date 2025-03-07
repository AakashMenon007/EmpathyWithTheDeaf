using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public GameObject stageOneCircle, stageTwoCircle, stageThreeCircle;

    public void StartNextOnboardingStage(string nextOnboardingStage)
    {
        Debug.Log($"Starting dialogue: {nextOnboardingStage}");
        dialogueRunner.StartDialogue(nextOnboardingStage);
    }

    [YarnCommand("setup_stage_two")]
    public void SetupStageTwo()
    {
        Debug.Log("Executing se" +
            "tup_stage_two command.");
        stageOneCircle.SetActive(false);
        stageTwoCircle.SetActive(true);
    }

    [YarnCommand("setup_stage_three")]
    public void SetupStageThree()
    {
        Debug.Log("Executing se" +
            "tup_stage_three command.");
        stageOneCircle.SetActive(false);
        stageTwoCircle.SetActive(true);
    }
}
