using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapCreater : MonoBehaviour
{
    [Header("地图长宽")]
    public int widthSize = 20;
    public int lengthSize = 30;

    [Header("地图元素")]
    public GameObject cubePre;
    public GameObject platformPre;
    public GameObject castlePre;
    public GameObject housePre;
    public GameObject obstaclePre;

    [Header("高台数量")]
    public int platformNum;
    [Header("高台大小范围,为方便计算，平台实际大小暂时限制为奇数")]
    public int platformSizeMin;
    public int platformSizeMax;
    [Header("高台高度")]
    public int platformHeightMin;
    public int platformHeightMax;
    [Header("房子数量")]
    public int houseNum;
    [Header("障碍物数量")]
    public int obstacleNumMin;
    public int obstacleNumMax;


    private PlaneCubeManager m_PlaneCubeManager;
    private PlaneCube m_CubeScript;

    void Awake()
    {
        m_PlaneCubeManager = GetComponent<PlaneCubeManager>();
        createPlane();
        createPlatform();
        createCastle();
        createHouse();
        createObstacle();
    }
    void createPlane()
    {
        GameObject plane = new GameObject();
        plane.AddComponent<NavMeshSurface>();

        int x = 0;
        int z = 0;
        for (int i = 0; i < widthSize; i++)
        {
            for (int j = 0; j < lengthSize; j++)
            {
                GameObject cube = GameObject.Instantiate(cubePre, new Vector3(x, transform.position.y, z), cubePre.transform.rotation);
                cube.transform.parent = plane.transform;
                m_PlaneCubeManager.AddCube(cube.transform);
                x++;
            }
            z++;
            x = 0;
        }

        plane.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    void createPlatform()
    {
        
        for (int i = 0; i < platformNum; i++)
        {
            int platformSize = (int)(Random.value * (platformSizeMax - platformSizeMin) + platformSizeMin);
            while(platformSize % 2 == 0) platformSize = (int)(Random.value * (platformSizeMax - platformSizeMin) + platformSizeMin);

            int platformHeight = (int)(Random.value * (platformHeightMax - platformHeightMin) + platformHeightMin);
            int areaNum = (int)(Random.value * 6) + 1;
            int x = (int)(Random.value * (10 - platformSize) + platformSize / 2);
            int z = (int)(Random.value * (10 - platformSize) + platformSize / 2);

            if (areaNum > 3)
            {
                x += 10;
                z += 10 * (areaNum - 4);
            }
            else
            {
                z += 10 * (areaNum - 1);
            }

            m_CubeScript = m_PlaneCubeManager.getCube(x, z).GetComponent<PlaneCube>();
            //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量(自身高度的一半)
            float y = (float)(transform.position.y + m_CubeScript.height + platformHeight * platformPre.transform.localScale.y * 0.5f);

            GameObject platform = GameObject.Instantiate(platformPre, new Vector3(z, y, x), platformPre.transform.rotation);
            platform.transform.localScale = new Vector3(platformSize, platformHeight, platformSize);

            //生成平台后地块高度改变
            for(int xi = x - platformSize / 2; xi <= x + platformSize / 2; xi++)
            {
                for (int zi = z - platformSize / 2; zi <= z + platformSize / 2; zi++)
                {
                    m_CubeScript = m_PlaneCubeManager.getCube(xi, zi).GetComponent<PlaneCube>();
                    m_CubeScript.height += platformHeight;
                    m_CubeScript.isShowGrid = false;
                }
            }
        }
    }

    void createCastle()
    {
        int x = 0;
        int z = 0;

        //城堡大小占3格
        int castleSize = 3;

        bool haveSpace = false;
        bool[] areaVisited = new bool[7];
        int tryCnt = 0;
        while (!haveSpace)
        {
            haveSpace = true;
            int areaNum = (int)(Random.value * 6) + 1;
            while (areaVisited[areaNum])
            {
                areaNum++;
                if (areaNum == areaVisited.Length) areaNum = 1;
                tryCnt++;
                if (tryCnt == 6)
                {
                    Debug.Log("Don't have area to create castle!");
                    return;
                }
            }

            x = (int)(Random.value * (10 - castleSize) + castleSize / 2);
            z = (int)(Random.value * (10 - castleSize) + castleSize / 2);

            if (areaNum > 3)
            {
                x += 10;
                z += 10 * (areaNum - 4);
            }
            else
            {
                z += 10 * (areaNum - 1);
            }
            tryCnt++;
            m_CubeScript = m_PlaneCubeManager.getCube(x, z).GetComponent<PlaneCube>();
            float height = m_CubeScript.height;

            for (int xi = x - castleSize / 2; xi <= x + castleSize / 2; xi++)
            {
                for (int zi = z - castleSize / 2; zi <= z + castleSize / 2; zi++)
                {
                    m_CubeScript = m_PlaneCubeManager.getCube(xi, zi).GetComponent<PlaneCube>();
                    
                    if(m_CubeScript.height != height)
                    {
                        haveSpace = false;
                        break;
                    }
                }
                if (!haveSpace) break;
            }

            if(tryCnt == 1000)
            {
                Debug.Log("Don't have space to create castle!");
                return;
            }
        }

        m_CubeScript = m_PlaneCubeManager.getCube(x, z).GetComponent<PlaneCube>();
        //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量(自身高度的一半) 
        float y = (float)(transform.position.y + m_CubeScript.height + castlePre.transform.localScale.y * 0.5f);

        GameObject.Instantiate(castlePre, new Vector3(z, y, x), castlePre.transform.rotation);
        for (int xi = x - 1; xi <= x + 1; xi++)
        {
            for (int zi = z - 1; zi <= z + 1; zi++)
            {
                m_CubeScript = m_PlaneCubeManager.getCube(xi, zi).GetComponent<PlaneCube>();
                m_CubeScript.isShowGrid = false;
            }
        }
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

            m_CubeScript = m_PlaneCubeManager.getCube(x, z).GetComponent<PlaneCube>();
            //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量(自身高度的一半)
            float y = (float)(transform.position.y + m_CubeScript.height + obstaclePre.transform.localScale.y * 0.05f);
            
            GameObject.Instantiate(obstaclePre, new Vector3(z, y, x), obstaclePre.transform.rotation);
            m_CubeScript.isShowGrid = false;
        }
    }

    void createHouse()
    {
        
        for (int i = 0; i < houseNum; i++)
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

            m_CubeScript = m_PlaneCubeManager.getCube(x, z).GetComponent<PlaneCube>();
            //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量(自身高度的一半) （由于城堡尺寸放大了10倍，所以用0.05）
            float y = (float)(transform.position.y + m_CubeScript.height + housePre.transform.localScale.y * 0.05f);

            GameObject.Instantiate(housePre, new Vector3(z, y, x), housePre.transform.rotation);
            m_CubeScript.isShowGrid = false;
        }
    }

    }
