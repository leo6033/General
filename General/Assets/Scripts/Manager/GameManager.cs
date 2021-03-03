﻿using System.Collections;
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
        CreatePlayer();
        CreateEnemy();
    }

    private void CreateEnemy()
    {
        for(int i = 0; i < m_EnemyPrefabs.Length; i++)
        {
            GameObject legion = Instantiate(m_EnemyPrefabs[i].LegionPrefeb, m_EnemyPrefabs[i].m_LegionPosition.position, m_EnemyPrefabs[i].m_LegionPosition.rotation) as GameObject;
            EnemyManager enemyManager = legion.GetComponent<EnemyManager>();
            enemyManager.m_Instance = legion;
            enemyManager.Setup(m_housePoint);
        }
    }

    private void CreatePlayer()
    {
        for(int i = 0; i < m_PlayerPrefabs.Length; i++)
        {
            GameObject legion = Instantiate(m_PlayerPrefabs[i].LegionPrefeb, m_PlayerPrefabs[i].m_LegionPosition.position, m_PlayerPrefabs[i].m_LegionPosition.rotation) as GameObject;
            PlayerManager playerManager = legion.GetComponent<PlayerManager>();
            playerManager.m_Instance = legion;
            playerManager.Setup();
        }
    }
}
