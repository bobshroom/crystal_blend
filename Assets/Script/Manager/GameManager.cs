using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int highScore;
    public Transform suikatati;
    public GameObject platform;
    public GameObject platformAsiba;
    public List<GameObject> buttobiList;
    private float timer = 10f;
    public int score;
    public int maxCombo;
    public int maxMargeLevel = 2;
    public int mergedSuikaCount;
    public int summondSuikaCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void OnEnable()
    {
        MasterGameManager.instance.gameState = "playing";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 10f;
            MasterGameManager.instance.uploadScoreToUnityroom();
        }
    }

    void Awake()
    {
        instance = this;
        score = 0;
        maxCombo = 0;
        maxMargeLevel = 2;
        summondSuikaCount = 0;
        mergedSuikaCount = 0;
    }
    public void AddScore(int add)
    {
        score += add;
    }
    public void gameOver()
    {
        Debug.Log("ゲームオーバー");
        SoundManager.instance.PlaySE("miss");
        MasterGameManager.instance.uploadScoreToUnityroom();
        MasterGameManager.instance.gameState = "gameOver";
        SuikaManager.instance.GameOver();
        if (score > highScore) highScore = score;
        TimeManager.instance.SetTimeScale(0.5f);
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
                float torque = Random.Range(-1f, 1f);
                rb.AddTorque(torque, ForceMode2D.Impulse);
            }
        }
        Rigidbody2D platformRb = platformAsiba.GetComponent<Rigidbody2D>(); // 足場の回転と移動の制約を解除
        platformRb.constraints = RigidbodyConstraints2D.None;

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
        foreach (var obj in buttobiList)
        {
            if (obj != null)
            {
                var rb = obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = 1;
                    Vector2 force = new Vector2(Random.Range(-5f, 5f), Random.Range(10f, 15f));
                    rb.AddForce(force, ForceMode2D.Impulse);
                    float torque = Random.Range(-2f, 2f);
                    rb.AddTorque(torque, ForceMode2D.Impulse);
                }
            }
        }
        SoundManager.instance.fadeOutBGM(1.5f);
    }
    public void Reset()
    {
        score = 0;
        mergedSuikaCount = 0;
        summondSuikaCount = 0;
        maxCombo = 0;
        TimeManager.instance.SetTimeScale(1f);
        MasterGameManager.instance.gameState = "playing";
        SceneManager.LoadScene("game");
    }
}
