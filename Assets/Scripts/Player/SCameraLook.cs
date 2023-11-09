using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCameraLook : MonoBehaviour
{
    [SerializeField] float mousexSensitivity = 80f, mouseySensitivity = 80f;
    float xRotation = 0;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mousexSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseySensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.transform.rotation *= Quaternion.Euler(0, mouseX, 0);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
