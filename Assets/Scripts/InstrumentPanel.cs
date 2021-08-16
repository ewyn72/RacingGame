using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class InstrumentPanel : MonoBehaviour
{
    public Slider throttle;
    public Slider brakes;
    public Text speed;
    public Text rpm;
    public GameObject vehicle1;
    public GameObject vehicle2;
    public Image rpmUI;
    private GameObject vehicle;

    // Start is called before the first frame update
    void Awake()
    {
        if(PlayerPrefs.GetInt("car") ==0)
        {
            vehicle = vehicle1;
        }
        else
        {
            vehicle = vehicle2;
        }
        vehicle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        throttle.value = CrossPlatformInputManager.GetAxis("Throttle");
        brakes.value = CrossPlatformInputManager.GetAxis("Brake");
        speed.text = vehicle.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().CurrentSpeed.ToString("F0");
        rpm.text = (800 + (vehicle.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().Revs*6000)).ToString("F0");
        rpmUI.fillAmount = (float)((vehicle.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().Revs * 0.65) + 0.05);
    }
}
