using System.Collections.Generic;
using UnityEngine;

public class SettingPanelManager : MonoBehaviour
{
    public static SettingPanelManager instance;
    public List<GameObject> soundImages;
    public List<GameObject> effectImages;
    public trueFalseObjects sendScoreImages;
    public trueFalseObjects lowQualityImages;
    public trueFalseObjects desktopModeImages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("生存中");
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
    void OnEnable()
    {
        UpdateSoundImage();
        UpdateEffectImage();
        UpdateSendScoreImage();
        UpdateLowQualityImage();
        UpdateDesktopModeImage();
    }
    public void UpdateSoundImage()
    {
        for (int i = 1; i < soundImages.Count; i++)
        {
            if (i <= SoundManager.instance.soundLevel) soundImages[i].SetActive(true);
            else soundImages[i].SetActive(false);
        }
        if (SoundManager.instance.soundLevel == 0) soundImages[0].SetActive(true);
        else soundImages[0].SetActive(false);
    }
    public void UpdateEffectImage()
    {
        for (int i = 1; i < effectImages.Count; i++)
        {
            if (i <= SoundManager.instance.effectLevel) effectImages[i].SetActive(true);
            else effectImages[i].SetActive(false);
        }
        if (SoundManager.instance.effectLevel == 0) effectImages[0].SetActive(true);
        else effectImages[0].SetActive(false);
    }
    public void UpdateSendScoreImage()
    {
        if (MasterGameManager.instance.isSendScore)
        {
            sendScoreImages.trueObject.SetActive(true);
            sendScoreImages.falseObject.SetActive(false);
        }
        else
        {
            sendScoreImages.trueObject.SetActive(false);
            sendScoreImages.falseObject.SetActive(true);
        }
    }
    public void UpdateLowQualityImage()
    {
        if (MasterGameManager.instance.lowQuality)
        {
            lowQualityImages.trueObject.SetActive(true);
            lowQualityImages.falseObject.SetActive(false);
        }
        else
        {
            lowQualityImages.trueObject.SetActive(false);
            lowQualityImages.falseObject.SetActive(true);
        }
    }
    public void UpdateDesktopModeImage()
    {
        if (MasterGameManager.instance.desktopMode)
        {
            desktopModeImages.trueObject.SetActive(true);
            desktopModeImages.falseObject.SetActive(false);
        }
        else
        {
            desktopModeImages.trueObject.SetActive(false);
            desktopModeImages.falseObject.SetActive(true);
        }
    }
}
