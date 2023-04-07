using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESCCntrol : MonoBehaviour
{
    //ログシステム
    [SerializeField]
    private LogSystem _logSystem;

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
        //ログシステム
        _logSystem = GameObject.Find("LogSystem").GetComponent<LogSystem> ();
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(cloaseEscMenu)
            {
                inGamePanel.SetActive(false);
                menuPanel.SetActive(true);
                selectPanel.SetActive(true);
                tutorialExp.SetActive(false);
                cloaseEscMenu = false;
                _logSystem.StopTextAnim();
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
                _logSystem.StartTextAnim();
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
        _logSystem.StartTextAnim();
    }

    //設定パネル
    public void CloseSetting()
    {
        selectPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
