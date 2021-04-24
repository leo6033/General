using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Rounds = 0;//回合数

    //宝石和金钱数
    public int RedJewel = 0;
    public int YellowJewel = 0;
    public int BlueJewel = 0;
    public int Money = 0;

    //关卡层级（我不知道具体有没有用到这个变量)
    public int CurrentLevelLayer = 1;

    public Arms[] CurrentArms = new Arms[4];//当前上场兵种——四个
    public List<Arms> AllArms = new List<Arms>();//所有可用兵种——任意数量

    public List<Skill> ALLSkills = new List<Skill>();//所有已学习技能——任意数量
    public List<Good> ALLGoods = new List<Good>();//所有物品——任意数量
}
