using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManagerGame : MonoBehaviour
{
    public Transform canvasParent;
    public static UiManagerGame instance;
    public TMP_Text fpsText;
    private float showFpsInterval = 0.4f;
    public GameObject nextColor;
    [SerializeField] private List<Color> suikaColors;
    public TMP_Text scoreText;
    public TMP_Text comboEffectText;
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
        Color color = suikaColors[SuikaManager.instance.nextSuika];
        nextColor.GetComponent<SpriteRenderer>().color = color;
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
}
