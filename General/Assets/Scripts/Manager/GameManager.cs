﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_Money;
    public Legion[] m_EnemyPrefabs;
    public Legion[] m_PlayerPrefabs;

    [Header("胜利结算面板")]
    public Canvas WinCanvas;

    public GameObject EnemyLegionPrefab;
    public GameObject PlayerLegionPrefab;
    public GameObject WarnPrefab;

    public AudioSource audioSource;
    public AudioClip WinClip;
    public AudioClip LoseClip;

    private MapCreater mapCreater;
    private List<StateController> enemines;
    private List<StateController> players;
    private WaitForSeconds m_EndWait;

    private int EnemyCreateCount = 0;
    private bool EnemyCreateFinish = false;
    private Dictionary<string, int> playerTypeNumDict;
    private Dictionary<string, int> enemyTypeNumDict;

    private PlayerData playerData;
    private int houseAliveNum;
    private int playerLegionNum;

    private void Start()
    {
        playerData = SaveSystem.LoadPlayer();
        initPlayerLegion();
        m_EndWait = new WaitForSeconds(3);

        mapCreater = GetComponent<MapCreater>();
        playerTypeNumDict = new Dictionary<string, int>();
        enemyTypeNumDict = new Dictionary<string, int>();

        houseAliveNum = mapCreater.houses.Count;

        CreatePlayer();
        CreateEnemy(mapCreater.houses);

        StartCoroutine(Game());
    }

    private void initPlayerLegion()
    {
        playerLegionNum = 0;
        for (int i = 0; i < playerData.CurrentArms.Length; i++)
        {
            if(playerData.CurrentArms[i] != null)
            {
                m_PlayerPrefabs[playerLegionNum++].LegionPrefeb = (GameObject)Resources.Load(playerData.CurrentArms[i].getLegionPrefebPath());
            }
        }
    }

    private IEnumerator Game()
    {
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        foreach(StateController stateController in enemines)
        {
            stateController.enabled = false;
        }

        foreach(StateController stateController in players)
        {
            stateController.enabled = false;
        }
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
        for(int i = 0; i < playerLegionNum; i++)
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
        Settlement settlement = WinCanvas.GetComponent<Settlement>();
        int value;
        foreach(StateController player in players)
        {
            if (!player.gameObject.activeSelf)
            {
                playerTypeNumDict.TryGetValue(player.Types, out value);
                playerTypeNumDict[player.Types] = value + 1;
            }
        }

        foreach(StateController enemy in enemines)
        {
            enemyTypeNumDict.TryGetValue(enemy.Types, out value);
            enemyTypeNumDict[enemy.Types] = value + 1;
        }

        // 计算宝石数 red green blue
        int[] jewel = new int[3];
        foreach(GameObject house in mapCreater.houses)
        {
            if (house.activeSelf)
            {
                int random = Random.Range(0, 2);
                jewel[random]++;
                houseAliveNum--;
            }
        }
        playerData.RedJewel += jewel[0];
        playerData.YellowJewel += jewel[1];
        playerData.BlueJewel += jewel[2];
        playerData.Money += m_Money;
        // TODO: 暂时通过硬编码来增加兵团数量
        if(playerData.Rounds < 5 && playerData.Rounds != 2)
        {
            playerData.AllArms.Add(new Arms("arms" + playerData.AllArms.Count, "0010"));
        }
        playerData.Rounds += 1;
        playerData.CurrentLevelPoint.Add(playerData.CuttentPointNum);
        playerData.CurrentArms = new Arms[4];
        SaveSystem.SavePlayer(playerData);


        audioSource.clip = WinClip;
        audioSource.Play();
        settlement.Win(playerTypeNumDict, enemyTypeNumDict, houseAliveNum, jewel, m_Money);
    }

    private void Lose()
    {
        WinCanvas.enabled = true;
        Settlement settlement = WinCanvas.GetComponent<Settlement>();
        int value;
        foreach (StateController player in players)
        {
            if (!player.gameObject.activeSelf)
            {
                playerTypeNumDict.TryGetValue(player.Types, out value);
                playerTypeNumDict[player.Types] = value + 1;
            }
        }

        foreach (StateController enemy in enemines)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemyTypeNumDict.TryGetValue(enemy.Types, out value);
                enemyTypeNumDict[enemy.Types] = value + 1;
            }
        }

        foreach (GameObject house in mapCreater.houses)
        {
            if (house.activeSelf)
            {
                houseAliveNum--;
            }
        }
        audioSource.clip = LoseClip;
        audioSource.Play();

        playerData.CurrentArms = new Arms[4];
        SaveSystem.SavePlayer(playerData);

        settlement.Lose(playerTypeNumDict, enemyTypeNumDict, houseAliveNum);
    }

}
