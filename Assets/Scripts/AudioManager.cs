using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public AudioSource music;
    public AudioClip gameMusic;
    public AudioClip lvlSolvedMusic;
    public AudioClip newLvlUnlockedMusic;
    
    public AudioSource click;
    
    void Awake()
    {
        if (audioManager != null && audioManager != this)
        {
            Destroy(this.gameObject);
            return;
        }

        audioManager = this;
        DontDestroyOnLoad(this);
    }

    public void PlayClick()
    {
        click.loop = false;
        click.Play();
    }

    public void PlayMusic(int audioClip)
    {
        if (audioClip == 0)//play game music
        {
            music.loop = true;
            music.clip = gameMusic;
            music.PlayDelayed(2);
        }

        if (audioClip == 1) // play once lvlSolved music
        {
            music.Stop();
            music.loop = false;
            music.PlayOneShot(lvlSolvedMusic);
        }

        if (audioClip == 2)// play once new lvl unlocked music
        {
            music.Stop();
            music.loop = false;
            music.PlayOneShot(newLvlUnlockedMusic);
        }
    }
}
