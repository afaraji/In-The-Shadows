using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


// https://www.youtube.com/watch?v=YOaYQrN1oYQ
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer mixer;
    Resolution[] resolutions;
    public Dropdown ResDdropdown;

    private void OnEnable()
    {
        mixer.SetFloat("GeneralVolume", PlayerPrefs.GetFloat("GeneralVolume", 0));
        mixer.SetFloat("Music", PlayerPrefs.GetFloat("Music", 0));
        mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX", 0));
    }

    private void Start()
    {
        resolutions = Screen.resolutions;

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
    }

    public void OnResoulutionChange(int resIndex)
    {
        
    }
}
