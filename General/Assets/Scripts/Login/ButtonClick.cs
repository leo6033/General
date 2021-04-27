using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    public AudioClip clickClip;

    public void NewGame()
    {
        Sound();
        PlayerData data = new PlayerData();
        for (int i = 0; i < 1; i++)
        {
            Arms a = new Arms("arms" + i, "0010");
            data.AllArms.Add(a);
        }

        SaveSystem.SavePlayer(data);
        SceneManager.LoadScene("MenuScene");
    }

    public void Continue()
    {
        Sound();
        SceneManager.LoadScene("MenuScene");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Sound();
        
        Application.Quit();
    }

    private void Sound()
    {
        AudioSource audioSource = GetComponentInParent<AudioSource>();
        audioSource.clip = clickClip;
        audioSource.Play();
    }
}
