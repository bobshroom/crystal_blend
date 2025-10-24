using UnityEngine;
using unityroom.Api;

public class MasterGameManager : MonoBehaviour
{
    public static MasterGameManager instance;
    public bool isSendScore = true;
    public bool lowQuality = false;
    public bool desktopMode = true;
    public string gameState;

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
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void uploadScoreToUnityroom()
    {
        if (!isSendScore) return;
        UnityroomApiClient.Instance.SendScore(1, GameManager.instance.score, ScoreboardWriteMode.HighScoreDesc);
    }
    public void upLoadMaxComboToUnityroom()
    {
        if (!isSendScore) return;
        UnityroomApiClient.Instance.SendScore(2, GameManager.instance.maxCombo, ScoreboardWriteMode.HighScoreDesc);
    }
}
