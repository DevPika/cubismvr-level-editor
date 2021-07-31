using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotateSensitivity = 1f;
    public float scrollWheelSensitivity = 3f;
    public float flySensitivity = 0.1f;
    public float panSensitivity = 0.1f;
    public bool isVerticalInverted;

    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    private void Start() {
        xRotation = transform.rotation.eulerAngles.x;
        yRotation = transform.rotation.eulerAngles.y;
    }

    void FixedUpdate() {
        // Vector3 origin = transform.position + transform.forward * 20f;
        if (Input.GetMouseButton(1)) {
            // adapted from https://answers.unity.com/questions/1397655/how-do-i-move-a-camera-with-mouse.html
            // not the effect I was looking for, but still works

            //float rotateHorizontal = Input.GetAxis("Mouse X");
            //float rotateVertical = Input.GetAxis("Mouse Y") * (isVerticalInverted ? -1 : 1);
            //transform.RotateAround(origin, Vector3.up, rotateHorizontal * rotateSensitivity);
            //transform.RotateAround(Vector3.zero, transform.right, rotateVertical * rotateSensitivity);

            // finally used a standard FPS camera implementation
            float mouseX = Input.GetAxis("Mouse X") * rotateSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * rotateSensitivity;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            transform.rotation = Quaternion.Euler(new Vector3(xRotation, yRotation, 0.0f));


            // fly only when right click held down
            if (Input.GetKey(KeyCode.W)) {
                transform.position += transform.forward * flySensitivity;
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.position -= transform.right * flySensitivity;
            }
            if (Input.GetKey(KeyCode.D)) {
                transform.position += transform.right * flySensitivity;
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.position -= transform.forward * flySensitivity;
            }
        }

        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0) {
            transform.position += transform.forward * scrollWheel * scrollWheelSensitivity;
        }

        if (Input.GetMouseButton(2)) { // middle click to pan / drag view
            float panHorizontal = Input.GetAxis("Mouse X");
            float panVertical = Input.GetAxis("Mouse Y");

            // move camera in opposite direction of drag
            transform.position -= (transform.right * panHorizontal + transform.up * panVertical) * panSensitivity;
        }
    }
}
