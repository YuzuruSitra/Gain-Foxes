using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCnt2 : MonoBehaviour
{
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
    public bool FadeNow2 = false;

    [SerializeField] 
    private GameObject tutorialExp;
    [SerializeField] 
    private GameObject settingsPanel;

    void Start()
    {
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
        if(FadeNow2) return;
        //ESCでメニューを開き、もう一度押すと閉じる
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(cloaseEscMenu)
            {
                inGamePanel.SetActive(false);
                menuPanel.SetActive(true);
                selectPanel.SetActive(true);
                tutorialExp.SetActive(false);
                cloaseEscMenu = false;
                Time.timeScale = 0;
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
                Time.timeScale = 1;
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
        Time.timeScale = 1;
    }

    //設定パネル
    public void CloseSetting()
    {
        selectPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
