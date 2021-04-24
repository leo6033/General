using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settlement : MonoBehaviour
{
    [Header("关卡用时")]
    public Text GameTimeText;
    [Header("关卡战损")]
    public Text BattleDamageText;
    [Header("关卡杀敌")]
    public Text KillText;
    [Header("金钱")]
    public Text MoneyText;
    [Header("宝石")]
    public Text Red;
    public Text Green;
    public Text Blue;

    public void Win(Dictionary<string, int> playerTypeNumDict, Dictionary<string, int> enemyTypeNumDict)
    {
        GameTimeText.text = "关卡用时:    " + (int)Time.time + " 秒";
        BattleDamageText.text = dictToString("关卡战损:    ", playerTypeNumDict);
        KillText.text = dictToString("关卡杀敌:    ", enemyTypeNumDict);
    }

    private string dictToString(string preStr, Dictionary<string, int> dict)
    {
        string result = preStr;
        foreach(string type in dict.Keys)
        {
            result += type + "-" + dict[type] + "\n" + "                    ";
        }
        return result;
    }
}
