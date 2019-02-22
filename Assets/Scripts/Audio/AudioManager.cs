using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioChannel { Master, Sfx, Music };

    public float MasterVolumePercent { get; private set; }
    public float SfxVolumePercent { get; private set; }
    public float MusicVolumePercent { get; private set; }

    AudioSource sfx2DSource;
    AudioSource[] musicSources;
    int activeMusicSourceIndex;
    public static AudioManager instance;

    SoundLibrary library;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();

            musicSources = new AudioSource[2];
            for (int i = 0; i < 2; i++)
            {
                GameObject newMusicSource = new GameObject("Music Source " + (i + 1));
                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
                musicSources[i].loop = true;
            }


            

            GameObject newSfx2Dsource = new GameObject("2D sfx source");
            sfx2DSource = newSfx2Dsource.AddComponent<AudioSource>();
            newSfx2Dsource.transform.parent = transform;

            //the ones are the default values
            MasterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
            SfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
            MusicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
            PlayerPrefs.Save();
        }

    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                MasterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                SfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                MusicVolumePercent = volumePercent;
                break;
        }

        musicSources[0].volume = MusicVolumePercent * MasterVolumePercent;
        musicSources[1].volume = MusicVolumePercent * MasterVolumePercent;

        PlayerPrefs.SetFloat("master vol", MasterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", SfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", MusicVolumePercent);
        PlayerPrefs.Save();
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();
        /*
        if(PauseMenu.GameIsPaused)
        {
            masterVolumePercent = masterVolumePercent / 2;
        }
        */
        StartCoroutine(AnimateMusicCrossFade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, SfxVolumePercent * MasterVolumePercent);
        }
    }

    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(library.GetClipFromName(soundName), pos);
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(soundName), SfxVolumePercent * MasterVolumePercent);

    }

    IEnumerator AnimateMusicCrossFade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, MusicVolumePercent * MasterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(MusicVolumePercent * MasterVolumePercent, 0, percent);
            yield return null;
        }

    }
}
