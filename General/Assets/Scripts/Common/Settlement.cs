using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settlement : MonoBehaviour
{
    public Text isWinText;
    [Header("关卡用时")]
    public Text GameTimeText;
    [Header("关卡战损")]
    public Text BattleDamageText;
    [Header("关卡杀敌")]
    public Text KillText;
    [Header("金钱")]
    public Text MoneyText;
    [Header("宝石")]
    public Image redJewel;
    public Text Red;
    public Image greenJewel;
    public Text Green;
    public Image blueJewel;
    public Text Blue;

    public void Win(Dictionary<string, int> playerTypeNumDict, Dictionary<string, int> enemyTypeNumDict, int houseAliveNum, int[] jewel, int money)
    {
        isWinText.text = "胜利";

        GameTimeText.text = "关卡用时:    " + (int)Time.time + " 秒";
        BattleDamageText.text = dictToString("关卡战损:    ", playerTypeNumDict) + "建筑-" + houseAliveNum + "\n";
        KillText.text = dictToString("关卡杀敌:    ", enemyTypeNumDict);

        Red.text = "" + jewel[0];
        redJewel.color = jewel[0] == 0 ? Color.gray : redJewel.color;
        Green.text = "" + jewel[1];
        greenJewel.color = jewel[1] == 0 ? Color.gray : greenJewel.color;
        Blue.text = "" + jewel[2];
        blueJewel.color = jewel[2] == 0 ? Color.gray : blueJewel.color;

        MoneyText.text = "获取金钱:    " + money;
    }

    public void Lose(Dictionary<string, int> playerTypeNumDict, Dictionary<string, int> enemyTypeNumDict, int houseAliveNum)
    {
        isWinText.text = "失败";

        GameTimeText.text = "关卡用时:    " + (int)Time.time + " 秒";
        BattleDamageText.text = dictToString("关卡战损:    ", playerTypeNumDict) + "建筑-" + houseAliveNum + "\n";
        KillText.text = dictToString("关卡杀敌:    ", enemyTypeNumDict);

        Red.text = "" + 0;
        redJewel.color = Color.gray;
        Green.text = "" + 0;
        greenJewel.color = Color.gray;
        Blue.text = "" + 0;
        blueJewel.color = Color.gray;

        MoneyText.text = "获取金钱:    " + 0;
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
