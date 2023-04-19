using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//シーンAのUI、演出管理
public class UICont1 : MonoBehaviour
{

    //シーン管理クラスの取得
    [SerializeField]
    private SceneCnt1 sceneCnt1;
    //言語用のクラス
    [SerializeField]
    private ChangeLanguageScene1 _languageCnt;
    //戦略パネル用
    [SerializeField]
    private StrExpText _strExpText;
    //チュートリアル用
    [SerializeField]
    private GameObject tutrialExp;
    [SerializeField]
    private Sprite[] _turtrialSprites = new Sprite[2];
    [SerializeField]
    private GameObject haveMoneyPanel;
    [SerializeField]
    private GameObject slaveTurnPanel;
    private bool firstESC;

    [SerializeField]
    private Text slaveTx;
    [SerializeField]
    private Text crimeRateTx;
    [SerializeField]
    private Text havemoneyT;
    [SerializeField]
    private Text turnCountText;

    public int PushN;   //ステート管理
    private int pushNtutorial;
    [SerializeField]
    private GameObject foxDia1;
    [SerializeField]
    private Text foxDia1_Text;
    [SerializeField]
    private GameObject foxDia2;
    [SerializeField]
    private Text foxDia2_Text;
    [SerializeField]
    private GameObject foxDia3;
    [SerializeField]
    private Text foxDia3_Text;
    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private GameObject strUI;
    [SerializeField]
    private GameObject itemSelectUI;
    [SerializeField]
    private Text itemName;
    //戦略Doneボタン
    [SerializeField]
    private GameObject doneButton;

    //暗殺パネル
    [SerializeField]
    private GameObject killPanel;
    [SerializeField]
    private GameObject strPanel;
    [SerializeField]
    private GameObject strDoneButton;
    private bool openPanel; //パネル展開
    private int selectKill; //暗殺対象
    [SerializeField]
    private Text peopleNameText; //名前を入れる箱
    private Animator animator; //アニメーター
    [SerializeField]
    private GameObject peopleAnim; //アニメーションさせるオブジェクト

    //交渉パネル
    [SerializeField]
    private GameObject publicityPanel;
    [SerializeField]
    private Text publiMoneyText;
    [SerializeField]
    private Text publiPlayerText;
    [SerializeField]
    private Text publiOtherText;
    [SerializeField]
    private GameObject publidoneButton;
    [SerializeField]
    private Button publiDoneBt;

    //入荷パネル
    [SerializeField]
    private GameObject dealPanel;
    [SerializeField]
    private Button dealButton;
    [SerializeField]
    private Text dealItem;
    private int selectItem_D;
    [SerializeField]
    private Text dNeedPrice; //必要価格
    [SerializeField]
    private Text dOfferPrice; //販売価格
    [SerializeField]
    private Text dButton; //ボタン
    [SerializeField]
    private Text dExpText; //説明文入れ替え↓
    private string[] words;
    private bool runDispo;
    [SerializeField]
    private GameObject foxyPanel;
    [SerializeField]
    private Text foxyText; //主人公一言
    private string foxySay; //主人公セリフ格納
    private bool pushReple = false;

    [SerializeField]
    private GameObject dDoneButton; //入荷待ちに非アクティブ化

    //入荷後の画像入れ替え用
    [SerializeField]
    private GameObject dPotionFlame;
    [SerializeField]
    private GameObject dStockflame;

    //株専用パネル
    [SerializeField]
    private GameObject stockCountPanel;
    [SerializeField]
    private Text stockCountText;

    //アイテムゲットUI
    [SerializeField]
    private GameObject getItemPanel;
    [SerializeField]
    private GameObject getItemFlame;
    [SerializeField]
    private Text getItemExpText; //アイテム獲得
    [SerializeField]
    private Text getExpText; //一言
    [SerializeField]
    private Text getChildText; //子分
    private string getChildS;
    private string getPorS; //ポーションorカブ格納


    //アイテム選択
    [SerializeField]
    private Button buttonSelectItem;
    [SerializeField]
    private Button buttonKill;
    [SerializeField]
    private Button buttonPubliUp;
    [SerializeField]
    private Button buttonPubliDown;

    //イベント発生フラグ
    private bool potionRepleIvent = true;
    private bool stockRepleIvent = true;
    private bool geneRepleEvent = true; //次のターンにイベント発生フラグ
    public bool ClearAnim;
    public bool PlClearDoAnim = false;
    public bool FinClearAnim = false;
    [SerializeField]
    private GameObject gameClearPanel;
    [SerializeField]
    private Text clearResultText;
    //クリア時のescキー無効判定
    public bool NowClear;
    //ゲームオーバー
    public bool DoOverAnim = false;
    public bool GameOverFade = false;
    [SerializeField]
    private GameObject gameOverPanel;

    //サウンド用スクリプト取得
	[SerializeField] 
    private SoundCnt soundA;
    [SerializeField]
    private AudioClip sceneA_BGM;
    [SerializeField]
    private AudioClip gameClearBGM;
    [SerializeField]
    private AudioClip gameOverBGM;

    [SerializeField] 
    private AudioClip pushButtonSE;
    [SerializeField] 
    private AudioClip buyNewItemsSE;
    [SerializeField] 
    private AudioClip textFlowSE;
    public bool ReadNow = false;

    /*---------商品選択用----------*/

    //入手後の素材
    [SerializeField]
    private Sprite havePotion;
    [SerializeField]
    private Sprite haveStock;
    //コンポーネント取得先
    [SerializeField]
    private GameObject potionFlame;
    [SerializeField]
    private GameObject stockFlame;

    //表示制御用所持チェック
    private bool havePotionN = false;
    private bool haveStockN = false;

    //テキスト入れ替え
    [SerializeField]
    private Text BrSwordName;
    [SerializeField]
    private Text potionName;
    [SerializeField]
    private Text stockName;

    //購入パネル初期テキスト
    private bool drawSelectItem;

    //クリック重複ケア
    private bool clickJudge;
    //戦略パネルケア
    private bool _strStaste = true;
    //戦略選択チェック
    public int SelectStr;
    private const float WaitSecondsJP = 0.03f;
    private const float WaitSecondsEN = 0.007f;

