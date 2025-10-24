using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public BGMController bgmController;
    [SerializeField] private AudioSource bgmAudioSource;
    public SEController seController;
    [SerializeField] private AudioSource[] seSource;
    public AudioClip suikaMergeClip;
    public AudioClip comboMergeClip;
    public AudioClip gameOverClipInto;
    public AudioClip gameOverClipMiddle;
    [Tooltip("-1:特殊,0:大きな歓声,1:歓声,2:ハンバーグのアレと拍手,3:ハンバーグのあれ,4:ちーん")]
    public List<AudioClip> gameOverClipLast;
    public AudioClip dodonClip;
    public AudioClip open;
    public AudioClip close;
    public AudioClip click;
    public AudioClip miss;
    public int soundLevel;  //0～3
    public int effectLevel; //0～3
    [Header("以下音楽素材")]
    public AudioClip titleBGM;
    public float titleBGMVolume = 0.5f;
    public AudioClip gameBGM;
    public float gameBGMVolume = 0.7f;
    private bool isFadingOut = false;
    private float fadeOutDuration = 0.0f;
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
            bgmController = new BGMController(bgmAudioSource);
            seController = new SEController(seSource);
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "title")
        {
            playBGM("title");
            MasterGameManager.instance.gameState = "title";
        }
        else if (scene.name == "game")
        {
            playBGM("game");
        }
        // シーンがロードされたときの処理
    }
    public void PlaySE(string soundName, float volume = 1.0f, int soundType = 0)
    {
        switch (soundName)
        {
            case "suikaMerge":
                int combo = ComboManager.instance.currentCombo;
                if (combo == 0) seController.PlayOneShot(suikaMergeClip);
                else
                {
                    /*int index = Mathf.Min(combo - 1, comboMergePitches.Count - 1);
                    audioSource.pitch = comboMergePitches[index];*/
                    seController.PlayOneShot(comboMergeClip, 1f, 1.0f + 0.06f * (combo - 1));
                }
                break;
            case "gameOverInto":
                seController.PlayOneShot(gameOverClipInto);
                break;
            case "gameOverMiddle":
                seController.PlayOneShot(gameOverClipMiddle);
                break;
            case "gameOverLast":    //-1:特殊,0:大きな歓声,1:歓声,2:ハンバーグのアレと拍手,3:ハンバーグのあれ,4:ちーん
                if (soundType == -1) seController.PlayOneShot(gameOverClipLast[5]);
                else if (soundType < 2) seController.PlayOneShot(gameOverClipLast[soundType]);
                else if (soundType < 4)
                {
                    seController.PlayOneShot(gameOverClipLast[3]);
                    if (soundType == 2) StartCoroutine(PlaySEAlt(gameOverClipLast[2], 0.35f));
                }
                else seController.PlayOneShot(gameOverClipLast[4]);
                break;
            case "dodon":
                seController.PlayOneShot(dodonClip);
                break;
            case "test":
                seController.PlayOneShot(suikaMergeClip);
                break;
            case "open":
                seController.PlayOneShot(open, 0.7f);
                break;
            case "close":
                seController.PlayOneShot(close, 0.7f);
                break;
            case "click":
                seController.PlayOneShot(click);
                break;
            case "miss":
                seController.PlayOneShot(miss, 0.5f);
                break;
            default:
                Debug.Log("No Sound Name :" + soundName);
                break;
        }
    }
    IEnumerator PlaySEAlt(AudioClip clip, float delay, float volume = 1f, float pitch = 1f)
    {
        yield return new WaitForSeconds(delay);
        seController.PlayOneShot(clip, volume, pitch);
    }
    public void playBGM(string bgmName)
    {
        switch (bgmName)
        {
            case "title":
                bgmController.PlayBGM(titleBGM, true, titleBGMVolume);
                break;
            case "game":
                bgmController.PlayBGM(gameBGM, true, gameBGMVolume);
                break;
            default:
                Debug.Log("No BGM Name :" + bgmName);
                break;
        }
    }
    public void fadeOutBGM(float duration)
    {
        if (!isFadingOut)
        {
            isFadingOut = true;
            fadeOutDuration = duration;
            StartCoroutine(FadeOutCoroutine(fadeOutDuration));
        }
    }
    public void ChangeBGMVolume(int Level)
    {
        soundLevel = Level;
        bgmController.ChangeVolume(soundLevel / 3.0f);
    }

    IEnumerator<WaitForEndOfFrame> FadeOutCoroutine(float fadeOutDuration)
    {
        float startVolume = fadeOutDuration;
        float time = 0f;

        while (time < fadeOutDuration)
        {
            time += Time.unscaledDeltaTime;
            bgmController.FadeOut(Mathf.Lerp(1f, 0f, time / fadeOutDuration));
            yield return new WaitForEndOfFrame();
        }

        bgmAudioSource.Stop();
        bgmAudioSource.volume = startVolume; // ボリュームを元に戻す
        isFadingOut = false;
    }
}
