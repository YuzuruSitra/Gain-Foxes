using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguageScene1 : MonoBehaviour
{
    [SerializeField]
    private Text[] _settingUIs = new Text[3];
    [SerializeField]
    private Text[] _menuPanels = new Text[6];
    [SerializeField]
    private Text[] _buttonUIs = new Text[3];
    public string[] Scene1LanguaeData = new string[76];
    [SerializeField]
    private Image _turtrialImage;
    [SerializeField]
    private Sprite[] _turtrialSprites = new Sprite[2];
    public string LanguageState_Scene1;

    public void ChangeSettingsUI(string[] settingsData)
    {
        _settingUIs[0].text = settingsData[2];
        _settingUIs[1].text = settingsData[3];
        _settingUIs[2].text = settingsData[5];
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

        for(int i = 0; i < 6; i++)
        {
            int n = i / 2; 
            _menuPanels[i].text = stringData[n];
        }

        for(int i = 0; i < 76; i++)
        {
            Scene1LanguaeData[i] = stringData[i];
        }

        _buttonUIs[0].text = Scene1LanguaeData[73];
        _buttonUIs[1].text = Scene1LanguaeData[74];
        _buttonUIs[2].text = Scene1LanguaeData[75];
        LanguageState_Scene1 = languageState;
    }
}
