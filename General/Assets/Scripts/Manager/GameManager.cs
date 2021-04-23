using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public Transform m_test;
    public Legion[] m_EnemyPrefabs;
    public Legion[] m_PlayerPrefabs;

    [Header("胜利结算面板")]
    public Canvas WinCanvas;
    public Text TimeText;

    public GameObject EnemyLegionPrefab;
    public GameObject PlayerLegionPrefab;
    public GameObject WarnPrefab;

    private MapCreater mapCreater;
    private List<StateController> enemines;
    private List<StateController> players;
    private WaitForSeconds m_EndWait;

    private int EnemyCreateCount = 0;
    private bool EnemyCreateFinish = false;

    private void Start()
    {
        m_EndWait = new WaitForSeconds(3);
        mapCreater = GetComponent<MapCreater>();

        CreatePlayer();
        CreateEnemy(mapCreater.houses);

        StartCoroutine(Game());
    }

    private IEnumerator Game()
    {
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());


        //StartCoroutine(Game());
    }

    private IEnumerator RoundPlaying()
    {
        WinCanvas.enabled = false;
        while (!CheckEnd())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        if (CheckWin())
            Win();
        else if (CheckLose())
            Lose();

        yield return m_EndWait;
    }

    private bool CheckEnd()
    {
        if (CheckWin())
            return true;
        if (CheckLose())
            return true;
        return false;
    }

    private bool CheckWin()
    {
        for (int i = 0; i < enemines.Count; i++)
        {
            if (enemines[i].gameObject.activeSelf)
                return false;
        }
        return EnemyCreateFinish;
    }

    private bool CheckLose()
    {
        bool flag = true;
        for (int i = 0; i < mapCreater.houses.Count; i++)
        {
            if (mapCreater.houses[i].tag == "Castle" && mapCreater.houses[i].GetComponent<Health>().Dead())
                return true;
            else if (!mapCreater.houses[i].GetComponent<Health>().Dead())
                flag = false;
        }
        if (flag)
            return true;
        
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].gameObject.activeSelf)
                return false;
        }

        return true;
    }


    private void CreateEnemy(List<GameObject> houseTransform)
    {
        enemines = new List<StateController>();
        for (int i = 0; i < m_EnemyPrefabs.Length; i++)
        {
            StartCoroutine(CreateEnemy(m_EnemyPrefabs[i], houseTransform));
        }
    }

    IEnumerator CreateEnemy(Legion m_EnemyPrefab, List<GameObject> houseTransform)
    {
        yield return new WaitForSeconds(m_EnemyPrefab.Time - 2);
        Instantiate(WarnPrefab, m_EnemyPrefab.m_LegionPosition.position, m_EnemyPrefab.m_LegionPosition.rotation);
        yield return new WaitForSeconds(2);
        GameObject legion = Instantiate(EnemyLegionPrefab, m_EnemyPrefab.m_LegionPosition.position, m_EnemyPrefab.m_LegionPosition.rotation) as GameObject;
        EnemyManager enemyManager = legion.GetComponent<EnemyManager>();
        enemyManager.Init(m_EnemyPrefab.Number, m_EnemyPrefab.LegionPrefeb);
        enemyManager.m_Instance = legion;
        enemyManager.Setup(houseTransform, ref enemines);
        EnemyCreateCount++;
        if (EnemyCreateCount == m_EnemyPrefabs.Length)
            EnemyCreateFinish = true;
    }

    private void CreatePlayer()
    {
        players = new List<StateController>();
        for(int i = 0; i < m_PlayerPrefabs.Length; i++)
        {
            GameObject legion = Instantiate(PlayerLegionPrefab, m_PlayerPrefabs[i].m_LegionPosition.position, m_PlayerPrefabs[i].m_LegionPosition.rotation) as GameObject;
            PlayerManager playerManager = legion.GetComponent<PlayerManager>();
            playerManager.Init(m_PlayerPrefabs[i].Number, m_PlayerPrefabs[i].LegionPrefeb);
            playerManager.m_Instance = legion;
            playerManager.Setup(ref players);
        }
        Debug.Log(players.Count);
    }

    private void Win()
    {
        WinCanvas.enabled = true;
        TimeText.text = "关卡用时:  " + (int)Time.time + "秒";
    }

    private void Lose()
    {

    }

}
