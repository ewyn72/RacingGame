using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1Trigger : MonoBehaviour
{
    public GameObject s2Trigger;
    public GameObject s1Trigger;

    private void OnTriggerEnter(Collider other)
    {
        s1Trigger.SetActive(false);
        s2Trigger.SetActive(true);
        TimerController.instance.updateS1();
    }
}
