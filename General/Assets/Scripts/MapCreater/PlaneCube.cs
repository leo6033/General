using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCube : MonoBehaviour
{
    public float height;

    private void Awake()
    {
        height = transform.position.y + transform.localScale.y * 0.5f;
    }
}
