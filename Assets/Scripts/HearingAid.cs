using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HearingAid : MonoBehaviour
{
    public void RemoveRidgedbody()
    {
       Rigidbody rb = GetComponent<Rigidbody>();
       Destroy(rb);
    }
}
