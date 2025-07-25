using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private AudioSource musicsource;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        musicsource = transform.GetChild(0).GetComponent<AudioSource>();
        //usu� to je�li odkometowujesz kod poni�ej
        instance = this;
        //nie niszcz obiektu po przej�ciu do nowej sceny
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //usuwanie duplikat�w
        else if (instance != null && instance != this)
            Destroy(gameObject);
        */
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource sourcee)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume <0)
            currentVolume = 1;

        float finalVolume = currentVolume * baseVolume;
        sourcee.volume = finalVolume;
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1, "soundVolume", _change, source);
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f ,"musicVolume", _change, musicsource);
    }
}
