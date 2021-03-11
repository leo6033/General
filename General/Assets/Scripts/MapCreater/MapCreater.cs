using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapCreater : MonoBehaviour
{
    [Header("地板位置示意 /*脚本开始时会删除*/")]
    public GameObject Plane;

    [Header("地图长宽")]
    public int widthSize = 20;
    public int lengthSize = 30;
    public int cubeSize = 2;

    [Header("地图元素")]
    public GameObject cubePre;
    public GameObject platformPre;
    public GameObject castlePre;
    public GameObject housePre;
    public GameObject obstaclePre;

    [Header("高台数量")]
    public int platformNum;
    [Header("高台大小范围")]
    public int platformSizeMin;
    public int platformSizeMax;
    [Header("高台高度")]
    public int platformHeightMin;
    public int platformHeightMax;
    
    [Header("障碍物数量")]
    public int obstacleNumMin;
    public int obstacleNumMax;


    [Header("城堡位置")]
    public Transform castlePos;
    public List<Transform> housePoss;

    [HideInInspector] public List<GameObject> houses;


    private PlaneCubeManager m_PlaneCubeManager;
    private PlaneCube m_CubeScript;

    //初始位置
    private float x;
    private float z;

    void Awake()
    {
        Destroy(Plane);
        x = -widthSize / 2;
        z = -lengthSize / 2;
        m_PlaneCubeManager = GetComponent<PlaneCubeManager>();

        createPlane();
        createCastle();
        createHouse();
        //createPlatform();
        createObstacle();
    }
    void createPlane()
    {
        float xi = x;
        float zi = z;
        GameObject plane = new GameObject();
        plane.AddComponent<NavMeshSurface>();

        int cnt = 0;
        for (int i = 0; i < widthSize/cubeSize; i++, xi += cubeSize)
        {
            for (int j = 0; j < lengthSize/cubeSize; j++, zi += cubeSize)
            {
                GameObject cube = GameObject.Instantiate(cubePre, new Vector3(xi, transform.position.y, zi), cubePre.transform.rotation);
                cube.transform.parent = plane.transform;
                cube.name = "Cube" + cnt++;
                m_PlaneCubeManager.AddCube(cube.transform);
            }
            zi = z;
        }

        plane.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void createPlatform()
    {

    }


    void createCastle()
    {
        Vector3 pos = castlePos.position;

        m_CubeScript = m_PlaneCubeManager.getCube(pos.x, pos.z).GetComponent<PlaneCube>();
        m_CubeScript.isShowGrid = false;
        //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量
        float y = (float)(transform.position.y + m_CubeScript.height + castlePre.transform.localScale.y);
        pos.y = y;
        
        GameObject castle = GameObject.Instantiate(castlePre, pos, castlePre.transform.rotation);
        houses.Add(castle);
    }

    void createObstacle()
    {
        int obstacleNum = (int)(Random.value * (obstacleNumMax - obstacleNumMin) + obstacleNumMin);
        for (int i = 0; i < obstacleNum; i++)
        {
            int areaNum = (int)(Random.value * 6) + 1;
            int xi = (int)(Random.value * 10);
            int zi = (int)(Random.value * 10);

            Vector3 pos = calPos(areaNum, xi, zi);
            if (pos.x % 2 != 0) pos.x++;
            if (pos.x > widthSize / 2 - 2) pos.x -= 2;
            if (pos.z % 2 == 0) pos.z++;
            if (pos.z > lengthSize / 2 - 2) pos.z -= 2;

            m_CubeScript = m_PlaneCubeManager.getCube(pos.x, pos.z).GetComponent<PlaneCube>();
            if (!m_CubeScript.isShowGrid)
            {
                i--;
                continue;
            }
            m_CubeScript.isShowGrid = false;
            //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量(自身高度的一半)
            float y = (float)(transform.position.y + m_CubeScript.height + obstaclePre.transform.localScale.y);
            pos.y = y;
            GameObject.Instantiate(obstaclePre, pos, obstaclePre.transform.rotation);
        }
    }

    void createHouse()
    {
        for (int i = 0; i < housePoss.Count; i++)
        {
            Vector3 pos = housePoss[i].position;
            m_CubeScript = m_PlaneCubeManager.getCube(pos.x, pos.z).GetComponent<PlaneCube>();
            //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量
            float y = (float)(transform.position.y + m_CubeScript.height + housePre.transform.localScale.y * 0.05f);
            pos.y = y;

            GameObject house = GameObject.Instantiate(housePre, pos, housePre.transform.rotation);
            houses.Add(house);
            m_CubeScript.isShowGrid = false;
        }
    }

    Vector3 calPos(int areaNum, float xi, float zi)
    {
        if (areaNum > 3)
        {
            zi -= (15 - 10 * (areaNum - 4));
        }
        else
        {
            xi -= 10;
            zi -= (15 - 10 * (areaNum - 1));
        }
        Vector3 ret = new Vector3();
        ret.x = xi;
        ret.z = zi;
        return ret;
    }
}
