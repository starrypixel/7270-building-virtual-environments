using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target; // The target object to rotate around
    public float speed = 5f; // Rotation speed

    public void RotateCamera()
    {
        if (target != null)
        {
            transform.RotateAround(target.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
