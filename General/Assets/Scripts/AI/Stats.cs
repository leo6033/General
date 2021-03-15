﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Stats")]
public class Stats : ScriptableObject
{
    [Header("攻击范围")]
    public float attackRange;
    [Header("攻击间隔时间(攻速)")]
    public float attackRate;
    [Header("视野距离")]
    public float visionRange;
    [Header("物攻")]
    public float physicalAttack;
    [Header("魔攻")]
    public float magicAttack;
    [Header("物防")]
    public float physicalDefense;
    [Header("魔防")]
    public float magicDefense;
    [Header("移速")]
    public float moveSpeed;
    [Header("血量")]
    public float HP;
    [Header("暴击")]
    public float critRate;
    [Header("暴伤")]
    public float critHarm;
}

