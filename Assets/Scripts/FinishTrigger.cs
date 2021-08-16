using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    public GameObject FinishLine;
    public GameObject s1Trigger;

    private void OnTriggerEnter(Collider other)
    {
        FinishLine.SetActive(false);
        s1Trigger.SetActive(true);
        TimerController.instance.updateS3();
        TimerController.instance.Lap();
    }
}
