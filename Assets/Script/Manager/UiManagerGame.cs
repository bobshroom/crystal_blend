using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;

public class UiManagerGame : MonoBehaviour
{
    public Transform canvasParent;
    public static UiManagerGame instance;
    public TMP_Text fpsText;
    private float showFpsInterval = 0.4f;
    public GameObject nextColor;
    [SerializeField] private Sprite defimg;
    [SerializeField] private List<Color> suikaColors;
    [SerializeField] private List<Sprite> suikaSprites;
    [SerializeField] private List<float> nextSizes;
    public TMP_Text scoreText;
    public TMP_Text comboEffectText;
    [Tooltip("入れた順番に表示される(ただし最初と最後は別)")]
    public List<TMP_Text> gameOverTexts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        showFpsInterval -= Time.deltaTime;
        if (showFpsInterval <= 0)
        {
            UpdateFps();
            showFpsInterval = 0.4f;
        }
        UpdateNextColor();
        UpdateScore();
    }
    void UpdateFps()
    {
        if (fpsText == null) fpsText = GameObject.Find("FpsText").GetComponent<TMP_Text>();
        float current = 1f / Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + Mathf.CeilToInt(current).ToString("F1");
    }
    void UpdateNextColor()
    {
        Sprite sprite = suikaSprites[SuikaManager.instance.nextSuika];
        //Color color = suikaColors[SuikaManager.instance.nextSuika];
        nextColor.GetComponent<SpriteRenderer>().sprite = sprite;
        nextColor.transform.localScale = new Vector3(211.68f * nextSizes[SuikaManager.instance.nextSuika], 211.68f * nextSizes[SuikaManager.instance.nextSuika], 1);
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + GameManager.instance.score.ToString();
    }
    public void ComboEffect(Vector3 pos, int combo)
    {
        Vector3 comboPos = pos + new Vector3(0, 0.5f, 0);
        if (combo <= 1) return;
        GameObject instnace = Instantiate(comboEffectText.gameObject, comboPos, Quaternion.identity, canvasParent);
    }

    public IEnumerator GameOver() //Time.timeScale = 0.5fなので注意
    {
        yield return new WaitForSeconds(0.5f);
        gameOverTexts[0].gameObject.SetActive(true);
        SoundManager.instance.PlaySound("gameOverInto");
        yield return new WaitForSeconds(0.75f);
        for (int i = 1; i < gameOverTexts.Count - 1; i++)
        {
            gameOverTexts[i].gameObject.SetActive(true);
            if(i==1) gameOverTexts[i].text = "Score: " + GameManager.instance.score.ToString();
            else if(i==2) gameOverTexts[i].text = "Merged: " + GameManager.instance.mergedSuikaCount.ToString();
            else if(i==3) gameOverTexts[i].text = "Summoned: " + GameManager.instance.summondSuikaCount.ToString();
            else if(i==4) gameOverTexts[i].text = "Max Combo: " + GameManager.instance.maxCombo.ToString();
            SoundManager.instance.PlaySound("gameOverMiddle");
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.1f);
        string rank;
        int score = GameManager.instance.score;
        if (score >= 30000) rank = "SSS!!!";
        else if (score >= 20000) rank = "SS!!";
        else if (score >= 10000) rank = "S!";
        else if (score >= 5000) rank = "A";
        else if (score >= 2000) rank = "B";
        else if (score >= 1000) rank = "C";
        else rank = "D";
        gameOverTexts[gameOverTexts.Count - 1].text = "Your Rank: " + rank;
        gameOverTexts[gameOverTexts.Count - 1].gameObject.SetActive(true);
        SoundManager.instance.PlaySound("gameOverLast");
    }
}
