using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip audioBGSongLv1;
    public AudioClip audioBGSongLv2;
    public AudioClip audioBGSongLv3;
    public AudioClip audioBGSongLv4;
    private int selectSong;

    public AudioClip bulletHit;
    public AudioClip enemyDie;
    public AudioClip earthHit;
    public AudioClip earthDie;
    public AudioClip blackHoleAppear;
    public AudioClip earthSuckedIntoBH;

    public List<AudioSource> listAudioSource;

    public int GetIndexCurrentLv
    {
        get
        {
            return selectSong;
        }
    }

    public void SelectSong(int serialSong)
    {
        selectSong = serialSong;
    }

    public AudioClip GetAudioSource
    {
        get
        {
            switch (selectSong)
            {
                case 1:
                    return audioBGSongLv1;

                case 2:
                    return audioBGSongLv2;

                case 3:
                    return audioBGSongLv3;

                case 4:
                    return audioBGSongLv4;

                default:
                    return audioBGSongLv1;
            }
        }
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayEffectSound(AudioClip audioClip)
    {
        AudioSource audioSource = GetAudioSourceAvaiable(listAudioSource);

        audioSource.clip = audioClip;
        audioSource.Play();
    }


    bool IsAvailable(AudioSource auSource)
    {
        if (auSource.clip == null || auSource.isPlaying == false)
            return true;

        return false;
    }

    AudioSource GetAudioSourceAvaiable(List<AudioSource> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            if (IsAvailable(list[i]))
                return list[i];
        }

        list.Add(new AudioSource());
        list[list.Count - 1] = gameObject.AddComponent<AudioSource>();
        list[list.Count - 1].playOnAwake = false;

        return list[list.Count - 1];
    }
}
