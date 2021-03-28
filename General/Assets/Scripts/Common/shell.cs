using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shell : MonoBehaviour
{

    public Transform target;

    private float v;
    // Start is called before the first frame update
    void Start()
    {
        v = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        Quaternion rotate = Quaternion.LookRotation(dir);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, 30);
        transform.position = transform.position + dir * v * Time.deltaTime;
    }
}
