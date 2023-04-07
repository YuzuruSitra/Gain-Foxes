using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguageScene2 : MonoBehaviour
{
    [SerializeField]
    private Text[] _settingUIs = new Text[3];
    [SerializeField]
    private Text[] _menuPanels = new Text[6];
    [SerializeField]
    private Image _turtrialImage;
    [SerializeField]
    private Sprite[] _turtrialSprites = new Sprite[2];

    public void ChangeSettingsUI2(string[] settingsData)
    {
        _settingUIs[0].text = settingsData[2];
        _settingUIs[1].text = settingsData[3];
        _settingUIs[2].text = settingsData[5];
    }
    public void ChangeMenusUI2(string languageState, string[] menusData)
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
            _menuPanels[i].text = menusData[n];
        }
    }
}
