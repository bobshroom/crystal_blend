using UnityEngine;

public class SEController
{
    public AudioSource[] audioSource;
    int sourceIndex = 0;
    public SEController(AudioSource[] source)
    {
        audioSource = source;
    }
    public void PlayOneShot(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
    {
        audioSource[sourceIndex].pitch = pitch;
        audioSource[sourceIndex].PlayOneShot(clip, volume * SoundManager.instance.effectLevel / 3.0f);
        sourceIndex = (sourceIndex + 1) % audioSource.Length;
    }
}
