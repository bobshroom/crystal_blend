using UnityEngine;

public class BGMController
{
    public AudioSource audioSource;
    private float playingVolume = 1.0f;
    public BGMController(AudioSource source)
    {
        audioSource = source;
    }
    public void PlayBGM(AudioClip clip, bool loop = true, float volume = 1.0f)
    {
        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            return; // すでに同じBGMが再生中の場合は何もしない
        }
        playingVolume = volume;
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.pitch = 1.0f;
        audioSource.Play();
    }
    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume * playingVolume;
    }
    public void FadeOut(float volume)
    {
        audioSource.volume = volume * playingVolume;
        audioSource.pitch = volume;
    }
}
