using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public List<AudioSource> audioSourcePool;
    private Dictionary<string, List<AudioClip>> soundGroups;
    public AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSoundGroups();
            InitializeAudioSourcePool();
            InitializeBGMSource();
            PlayBGM("Theme(석양의 무법자)");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSourcePool()
    {
        audioSourcePool = new List<AudioSource>();

        for (int i = 0; i < 10; i++)
        {
            GameObject audioSourceObj = new GameObject("PooledAudioSource");
            audioSourceObj.transform.SetParent(transform);
            AudioSource audioSource = audioSourceObj.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 0.0f; // 2D 사운드로 설정
            audioSourcePool.Add(audioSource);
        }
    }

    private void InitializeBGMSource()
    {
        GameObject bgmSourceObj = new GameObject("BGMSource");
        bgmSourceObj.transform.SetParent(transform);
        bgmSource = bgmSourceObj.AddComponent<AudioSource>();
        bgmSource.loop = true; // BGM은 반복 재생
        bgmSource.spatialBlend = 0.0f; // 2D 사운드로 설정
    }

    private void LoadSoundGroups()
    {
        soundGroups = new Dictionary<string, List<AudioClip>>();

        // 각 그룹에 사운드 클립을 로드하는 로직
        soundGroups["Object"] = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Object"));
        soundGroups["Character"] = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Character"));
        soundGroups["BGM"] = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/BGM"));
        soundGroups["Button"] = new List<AudioClip>(Resources.LoadAll<AudioClip>("Sounds/Button"));
    }

    public void PlaySound(string group, string clipName)
    {
        if (soundGroups.TryGetValue(group, out List<AudioClip> clips))
        {
            AudioClip clip = clips.Find(c => c.name == clipName);
            if (clip != null)
            {
                AudioSource audioSource = GetAudioSource();
                audioSource.clip = clip;
                audioSource.volume *= 0.3f;
                audioSource.PlayOneShot(clip);
/*                audioSource.Play();*/
                StartCoroutine(ReturnAfterPlaying(audioSource, clip.length));
            }
        }
    }

    public void PlayBGM(string clipName)
    {
        if (soundGroups.TryGetValue("BGM", out List<AudioClip> clips))
        {
            AudioClip clip = clips.Find(c => c.name == clipName);
            if (clip != null && bgmSource.clip != clip)
            {
                bgmSource.volume *= 0.2f;
                bgmSource.clip = clip;
                bgmSource.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
        bgmSource.clip = null;
    }

    private AudioSource GetAudioSource()
    {
        foreach (AudioSource audioSource in audioSourcePool)
        {
            if (!audioSource.isPlaying)
            {
                return audioSource;
            }
        }

        // 사용 가능한 오디오 소스가 없으면 새로운 오디오 소스를 생성
        GameObject audioSourceObj = new GameObject("PooledAudioSource");
        audioSourceObj.transform.SetParent(transform);
        AudioSource newAudioSource = audioSourceObj.AddComponent<AudioSource>();
        newAudioSource.playOnAwake = false;
        newAudioSource.spatialBlend = 0.0f; // 2D 사운드로 설정
        audioSourcePool.Add(newAudioSource);
        return newAudioSource;
    }

    private void ReturnAudioSource(AudioSource audioSource)
    {
        audioSource.clip = null;
    }

    private IEnumerator ReturnAfterPlaying(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnAudioSource(audioSource);
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public float GetBGMVolume()
    {
        return bgmSource.volume;
    }

    public void SetSFXVolume(float volume)
    {
        foreach (var audioSource in audioSourcePool)
        {
            audioSource.volume = volume;
        }
    }

    public float GetSFXVolume()
    {
        if (audioSourcePool.Count > 0)
        {
            return audioSourcePool[0].volume;
        }
        return 1.0f;
    }
}

