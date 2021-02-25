﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCubeManager : MonoBehaviour
{
    List<Transform> cubes = new List<Transform>();

    public void AddCube(Transform cube)
    {
        cubes.Add(cube);
    }

    public Transform getCube(int i)
    {
        return cubes[i];
    }
    public Transform getCube(int x, int z)
    {
        int i = x * 30 + z;
        return cubes[i];
    }

}