using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float translateSpeed = 10;
    public float rotationSpeed = 10;
    public Transform trans;

    bool isRotating = false;
    Vector3 rot;

    // Update is called once per frame
    void Update()
    {

        TranslateUpdate();
        RotateUpdate();

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.fieldOfView <= 100)
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
            if (Input.mousePosition.x >= Screen.width * 0.98)
            {
                transform.Translate(trans.right * translateSpeed * Time.deltaTime, Space.World);
            }
            else if (Input.mousePosition.x <= Screen.width * 0.02)
            {
                transform.Translate(-trans.right * translateSpeed * Time.deltaTime, Space.World);
            }

            if (Input.mousePosition.y >= Screen.height * 0.98)
            {
                transform.Translate(trans.forward * translateSpeed * Time.deltaTime, Space.World);
            }
            else if (Input.mousePosition.y <= Screen.height * 0.02)
            {
                transform.Translate(-trans.forward * translateSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    private void RotateUpdate()
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
