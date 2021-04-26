using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float translateSpeed = 10;
    public float rotationSpeed = 10;
    public Transform trans;

    bool isRotating = false;
    bool isTranslate = false;
    Vector3 rot;

    // Update is called once per frame
    void Update()
    {

        TranslateUpdate();
        RotateUpdate();

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 60)
                Camera.main.fieldOfView += 2;
            if (Camera.main.orthographicSize <= 20)
                Camera.main.orthographicSize += 0.5F;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.fieldOfView > 2)
                Camera.main.fieldOfView -= 2;
            if (Camera.main.orthographicSize >= 1)
                Camera.main.orthographicSize -= 0.5F;
        }
    }

    private void TranslateUpdate()
    {
        if (!isRotating)
        {
            if (Input.GetMouseButtonDown(2))
            {
                isTranslate = true;
            }
            else if (Input.GetMouseButtonUp(2))
            {
                isTranslate = false;
            }
            if (isTranslate)
            {
                var mouse_x = Input.GetAxis("Mouse X");
                var mouse_y = Input.GetAxis("Mouse Y");
                Vector3 right = mouse_x * trans.right;
                Vector3 forward = mouse_y * trans.forward;
                transform.Translate(-right - forward, Space.World);
            }
        }
    }

    private void RotateUpdate()
    {
        if (!isTranslate)
        {
            if (Input.GetMouseButtonDown(1))
            {
                isRotating = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isRotating = false;
            }
            if (isRotating)
            {
                //transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime); //摄像机围绕目标旋转
                var mouse_x = Input.GetAxis("Mouse X");//获取鼠标X轴移动
                                                       //var mouse_y = -Input.GetAxis("Mouse Y");//获取鼠标Y轴移动

                transform.RotateAround(Vector3.zero, Vector3.up, mouse_x * 5);
                //transform.RotateAround(Vector3.zero, transform.right, mouse_y * 5);
            }
        }
    }
}
