using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodsShowManager : MonoBehaviour
{
    public GameObject IconPre;
    public Transform content;

    PlayerData data;
    void Start()
    {
        showGoods();
    }

    public void showGoods()
    {
        data = SaveSystem.LoadPlayer();
        List<Good> goods = data.ALLGoods;
        for (int i = 0; i < goods.Count; i++)
        {
            GameObject icon = GameObject.Instantiate(IconPre);
            icon.transform.SetParent(content);
            icon.GetComponent<RawImage>().texture = (Texture2D)Resources.Load(goods[i].icon);
            icon.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);            //主要是z=0
            icon.GetComponent<RectTransform>().localScale = Vector3.one;
            icon.name = i + "";

        }
    }
}
