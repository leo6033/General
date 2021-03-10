using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public Transform m_test;
    public Legion[] m_EnemyPrefabs;
    public Legion[] m_PlayerPrefabs;
    public Text m_MessageText;

    private MapCreater mapCreater;
    private List<StateController> enemines;
    private List<StateController> players;
    private WaitForSeconds m_EndWait;
    //private bool isWin = false;

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
        m_MessageText.text = string.Empty; ;
        while (!CheckEnd())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {

        m_MessageText.text = EndMessage();

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
        return true;
    }

    private bool CheckLose()
    {
        bool flag = true;
        for (int i = 0; i < mapCreater.houses.Count; i++)
        {
            if (mapCreater.houses[i].tag == "Castle" && !mapCreater.houses[i].activeSelf)
                return true;
            else if (mapCreater.houses[i].gameObject.activeSelf)
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
        for(int i = 0; i < m_EnemyPrefabs.Length; i++)
        {
            GameObject legion = Instantiate(m_EnemyPrefabs[i].LegionPrefeb, m_EnemyPrefabs[i].m_LegionPosition.position, m_EnemyPrefabs[i].m_LegionPosition.rotation) as GameObject;
            EnemyManager enemyManager = legion.GetComponent<EnemyManager>();
            enemyManager.m_Instance = legion;
            enemyManager.Setup(houseTransform, out enemines);
        }
    }

    private void CreatePlayer()
    {
        for(int i = 0; i < m_PlayerPrefabs.Length; i++)
        {
            GameObject legion = Instantiate(m_PlayerPrefabs[i].LegionPrefeb, m_PlayerPrefabs[i].m_LegionPosition.position, m_PlayerPrefabs[i].m_LegionPosition.rotation) as GameObject;
            PlayerManager playerManager = legion.GetComponent<PlayerManager>();
            playerManager.m_Instance = legion;
            playerManager.Setup(out players);
        }
    }

    private string EndMessage()
    {
        string message = "DRAW!";

        if (CheckWin())
            message = " WINS!";
        else if(CheckLose())
            message = "Lose";

        message += "\n\n\n\n";

        return message;
    }
}
