using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Rounds = 0;

    public int RedJewel = 0;
    public int YellowJewel = 0;
    public int BlueJewel = 0;
    public int Money = 0;

    public int CurrentLevelLayer = 1;

    public Arms[] CurrentArms = new Arms[4];
    public List<Arms> AllArms = new List<Arms>();

    public Skill[] skills = new Skill[48];
}
