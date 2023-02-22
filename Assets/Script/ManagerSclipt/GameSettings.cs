using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//設定項目管理
public class GameSettings : MonoBehaviour
{
    //サウンド管理用
    [SerializeField] 
    private SoundCnt soundManager; 
    //シーン管理用
    [SerializeField]
    private StateManagement settings; 
    [SerializeField]
    private Slider sliderBGM;
    [SerializeField]
    private Slider sliderSE;
    [SerializeField]
    private Text textBGM;
    [SerializeField]
    private Text textSE;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
        settings = GameObject.Find("StateManagement").GetComponent<StateManagement> ();
        //起動時に前回設定のセット
        launchScreenMode();
        LaunchVolume();
    }

    /*--画面モード切り替え--*/

    //起動時のセット
    void launchScreenMode()
    {
        switch (settings.WindowMode)
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
    public void WindowsScreenMode()
    {
        Screen.fullScreen = false;
        settings.WindowMode = "Window";
    }

    //フルスクリーンモード
    public void FullScreenMode()
    {
        Screen.fullScreen = true;
        settings.WindowMode = "FullScreen";
    }

    /*-----サウンド設定-----*/

    //起動時のセット
    public void LaunchVolume()
    {
        soundManager.SetnewValueBGM(settings.SetvolumeBGM);
        soundManager.SetnewValueSE(settings.SetvolumeSE);

        sliderBGM.value = settings.SetvolumeBGM;
        sliderSE.value = settings.SetvolumeSE;
        float BGM = sliderBGM.value * 100;
        textBGM.text = "" + (int)BGM;
        float SE = sliderSE.value * 100;
        textSE.text = "" + (int)SE;
    }

    //bgm用 
    public void SoundSliderOnValueChangeBGM()
    {
        float BGM = sliderBGM.value * 100;
        textBGM.text = "" + (int)BGM;
    }
    //効果音用
    public void SoundSliderOnValueChangeSE()
    {
        float SE = sliderSE.value * 100;
        textSE.text = "" + (int)SE;
    }
    
    /*-----適応ボタン-----*/

    //設定反映
    public void Commitsettings()
    {
        //スクリーンモード反映
        switch(settings.WindowMode)
        {
            case "Window":
            Screen.fullScreen = false;
                break;

            case "FullScreen":
            Screen.fullScreen = true;
                break;
        }
        
        settings.SetvolumeBGM = sliderBGM.value;
        settings.SetvolumeSE = sliderSE.value;

        //音量設定反映
        soundManager.SetnewValueBGM(settings.SetvolumeBGM);
        soundManager.SetnewValueSE(settings.SetvolumeSE);

        //設定の保存
        settings.WriteSettings();
    }
}
