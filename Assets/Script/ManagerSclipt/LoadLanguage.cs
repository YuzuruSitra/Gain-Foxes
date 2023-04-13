using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class LoadLanguage : MonoBehaviour
{
    TrnMaster _trnMaster;
    private int _nowScene;
    private string _languageKind;

    void Awake()
    {
        _trnMaster = Resources.Load("GainFoxesTrnMaster") as TrnMaster; 
    }

    public void SetLanguage(string languageState)
    {
        _languageKind = languageState;
        Commit();
    }

    //コミットボタンもここから
    public void Commit()
    {
        _nowScene = SceneManager.GetActiveScene ().buildIndex;
        switch(_nowScene)
        {
            case 0:
                LoadLanguageData_Title(_languageKind);
                break; 
            case 1:
                LoadLanguageData_Scene1(_languageKind);
                break; 
            case 2:
                LoadLanguageData_Scene2(_languageKind);
                break;
            case 3:
                LoadLanguageData_Scene3(_languageKind);
                break; 
        }
    }

    public void LoadLanguageData_Title(string languageState)
    {
        ChangeLanguageTitle changeLanguageTitle; 
        changeLanguageTitle = GameObject.Find("LanguageUI_Title").GetComponent<ChangeLanguageTitle> ();
        string[] TitleString = new string[11];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 11; i++)
                {
                    TitleString[i] = _trnMaster.sheets[0].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 11; i++)
                {
                    TitleString[i]  = _trnMaster.sheets[0].list[i].English;
                }
                break;
        }
        changeLanguageTitle.ChangeUI(TitleString);
    }
    public void LoadLanguageData_Scene1(string languageState)
    {
        ChangeLanguageScene1 changeLanguageScene1;
        changeLanguageScene1 = GameObject.Find("LanguageUI_Scene1").GetComponent<ChangeLanguageScene1> ();
        string[] settingsString = new string[11];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 11; i++)
                {
                    settingsString[i] = _trnMaster.sheets[0].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 11; i++)
                {
                    settingsString[i]  = _trnMaster.sheets[0].list[i].English;
                }
                break;
        }   
        changeLanguageScene1.ChangeSettingsUI(settingsString); 

        string[] Scene1String = new string[76];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 76; i++)
                {
                    Scene1String[i] = _trnMaster.sheets[1].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 76; i++)
                {
                    Scene1String[i]  = _trnMaster.sheets[1].list[i].English;
                }
                break;
        }
        changeLanguageScene1.ChangeUI(languageState,Scene1String);
    }

    public void LoadLanguageData_Scene2(string languageState)
    {
        ChangeLanguageScene2 changeLanguageScene2;
        changeLanguageScene2 = GameObject.Find("LanguageUI_Scene2").GetComponent<ChangeLanguageScene2> ();
        string[] settingsString = new string[11];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 11; i++)
                {
                    settingsString[i] = _trnMaster.sheets[0].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 11; i++)
                {
                    settingsString[i]  = _trnMaster.sheets[0].list[i].English;
                }
                break;
        }   
        changeLanguageScene2.ChangeSettingsUI2(settingsString);

        string[] menuString = new string[3];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 3; i++)
                {
                    menuString[i] = _trnMaster.sheets[1].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 3; i++)
                {
                    menuString[i]  = _trnMaster.sheets[1].list[i].English;
                }
                break;
        }
        changeLanguageScene2.ChangeMenusUI2(languageState,menuString);
    }

    public void LoadLanguageData_Scene3(string languageState)
    {
        ChangeLanguageScene3 changeLanguageScene3;
        changeLanguageScene3 = GameObject.Find("LanguageUI_Scene3").GetComponent<ChangeLanguageScene3> ();
        string[] settingsString = new string[11];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 11; i++)
                {
                    settingsString[i] = _trnMaster.sheets[0].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 11; i++)
                {
                    settingsString[i]  = _trnMaster.sheets[0].list[i].English;
                }
                break;
        }   
        changeLanguageScene3.ChangeSettingsUI3(settingsString);

        string[] menuString = new string[3];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 3; i++)
                {
                    menuString[i] = _trnMaster.sheets[1].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 3; i++)
                {
                    menuString[i]  = _trnMaster.sheets[1].list[i].English;
                }
                break;
        }
        changeLanguageScene3.ChangeMenusUI3(menuString);

        string[] trnString = new string[47];
        switch (languageState)
        {
            case "Japanese":
                for(int i= 0; i < 47; i++)
                {
                    trnString[i] = _trnMaster.sheets[2].list[i].Japanese;
                }
                break;
            case "English":
                for(int i= 0; i < 47; i++)
                {
                    trnString[i]  = _trnMaster.sheets[2].list[i].English;
                }
                break;
        }   
        changeLanguageScene3.ChangeUI(languageState,trnString); 
    }
}
