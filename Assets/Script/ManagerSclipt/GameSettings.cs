using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//設定項目管理
public class GameSettings : MonoBehaviour
{
    //サウンド管理用
    [SerializeField] 
    private soundCnt soundManager; 
    //シーン管理用
    [SerializeField]
    private StateManagement Settings; 
    [SerializeField]
    private Slider sliderBGM;
    [SerializeField]
    private Slider sliderSE;
    [SerializeField]
    private Text BGMtext;
    [SerializeField]
    private Text SEtext;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<soundCnt> ();
        Settings = GameObject.Find("StateManagement").GetComponent<StateManagement> ();
        //起動時に前回設定のセット
        launchScreenMode();
        launchVolume();
    }

    /*--画面モード切り替え--*/

    //起動時のセット
    void launchScreenMode()
    {
        switch (Settings.windowMode)
        {
            case "Window":
                Screen.fullScreen = false;
                break;

            case "FullScreen":
                Screen.fullScreen = true;
                break;

            default:
                Screen.fullScreen = true;
                break;
        }
    }

    //ウィンドウモード
    public void windowsScreenMode()
    {
        Screen.fullScreen = false;
        Settings.windowMode = "Window";
    }

    //フルスクリーンモード
    public void FullScreenMode()
    {
        Screen.fullScreen = true;
        Settings.windowMode = "FullScreen";
    }

    /*-----サウンド設定-----*/

    //起動時のセット
    public void launchVolume()
    {
        soundManager.setnewValueBGM(Settings.SetvolumeBGM);
        soundManager.setnewValueSE(Settings.SetvolumeSE);

        sliderBGM.value = Settings.SetvolumeBGM;
        sliderSE.value = Settings.SetvolumeSE;
        float BGM = sliderBGM.value * 100;
        BGMtext.text = "" + (int)BGM;
        float SE = sliderSE.value * 100;
        SEtext.text = "" + (int)SE;
    }

    //bgm用 
    public void SoundSliderOnValueChangeBGM()
    {
        float BGM = sliderBGM.value * 100;
        BGMtext.text = "" + (int)BGM;
    }
    //効果音用
    public void SoundSliderOnValueChangeSE()
    {
        float SE = sliderSE.value * 100;
        SEtext.text = "" + (int)SE;
    }
    
    /*-----適応ボタン-----*/

    //設定反映
    public void CommitSettings()
    {
        //スクリーンモード反映
        switch(Settings.windowMode)
        {
            case "Window":
            Screen.fullScreen = false;
                break;

            case "FullScreen":
            Screen.fullScreen = true;
                break;
        }
        
        Settings.SetvolumeBGM = sliderBGM.value;
        Settings.SetvolumeSE = sliderSE.value;

        Debug.Log(Settings.SetvolumeBGM);

        //音量設定反映
        soundManager.setnewValueBGM(Settings.SetvolumeBGM);
        soundManager.setnewValueSE(Settings.SetvolumeSE);

        //設定の保存
        Settings.writeSettings();
    }
}
