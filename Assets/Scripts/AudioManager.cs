using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource specialMusicSource;
    public AudioSource sfxSource;

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("Special Music")]
    public AudioClip pauseMusic;
    public AudioClip gameOverMusic;
    public AudioClip stageCompleteMusic;

    [Header("Volume")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;
    [Range(0f, 1f)]
    public float pauseMusicVolume = 0.25f;
    [Range(0f, 1f)]
    public float gameOverMusicVolume = 1f;
    [Range(0f, 1f)]
    public float stageCompleteMusicVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if(musicSource == null || backgroundMusic == null)
        {
            return;
        }

        musicSource.clip = backgroundMusic;
        musicSource.volume = musicVolume;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if(musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlayPauseMusic()
    {
        StopMusic();
        if(specialMusicSource == null || pauseMusic == null)
        {
            return;
        }

        specialMusicSource.Stop();
        specialMusicSource.clip = pauseMusic;
        specialMusicSource.volume = pauseMusicVolume;
        specialMusicSource.loop = true;
        specialMusicSource.Play();
    }

    public void PlayGameOverMusic()
    {
        StopMusic();
        if (specialMusicSource == null || gameOverMusic == null)
        {
            return;
        }

        specialMusicSource.Stop();
        specialMusicSource.clip = gameOverMusic;
        specialMusicSource.volume = gameOverMusicVolume;
        specialMusicSource.loop = false;
        specialMusicSource.Play();
    }

    public void PlayStageCompleteMusic()
    {
        StopMusic();
        if (specialMusicSource == null || stageCompleteMusic == null)
        {
            return;
        }

        specialMusicSource.Stop();
        specialMusicSource.clip = stageCompleteMusic;
        specialMusicSource.volume = stageCompleteMusicVolume;
        specialMusicSource.loop = false;
        specialMusicSource.Play();
    }

    public void StopSpecialMusic()
    {
        if(specialMusicSource != null)
        {
            specialMusicSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if(sfxSource == null || clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip, sfxVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        if(musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}
