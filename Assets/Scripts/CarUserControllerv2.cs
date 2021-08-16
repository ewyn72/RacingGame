using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarUserControllerv2 : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        public GameObject thirdCamera;
        public GameObject firstCamera;
        public GameObject frontCamera;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("ChangeCamera"))
            {
                SwitchView();
            }
        }

        private void SwitchView()
        {
            if (thirdCamera.activeSelf)
            {
                thirdCamera.SetActive(false);
                firstCamera.SetActive(true);
            }
            else if (firstCamera.activeSelf)
            {
                frontCamera.SetActive(true);
                firstCamera.SetActive(false);
            }
            else
            {
                thirdCamera.SetActive(true);
                frontCamera.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            //float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Cancel");
            float throttle = CrossPlatformInputManager.GetAxis("Throttle");
            float brake = CrossPlatformInputManager.GetAxis("Brake");
            
            m_Car.Move(h, throttle, -brake, 0f);
#else  
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}