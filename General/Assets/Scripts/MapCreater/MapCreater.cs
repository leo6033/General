using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapCreater : MonoBehaviour
{
    public NavMeshAgent player;

    [Header("地板位置示意 /*脚本开始时会删除*/")]
    public GameObject Plane;

    [Header("地图长宽")]
    public int widthSize = 10;
    public int lengthSize = 15;
    public int cubeSize = 2;

    [Header("地图元素")]
    public GameObject cubePre;
    public GameObject castlePre;
    public GameObject housePre;
    public GameObject obstaclePre;
    public GameObject ladderPre;

    [Header("材质")]
    public Material platMaterail;

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

    [Header("城堡和房子血量")]
    public float casHp = 100;
    public float houHp = 100;

    [HideInInspector] public List<GameObject> houses;


    private PlaneCubeManager m_PlaneCubeManager;
    private PlaneCube m_CubeScript;

    //初始位置
    private float x;
    private float z;
    private GameObject plane;
    private GameObject castle;
    void Awake()
    {
        Destroy(Plane);

        plane = new GameObject();
        
        x = -widthSize;
        z = -lengthSize;
        m_PlaneCubeManager = GetComponent<PlaneCubeManager>();
        
        createPlane();
        //createCastle();
        createPlatform();
        createHouse(); 
        createObstacle();

        plane.AddComponent<NavMeshSurface>();
        plane.GetComponent<NavMeshSurface>().collectObjects = CollectObjects.All;
        plane.GetComponent<NavMeshSurface>().BuildNavMesh();

    }
    void createPlane()
    {
        float xi = x;
        float zi = z;

        int cnt = 0;
        for (int i = 0; i < widthSize; i++, xi += cubeSize)
        {
            for (int j = 0; j < lengthSize; j++, zi += cubeSize)
            {
                GameObject cube = GameObject.Instantiate(cubePre, new Vector3(xi, transform.position.y, zi), cubePre.transform.rotation);
                cube.transform.localScale = new Vector3(cube.transform.localScale.x * cubeSize, cube.transform.localScale.y * cubeSize, cube.transform.localScale.z);
                cube.transform.parent = plane.transform;
                cube.name = "Cube" + cnt++;
                m_PlaneCubeManager.AddCube(cube.transform);
            }
            zi = z;
        }

    }
    void createPlatform()
    {
        int trycnt = 0;
        for(int i = 0; i < platformNum; i++)
        {
            trycnt++;
            if (trycnt > platformNum * 2) return;
            
            int platSize = (int)(Random.value * (platformSizeMax - platformSizeMin) + platformSizeMin);
            int platHeight = (int)(Random.value * (platformHeightMax - platformHeightMin) + platformHeightMin);
            float sizeOffset = platSize % 2 == 0 ? platSize/2 : platSize/2+1;
            float xi = (int)(Random.value * (widthSize - sizeOffset)) + x;
            float zi = (int)(Random.value * (lengthSize - sizeOffset)) + z;

            Dictionary<Transform,Transform> secPlats = new Dictionary<Transform,Transform>();
            float w = xi;
            float l = zi;
            bool canBuild = true;
            for (float j = 0; j < platSize; j++, w += cubeSize)
            {
                for (float k = 0; k < platSize; k++, l += cubeSize)
                {
                    Transform cubeTransform = m_PlaneCubeManager.getCube(w, l);
                    if (!cubeTransform)
                    {
                        canBuild = false;
                        break;
                    }
                    m_CubeScript = cubeTransform.GetComponent<PlaneCube>();
                    
                    if(m_CubeScript.height != 0.5f)
                    {
                        canBuild = false;
                        break;
                    }

                }
                if (!canBuild) break;
                l = zi;
            }
            if (!canBuild)
            {
                i--;
                continue;
            }
            w = xi;
            l = zi;
            for (int j = 0; j < platSize; j++,w+=cubeSize)
            {
                for(int k = 0; k < platSize; k++,l+=cubeSize)
                {
                    Transform cubeTransform = m_PlaneCubeManager.getCube(w, l);
                    m_CubeScript = cubeTransform.GetComponent<PlaneCube>();


                    Transform platCube = m_CubeScript.transform;
                    platCube.localScale = new Vector3(platCube.localScale.x, platCube.localScale.y, platCube.localScale.z * platHeight);
                    float z = platCube.localScale.z / 50 - m_CubeScript.height;
                    platCube.position = new Vector3(platCube.position.x, z, platCube.position.z);
                    m_CubeScript.height = platCube.localScale.z / 50 + z;
                    platCube.name = "高台" + i;


                    m_CubeScript.planeCube = platMaterail;
                    m_CubeScript.transform.GetComponent<MeshRenderer>().material = platMaterail;

                    if (j == 0)
                    {
                        Transform secPlat = m_PlaneCubeManager.getCube(w - cubeSize, l);
                        if (secPlat != null) secPlats.Add(secPlat, platCube);
                    }
                    if(j == platSize - 1)
                    {
                        Transform secPlat = m_PlaneCubeManager.getCube(w + cubeSize, l);
                        if (secPlat != null) secPlats.Add(secPlat, platCube);
                    }
                    if(k == 0)
                    {
                        Transform secPlat = m_PlaneCubeManager.getCube(w, l - cubeSize);
                        if (secPlat != null) secPlats.Add(secPlat, platCube);
                    }
                    if (k == platSize - 1)
                    {
                        Transform secPlat = m_PlaneCubeManager.getCube(w, l + cubeSize);
                        if (secPlat != null) secPlats.Add(secPlat, platCube);
                    }

                    if (castle == null)
                    {
                        castlePos.position = platCube.transform.position;
                        createCastle();
                    }
                }
                l = zi;
            }
            createSecondaryPlat(secPlats, platHeight-1,0);
        }
    }

    void createSecondaryPlat(Dictionary<Transform,Transform> secPlats,int height,int tryCnt)
    {
        tryCnt++;
        if (tryCnt > 30) return;
        if (height == 0) return;
        Transform[] secs = new Transform[secPlats.Keys.Count];
        secPlats.Keys.CopyTo(secs,0);
        int i = 0;
        do
        {
            i = (int)(Random.value * secs.Length);
        } while (i >= secs.Length || i < 0);

        Transform secPlat = secs[i];
        Transform oriPlat = null;
        secPlats.TryGetValue(secPlat, out oriPlat);

        m_CubeScript = secPlat.GetComponent<PlaneCube>();
        secPlat.localScale = new Vector3(secPlat.localScale.x, secPlat.localScale.y, secPlat.localScale.z * height);
        float z = secPlat.localScale.z / 50 - m_CubeScript.height;
        secPlat.position = new Vector3(secPlat.position.x, z, secPlat.position.z);
        m_CubeScript.height = secPlat.localScale.z / 50 + z;

        m_CubeScript.planeCube = platMaterail;
        m_CubeScript.transform.GetComponent<MeshRenderer>().material = platMaterail;
        ////添加OffMeshLink
        //Vector3 startPos = secPlat.position;
        //float hei = (float)(transform.position.y + m_CubeScript.height + transform.localScale.y * 0.05f);
        //startPos.y = hei;
        //GameObject startPoint = new GameObject();
        //startPoint.name = "startPoint";
        //startPoint.transform.position = startPos;
        //Vector3 endPos = oriPlat.position;
        //hei = (float)(transform.position.y + m_CubeScript.height + transform.localScale.y * 1f);
        //endPos.y = hei;
        //GameObject endPoint = new GameObject();
        //endPoint.name = "EndPoint";
        //endPoint.transform.position = endPos;
        //oriPlat.gameObject.AddComponent<OffMeshLink>();
        //OffMeshLink oml = oriPlat.GetComponent<OffMeshLink>();
        //oml.startTransform = startPoint.transform;
        //oml.endTransform = endPoint.transform;
        //oml.area = 2;//walkable = 0;not walkable = 1; jump = 2

        //生成下一梯度
        secPlats.Clear();
        Transform secPlatNew1 = m_PlaneCubeManager.getCube(secPlat.position.x + cubeSize, secPlat.position.z);
        if (secPlatNew1 != null && secPlatNew1.GetComponent<PlaneCube>().height == 0.5) secPlats.Add(secPlatNew1, secPlat);
        Transform secPlatNew2 = m_PlaneCubeManager.getCube(secPlat.position.x - cubeSize, secPlat.position.z);
        if (secPlatNew2 != null && secPlatNew2.GetComponent<PlaneCube>().height == 0.5) secPlats.Add(secPlatNew2, secPlat);
        Transform secPlatNew3 = m_PlaneCubeManager.getCube(secPlat.position.x, secPlat.position.z + cubeSize);
        if (secPlatNew3 != null && secPlatNew3.GetComponent<PlaneCube>().height == 0.5) secPlats.Add(secPlatNew3, secPlat);
        Transform secPlatNew4 = m_PlaneCubeManager.getCube(secPlat.position.x, secPlat.position.z - cubeSize);
        if (secPlatNew4 != null && secPlatNew4.GetComponent<PlaneCube>().height == 0.5) secPlats.Add(secPlatNew4, secPlat);

        createSecondaryPlat(secPlats, height - 1, tryCnt);
    }

    void createCastle()
    {
        Vector3 pos = castlePos.position;

        Transform cubeTransform = m_PlaneCubeManager.getCube(pos.x, pos.z);
        m_CubeScript = cubeTransform.GetComponent<PlaneCube>();
        m_CubeScript.isShowGrid = false;
        //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量
        float y = m_CubeScript.height;
        pos.y = y;
        
        castle = GameObject.Instantiate(castlePre, pos, castlePre.transform.rotation);
        castle.GetComponent<Health>().Init(casHp);
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

            Transform cubeTransform = m_PlaneCubeManager.getCube(pos.x, pos.z);
            m_CubeScript = cubeTransform.GetComponent<PlaneCube>();
            if (!m_CubeScript.isShowGrid)
            {
                i--;
                continue;
            }
            m_CubeScript.isShowGrid = false;
            //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量(自身高度的一半)
            float y = 0.15f+m_CubeScript.height;
            pos.y = y;
            GameObject.Instantiate(obstaclePre, pos, obstaclePre.transform.rotation);
        }
    }

    void createHouse()
    {
        for (int i = 0; i < housePoss.Count; i++)
        {
            Vector3 pos = housePoss[i].position;


            Transform cubeTransform = m_PlaneCubeManager.getCube(pos.x, pos.z);
            m_CubeScript = cubeTransform.GetComponent<PlaneCube>();
            //初始高度 = 水平位置 + 地板方块高度偏移量 + 中心偏移量
            float y = 0.35f+m_CubeScript.height;
            pos.y = y;

            GameObject house = GameObject.Instantiate(housePre, pos, housePre.transform.rotation);
            house.GetComponent<Health>().Init(houHp);
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
