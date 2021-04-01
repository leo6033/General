using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataManager : MonoBehaviour
{
    public Text roundText;
    public Text redJewelText;
    public Text yellowJewelText;
    public Text blueJewelText;
    public Text moneyText;

    public Transform armsCanvas;
    public Transform skillCanvas;

    public GameObject armsSelectionCanvas;

    public PlayerData data;
    void Start()
    {
        data = SaveSystem.LoadPlayer();
        roundText.text = "第" + data.Rounds + "回合";
        redJewelText.text = data.RedJewel + "";
        yellowJewelText.text = data.YellowJewel + "";
        blueJewelText.text = data.BlueJewel + "";
        moneyText.text = data.Money + "";

        showArms();
        showSkill();
        
    }
    bool isShowArms = false;
    public void showArms()
    {
        data = SaveSystem.LoadPlayer();
        Arms[] arms = data.CurrentArms;
        for (int i = 0; i < arms.Length; i++)
        {
            if (arms[i] != null)
            {
                Texture2D s = (Texture2D)Resources.Load(arms[i].icon);
                armsCanvas.GetChild(i).GetComponent<RawImage>().texture = s;
            }
        }
        isShowArms = false;
    }

    public void showSkill()
    {
        data = SaveSystem.LoadPlayer();
        Skill[] skills = data.skills;
        for (int i = 0; i < skills.Length && i < 5; i++)
        {
            if (skills[i] != null)
            {
                Texture2D s = (Texture2D)Resources.Load(skills[i].icon);
                skillCanvas.GetChild(i).GetComponent<RawImage>().texture = s;
            }
        }
    }
    public void showArmsSelectionCanvas(int i)
    {
        armsSelectionCanvas.GetComponent<ArmsSelectionManager>().iconIndex = i;
        armsSelectionCanvas.GetComponent<ArmsSelectionManager>().p = this;
        armsSelectionCanvas.SetActive(true);
    }
    

    private void Update()
    {
        if (isShowArms)
        {
            showArms();
            Debug.Log("flash");
        }
    }
}