    void Start()
    {
        PushN = 0;
        pushNtutorial = 0;
        foxDia1.gameObject.SetActive(false);
        foxDia2.gameObject.SetActive(false);
        foxDia3.gameObject.SetActive(false);
        mainUI.gameObject.SetActive(false);
        itemSelectUI.gameObject.SetActive(false);
        strUI.gameObject.SetActive(false);
        killPanel.gameObject.SetActive(false);
        dealPanel.gameObject.SetActive(false);
        strDoneButton.gameObject.SetActive(false);
        getItemPanel.gameObject.SetActive(false);
        gameClearPanel.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        publicityPanel.SetActive(false);
        foxyPanel.SetActive(false);

        ClearAnim = false;
        clickJudge = true;
        openPanel = false;
        pushReple = false;
        NowClear = false;
        drawSelectItem = true;
        SelectStr = 0;
        selectKill = 0; //暗殺対象リセット
        selectItem_D = 0; //取引のアイテム選択リセット

        tutrialExp.gameObject.SetActive(false);

        runDispo = true;

        /*---bgm設定---*/
        soundA = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();

        // 言語設定用
        _languageCnt = GameObject.Find("LanguageUI_Scene1").GetComponent<ChangeLanguageScene1> ();
        _strExpText = GameObject.Find("UICont").GetComponent<StrExpText> (); 

        if (ParameterCalc.instanceCalc.GameClear)  //クリア処理
        {
           soundA.PlayBgm(gameClearBGM);
        }
        else if(ParameterCalc.instanceCalc.GameOver) //ゲームオーバー処理
        {
            soundA.PlayBgm(gameOverBGM);
        }
        else    //通常BGM
        {
            soundA.PlayBgm(sceneA_BGM);
        }

        // UI制御用商品の解放管理
        if(ParameterCalc.instanceCalc.TurnCount >= ParameterCalc.instanceCalc.PopTurnEvent)
        {
            if(ParameterCalc.instanceCalc.HavePotionJ)havePotionN = true;
            if(ParameterCalc.instanceCalc.HaveStockJ)haveStockN = true;
        }

    }

