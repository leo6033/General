using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PointAttr : MonoBehaviour, IPointerClickHandler
{
    public List<GameObject> points;
    public string SceneName;
    public int SceneNum;

    [HideInInspector] public int number;
    [HideInInspector] public int level;
    [HideInInspector] public int round;

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayerData player = SaveSystem.LoadPlayer();
        int armCount = 0;
        for(int i = 0; i < player.CurrentArms.Length; i++)
        {
            if (player.CurrentArms[i] != null)
                armCount++;
        }

        if (player.Rounds == round && player.CurrentLevelLayer == level && armCount != 0)
        {
            if(SceneName == "")
            {
                SceneName = "Level " + player.CurrentLevelLayer + "-" + player.Rounds;
                if(SceneNum > 1)
                {
                    SceneName += "(" + Random.Range(1, SceneNum) + ")";
                }
            }
            player.CuttentPointNum = number;
            SaveSystem.SavePlayer(player);
            SceneManager.LoadScene(SceneName);
        }
    }
}
