using System.Collections;
using UnityEngine;

public class UiManagerTitle : MonoBehaviour
{
    public static UiManagerTitle instance;
    public GameObject settingPanel;
    public bool isSettingPanelActive = false;
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
        }
        else
        {
            Destroy(this.gameObject);
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
}
