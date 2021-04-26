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

    public GameObject armsSelectionCanvas;
    public GameObject MainCanvas;
    public GameObject SkillCancas;

    public PlayerData data;

    
    private void init()
    {
        data = new PlayerData();
        for (int i = 0; i < 5; i++)
        {
            Arms a = new Arms();
            a.name = "arms" + i;
            a.icon = "001" + i;
            data.AllArms.Add(a);
        }
        
        for (int i = 0; i < 5; i++)
        {
            Good s = new Good();
            s.name = "good" + i;
            s.icon = i + "";
            data.ALLGoods.Add(s);
        }
        SaveSystem.SavePlayer(data);
        Debug.Log("SAVE");
    }

    void Start()
    {
        //init();
        data = SaveSystem.LoadPlayer();
        roundText.text = "第" + data.Rounds + "回合";
        redJewelText.text = data.RedJewel + "";
        yellowJewelText.text = data.YellowJewel + "";
        blueJewelText.text = data.BlueJewel + "";
        moneyText.text = data.Money + "";

    }
    

    public void showArmsSelectionCanvas(int i)
    {
        armsSelectionCanvas.GetComponent<ArmsSelectionManager>().iconIndex = i;
        armsSelectionCanvas.GetComponent<ArmsSelectionManager>().p = armsCanvas.GetComponent<ArmsShowManager>();
        armsSelectionCanvas.SetActive(true);
    }

    public void showSkillCanvas()
    {
        SkillCancas.SetActive(true);
        MainCanvas.SetActive(false);
    }
}
