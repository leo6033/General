using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 和PlayerData原理相同，之所以分开存放是因为这个类只适用于展示技能树，学习技能时会在PlayerData类中的AllSkill列表中对应加入Skill对象
 */
[System.Serializable]
public class PlayerSkill
{
    public int SkillLevel = 0;

    public int[][] RedTree = new int[2][];
    public int[][] YellowTree = new int[2][];//实际是绿树，设计图中是黄树，反正绿树和黄树就是一个东西
    public int[][] BlueTree = new int[2][];
    /*
      数组下标0-4指四个技能等级
      前三位值0-3，第四位值为0-1,0表示未选择
           RedTree:[
                       [1,2,3,1],
                       [1,2,3,1],
                       [1,2,3,1],
                       [1,2,3,1],    
                   ],    
        YellowTree:[
                       [1,2,3,1],
                       [1,2,3,1],
                       [1,2,3,1],
                       [1,2,3,1],    
                   ], 
          BlueTree:[
                       [1,2,3,1],
                       [1,2,3,1],
                       [1,2,3,1],
                       [1,2,3,1],    
                   ], 
      
    */
}
