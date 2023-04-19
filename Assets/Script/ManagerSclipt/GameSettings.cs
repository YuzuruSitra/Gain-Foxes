using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//設定項目管理
public class GameSettings : MonoBehaviour
{
    //サウンド管理用
    [SerializeField] 
    private SoundCnt soundManager; 
    //設定保存用
    [SerializeField]
    private StateManagement settings; 
    //言語管理用
    [SerializeField]
    private LoadLanguage _loadLanguageTitle;
    [SerializeField]
    private Slider sliderBGM;
    [SerializeField]
    private Slider sliderSE;
    [SerializeField]
    private Text textBGM;
    [SerializeField]
    private Text textSE;
    public string LanguageMode;

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
        settings = GameObject.Find("StateManagement").GetComponent<StateManagement> ();
        LaunchSystem();
    }
    public void LaunchSystem()
    {
        launchScreenMode();
        LaunchVolume();
        LaunchLanguage();
    }

    /*--画面モード切り替え--*/

    //起動時のセット
    void launchScreenMode()
    {
        switch (settings.WindowMode)
        {
            case "Window":     
                //Screen.SetResolution(1280, 720, false);
                Screen.fullScreen = false;
                break;

            case "FullScreen":
                Screen.SetResolution(1920, 1080, true);
                //Screen.fullScreen = true;
                break;

            default:
                Screen.SetResolution(1920, 1080, true);
                break;
        }
    }

    //ウィンドウモード
    public void WindowsScreenMode()
    {
        settings.WindowMode = "Window";
    }

    //フルスクリーンモード
    public void FullScreenMode()
    {
        settings.WindowMode = "FullScreen";
    }

    void LaunchLanguage()
    {
        LanguageMode = settings.UseLanguage;
        _loadLanguageTitle = GameObject.Find("LanguageManager").GetComponent<LoadLanguage> ();
        _loadLanguageTitle.SetLanguage(LanguageMode);
    }

    //言語の設定
    public void SetJapanese()
    {
        LanguageMode = "Japanese";
    }
    public void SetEnglish()
    {
        LanguageMode = "English";
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
            Screen.SetResolution(1280, 720, false);
            //Screen.fullScreen = false;
                break;

            case "FullScreen":
            Screen.SetResolution(1920, 1080, true);
            //Screen.fullScreen = true;
                break;
        }
        settings.UseLanguage = LanguageMode;
        
        settings.SetvolumeBGM = sliderBGM.value;
        settings.SetvolumeSE = sliderSE.value;
        _loadLanguageTitle.SetLanguage(LanguageMode);

        //音量設定反映
        soundManager.SetnewValueBGM(settings.SetvolumeBGM);
        soundManager.SetnewValueSE(settings.SetvolumeSE);

        //設定の保存
        settings.WriteSettings();
    }
}
