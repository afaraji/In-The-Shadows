using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


// https://www.youtube.com/watch?v=YOaYQrN1oYQ
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    Resolution[] resolutions;
    public Dropdown ResDdropdown;
    public Slider generalVol;
    public Slider musicVol;
    public Slider SFXVol;
    public Slider rotSpeed;
    public Text rotSpeedText;
    

    private void OnEnable()
    {
        
        mixer.SetFloat("GeneralVolume", PlayerPrefs.GetFloat("GeneralVolume", 0));
        generalVol.value = PlayerPrefs.GetFloat("GeneralVolume", 0);
        
        mixer.SetFloat("Music", PlayerPrefs.GetFloat("Music", 0));
        musicVol.value = PlayerPrefs.GetFloat("Music", 0);
        
        mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX", 0));
        SFXVol.value = PlayerPrefs.GetFloat("SFX", 0);

        rotSpeed.value = PlayerPrefs.GetFloat("RotationSpeed", 0);
        rotSpeedText.text = PlayerPrefs.GetFloat("RotationSpeed", 0).ToString("0.00");
        
    }

    private void Start()
    {
        resolutions = Screen.resolutions;
        ResDdropdown.ClearOptions();
        List<string> options = new List<string>();
        options.Add(resolutions[resolutions.Length - 1].ToString());
        options.Add(resolutions[resolutions.Length - 2].ToString());
        options.Add(resolutions[resolutions.Length - 3].ToString());
        ResDdropdown.AddOptions(options);
    }
    public void OnGeneralVolumeChange(float value)
    {
        mixer.SetFloat("GeneralVolume", value);
        PlayerPrefs.SetFloat("GeneralVolume", value);
    }

    public void OnMusicVolumeChange(float value)
    {
        mixer.SetFloat("Music", value);
        PlayerPrefs.SetFloat("Music", value);
    }

    public void OnSFXlVolumeChange(float value)
    {
        mixer.SetFloat("SFX", value);
        PlayerPrefs.SetFloat("SFX", value);
    }

    public void OnRotationSpeedChange(float value)
    {
        PlayerPrefs.SetFloat("RotationSpeed", value);
        rotSpeedText.text = PlayerPrefs.GetFloat("RotationSpeed", 0).ToString("0.00");

    }

    public void OnResoulutionChange(int resIndex)
    {
        Resolution res = resolutions[resolutions.Length - 1 - resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void OnFullScreenPressed(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}

/*
 * ************  PlayerPrefs content *************
 * GeneralVolume
 * SFX
 * Music
 * RotationSpeed
 * UnlockedLvl
 */