using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class YarnCommands : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public GameObject stageOneCircle, stageTwoCircle, stageThreeObject;

    public void StartNextOnboardingStage(string nextOnboardingStage)
    {
        Debug.Log($"Starting dialogue: {nextOnboardingStage}");
        dialogueRunner.StartDialogue(nextOnboardingStage);
    }

    [YarnCommand("setup_stage_two")]
    public void SetupStageTwo()
    {
        Debug.Log("Executing setup_stage_two command.");
        stageOneCircle.SetActive(false);
        stageTwoCircle.SetActive(true);
    }

    [YarnCommand("setup_stage_three")]
    public void SetupStageThree()
    {
        Debug.Log("Executing setup_stage_three command.");
        stageTwoCircle.SetActive(false);
        stageThreeObject.SetActive(true);
    }

    [YarnCommand("restart_onboarding")]
    public void RestartOnboarding()
    {
        Debug.Log("Restarting onboarding.");
        SceneManager.LoadScene("onboardingDemo");
    }

    [YarnCommand("begin_main_experience")]
    public void BeginMainExperience()
    {
        Debug.Log("Beginning main experience.");
        SceneManager.LoadScene(1);
    }
}
