﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomMapMaker : MonoBehaviour
{

    private float _seedX, _seedZ;

    [SerializeField]
    private float _width = 50;
    [SerializeField]
    private float _depth = 50;

    [SerializeField]
    private bool _needToCollider = false;

    [SerializeField]
    private float _maxHeight = 10;

    [SerializeField]
    private float _relief = 15f;

    [SerializeField]
    private float _mapSize = 1f;
    //=================================================================================
    //初期化
    //=================================================================================
    private void Awake()
    {

        transform.localScale = new Vector3(_mapSize, _mapSize, _mapSize);

        _seedX = Random.value * 100f;
        _seedZ = Random.value * 100f;

        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _depth; z++)
            {

                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localPosition = new Vector3(x, 0, z);
                cube.transform.SetParent(transform);
                if (!_needToCollider)
                {
                    Destroy(cube.GetComponent<BoxCollider>());
                }

                SetY(cube);
            }
        }
    }

    private void OnValidate()
    {

        if (!Application.isPlaying)
        {
            return;
        }

        transform.localScale = new Vector3(_mapSize, _mapSize, _mapSize);

        foreach (Transform child in transform)
        {
            SetY(child.gameObject);
        }
    }

    private void SetY(GameObject cube)
    {
        float y = 0;

        float xSample = (cube.transform.localPosition.x + _seedX) / _relief;
        float zSample = (cube.transform.localPosition.z + _seedZ) / _relief;
        float noise = Mathf.PerlinNoise(xSample, zSample);
        y = _maxHeight * noise;

        y = Mathf.Round(y);

        cube.transform.localPosition = new Vector3(cube.transform.localPosition.x, y, cube.transform.localPosition.z);

        Color color = Color.black;
        if (y > _maxHeight * 0.3f)
        {
            ColorUtility.TryParseHtmlString("#019540FF", out color);
        }
        else if (y > _maxHeight * 0.2f)
        {
            ColorUtility.TryParseHtmlString("#2432ADFF", out color);
        }
        else if (y > _maxHeight * 0.1f)
        {
            ColorUtility.TryParseHtmlString("#D4500EFF", out color);
        }
        cube.GetComponent<MeshRenderer>().material.color = color;
    }
}