    void Update()
    {
        //UI更新
        slaveTx.text = ParameterCalc.instanceCalc.Slave + "";
        crimeRateTx.text = ParameterCalc.instanceCalc.CrimeRate + "/100";
        havemoneyT.text = ParameterCalc.instanceCalc.HaveMoney + "";
        // 日付UI
        switch(_languageCnt.LanguageState_Scene1)
        {
            case "Japanese":
                turnCountText.text = ParameterCalc.instanceCalc.TurnCount + _languageCnt.Scene1LanguaeData[38];
                break;
            case "English":
                turnCountText.text = _languageCnt.Scene1LanguaeData[38] + " " + ParameterCalc.instanceCalc.TurnCount;
                break;                        
        }

        //Doneボタン管理
        if (SelectStr == 0)
        {
            doneButton.gameObject.SetActive(false);
        }
        else
        {
            doneButton.gameObject.SetActive(true);
        }

        //戦略ボタンDoneを押せなくする
        if (SelectStr == 3 || SelectStr == 4) //窃盗か暗殺の時
        {
            if (ParameterCalc.instanceCalc.Slave > 0)
            {
                doneButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                doneButton.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            doneButton.GetComponent<Button>().interactable = true;
        }

        //狐の会話
        if (Input.GetMouseButtonDown(0) && clickJudge && !MenuCnt.ESCnow)
        {
            if(ParameterCalc.instanceCalc.InitialPlay)
            {
                //チュートリアル用ESC説明
                if(firstESC && pushNtutorial != 6)
                {
                    pushNtutorial += 1;
                }
                clickJudge = false;
            }
            else
            {
                PushN += 1;
                clickJudge = false;
            }
        }

        /*--------SE--------*/

        if(ReadNow)
        {
            if(!soundA.CheckReadSE())
            {
                //テキストを表示している間再生
                soundA.PlayReadSe(textFlowSE);
            }
        }
        else
        {
            soundA.StopSE();
        }

        //チュートリアル
        if(ParameterCalc.instanceCalc.InitialPlay)
        {
            Tutorial();
        }
        else if (ParameterCalc.instanceCalc.GameClear && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent)  //クリア処理
        {
            ClearIvent();
        }
        else if(ParameterCalc.instanceCalc.GameOver && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent) //ゲームオーバー処理
        {
            GameOverEvent();
        }
        else if(geneRepleEvent && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent) //前ターンの入荷イベント発生
        {
            GetFirst();
            geneRepleEvent = false;
        }
        else    //通常処理
        {
            var serifTmp = "";
            switch (PushN)
            {
                case 1:
                    foxDia1.gameObject.SetActive(true);
                    //foxDia1_Text.text = "おかしら！\n" + ParameterCalc.instanceCalc.TurnCount + "日目ですね！";
                    switch(_languageCnt.LanguageState_Scene1)
                    {
                        case "Japanese":
                            serifTmp = _languageCnt.Scene1LanguaeData[3] + "! " + ParameterCalc.instanceCalc.TurnCount + _languageCnt.Scene1LanguaeData[4];
                            break;
                        case "English":
                            serifTmp = _languageCnt.Scene1LanguaeData[3] + "! " + _languageCnt.Scene1LanguaeData[4] + ParameterCalc.instanceCalc.TurnCount + "!";
                            break;                        
                    }
                    foxDia1_Text.text = serifTmp;
                    clickJudge = true;
                    break;

                case 2:
                    //foxDia1_Text.text = "今日はどんな工作を？";
                    serifTmp = _languageCnt.Scene1LanguaeData[5];
                    foxDia1_Text.text = serifTmp;
                    clickJudge = true;
                    break;

                case 3:
                    foxDia1.gameObject.SetActive(false);
                    mainUI.gameObject.SetActive(true);
                    strUI.gameObject.SetActive(true);
                    if(!_strStaste)return;
                    _strExpText._DoSetFirst = true;
                    _strExpText.SetFirstExp();
                    ReadNow = false;
                    _strStaste = false;
                    if (!openPanel)
                    {
                        strDoneButton.gameObject.SetActive(true);
                    }
                    break;

                case 4:
                    mainUI.gameObject.SetActive(false);
                    foxDia1.gameObject.SetActive(true);
                    //foxDia1_Text.text = "かしこまり！";
                    serifTmp = _languageCnt.Scene1LanguaeData[6];
                    foxDia1_Text.text = serifTmp;
                    clickJudge = true;
                    break;

                case 5:
                    foxDia1.gameObject.SetActive(false);
                    foxDia2.gameObject.SetActive(true);
                    //foxDia2_Text.text = "おかしら！\n次はどんな工作を？";
                    switch(_languageCnt.LanguageState_Scene1)
                    {
                        case "Japanese":
                            serifTmp = _languageCnt.Scene1LanguaeData[3] + "!\n" + _languageCnt.Scene1LanguaeData[7];
                            break;
                        case "English":
                            serifTmp = _languageCnt.Scene1LanguaeData[7];
                            break;                        
                    }
                    foxDia2_Text.text = serifTmp;
                    clickJudge = true;
                    break;

                case 6:
                    foxDia2.gameObject.SetActive(false);
                    mainUI.gameObject.SetActive(true);
                    strUI.gameObject.SetActive(true);
                    if(_strStaste)return;
                    _strExpText._DoSetFirst = true;
                    _strExpText.SetFirstExp();
                    ReadNow = false;
                    _strStaste = true;
                    if (!openPanel)
                    {
                        strDoneButton.gameObject.SetActive(true);
                    }
                    break;

                case 7:
                    mainUI.gameObject.SetActive(false);
                    strUI.gameObject.SetActive(false);
                    foxDia2.gameObject.SetActive(true);
                    //foxDia2_Text.text = "はい！";
                    serifTmp = _languageCnt.Scene1LanguaeData[8];
                    foxDia2_Text.text = serifTmp;
                    clickJudge = true;
                    break;

                case 8:
                    foxDia2.gameObject.SetActive(false);
                    foxDia3.gameObject.SetActive(true);
                    //foxDia3_Text.text = "おかしら。\n今日は何を売る？";
                    switch(_languageCnt.LanguageState_Scene1)
                    {
                        case "Japanese":
                            serifTmp = _languageCnt.Scene1LanguaeData[3] + "。\n" + _languageCnt.Scene1LanguaeData[9];
                            break;
                        case "English":
                            serifTmp = _languageCnt.Scene1LanguaeData[9];
                            break;                        
                    }
                    foxDia3_Text.text = serifTmp;
                    clickJudge = true;
                    break;

                case 9:
                    foxDia3.gameObject.SetActive(false);
                    mainUI.gameObject.SetActive(true);
                    itemSelectUI.gameObject.SetActive(true);
                    if (drawSelectItem)
                    {
                        BrSword();
                        drawSelectItem = false;
                    }
                    //アイテム選択画面UI更新
                    ForDrawPanel();
                    break;

                case 10:
                    foxDia3.gameObject.SetActive(true);
                    //foxDia3_Text.text ="御意。";
                    serifTmp = _languageCnt.Scene1LanguaeData[10];
                    foxDia3_Text.text = serifTmp;
                    mainUI.gameObject.SetActive(false);
                    itemSelectUI.gameObject.SetActive(false);
                    clickJudge = true;
                    break;

                case 11:
                    foxDia3.gameObject.SetActive(false);
                    clickJudge = true;
                    break;

                case 12:
                    //シーンコントロールへ
                    sceneCnt1.IsFadeOut_A = true;
                    break;
            }
        }
    }

    //戦略選択
    public void SetGossip()
    {
        if(ReadNow)return;
        SelectStr = 1;
        PushButtonSE_A();
    }

    public void SetPray()
    {
        if(ReadNow)return;
        SelectStr = 2;
        PushButtonSE_A();
    }
    public void SetSteal()
    {
        if(ReadNow)return;
        SelectStr = 3;
        PushButtonSE_A();
    }

    public void SetKill()
    {
        if(ReadNow)return;
        SelectStr = 4;
        PushButtonSE_A();
    }

    public void SetDeal()
    {
        if(ReadNow)return;
        SelectStr = 5;
        PushButtonSE_A();
    }

    //戦略決定
    public void StrDone()
    {
        var serifTmp = "";
        switch (SelectStr)
        {
            case 1:
                ParameterCalc.instanceCalc.StrGossip();
                PushN++;
                break;

            case 2:
                ParameterCalc.instanceCalc.StrPray();
                PushN++;
                break;

            case 3:
                DrowPubli();  //戦略パネル更新
                strDoneButton.gameObject.SetActive(false);
                strPanel.gameObject.SetActive(false);
                publicityPanel.SetActive(true);
                openPanel = true;
                foxyPanel.SetActive(true);//画面下部フィネ

                //交渉済みかどうか
                if (!ParameterCalc.instanceCalc.usePubli)
                {
                    //foxySay = " 少 し う さ ん く さ い な . . . ";
                    serifTmp = _languageCnt.Scene1LanguaeData[39];
                    foxySay = serifTmp;
                }
                else
                {
                    //foxySay = " 忙 し そ う だ . . . ";
                    serifTmp = _languageCnt.Scene1LanguaeData[40];
                    foxySay = serifTmp;
                }
                StartCoroutine(Publi_Log());
                break;

            case 4:
                strDoneButton.gameObject.SetActive(false);
                strPanel.gameObject.SetActive(false);
                killPanel.gameObject.SetActive(true);
                openPanel = true;
                foxyPanel.SetActive(true);//画面下部フィネ

                //暗殺パネル
                if (!ParameterCalc.instanceCalc.ExeKill)
                {
                    //foxySay = " 裏 工 作 . . . あ ま り 気 が 乗 ら な い 。 ";
                    serifTmp = _languageCnt.Scene1LanguaeData[41];
                    foxySay = serifTmp;
                    buttonKill.interactable = true;
                }
                else
                {
                    //foxySay = " こ れ 以 上 騒 ぎ は 起 こ せ な い . . . ";
                    serifTmp = _languageCnt.Scene1LanguaeData[42];
                    foxySay = serifTmp;
                    buttonKill.interactable = false;
                }
                StartCoroutine(Publi_Log());
                break;
                
            case 5:
                selectItem_D = 0;
                D_Apparent(); //戦略パネル更新
                strDoneButton.gameObject.SetActive(false);
                strPanel.gameObject.SetActive(false);
                dealPanel.gameObject.SetActive(true);
                openPanel = true;
                foxyPanel.SetActive(true);
                break;
        }
        SelectStr = 0;
    }

    /*----------キルパネル----------*/
    
    //実行処理
    public void KillDone()
    {
        ParameterCalc.instanceCalc.SelectKillPanel = selectKill;
        PushN++;
        ParameterCalc.instanceCalc.StrKill();

        SelectStr = 4;
        strDoneButton.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(true);
        killPanel.gameObject.SetActive(false);
        foxyPanel.SetActive(false);
        openPanel = false;
    }

    public void KillChoseDown() //民衆選択↓
    {
        //0-3を循環
        selectKill -= 1;
        if(selectKill < 0)
        {
            selectKill = 3;
        }
        KillPanelAnim();
    }

    public void KillChoseUp() //民衆選択↑
    {
        //0-3を循環
        selectKill += 1;
        if (selectKill > 3)
        {
            selectKill = 0;
        }
        KillPanelAnim();
    }

    //演出処理
    public void KillPanelAnim()
    {
        animator = peopleAnim.GetComponent<Animator>();
        string[] peopleName = { 
            _languageCnt.Scene1LanguaeData[27], _languageCnt.Scene1LanguaeData[28], _languageCnt.Scene1LanguaeData[29], _languageCnt.Scene1LanguaeData[30]
            }; //名前
        peopleNameText.text = peopleName [selectKill];
        animator.SetInteger("SelectPeo", selectKill);
    }

    //戻る
    public void KillSTRback()
    {
        SelectStr = 4;
        strDoneButton.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(true);
        killPanel.gameObject.SetActive(false);
        foxyPanel.SetActive(false);
        openPanel = false;
    }

    /*----------取引パネル----------*/

    //交渉金額
    public void PubliWayUp()
    {
        if(runDispo)
        {
            ParameterCalc.instanceCalc.PubliWay += 1;
            DrowPubli();
        }
    }

    public void PubliWayDown()
    {
        if(runDispo)
        {
            ParameterCalc.instanceCalc.PubliWay -= 1;
            DrowPubli();
        }
    }

    public void PubliDone()
    {
        ParameterCalc.instanceCalc.PubliCalc();
        SelectStr = 2;
        strDoneButton.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(true);
        publicityPanel.SetActive(false);
        openPanel = false;
        foxyPanel.SetActive(false);
        PushN++;
    }

    //交渉パネルセリフ更新
    private void DrowPubli()
    {
        // ボタンの制御
        int publiWayInt = ParameterCalc.instanceCalc.PubliWay;
        switch(publiWayInt)
        {
            case 0:
                buttonPubliDown.interactable = false;
                break;
            case 2:
                buttonPubliUp.interactable = false;
                break;
            default:
                buttonPubliDown.interactable = true;
                buttonPubliUp.interactable = true;
                break;
        }

        var serifTmp1 = "";
        var serifTmp2 = "";
        runDispo = false;
        publiDoneBt = publidoneButton.GetComponent<Button>();
        //計算処理
        double Conv = ParameterCalc.instanceCalc.HaveMoney * ParameterCalc.instanceCalc.PubliRisk[ParameterCalc.instanceCalc.PubliWay];
        ParameterCalc.instanceCalc.PubliWayPay = (int)Conv;
        publiMoneyText.text = ParameterCalc.instanceCalc.PubliWayPay + "";
        if (!ParameterCalc.instanceCalc.usePubli)
        {
            //publiPlayerSay = " . . . " + ParameterCalc.instanceCalc.PubliWayPay + " z で ど う だ ？ ";
            //publiOtherSay = " そ の 報 酬 だ と 成 功 率 は "+ ParameterCalc.instanceCalc.PubliRisk[ParameterCalc.instanceCalc.PubliWay] * 100 +" % っ て と こ だ 。 ";
            switch(_languageCnt.LanguageState_Scene1)
            {
                case "Japanese":
                    serifTmp1 = ".^.^.^" + ParameterCalc.instanceCalc.PubliWayPay + _languageCnt.Scene1LanguaeData[43];
                    serifTmp2 = _languageCnt.Scene1LanguaeData[44] + ParameterCalc.instanceCalc.PubliRisk[ParameterCalc.instanceCalc.PubliWay] * 100 + _languageCnt.Scene1LanguaeData[45];
                    break;
                case "English":
                    serifTmp1 = _languageCnt.Scene1LanguaeData[43] + ParameterCalc.instanceCalc.PubliWayPay + "z.";
                    serifTmp2 = _languageCnt.Scene1LanguaeData[44] + ParameterCalc.instanceCalc.PubliRisk[ParameterCalc.instanceCalc.PubliWay] * 100 + _languageCnt.Scene1LanguaeData[45];
                    break;                        
            }        
        }
        else
        {
            //publiPlayerSay = " . . . . . . ";
            //publiOtherSay = " 大 人 し く 結 果 を 待 つ ん だ な 。 ";
            serifTmp1 = ".^.^.^.^.^.^";
            serifTmp2 = _languageCnt.Scene1LanguaeData[46];
            publiDoneBt.interactable = false;
        }
        StartCoroutine(PubliSay(serifTmp1,serifTmp2));
    }

    //交渉パネル二人の会話処理
    IEnumerator PubliSay(string publiPlayerSayTmp ,string publiOtherSayTmp)
    {
        ReadNow = true; //文字表示用SE開始

        publiPlayerText.text = "";
        publiOtherText.text = "";

        words = publiPlayerSayTmp.Split('^');
        foreach (var word in words)
        {
            publiPlayerText.text = publiPlayerText.text + word;
            switch(_languageCnt.LanguageState_Scene1)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }

        //改行コード変換
        if (publiOtherSayTmp.Contains("\\n"))
        {
            publiOtherSayTmp = publiOtherSayTmp.Replace(@"\n", Environment.NewLine);
        }

        words = publiOtherSayTmp.Split('^');
        
        foreach (var word in words)
        {
            publiOtherText.text = publiOtherText.text + word;
            switch(_languageCnt.LanguageState_Scene1)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }
        ReadNow = false; //文字表示用SE終了
        runDispo = true; 
    }

    /*----------取引パネル----------*/

    //実行処理
    public void DealDone()
    {
        //商品強化処理
        ParameterCalc.instanceCalc.SelectRepleItem = selectItem_D;
        ParameterCalc.instanceCalc.StrReple();
        SelectStr = 5;
        strDoneButton.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(true);
        dealPanel.gameObject.SetActive(false);
        foxyPanel.SetActive(false);
        openPanel = false;
        PushN++;

        if (selectItem_D == 3) //ポーション初契約
        {
            ParameterCalc.instanceCalc.PopTurnEvent = ParameterCalc.instanceCalc.TurnCount + 1; //次ターン制限
            pushReple = true; //１ターンに入荷は一度まで
        }
        else if (selectItem_D == 4) //株初契約
        {
            ParameterCalc.instanceCalc.PopTurnEvent = ParameterCalc.instanceCalc.TurnCount + 1; //次ターン制限
            pushReple = true; //１ターンに入荷は一度まで
        }
    }

    //戻る
    public void Publi_STRback()
    {
        SelectStr = 3;
        strDoneButton.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(true);
        publicityPanel.SetActive(false);
        openPanel = false;
        foxyPanel.SetActive(false);
    }

    //アイテム選択
    public void D_DelectSwords() //剣
    {
        if (runDispo)
        {
            selectItem_D = 0;
            D_Apparent();
            PushButtonSE_A();
        }
    }

    public void D_DelectPotion() //薬
    {
        if (runDispo)
        {
            if (havePotionN)
            {
                selectItem_D = 1;
            }
            else
            {
                selectItem_D = 3;
            }
            D_Apparent();
            PushButtonSE_A();
        }
    }
    
    public void D_DelectStock() //株
    {
        if (runDispo)
        {
            if (haveStockN)
            {
                selectItem_D = 2;
            }
            else
            {
                selectItem_D = 4;
            }
            D_Apparent();
            PushButtonSE_A();
        }
    }

    
    //株の個数選択
    public void StockCountUp()
    {
        var serifTmp = "";
        ParameterCalc.instanceCalc.StockReceived += 5;
        const int MaxQuantity = 500;
        int purchaseLimit = MaxQuantity - ParameterCalc.instanceCalc.StockQuantity;
        if(ParameterCalc.instanceCalc.StockReceived > purchaseLimit)
        {
            ParameterCalc.instanceCalc.StockReceived = purchaseLimit;
        }
        stockCountText.text = "" + ParameterCalc.instanceCalc.StockReceived;
        //dNeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";
        serifTmp = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + _languageCnt.Scene1LanguaeData[47];
        dNeedPrice.text = serifTmp;
        //0個以上かつ買える時はインタラクティブ
        if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived && 0 < ParameterCalc.instanceCalc.StockReceived)
        {
            dealButton.interactable = true;
        }
        else
        {
            dealButton.interactable = false;
        }
    }

