using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StableCamera : MonoBehaviour
{
    public GameObject car;
    public float x;
    public float y;
    public float z;

    // Update is called once per frame
    void Update()
    {
        x = car.transform.eulerAngles.x;
        y = car.transform.eulerAngles.y;
        z = car.transform.eulerAngles.z;

        transform.eulerAngles = new Vector3(x - x, y, z - z);
    }
}
