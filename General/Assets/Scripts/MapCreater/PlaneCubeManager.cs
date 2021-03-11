using System.Collections;
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
    public Transform getCube(float x, float z)
    {
        int i = (int)((x+10)*7.5 + (z+15)/2);
        return cubes[i];
    }

    public void showGrid()
    {
        foreach (Transform cube in cubes)
        {
            cube.GetComponent<PlaneCube>().showGrid();
        }
    }

    public void cancleGrid()
    {
        foreach (Transform cube in cubes)
        {
            cube.GetComponent<PlaneCube>().cancelGrid();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            showGrid();
        }
    }
}
