using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float x;
    private float y;
    public float sensitivity = -1f;
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    private Vector3 rotateOffset;

    void Start()
    {
        offset = transform.position - player.transform.position; 
        //Cursor.lockState = CursorLockMode.Locked;  // Hide the cursor
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;  // Follow the player with a smooth transition
        //transform.rotation = player.transform.rotation;  // Keep the camera facing the player
        
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        rotateOffset = new Vector3(x, y * sensitivity, 0);
        transform.eulerAngles = transform.eulerAngles - rotateOffset;
    }

}
