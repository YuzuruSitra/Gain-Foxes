using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//プレイ中の設定パネル管理
public class MenuCnt : MonoBehaviour
{
    //UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    //ゲーム中断用
    public static bool ESCnow;
    //チュートリアルパネルを展開しているか
    private bool openTutorialPanel;

    //ゲームプレイ中のUIを格納
    [SerializeField] 
    private GameObject inGamePanel;
    [SerializeField] 
    private GameObject menuPanel;
    [SerializeField] 
    private GameObject selectPanel;
    //メニュー用bool
    private bool cloaseEscMenu = true;

    [SerializeField] 
    private GameObject tutorialExp;
    [SerializeField] 
    private GameObject settingsPanel;

    void Start()
    {
        ESCnow = false;
        openTutorialPanel = false;
        //パネルは全て非アクティブ
        inGamePanel.SetActive(true);
        menuPanel.SetActive(false);
        selectPanel.SetActive(false);
        tutorialExp.SetActive(false);
        settingsPanel.SetActive(false);
    }
    void Update()
    {
        //ESCでメニューを開き、もう一度押すと閉じる
        if (Input.GetKeyDown(KeyCode.Escape) && !uiCont1.NowClear)
        {
            if(cloaseEscMenu)
            {
                inGamePanel.SetActive(false);
                menuPanel.SetActive(true);
                selectPanel.SetActive(true);
                tutorialExp.SetActive(false);
                cloaseEscMenu = false;
                ESCnow = true;
            }
            else
            {
                //ESCパネルを閉じる
                inGamePanel.SetActive(true);
                menuPanel.SetActive(false);
                selectPanel.SetActive(false);
                tutorialExp.SetActive(false);
                settingsPanel.SetActive(false);
                cloaseEscMenu = true;
                ESCnow = false;
            }
        }
    }
    
    //設定パネルを開く
    public void PushSetting()
    {
        selectPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    //チュートリアル
    public void PushTutrial()
    {
        selectPanel.SetActive(false);
        tutorialExp.SetActive(true);
        openTutorialPanel = true;
    }
    //ホームに戻る
    public void PushHome()
    {
        SceneManager.LoadScene("Title");
        ESCnow = false;
    }

    /*------パネルを閉じる------*/

    //メニューパネル
    public void CloseMenu()
    {
        //チュートリアルパネルが開いている場合はそちらを閉じ処理を終える
        if(openTutorialPanel)
        {
            selectPanel.SetActive(true);
            tutorialExp.SetActive(false);
            openTutorialPanel = false;
            return;
        }

        //ESCパネルを閉じる
        inGamePanel.SetActive(true);
        menuPanel.SetActive(false);
        selectPanel.SetActive(false);
        cloaseEscMenu = true;
        ESCnow = false;
    }

    //設定パネル
    public void CloseSetting()
    {
        selectPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
