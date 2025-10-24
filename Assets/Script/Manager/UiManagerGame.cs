using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UiManagerGame : MonoBehaviour
{
    public Transform canvasParent;
    public static UiManagerGame instance;
    public TMP_SpriteAsset spriteAsset;
    public TMP_Text fpsText;
    private float showFpsInterval = 0.4f;
    public GameObject nextColor;
    [SerializeField] private Sprite defimg;
    [SerializeField] private List<Color> suikaColors;
    [SerializeField] private List<Sprite> suikaSprites;
    [SerializeField] private List<float> nextSizes;
    public TMP_Text scoreText;
    public TMP_Text comboEffectText;
    public GameObject gameOverImage;
    [Tooltip("入れた順番に表示される(ただし最初と最後は別)")]
    public List<TMP_Text> gameOverTexts;
    public GameObject gameOverPanel;
    public TMP_Text test;
    public GameObject settingPanel;
    public GameObject comboEffect;
    private Image comboEffectImage;
    private bool isComboEffectPlaying = false;
    private bool stopComboEffect = false;
    public bool isSettingPanelActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        comboEffectImage = comboEffect.GetComponent<Image>();
        instance = this;
        changeToSpriteAsset(test, spriteAsset);
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
        // if (Input.GetKeyDown(KeyCode.T)) makeStoneTips(new Vector3(0, 0, 0));    //テスト用Tキーで石チップス生成
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
        nextColor.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        nextColor.transform.localScale = new Vector3(211.68f * nextSizes[SuikaManager.instance.nextSuika], 211.68f * nextSizes[SuikaManager.instance.nextSuika], 1);
    }
    void UpdateScore()
    {
        scoreText.text = "SCORE: " + GameManager.instance.score.ToString();
        changeToSpriteAsset(scoreText, spriteAsset);
    }
    public void ComboEffect(Vector3 pos, int combo)
    {
        Vector3 comboPos = pos + new Vector3(0, 0.5f, 0);
        if (combo <= 1) return;
        GameObject instnace = Instantiate(comboEffectText.gameObject, comboPos, Quaternion.identity, canvasParent);
        StartCoroutine(ComboEffectCoroutine(combo));
    }

    public IEnumerator GameOver() //Time.timeScale = 0.5fなので注意
    {
        yield return new WaitForSeconds(0.5f);
        gameOverImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        for (int i = 0; i < gameOverTexts.Count - 1; i++)
        {
            gameOverTexts[i].gameObject.SetActive(true);
            if (i == 0) gameOverTexts[i].text = "SCORE: " + GameManager.instance.score.ToString();
            else if (i == 1) gameOverTexts[i].text = "MERGED: " + GameManager.instance.mergedSuikaCount.ToString();
            else if (i == 2) gameOverTexts[i].text = "SUMMONED: " + GameManager.instance.summondSuikaCount.ToString();
            else if (i == 3) gameOverTexts[i].text = "MAX COMBO: " + GameManager.instance.maxCombo.ToString();
            changeToSpriteAsset(gameOverTexts[i], spriteAsset);
            SoundManager.instance.PlaySE("gameOverMiddle");
            yield return new WaitForSeconds(0.15f);
        }
        yield return new WaitForSeconds(0.1f);
        string rank;
        int score = GameManager.instance.score;
        int soundType;  //-1:特殊,0:大きな歓声,1:歓声,2:ハンバーグのアレと拍手,3:ハンバーグのあれ,4:ちーん
        if (score >= 30000) {rank = "SSS!!!"; soundType = -1;}
        else if (score >= 20000) {rank = "SS!!"; soundType = 0;}
        else if (score >= 15000) {rank = "S!"; soundType = 0;}
        else if (score >= 10000) {rank = "A"; soundType = 1;}
        else if (score >= 5000) {rank = "B"; soundType = 2;}
        else if (score >= 2000) {rank = "C"; soundType = 3;}
        else if (score >= 1000) {rank = "D"; soundType = 3;}
        else if (score > 0) {rank = "E"; soundType = 3;}
        else {rank = "???"; soundType = 4;}

        gameOverTexts[gameOverTexts.Count - 1].gameObject.SetActive(true);

        gameOverTexts[gameOverTexts.Count - 1].text = "YOUR RANK: ";
        changeToSpriteAsset(gameOverTexts[gameOverTexts.Count - 1], spriteAsset);
        SoundManager.instance.PlaySE("dodon");
        yield return new WaitForSeconds(0.4f);

        gameOverTexts[gameOverTexts.Count - 1].text = "YOUR RANK: " + rank;
        changeToSpriteAsset(gameOverTexts[gameOverTexts.Count - 1], spriteAsset);

        SoundManager.instance.PlaySE("gameOverLast", 1f, soundType);

        yield return new WaitForSeconds(0.5f);
        
        gameOverPanel.SetActive(true);
    }

    public void changeToSpriteAsset(TMP_Text text, TMP_SpriteAsset spriteAsset)
    {
        text.spriteAsset = spriteAsset;
        string spriteText = text.text;
        text.text = "";
        for (int i = 0; i < spriteText.Length; i++)
        {
            int id = changeToSpriteAssetId(spriteText[i]);
            if (id == 100) text.text += " "; //スペース
            else if (id == 101) text.text += "\n"; //改行
            else if (id == -1)
            {
                text.text += "<sprite=" + 63 + ">"; //不明な文字は？にする
                Debug.LogError("不明な文字:" + spriteText[i]);
            }
            else text.text += "<sprite=" + id + ">";
        }
    }
    int changeToSpriteAssetId(char c)
    {
        if (c >= '0' && c <= '9') return c - '0';
        else if (c >= 'A' && c <= 'Z') return c - 'A' + 10;
        else if (c >= 'a' && c <= 'z') return c - 'a' + 36;
        else
        {
            switch (c)
            {
                case '!': return 62;
                case '?': return 63;
                case '.': return 64;
                case ',': return 65;
                case ':': return 66;
                case ';': return 67;
                case '+': return 68;
                case '-': return 69;
                case '*': return 70;
                case '/': return 71;
                case '=': return 72;
                case '%': return 73;
                case '#': return 74;
                case '&': return 75;
                case '@': return 76;
                case '$': return 77;
                case '^': return 78;
                case '(': return 79;
                case ')': return 80;
                case '[': return 81;
                case ']': return 82;
                case '{': return 83;
                case '}': return 84;
                case '<': return 85;
                case '>': return 86;
                case '_': return 87;
                case '"': return 88;
                case '\'': return 89;
                case '\\': return 90;
                case '|': return 91;
                case '~': return 92;
                case '`': return 93;
                case ' ': return 100; //スペース
                case '\n': return 101; //改行
                default: return -1;
            }
        }
    }
    public void showSettingPanel()
    {
        StartCoroutine(SettingPanelCoroutine(true));
    }
    public void hideSettingPanel()
    {
        StartCoroutine(SettingPanelCoroutine(false));
    }
    IEnumerator SettingPanelCoroutine(bool isShow)
    {
        if (isShow)
        {
            float time = 0f;
            settingPanel.SetActive(true);
            isSettingPanelActive = true;
            while (time < 1f)
            {
                time += Time.unscaledDeltaTime * 5f;
                settingPanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
                yield return null;
            }
            TimeManager.instance.Pause(true);
        }
        else
        {
            float time = 1f;
            while (time > 0f)
            {
                time -= Time.unscaledDeltaTime * 5f;
                settingPanel.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
                yield return null;
            }
            settingPanel.SetActive(false);
            isSettingPanelActive = false;
            TimeManager.instance.Pause(false);
        }
    }
    public IEnumerator ComboEffectCoroutine(int combo)
    {
        if (isComboEffectPlaying)
        {
            stopComboEffect = true;
            yield return new WaitUntil(() => !isComboEffectPlaying);
            stopComboEffect = false;
        }
        comboEffect.SetActive(true);
        isComboEffectPlaying = true;
        float s;
        s = combo * 0.05f;
        while (s > 0)
        {
            comboEffectImage.color = new Color(comboEffectImage.color.r, comboEffectImage.color.g, comboEffectImage.color.b, s);
            s -= Time.deltaTime * 0.3f;
            if (stopComboEffect) break;
            yield return null;
        }
        isComboEffectPlaying = false;
        comboEffect.SetActive(false);
    }
}
