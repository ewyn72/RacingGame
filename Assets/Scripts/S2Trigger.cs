using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2Trigger : MonoBehaviour
{
    public GameObject FinishLine;
    public GameObject s2Trigger;

    private void OnTriggerEnter(Collider other)
    {
        s2Trigger.SetActive(false);
        FinishLine.SetActive(true);
        TimerController.instance.updateS2();
    }
}
