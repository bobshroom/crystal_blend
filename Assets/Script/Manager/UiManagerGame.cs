using TMPro;
using UnityEngine;

public class UiManagerGame : MonoBehaviour
{
    public TMP_Text fpsText;
    private float showFpsInterval = 0.4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
    }
    void UpdateFps()
    {
        if (fpsText == null) fpsText = GameObject.Find("FpsText").GetComponent<TMP_Text>();
        float current = 1f / Time.unscaledDeltaTime;
        fpsText.text = "FPS: " + Mathf.CeilToInt(current).ToString("F1");
    }
}
