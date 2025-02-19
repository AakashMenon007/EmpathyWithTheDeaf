using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnboardingStageTrigger : MonoBehaviour
{
    public YarnCommands yarnCommands;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string OnboardingStage;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            //move to next stage
            yarnCommands.StartNextOnboardingStage(OnboardingStage);
        }
    }
}