    public void StockCountDown()
    {
        var serifTmp = "";
        ParameterCalc.instanceCalc.StockReceived -= 5;
        if (ParameterCalc.instanceCalc.StockReceived < 0)
        {
            ParameterCalc.instanceCalc.StockReceived = 0;
        }
        stockCountText.text = "" + ParameterCalc.instanceCalc.StockReceived;
        //dNeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";
        serifTmp = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + _languageCnt.Scene1LanguaeData[47];
        dNeedPrice.text = serifTmp;

        //0個以上かつ買える時はインタラクティブ
        if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived && 0 < ParameterCalc.instanceCalc.StockReceived)
        {
            dealButton.interactable = true;
        }
        else
        {
            dealButton.interactable = false;
        }
    }

    //表示の処理
    public void D_Apparent()
    {
        dDoneButton.SetActive(true);
        //アイテム入荷画像の取得
        Image dSpritePotion = dPotionFlame.GetComponent<Image>(); //薬
        Image dSpriteStock = dStockflame.GetComponent<Image>(); //株

        //画像制御
        if (havePotionN)
        {
            dSpritePotion.sprite = havePotion;
        }

        if (haveStockN)
        {
            dSpriteStock.sprite = haveStock; 
        }
        //株専用パネルオフ
        stockCountPanel.SetActive(false);
        //商品名
        string[] dItemName = new string[5]; //名前
        //アイテム入荷用
        dItemName[0] = _languageCnt.Scene1LanguaeData[31];
        dItemName[1] = _languageCnt.Scene1LanguaeData[32];
        dItemName[2] = _languageCnt.Scene1LanguaeData[33];
        dItemName[3] = _languageCnt.Scene1LanguaeData[34];
        dItemName[4] = _languageCnt.Scene1LanguaeData[34];

        //必要価格と商品名と販売価格の入れ替え
        switch (selectItem_D)
        {
            case 0: //剣強化
                dealItem.text = dItemName[selectItem_D] + " Lv." + ParameterCalc.instanceCalc.BrSwordUpCount; //商品名
                dNeedPrice.text = ParameterCalc.instanceCalc.BrSwordUp[ParameterCalc.instanceCalc.BrSwordUpCount] + _languageCnt.Scene1LanguaeData[47];
                dOfferPrice.text = _languageCnt.Scene1LanguaeData[48] + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount] + "z → " + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount + 1] + "z";
                dButton.text = _languageCnt.Scene1LanguaeData[49];

                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.BrSwordUp[ParameterCalc.instanceCalc.BrSwordUpCount])
                {
                    dealButton.interactable = true;
                }
                else
                {
                    dealButton.interactable = false;
                }
                break;

            case 1: //薬強化
                dealItem.text = dItemName[selectItem_D] + " Lv." + ParameterCalc.instanceCalc.PotionUpCount; //商品名
                dNeedPrice.text = ParameterCalc.instanceCalc.PotionUp[ParameterCalc.instanceCalc.PotionUpCount] + _languageCnt.Scene1LanguaeData[47];
                dOfferPrice.text = _languageCnt.Scene1LanguaeData[48] + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount] + "z → " + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount + 1] + "z";
                dButton.text = _languageCnt.Scene1LanguaeData[49];
                
                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.PotionUp[ParameterCalc.instanceCalc.PotionUpCount])
                {
                    dealButton.interactable = true;
                }
                else
                {
                    dealButton.interactable = false;
                }
                break;

            case 2: //株仕入れ
                dealItem.text = dItemName[selectItem_D]; //商品名
                dNeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + _languageCnt.Scene1LanguaeData[47];
                dOfferPrice.text = _languageCnt.Scene1LanguaeData[48] + ParameterCalc.instanceCalc.StockSell + _languageCnt.Scene1LanguaeData[51];
                dButton.text = _languageCnt.Scene1LanguaeData[50];
                //0個以上かつ買える時はインタラクティブ
                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived && 0 < ParameterCalc.instanceCalc.StockReceived)
                {
                    dealButton.interactable = true;
                }
                else
                {
                    dealButton.interactable = false;
                }
                stockCountPanel.SetActive(true); //株専用パネルオン
                stockCountText.text = "" + ParameterCalc.instanceCalc.StockReceived;
                break;

            case 3: //薬入手
                dealItem.text = dItemName[selectItem_D]; //商品名
                dNeedPrice.text = ParameterCalc.instanceCalc.PotionGet + _languageCnt.Scene1LanguaeData[47];
                dOfferPrice.text = " ";
                dButton.text = _languageCnt.Scene1LanguaeData[52];

                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.PotionGet)
                {
                    dealButton.interactable = true;
                }
                else
                {
                    dealButton.interactable = false;
                }
                break;

            case 4: //株解放
                dealItem.text = dItemName[selectItem_D]; //商品名
                dNeedPrice.text = ParameterCalc.instanceCalc.StockOpen + _languageCnt.Scene1LanguaeData[47];
                dOfferPrice.text = " ";
                dButton.text = _languageCnt.Scene1LanguaeData[52];
                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.StockOpen)
                {
                    dealButton.interactable = true;
                }
                else
                {
                    dealButton.interactable = false;
                }
                break;
        }
        //右下セリフのテキスト
        switch (selectItem_D)
        {
            case 0:
                //foxySay = " こ の 地 域 で は よ く 売 れ る . . . ";
                foxySay = _languageCnt.Scene1LanguaeData[53];
                break;

            case 1:
                //foxySay = " 自 分 に 使 う の は や め て お こ う . . . ";
                foxySay = _languageCnt.Scene1LanguaeData[54];
                break;

            case 2:
                //株を所有していないとき
                if (ParameterCalc.instanceCalc.StockQuantity == 0)
                {
                    //foxySay = " 在 庫 . . . . . . な い や . . . ";
                    foxySay = _languageCnt.Scene1LanguaeData[55];
                }
                else
                {
                    //foxySay = " 在 庫 . . . . . . " + ParameterCalc.instanceCalc.StockQuantity + " か ぶ . . . ";
                    foxySay = _languageCnt.Scene1LanguaeData[56] + ParameterCalc.instanceCalc.StockQuantity + _languageCnt.Scene1LanguaeData[57];
                }   
                break;

            case 3:
                if(pushReple)
                {
                    //foxySay = " 部 下 の 交 渉 を 待 と う . . . . . . ";
                    foxySay = _languageCnt.Scene1LanguaeData[58];
                    dDoneButton.SetActive(false);
                }
                else
                {
                    //foxySay = " キ ケ ン な に お い . . . . . . ";
                    foxySay = _languageCnt.Scene1LanguaeData[59];
                }
                break;

            case 4:
                if (pushReple)
                {
                    //foxySay = " 部 下 の 交 渉 を 待 と う . . . . . . ";
                    foxySay = _languageCnt.Scene1LanguaeData[58];
                    dDoneButton.SetActive(false);
                }
                else
                {
                    //foxySay = " 仕 入 れ よ う か な . . . . . . ";
                    foxySay = _languageCnt.Scene1LanguaeData[60];
                }
                break;
        }
        //説明文入れ替え
        dExpText.text = "";
        foxyText.text = "";
        StartCoroutine(D_ExpLog());
    }

    //コルーチンを使って、１文字ごと表示する。
    IEnumerator D_ExpLog()
    {
        runDispo = false; //処理中に他のパネルの選択を出来なくする
        ReadNow = true; //文字表示用SE開始

        string[] itemExp = new string[5];
        itemExp[0] = _languageCnt.Scene1LanguaeData[35];
        itemExp[1] = _languageCnt.Scene1LanguaeData[36];
        itemExp[2] = _languageCnt.Scene1LanguaeData[37];
        itemExp[3] = "     ";
        itemExp[4] = "     ";

        //改行コード変換
        if (itemExp[selectItem_D].Contains("\\n"))
        {
            itemExp[selectItem_D] = itemExp[selectItem_D].Replace(@"\n", Environment.NewLine);
        }
        // 半角スペースで文字を分割する。
        words = itemExp[selectItem_D].Split('^');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            dExpText.text = dExpText.text + word;
            switch(_languageCnt.LanguageState_Scene1)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }

        //主人公のセリフ
        words = foxySay.Split('^');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            foxyText.text = foxyText.text + word;
            switch(_languageCnt.LanguageState_Scene1)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }
        ReadNow = false; //文字表示用SE終了
        runDispo = true; //他のパネルの選択を可能にする
       
    }

    //戻る
    public void D_STRback()
    {
        SelectStr = 5;
        strDoneButton.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(true);
        dealPanel.gameObject.SetActive(false);
        openPanel = false;
        ParameterCalc.instanceCalc.StockReceived = 30;
        foxyPanel.SetActive(false);
    }

    /*-----交渉パネルのテキスト処理-----*/

    IEnumerator Publi_Log()
    {
        ReadNow = true; //文字表示用SE開始

        //runDispo = false;
        foxyText.text = "";

        words = foxySay.Split('^');

        foreach (var word in words)
        {
            foxyText.text = foxyText.text + word;
            switch(_languageCnt.LanguageState_Scene1)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }
        ReadNow = false; //文字表示用SE終了
        //runDispo = true; 
    }

    /*-----商品選択UI-----*/

    //表示制御
    public void ForDrawPanel()
    {
        var stringTmp1 = "";
        var stringTmp2 = "";
        //アイテム選択のコンポーネント取得
        Image spritePotion = potionFlame.GetComponent<Image>(); //薬
        Image spriteStock = stockFlame.GetComponent<Image>(); //株
        
        stringTmp1 = _languageCnt.Scene1LanguaeData[31];
        BrSwordName.text = stringTmp1;

        //アイテム選択画面制御
        if (havePotionN)         
        {
            spritePotion.sprite = havePotion;
            stringTmp1 = _languageCnt.Scene1LanguaeData[32];
            potionName.text = stringTmp1;
        }
        else
        {
            stringTmp1 = _languageCnt.Scene1LanguaeData[34];
        }

        if (haveStockN)          
        {
            spriteStock.sprite = haveStock;
            stringTmp2 = _languageCnt.Scene1LanguaeData[33];
            stockName.text = stringTmp2;
        }
        else
        {
            stringTmp1 = _languageCnt.Scene1LanguaeData[34];
        }
    }
    
    public void BrSword()
    {
        var stringTmp = "";
        ParameterCalc.instanceCalc.ToolType = 0;
        //itemName.text = "どうのつるぎ     売値:" + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount] +"z";
        stringTmp = _languageCnt.Scene1LanguaeData[31] + " Lv." + ParameterCalc.instanceCalc.BrSwordUpCount + "     " + _languageCnt.Scene1LanguaeData[48] + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount] +"z";
        itemName.text = stringTmp;
        buttonSelectItem.interactable = true;
    }

    public void HiPotion()
    {
        ParameterCalc.instanceCalc.ToolType = 1;
        if (havePotionN)    //解放しているか否か
        {
            var stringTmp = "";
            //itemName.text = "こうかなくすり     売値:" + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount] + "z";
            stringTmp = _languageCnt.Scene1LanguaeData[32] + " Lv." + ParameterCalc.instanceCalc.PotionUpCount + "     " + _languageCnt.Scene1LanguaeData[48] + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount] + "z";
            itemName.text = stringTmp;
            buttonSelectItem.interactable = true;
        }
        else
        {
            itemName.text = "-----";
            buttonSelectItem.interactable = false;
        }
    }

    public void LuckyTurnip()
    {
        ParameterCalc.instanceCalc.ToolType = 2;
        if (haveStockN)
        {
            var stringTmp = "";
            //itemName.text = "まぼろしのかぶ × "+ ParameterCalc.instanceCalc.StockQuantity + "  設定価格:" + ParameterCalc.instanceCalc.StockSell +" / かぶ";
            stringTmp = _languageCnt.Scene1LanguaeData[33] + " × " + ParameterCalc.instanceCalc.StockQuantity +"     " + _languageCnt.Scene1LanguaeData[48] + + ParameterCalc.instanceCalc.StockSell + _languageCnt.Scene1LanguaeData[51];
            itemName.text = stringTmp;
            buttonSelectItem.interactable = true;
        }
        else 
        {
            itemName.text = "-----";
            buttonSelectItem.interactable = false;
        }
        //株を保有していないときは非インタラクティブ化
        if(ParameterCalc.instanceCalc.StockQuantity > 0)
        {
            buttonSelectItem.interactable = true;
        }
        else
        {
            buttonSelectItem.interactable = false;
        }
    }

    public void SelectDone()
    {
        PushN += 1;
        ParameterCalc.instanceCalc.GeneCalc(); //計算開始
        mainUI.gameObject.SetActive(false);
        itemSelectUI.gameObject.SetActive(false);
    }

    /*----------アイテム初回獲得パネル----------*/

    public void GetFirst()
    {
        getExpText.text = "";
        getChildText.text = "";
        //アイテム獲得パネルのコンポーネント取得
        Image getSpriteItem = getItemFlame.GetComponent<Image>(); //スプライト入れ替え用
        mainUI.gameObject.SetActive(true);
        strUI.gameObject.SetActive(true);
        getItemPanel.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(false);
        //狐の会話をオフ、処理の終わりにオン
        clickJudge = false;
        var stringTmp = "";

        if (potionRepleIvent && havePotionN)
        {
            getItemPanel.gameObject.SetActive(true);
            getSpriteItem.sprite = havePotion; //画像入れ替え
            //getPorS = " こ う か な く す り の 　　　　　　交 易 ル ー ト が 確 立 し た よ ! ! ";
            stringTmp = _languageCnt.Scene1LanguaeData[78] + "\n" + _languageCnt.Scene1LanguaeData[61];
            getPorS = stringTmp;
            if(runDispo)
            {
                getItemExpText.text =  _languageCnt.Scene1LanguaeData[77];
                getChildS = _languageCnt.Scene1LanguaeData[76];
                StartCoroutine(Get_Goods());
            }
            potionRepleIvent = false;
            //アイテム選択画面用
            havePotionN = true;
        }

        if (stockRepleIvent && haveStockN)
        {
            getItemPanel.gameObject.SetActive(true); //株だけ
            getSpriteItem.sprite = haveStock; //画像入れ替え
            //getPorS = " ま ぼ ろ し の か ぶ の 　　　　　　交 易 ル ー ト が 確 立 し た よ ! ! ";
            stringTmp = _languageCnt.Scene1LanguaeData[79] + "\n" + _languageCnt.Scene1LanguaeData[61];
            getPorS = stringTmp;
            if(runDispo)
            {
                getItemExpText.text =  _languageCnt.Scene1LanguaeData[77];
                getChildS = _languageCnt.Scene1LanguaeData[76];
                StartCoroutine(Get_Goods());
            }
            stockRepleIvent = false;
            //アイテム選択画面用
            haveStockN = true;
        }
    }

    //商品獲得UI処理
    public void D_GetItem()
    {
        mainUI.gameObject.SetActive(false);
        strUI.gameObject.SetActive(false);
        getItemPanel.gameObject.SetActive(false);
        strPanel.gameObject.SetActive(true);
        clickJudge = true;
    }

    //アイテム解放のテキスト処理
    IEnumerator Get_Goods()
    {
        runDispo = false;//処理中に他のパネルの選択を出来なくする
        yield return new WaitForSeconds(1.0f);
        // getExpText.text = "";
        // getChildText.text = "";
        ReadNow = true; //文字表示用SE開始

        // 半角スペースで文字を分割する。
        words = getPorS.Split('^');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する
            getExpText.text = getExpText.text + word;
            switch(_languageCnt.LanguageState_Scene1)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }

        // 半角スペースで文字を分割する。
        words = getChildS.Split('^');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する
            getChildText.text = getChildText.text + word;
            switch(_languageCnt.LanguageState_Scene1)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }

        runDispo = true; //他のパネルの選択を可能にする
        ReadNow = false; //文字表示用SE終了  
    }

    /*----------チュートリアル----------*/
    private void Tutorial()
    {
        firstESC = true;
        var serifTmp = "";
        switch(pushNtutorial)
        {
            case 1:
                foxDia1.gameObject.SetActive(true);
                //foxDia1_Text.text = "おかしら！\n今日もいい朝ですね！";
                serifTmp = _languageCnt.Scene1LanguaeData[3] + "! \n" + _languageCnt.Scene1LanguaeData[11];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 2:
                //foxDia1_Text.text = "ジャンジャン稼ぎましょう！";
                serifTmp = _languageCnt.Scene1LanguaeData[12];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 3:
                //foxDia1_Text.text = "え！稼ぎかた\n忘れたんですか！";
                serifTmp = _languageCnt.Scene1LanguaeData[13];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 4:
                //foxDia1_Text.text = "困ったらこれを見てください！";
                serifTmp = _languageCnt.Scene1LanguaeData[14];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 5:
                tutrialExp.gameObject.SetActive(true);
                Image tutrialExpImage = tutrialExp.GetComponent<Image>();
                //チュートリアル画像入れ替え
                switch(_languageCnt.LanguageState_Scene1)
                {
                    case "Japanese":
                        tutrialExpImage.sprite = _turtrialSprites[0];
                        break;
                    case "English":
                        tutrialExpImage.sprite = _turtrialSprites[1];
                        break;
                }
                mainUI.gameObject.SetActive(true);
                foxDia1.gameObject.SetActive(false);
                haveMoneyPanel.gameObject.SetActive(false);
                slaveTurnPanel.gameObject.SetActive(false);
                break;

            case 6:
                foxDia1.gameObject.SetActive(true);
                mainUI.gameObject.SetActive(false);
                //foxDia1_Text.text = "あとESC？ってキーを\n押してみてください！";
                serifTmp = _languageCnt.Scene1LanguaeData[15];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    firstESC = false;
                    pushNtutorial ++;
                }
                break;

            case 7:
                //foxDia1_Text.text = "これでバッチリですね！";
                serifTmp = _languageCnt.Scene1LanguaeData[16];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 8:
                //foxDia1_Text.text = "目標は・・・\n１０万Zです！";
                serifTmp = _languageCnt.Scene1LanguaeData[17];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 9:
                haveMoneyPanel.gameObject.SetActive(true);
                slaveTurnPanel.gameObject.SetActive(true);
                //foxDia1_Text.text = "早速稼ぎましょう！";
                serifTmp = _languageCnt.Scene1LanguaeData[18];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;
            
            case 10:
                foxDia1.gameObject.SetActive(false);
                ParameterCalc.instanceCalc.InitialPlay = false;
                PushN = 0;
                clickJudge = true;
                break;
        }
    }

    public void CloseTutorial()
    {
        tutrialExp.gameObject.SetActive(false);
        pushNtutorial += 1;
    }

    /*----------クリアイベント----------*/

    private void ClearIvent()
    {
        //MenuCntにてescキー無効
        NowClear = true;
        var serifTmp = "";
        switch(PushN)
        {
            case 1:
                foxDia1.gameObject.SetActive(true);
                //foxDia1_Text.text = "やったー！！";
                serifTmp = _languageCnt.Scene1LanguaeData[19];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;
        
            case 2:
                //foxDia1_Text.text = "おかしら！\n目標達成だ！";
                serifTmp = _languageCnt.Scene1LanguaeData[20];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 3:
                foxDia1.gameObject.SetActive(false);
                foxDia2.gameObject.SetActive(true);
                //foxDia2_Text.text = "おかしら！\nおつかれさま！";
                serifTmp = _languageCnt.Scene1LanguaeData[3] + "! \n" + _languageCnt.Scene1LanguaeData[21];
                foxDia2_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 4:
                foxDia2.gameObject.SetActive(false);
                foxDia3.gameObject.SetActive(true);
                //foxDia3_Text.text = "おかしら\n次の町にいこう。";
                serifTmp = _languageCnt.Scene1LanguaeData[3] + "\n" + _languageCnt.Scene1LanguaeData[22];
                foxDia3_Text.text = serifTmp;
                clickJudge = true;
                break;
        
            case 5:
                foxDia3.gameObject.SetActive(false);
                foxDia2.gameObject.SetActive(true);
                //foxDia2_Text.text = "おかしら！\nいこう！";
                serifTmp = _languageCnt.Scene1LanguaeData[3] + "! \n" + _languageCnt.Scene1LanguaeData[23];
                foxDia2_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 6:
                foxDia2.gameObject.SetActive(false);
                ClearAnim = true;
                clickJudge = true;
                break;

            case 7:
                PlClearDoAnim = true;
                break;
        }

        //主人公のアニメが終わったらパネルを開く
        if(FinClearAnim)
        {
            clickJudge = false;
            gameClearPanel.SetActive(true);
            clearResultText.text = ParameterCalc.instanceCalc.OutPutResult;   
        }
    }

    private void GameOverEvent()
    {
        var serifTmp = "";
        switch(PushN)
        {
            case 1:
                foxDia1.gameObject.SetActive(true);
                //foxDia1_Text.text = "おかしら...";
                serifTmp = _languageCnt.Scene1LanguaeData[3] + "...";
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;

            case 2:
                //foxDia1_Text.text = "お金...\nなくなったんですか";
                serifTmp = _languageCnt.Scene1LanguaeData[24];
                foxDia1_Text.text = serifTmp;
                clickJudge = true;
                break;
        
            case 3:
                foxDia1.gameObject.SetActive(false);
                foxDia2.gameObject.SetActive(true);
                //foxDia2_Text.text = "おかしら！\n報酬ないの...";
                serifTmp = _languageCnt.Scene1LanguaeData[25];
                foxDia2_Text.text = serifTmp;
                clickJudge = true;
                break;
        
            case 4:
                foxDia2.gameObject.SetActive(false);
                foxDia3.gameObject.SetActive(true);
                //foxDia3_Text.text = "おかしら\n解散しよう";
                serifTmp = _languageCnt.Scene1LanguaeData[3] + "\n" + _languageCnt.Scene1LanguaeData[26];
                foxDia3_Text.text = serifTmp;
                clickJudge = true;
                break;
            
            case 5:
                foxDia3.gameObject.SetActive(false);
                ClearAnim = true;
                clickJudge = true;
                break;
        
            case 6:
                sceneCnt1.IsFadeOut_A = true;
                GameOverFade = true;
                //フェードアウトの後にゲームオーバーパネルオン
                if (DoOverAnim)
                {
                    gameOverPanel.gameObject.SetActive(true);
                }
                break;
        }
    }

    /*--------------SE----------------*/

    //ボタン押したときの音
	public void PushButtonSE_A()
	{
		soundA.PlaySe(pushButtonSE);
	}
    //アイテムの購入改良
	public void BuyItemsSE()
	{
		soundA.PlaySe(buyNewItemsSE);
	}
}

