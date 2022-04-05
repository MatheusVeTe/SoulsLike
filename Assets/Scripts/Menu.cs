using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GameObject PauseMenu;
    public AudioMixer AudioMixer;

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Time.timeScale = 0;
            PauseMenu.SetActive(true);
        }
    }
    
    public void Resume() 
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    public void SetVolume(float volume) 
    {
        AudioMixer.SetFloat("Volume", volume);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
