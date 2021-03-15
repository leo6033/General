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

    public int getCubeCount()
    {
        return cubes.Count;
    }

    public Transform getCube(int i)
    {
        return cubes[i];
    }
    public Transform getCube(float x, float z)
    {
        if (x < -10 || x > 8 || z < -15 || z > 13) return null;

        int i = (int)((x+10)*7.5 + (z+15)/2);
        if (i >= cubes.Count || i < 0) return null;
        else return cubes[i];
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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    showGrid();
        //}
    }
}
