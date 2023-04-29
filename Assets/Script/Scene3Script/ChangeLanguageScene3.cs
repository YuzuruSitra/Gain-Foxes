using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguageScene3 : MonoBehaviour
{
    [SerializeField]
    private Text[] _settingUIs = new Text[3];
    [SerializeField]
    private Text[] _menuPanels = new Text[6];
    [SerializeField]
    private Text[] _scene3UIs = new Text[2];
    public string[] Scene3LanguaeData = new string[48];
    public string LanguageState_Scene3;
    [SerializeField]
    private LogSystem _logSystem;
    [SerializeField]
    private Image _turtrialImage;
    [SerializeField]
    private Sprite[] _turtrialSprites = new Sprite[2];
    private bool _oneTime = true;

    public void ChangeSettingsUI3(string[] settingsData)
    {
        _settingUIs[0].text = settingsData[2];
        _settingUIs[1].text = settingsData[3];
        _settingUIs[2].text = settingsData[5];
    }
    public void ChangeMenusUI3(string[] menusData)
    {
        for(int i = 0; i < 6; i++)
        {
            int n = i / 2; 
            _menuPanels[i].text = menusData[n];
        }
    }

    public void ChangeUI(string languageState, string[] stringData)
    {
        //チュートリアル画像入れ替え
        switch(languageState)
        {
            case "Japanese":
                _turtrialImage.sprite = _turtrialSprites[0];
                break;
            case "English":
                _turtrialImage.sprite = _turtrialSprites[1];
                break;
        }
        
        for(int i = 0; i < 2;i++)
        {
            _scene3UIs[i].text = stringData[i];
        }

        for(int i = 0; i < 48; i++)
        {
            Scene3LanguaeData[i] = stringData[i];
        }
        LanguageState_Scene3 = languageState;
        if(_oneTime)
        {
            _logSystem.GenerateLog();
            _oneTime = false;
        }
    }
}
