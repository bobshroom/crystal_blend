using UnityEngine;
using UnityEngine.SceneManagement;
using unityroom.Api;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string gameState;
    public int score;
    public int highScore;
    public int mergedSuikaCount;
    public int summondSuikaCount;
    public int maxCombo;
    public Transform suikatati;
    public GameObject platform;
    private float timer = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UiManagerGame.instance.ComboEffect(Vector3.zero, 2);
        }
        if (Input.GetKeyDown(KeyCode.R))// && gameState == "gameOver")
        {
            Reset();
        }
        if (timer <= 0)
        {
            timer = 10f;
            uploadScoreToUnityroom();
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            summondSuikaCount = 0;
            mergedSuikaCount = 0;
            score = 0;
            maxCombo = 0;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void AddScore(int add)
    {
        score += add;
    }
    public void gameOver()
    {
        uploadScoreToUnityroom();
        gameState = "gameOver";
        if (score > highScore) highScore = score;
        Time.timeScale = 0.5f;
        UiManagerGame.instance.StartCoroutine(UiManagerGame.instance.GameOver());
        foreach (Transform suika in suikatati)
        {
            var ctrl = suika.GetComponent<suikaController>();
            if (ctrl != null) ctrl.enabled = false;

            var rb = suika.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1;
                // 上方向＋左右ランダムな力
                Vector2 force = new Vector2(Random.Range(-2f, 2f), Random.Range(2f, 3f));
                rb.AddForce(force, ForceMode2D.Impulse);
                // ランダムな回転
                float torque = Random.Range(-3f, 3f);
                rb.AddTorque(torque, ForceMode2D.Impulse);
            }
        }

        // 足場を吹っ飛ばす
        if (platform != null)
        {
            var rb = platform.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1;
                Vector2 force = new Vector2(Random.Range(-5f, 5f), Random.Range(10f, 15f));
                rb.AddForce(force, ForceMode2D.Impulse);
                float torque = Random.Range(-10f, 10f);
                rb.AddTorque(torque, ForceMode2D.Impulse);
            }
        }
    }
    void Reset()
    {
        score = 0;
        mergedSuikaCount = 0;
        summondSuikaCount = 0;
        maxCombo = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene("game");
    }
    public void uploadScoreToUnityroom()
    {
        UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.HighScoreDesc);
        
    }
    public void upLoadMaxComboToUnityroom()
    {
        UnityroomApiClient.Instance.SendScore(2, maxCombo, ScoreboardWriteMode.HighScoreDesc);
    }
}
