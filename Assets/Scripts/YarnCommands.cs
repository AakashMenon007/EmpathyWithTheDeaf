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
        dialogueRunner.StartDialogue(nextOnboardingStage);
    }

    [YarnCommand("setup_stage_two")]
    public void SetupStageTwo()
    {
        stageOneCircle.SetActive(false);
        stageTwoCircle.SetActive(true);

    }

    [YarnCommand("setup_stage_three")]
    public void SetupStageThree()
    {
        stageTwoCircle.SetActive(false);
        stageThreeObject.SetActive(true);
    }
    [YarnCommand("Restart Onboarding")]
     public void RestartOnboarding()
    {
        SceneManager.LoadScene("onboardingDemo");
    }

    [YarnCommand("begin_main_experience")]
    public void BeginMainExperience()
    {
        SceneManager.LoadScene(1);
    }



}
