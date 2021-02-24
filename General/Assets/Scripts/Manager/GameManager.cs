using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform m_test;
    public Legion[] m_EnemyPrefabs;
    public Legion[] m_PlayerPrefabs;
    public List<Transform> m_housePoint;

    private List<GameObject> m_EnemyLegions;

    private void Start()
    {
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        for(int i = 0; i < m_EnemyPrefabs.Length; i++)
        {
            for(int j = 0; j < m_EnemyPrefabs[i].m_LegionNumber; j++)
            {
                GameObject legion = Instantiate(m_EnemyPrefabs[i].LegionPrefeb, m_test) as GameObject;
                EnemyManager enemyManager = legion.GetComponent<EnemyManager>();
                enemyManager.m_Instance = legion;
                enemyManager.Setup(m_housePoint);
            }
        }
    }
}
