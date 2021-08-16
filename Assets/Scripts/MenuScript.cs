using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject TitleScreen;
    public GameObject TrackSelect;
    public GameObject CarSelect;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("car") == null)
        {
            PlayerPrefs.SetInt("car", 0);
            PlayerPrefs.Save();
        }
        AudioListener.pause = false;
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Track1");
    }

    public void ToggleTrackSelect()
    {
        TitleScreen.SetActive(!TitleScreen.activeSelf);
        TrackSelect.SetActive(!TrackSelect.activeSelf);
    }

    public void ToggleCarSelect()
    {
        TitleScreen.SetActive(!TitleScreen.activeSelf);
        CarSelect.SetActive(!CarSelect.activeSelf);
    }

    public void SelectSubaru()
    {
        PlayerPrefs.SetInt("car", 0);
        PlayerPrefs.Save();
    }

    public void SelectPorche()
    {
        PlayerPrefs.SetInt("car", 1);
        PlayerPrefs.Save();
    }

    public void SelectTrack1()
    {
        PlayerPrefs.SetInt("track", 0);
        PlayerPrefs.Save();
    }

    public void SelectTrack2()
    {
        PlayerPrefs.SetInt("track", 1);
        PlayerPrefs.Save();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
