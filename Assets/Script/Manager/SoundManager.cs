using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public AudioClip suikaMergeClip;
    public AudioClip comboMergeClip;
    public AudioClip gameOverClipInto;
    public AudioClip gameOverClipMiddle;
    public AudioClip gameOverClipLast;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlaySound(string soundName)
    {
        audioSource.pitch = 1;
        switch (soundName)
        {
            case "suikaMerge":
                int combo = ComboManager.instance.currentCombo;
                if (combo == 0) audioSource.PlayOneShot(suikaMergeClip);
                else
                {
                    /*int index = Mathf.Min(combo - 1, comboMergePitches.Count - 1);
                    audioSource.pitch = comboMergePitches[index];*/
                    Debug.Log(1 + 0.06f * (combo - 1));
                    audioSource.pitch = 1.0f + 0.06f * (combo - 1);
                    audioSource.PlayOneShot(comboMergeClip);
                }
                break;
            case "gameOverInto":
                audioSource.PlayOneShot(gameOverClipInto);
                break;
            case "gameOverMiddle":
                audioSource.PlayOneShot(gameOverClipMiddle);
                break;
            case "gameOverLast":
                audioSource.PlayOneShot(gameOverClipLast);
                break;
            default:
                break;
        }
    }
}
