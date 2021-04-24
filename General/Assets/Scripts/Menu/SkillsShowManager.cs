using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsShowManager : MonoBehaviour
{
    public GameObject IconPre;
    public Transform content;

    public GameObject MainCancas;
    public GameObject ThisCanvas;

    PlayerData data;
    void Start()
    {
        showSkills();
    }

    public void showSkills()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        data = SaveSystem.LoadPlayer();
        List<Skill> skills = data.ALLSkills;
        for (int i = 0; i < skills.Count; i++)
        {
            GameObject icon = GameObject.Instantiate(IconPre);
            icon.transform.SetParent(content);
            icon.GetComponent<RawImage>().texture = (Texture2D)Resources.Load(skills[i].icon);
            icon.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);            //主要是z=0
            icon.GetComponent<RectTransform>().localScale = Vector3.one;
            icon.name = i + "";

        }
    }

    public void Back()
    {
        MainCancas.SetActive(true);
        ThisCanvas.SetActive(false);
    }
}
