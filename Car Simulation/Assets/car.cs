using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class car : MonoBehaviour
{
    [SerializeField]
    private WheelCollider[] wC;
    [SerializeField]
    private GameObject[] wM;
    
    private float torque = 200f;
    private float maxSteerAngle = 30;

    private InputManager inputManager;

    public WheelCollider[] WC { get => wC; set => wC = value; }
    public GameObject[] WM { get => wM; set => wM = value; }
    public float Torque { get => torque; private set { } }
    public float MaxSteerAngle { get => maxSteerAngle; set => maxSteerAngle = value; }

    void Go(float accel ,float steer)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * MaxSteerAngle;
        
        float thrustTorque = accel * Torque;
        
        for(int i = 0; i < 4; i++)
        {
            WC[i].motorTorque = thrustTorque;

            if (i < 2)
            {
                WC[i].steerAngle = steer;
            }

            if (i == 0) { 
                WM[i].transform.Rotate(0, 0, WC[i].rpm / 60 * 360 * Time.deltaTime);
                WM[i].transform.localEulerAngles = new Vector3(0f, (inputManager.steer * maxSteerAngle) + 90f, 0f);
            }
            if (i == 1)
            {
                WM[i].transform.Rotate(0, 0, -WC[i].rpm / 60 * 360 * Time.deltaTime);
                WM[i].transform.localEulerAngles = new Vector3(0f, (inputManager.steer * maxSteerAngle) - 90f, 0f);
            }
            if (i == 2)
                WM[i].transform.Rotate(0, 0, WC[i].rpm / 60 * 360 * Time.deltaTime);
            if (i == 3)
                WM[i].transform.Rotate(0, 0, -WC[i].rpm / 60 * 360 * Time.deltaTime);

        }
    }

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }
    void FixedUpdate()
    {
        Go(inputManager.throttle,inputManager.steer);  
    }
}
