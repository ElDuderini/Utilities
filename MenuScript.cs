using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsMenu;

    public GameObject leaderBoard;
    //http://dreamlo.com/lb/KD4BAWAwOE6iwJSA8M7oign70cmejRFUeeBF4ADx4HoA

    public AudioMixer master;

    public TextMeshProUGUI volumeText;

    private string masterParam = "Master";

    public TMP_Dropdown dropdown;

    public Slider volumeSlider;

    public Scrollbar qualitySlider;

    private void Start()
    {
        SavedPrefs();
    }

    private void SavedPrefs()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 70);
            ChangeVolume(70);
            volumeSlider.value = 70;
            print("Vol set");

        }
        else
        {
            float vol = PlayerPrefs.GetFloat("Volume");

            volumeSlider.value = vol;

            ChangeVolume(vol);
        }

        if (!PlayerPrefs.HasKey("Quality"))
        {
            PlayerPrefs.SetInt("Quality", 1);

            QualitySettings.SetQualityLevel(2, true);
            qualitySlider.value = .3f;
        }
        else
        {
            int qual = PlayerPrefs.GetInt("Quality");

            switch (qual)
            {
                case 0:
                    ChangeQuality(0);
                    qualitySlider.value = 0;
                    break;

                case 1:
                    ChangeQuality(.3f);
                    qualitySlider.value = 0.3f;
                    break;

                case 2:
                    ChangeQuality(.6666667f);
                    qualitySlider.value = .6666667f;
                    break;

                case 3:
                    ChangeQuality(1);
                    qualitySlider.value = 1;
                    break;
            }
        }

        if (!PlayerPrefs.HasKey("Resolution"))
        {
            PlayerPrefs.SetInt("Resolution", 1);
            dropdown.value = 1;
        }
        else
        {
            int res = PlayerPrefs.GetInt("Resolution");

            switch (res)
            {
                case 0:
                    ChangeRes(res);
                    dropdown.value = 0;
                    break;

                case 1:
                    ChangeRes(res);
                    dropdown.value = 1;
                    break;

                case 2:
                    ChangeRes(res);
                    dropdown.value = 2;
                    break;
            }
        }
    }
    public void PlaySolo()
    {
        SceneManager.LoadScene("ArenaThrow");
    }

    public void PlayMulti()
    {
        print("Multiplayer not added yet");
    }

    public void LeaderBoard()
    {
        print("Leaderboard not added yet");
        leaderBoard.SetActive(true);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenu.activeInHierarchy == true)
            {
                optionsMenu.SetActive(false);
            }
            else if(leaderBoard.activeInHierarchy == true)
            {
                leaderBoard.SetActive(false);
            }
            else
            {
                Exit();
            }
        }
    }


    public void ChangeQuality(float QualityNum)
    {

        switch (QualityNum)
        {
            case 0:
                QualitySettings.SetQualityLevel(0, true);
                PlayerPrefs.SetInt("Quality", 0);
                break;

            case .6666667f:
                QualitySettings.SetQualityLevel(4, true);
                PlayerPrefs.SetInt("Quality", 2);
                break;

            case 1:
                QualitySettings.SetQualityLevel(5, true);
                PlayerPrefs.SetInt("Quality", 3);
                break;

            default:
                QualitySettings.SetQualityLevel(2, true);
                PlayerPrefs.SetInt("Quality", 1);
                break;
        }
    }

    public void ChangeVolume(float Volume)
    {

        float volValue = -60 + (Volume - 0) * (10 - -60) / (100 - 0);

        float prefVal = Mathf.CeilToInt(Volume);

        volumeText.text = prefVal.ToString();

        master.SetFloat(masterParam, volValue);

        print(volValue);

        PlayerPrefs.SetFloat("Volume", prefVal);
    }

    public void ChangeRes(int res)
    {
        switch (res) 
        {
            case 0:
                Screen.SetResolution(1280, 720, true);
                PlayerPrefs.SetInt("Resolution", res);
                break;

            case 1:
                Screen.SetResolution(1920, 1080, true);
                PlayerPrefs.SetInt("Resolution", res);
                break;

            case 2:
                Screen.SetResolution(2560, 1440, true);
                PlayerPrefs.SetInt("Resolution", res);
                break;
        }
    }
    
}
