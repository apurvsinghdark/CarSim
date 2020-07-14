using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject car;
    public float distance = 5f;
    public float height = 2f;
    public float dampening = 1f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, car.transform.position + car.transform.TransformDirection(new Vector3(0f, height, -distance)), dampening * Time.deltaTime);
        transform.LookAt(car.transform);
    }
}