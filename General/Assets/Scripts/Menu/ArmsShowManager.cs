using UnityEngine;
using UnityEngine.UI;

public class ArmsShowManager : MonoBehaviour
{
    PlayerData data;
    void Start()
    {
        showArms();
    }

    public void showArms()
    {
        data = SaveSystem.LoadPlayer();
        Arms[] arms = data.CurrentArms;
        for (int i = 0; i < arms.Length; i++)
        {
            if (arms[i] != null)
            {
                Texture2D s = (Texture2D)Resources.Load(arms[i].icon);
                transform.GetChild(i).GetComponent<RawImage>().texture = s;
            }
        }
    }
}
