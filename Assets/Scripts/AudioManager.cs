using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class AudioClipData
{
    public string id;
    public AudioClip audioClip;
}
public class AudioObjectData
{
    public string id;
    public AudioSource audioSource;

    public AudioObjectData(string id, AudioSource audioSource)
    {
        this.id = id;
        this.audioSource = audioSource;
    }
}

public class AudioManager : MonoBehaviour
{
    public List<AudioClipData> audioClips;
    public GameObject audioSourcePrefab;
    
    public Transform persistentAudioParent;

    public List<AudioObjectData> curAudioSources = new();

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlaySound(string id, bool persistent, bool loopTime = false)
    {
        AudioSource audioSource = Instantiate(audioSourcePrefab, persistent ? persistentAudioParent : null).GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = loopTime;

        audioSource.clip = audioClips.Find(x => x.id == id).audioClip;
        
        curAudioSources.Add(new(id, audioSource));
        
        audioSource.Play();
    }

    public void StopSound(string id)
    {
        foreach (AudioObjectData audioSource in curAudioSources.FindAll(source => source.id == id))
        {
            audioSource.audioSource.Stop();
            Destroy(audioSource.audioSource.gameObject);
        }

        curAudioSources.RemoveAll(x => x.id == id);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        curAudioSources.RemoveAll(item => item.audioSource == null);
    }
}
