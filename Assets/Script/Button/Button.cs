using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SceneChange(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        TimeManager.instance.SetTimeScale(1f);
    }
    public void Test(string message)
    {
        Debug.Log(message);
    }
    public void Setting(int type)
    {
        switch (type)
        {
            case 0:
                //音量
                int change = SoundManager.instance.soundLevel - 1 % 4;
                if (change < 0) change = 3;
                SoundManager.instance.ChangeBGMVolume(change);
                SettingPanelManager.instance.UpdateSoundImage();
                break;
            case 1:
                //エフェクト
                SoundManager.instance.effectLevel = (SoundManager.instance.effectLevel - 1) % 4;
                if (SoundManager.instance.effectLevel < 0) SoundManager.instance.effectLevel = 3;
                SettingPanelManager.instance.UpdateEffectImage();
                SoundManager.instance.PlaySE("test");   //音量変更時に効果音を鳴らす
                break;
            case 2:
                //スコア送信true/false
                MasterGameManager.instance.isSendScore = !MasterGameManager.instance.isSendScore;
                SettingPanelManager.instance.UpdateSendScoreImage();
                SoundManager.instance.PlaySE("click");   //スコア送信設定変更時に効果音を鳴らす
                break;
            case 3:
                //低品質モードtrue/false
                MasterGameManager.instance.lowQuality = !MasterGameManager.instance.lowQuality;
                SettingPanelManager.instance.UpdateLowQualityImage();
                SoundManager.instance.PlaySE("click");   //低品質モード設定変更時に効果音を鳴らす
                break;
            case 4:
                //デスクトップモードtrue/false
                MasterGameManager.instance.desktopMode = !MasterGameManager.instance.desktopMode;
                SettingPanelManager.instance.UpdateDesktopModeImage();
                SoundManager.instance.PlaySE("click");   //デスクトップモード設定変更時に効果音を鳴らす
                break;
            default:
                break;
        }
    }
    public void showSettingPanel()
    {
        if (UiManagerTitle.instance.isSettingPanelActive) return;
        UiManagerTitle.instance.showSettingPanel();
        SoundManager.instance.PlaySE("open");   //設定パネル表示時に効果音を鳴らす
    }
    public void hideSettingPanel()
    {
        if (!UiManagerTitle.instance.isSettingPanelActive) return;
        UiManagerTitle.instance.hideSettingPanel();
        SoundManager.instance.PlaySE("close");   //設定パネル非表示時に効果音を鳴らす
    }
    public void showSettingPanelInGame()
    {
        if (UiManagerGame.instance.isSettingPanelActive || MasterGameManager.instance.gameState != "playing") return;
        UiManagerGame.instance.showSettingPanel();
        SoundManager.instance.PlaySE("open");   //設定パネル表示時に効果音を鳴らす
    }
    public void hideSettingPanelInGame()
    {
        if (!UiManagerGame.instance.isSettingPanelActive) return;
        UiManagerGame.instance.hideSettingPanel();
        SoundManager.instance.PlaySE("close");   //設定パネル非表示時に効果音を鳴らす
    }
    public void Reset()
    {
        GameManager.instance.Reset();
        TimeManager.instance.SetTimeScale(1f);
    }
}
