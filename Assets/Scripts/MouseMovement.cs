using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    float xRotation = 0f;
    float yRotation = 0f;

    float top = -90f;
    float bottom = 90f;

    public float MouseSensivity = 600f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, top, bottom);

        yRotation += mouseX;


        // Apply rotations to the local position;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);




    }
}
