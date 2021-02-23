using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    [Header("地图长宽")]
    public int widthSize = 20;
    public int lengthSize = 30;

    [Header("地图元素")]
    public GameObject cubePre;
    public GameObject housePre;
    public GameObject obstaclePre;

    [Header("房子数量")]
    public int houseNum;
    [Header("障碍物数量")]
    public int obstacleNumMin;
    public int obstacleNumMax;

    void Awake()
    {
        createPlane();
        createHouse();
        createObstacle();
    }

    void createObstacle()
    {
        int obstacleNum = (int)(Random.value * (obstacleNumMax - obstacleNumMin) + obstacleNumMin);
        for (int i = 0; i < obstacleNum; i++)
        {
            int areaNum = (int)(Random.value * 6) + 1;
            int x = (int)(Random.value * 10);
            int z = (int)(Random.value * 10);

            if (areaNum > 3)
            {
                x += 10;
                z += 10 * (areaNum - 4);
            }
            else
            {
                z += 10 * (areaNum - 1);
            }

            float y = (float)(transform.position.y + 0.75);
            GameObject.Instantiate(obstaclePre, new Vector3(z, y, x), obstaclePre.transform.rotation);
        }
    }

    void createHouse()
    {
        for(int i = 0; i < houseNum; i++)
        {
            int areaNum = (int)(Random.value * 6)+1;
            int x = (int)(Random.value * 10);
            int z = (int)(Random.value * 10);

            if (areaNum > 3)
            {
                x += 10;
                z += 10 * (areaNum - 4);
            }
            else
            {
                z += 10 * (areaNum - 1);
            }
            
            float y = (float)(transform.position.y + 0.75);
            GameObject.Instantiate(housePre, new Vector3(z, y, x), housePre.transform.rotation);
        }
    }

    void createPlane()
    {
        int x = 0;
        int z = 0;
        for (int i = 0; i < widthSize; i++)
        {
            for (int j = 0; j < lengthSize; j++)
            {
                GameObject.Instantiate(cubePre, new Vector3(x, transform.position.y, z), cubePre.transform.rotation);
                x++;
            }
            z++;
            x = 0;
        }
    }
}
