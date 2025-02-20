using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Helpers;
using UnityEngine.Video;

public class TV : MonoBehaviour, ISwitchable
{
    public bool switchedOn;
    public bool SwitchedOn { get { return switchedOn; } set { switchedOn = value; } }

    public VideoPlayer videoPlayer;
    public MeshRenderer screenRenderer;

    public void SwitchOff()
    {
        switchedOn = false;
        screenRenderer.enabled = false;
       videoPlayer.Stop();
    }

    public void SwitchOn()
    {
        switchedOn = true;
        screenRenderer.enabled = true;
        videoPlayer.Play();
    }
}
