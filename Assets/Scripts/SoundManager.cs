using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip explosionSound;
    public static AudioClip backgroundMusic;
    public static AudioClip gameoverSound;

    private static AudioSource audioSrc;

    void Start()
    {
        explosionSound = Resources.Load<AudioClip>("Sounds/explosion");
        backgroundMusic = Resources.Load<AudioClip>("Sounds/background");
        gameoverSound = Resources.Load<AudioClip>("Sounds/gameover");
        audioSrc = GetComponent<AudioSource>();
    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "explosion":
                {
                    audioSrc.PlayOneShot(explosionSound, 1.0f);
                    break;
                }
            case "background":
                {
                    audioSrc.clip = backgroundMusic;
                    audioSrc.loop = true;
                    audioSrc.Play();
                    break;
                }
            case "gameover":
                {
                    audioSrc.PlayOneShot(gameoverSound, 3.0f);
                    break;
                }
        }

    }

    public static void StopSound()
    {
        audioSrc.Stop();
    }
}
