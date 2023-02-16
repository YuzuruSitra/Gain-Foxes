using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//プレイ中の設定パネル管理
public class menuCnt : MonoBehaviour
{
    //ゲーム中断用
    public static bool ESCnow = false;
    //チュートリアルパネルを展開しているか
    private bool openTutorialPanel;

    //ゲームプレイ中のUIを格納
    [SerializeField] 
    private GameObject inGamePanel;
    [SerializeField] 
    private GameObject MenuPanel;
    [SerializeField] 
    private GameObject SelectPanel;
    //メニュー用bool
    private bool cloaseEscMenu = true;

    [SerializeField] 
    private GameObject TutorialExp;
    [SerializeField] 
    private GameObject SettingsPanel;

    void Start()
    {
        openTutorialPanel = false;
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
        if (Input.GetKeyDown(KeyCode.Escape) && !UICont1.instanceUI1.nowClear)
        {
            if(cloaseEscMenu)
            {
                inGamePanel.SetActive(false);
                MenuPanel.SetActive(true);
                SelectPanel.SetActive(true);
                cloaseEscMenu = false;
                ESCnow = true;
            }
            else
            {
                //ESCパネルを閉じる
                inGamePanel.SetActive(true);
                MenuPanel.SetActive(false);
                SelectPanel.SetActive(false);
                TutorialExp.SetActive(false);
                SettingsPanel.SetActive(false);
                cloaseEscMenu = true;
                ESCnow = false;
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
        openTutorialPanel = true;
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
        //チュートリアルパネルが開いている場合はそちらを閉じ処理を終える
        if(openTutorialPanel)
        {
            SelectPanel.SetActive(true);
            TutorialExp.SetActive(false);
            openTutorialPanel = false;
            return;
        }

        //ESCパネルを閉じる
        inGamePanel.SetActive(true);
        MenuPanel.SetActive(false);
        SelectPanel.SetActive(false);
        cloaseEscMenu = true;
        ESCnow = false;
    }

    //設定パネル
    public void closeSetting()
    {
        SelectPanel.SetActive(true);
        SettingsPanel.SetActive(false);
    }
}
