using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        //nie nieszcz obiektu po przej�ciu do nowej sceny
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //usuwanie duplikat�w
        else if (instance != null && instance != this)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
