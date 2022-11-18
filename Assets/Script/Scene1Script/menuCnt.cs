using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuCnt : MonoBehaviour
{
    //ゲーム中断用
    public static bool ESCnow = false;

    //ゲームプレイ中のUIを格納
    [SerializeField] 
    private GameObject inGamePanel;
    [SerializeField] 
    private GameObject MenuPanel;
    [SerializeField] 
    private GameObject SelectPanel;
    //メニュー用bool
    private bool menuSwitch = true;

    [SerializeField] 
    private GameObject TutorialExp;
    [SerializeField] 
    private GameObject SettingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        //パネルは全て非アクティブ
        inGamePanel.SetActive(true);
        MenuPanel.SetActive(false);
        SelectPanel.SetActive(false);
        TutorialExp.SetActive(false);
        SettingsPanel.SetActive(false);
    }
    void Update()
    {
        //ESCでメニューを開き、もう一度押すと閉じる
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuSwitch)
            {
                inGamePanel.SetActive(false);
                MenuPanel.SetActive(true);
                SelectPanel.SetActive(true);
                menuSwitch = false;
                ESCnow = true;
            }
            else
            {
                closeMenu();
            }
        }
    }
    
    //設定パネルを開く
    public void pushSetting()
    {
        SelectPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
    //チュートリアル
    public void pushTutrial()
    {
        SelectPanel.SetActive(false);
        TutorialExp.SetActive(true);
    }
    //ホームに戻る
    public void pushHome()
    {
        SceneManager.LoadScene("Title");
        ESCnow = false;
    }

    /*------パネルを閉じる------*/

    //メニューパネル
    public void closeMenu()
    {
        inGamePanel.SetActive(true);
        MenuPanel.SetActive(false);
        SelectPanel.SetActive(false);
        menuSwitch = true;
        ESCnow = false;
    }

    //設定パネル
    public void closeSetting()
    {
        SelectPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
    //チュートリアル    
    public void closeTutrial()
    {
        SelectPanel.SetActive(true);
        TutorialExp.SetActive(false);
    }

}
