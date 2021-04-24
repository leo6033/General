using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmsSelectionManager : MonoBehaviour
{
    public Transform content;
    public GameObject IconPre;

    public ArmsShowManager p;
    public int iconIndex;

    PlayerData data;
    void Start()
    {
        data = SaveSystem.LoadPlayer();
        List<Arms> allArms = data.AllArms;
        for(int i = 0; i < allArms.Count; i++)
        {
            GameObject icon = GameObject.Instantiate(IconPre);
            icon.transform.SetParent(content);
            icon.GetComponent<RawImage>().texture = (Texture2D)Resources.Load(allArms[i].icon);
            icon.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            icon.GetComponent<RectTransform>().localScale = Vector3.one;
            icon.name = i+"";
            icon.GetComponent<Button>().onClick.AddListener(delegate { selectArm(icon); });
        }
    }
    public void offArmsSelectionCanvas()
    {
        gameObject.SetActive(false);
    }

    public void selectArm(GameObject item)
    {
        int i = int.Parse(item.name);
        Arms a = data.AllArms[i];
        data.CurrentArms[iconIndex] = a;
        SaveSystem.SavePlayer(data);
        p.showArms();
        gameObject.SetActive(false);
    }
}
