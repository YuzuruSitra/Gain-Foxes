using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//シーンAのUI、演出管理
public class UICont1 : MonoBehaviour
{

    //シーン管理クラスの取得
    [SerializeField]
    private SceneCnt1 sceneCnt1;

    //チュートリアル用
    [SerializeField]
    private GameObject tutrialExp;
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
    private string[] peopleName = { "貧民", "市民", "富豪", "貴族" }; //名前
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
    private string publiPlayerSay;
    private string publiOtherSay;

    //入荷パネル
    [SerializeField]
    private GameObject dealPanel;
    [SerializeField]
    private Button dealButton;
    [SerializeField]
    private Text dealItem;
    private int selectItem_D;
    private string[] dItemName = new string[5]; //名前
    [SerializeField]
    private Text dNeedPrice; //必要価格
    [SerializeField]
    private Text dOfferPrice; //販売価格
    [SerializeField]
    private Text dButton; //ボタン
    [SerializeField]
    private Text dExpText; //説明文入れ替え↓
    private string[] itemExp = new string[5];
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
    private Text getExpText; //一言
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
    private Text potionName;
    [SerializeField]
    private Text stockName;

    //購入パネル初期テキスト
    private bool drawSelectItem;

    //クリック重複ケア
    private bool clickJudge;

    //戦略選択チェック
    public int SelectStr;

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

        //アイテム入荷用
        dItemName[0] = "どうのつるぎ";
        dItemName[1] = "こうかなくすり";
        dItemName[2] = "まぼろしのかぶ";
        dItemName[3] = "？？？";
        dItemName[4] = "？？？";

        runDispo = true;
        itemExp[0] = " い た っ て ふ つ う の ど う の つ る ぎ 。 　　　　お 客 さ ん の 心 を つ か も う . ";
        itemExp[1] = " 危 険 な に お い が す る や く ひ ん 。 　　　　　す こ し う し ろ め た い が 、 高 く 売 れ そ う だ 。 ";
        itemExp[2] = " 市 場 価 格 が 安 定 し な い 株 。 　　　　　　　大 き く 稼 げ そ う だ が そ の 裏 で は 多 く の リ ス ク を は ら ん で い る 。 ";
        itemExp[3] = "     ";
        itemExp[4] = "     ";

        /*---bgm設定---*/
        soundA = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();

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

        //UI制御用商品の解放管理
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
        turnCountText.text = ParameterCalc.instanceCalc.TurnCount + " 日目";

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
            switch (PushN)
            {
                case 1:
                    foxDia1.gameObject.SetActive(true);
                    foxDia1_Text.text = "おかしら！\n" + ParameterCalc.instanceCalc.TurnCount + "日目ですね！";
                    clickJudge = true;
                    break;

                case 2:
                    foxDia1_Text.text = "今日はどんな工作を？";
                    clickJudge = true;
                    break;

                case 3:
                    foxDia1.gameObject.SetActive(false);
                    mainUI.gameObject.SetActive(true);
                    strUI.gameObject.SetActive(true);
                    if (!openPanel)
                    {
                        strDoneButton.gameObject.SetActive(true);
                    }
                    break;

                case 4:
                    mainUI.gameObject.SetActive(false);
                    foxDia1.gameObject.SetActive(true);
                    foxDia1_Text.text = "かしこまり！";
                    clickJudge = true;
                    break;

                case 5:
                    foxDia1.gameObject.SetActive(false);
                    foxDia2.gameObject.SetActive(true);
                    foxDia2_Text.text = "おかしら！\n次はどんな工作を？";
                    clickJudge = true;
                    break;

                case 6:
                    foxDia2.gameObject.SetActive(false);
                    mainUI.gameObject.SetActive(true);
                    strUI.gameObject.SetActive(true);
                    if (!openPanel)
                    {
                        strDoneButton.gameObject.SetActive(true);
                    }
                    break;

                case 7:
                    mainUI.gameObject.SetActive(false);
                    strUI.gameObject.SetActive(false);
                    foxDia2.gameObject.SetActive(true);
                    foxDia2_Text.text = "はい！";
                    clickJudge = true;
                    break;

                case 8:
                    foxDia2.gameObject.SetActive(false);
                    foxDia3.gameObject.SetActive(true);
                    foxDia3_Text.text = "おかしら。\n今日は何を売る？";
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
                    foxDia3_Text.text ="御意。";
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
    }

    //戦略選択
    public void SetGossip()
    {
        SelectStr = 1;
    }

    public void SetPray()
    {
        SelectStr = 2;
    }

    public void SetSteal()
    {
        SelectStr = 3;
    }

    public void SetKill()
    {
        SelectStr = 4;
    }

    public void SetDeal()
    {
        SelectStr = 5;
    }

    //戦略決定
    public void StrDone()
    {
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
                    foxySay = " 少 し う さ ん く さ い な . . . ";
                }
                else
                {
                    foxySay = " 忙 し そ う だ . . . ";
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
                    foxySay = " 裏 工 作 . . . あ ま り 気 が 乗 ら な い 。 ";
                    buttonKill.interactable = true;
                }
                else
                {
                    foxySay = " こ れ 以 上 騒 ぎ は 起 こ せ な い . . . ";
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
            buttonPubliDown.interactable = true;
            if (ParameterCalc.instanceCalc.PubliWay > 1)
            {
                buttonPubliUp.interactable = false;
            }
            DrowPubli();
        }
    }

    public void PubliWayDown()
    {
        if(runDispo){
            ParameterCalc.instanceCalc.PubliWay -= 1;
            buttonPubliUp.interactable = true;
            if (ParameterCalc.instanceCalc.PubliWay < 1)
            {
                buttonPubliDown.interactable = false;
            }
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
        runDispo = false;
        publiDoneBt = publidoneButton.GetComponent<Button>();
        //計算処理
        double Conv = ParameterCalc.instanceCalc.HaveMoney * ParameterCalc.instanceCalc.PubliRisk[ParameterCalc.instanceCalc.PubliWay];
        ParameterCalc.instanceCalc.PubliWayPay = (int)Conv;
        publiMoneyText.text = ParameterCalc.instanceCalc.PubliWayPay + "";
        if (!ParameterCalc.instanceCalc.usePubli)
        {
            publiPlayerSay = " . . . " + ParameterCalc.instanceCalc.PubliWayPay + " z で ど う だ ？ ";
            publiOtherSay = " そ の 報 酬 だ と 成 功 率 は "+ ParameterCalc.instanceCalc.PubliRisk[ParameterCalc.instanceCalc.PubliWay] * 100 +" % っ て と こ だ 。 ";
        }
        else
        {
            publiPlayerSay = " . . . . . . ";
            publiOtherSay = " 大 人 し く 結 果 を 待 つ ん だ な 。 ";
            publiDoneBt.interactable = false;
        }
        StartCoroutine(PubliSay());
    }

    //交渉パネル二人の会話処理
    IEnumerator PubliSay()
    {
        ReadNow = true; //文字表示用SE開始

        publiPlayerText.text = "";
        publiOtherText.text = "";

        words = publiPlayerSay.Split(' ');
        foreach (var word in words)
        {
            publiPlayerText.text = publiPlayerText.text + word;
            yield return new WaitForSeconds(0.05f);

        }
        words = publiOtherSay.Split(' ');
        
        foreach (var word in words)
        {
            publiOtherText.text = publiOtherText.text + word;
            yield return new WaitForSeconds(0.05f);
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
        Debug.Log(ParameterCalc.instanceCalc.PopTurnEvent);
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
        }
    }

    //株の個数選択
    public void StockCountUp()
    {
        ParameterCalc.instanceCalc.StockReceived += 10;
        if(ParameterCalc.instanceCalc.StockReceived > 500)
        {
            ParameterCalc.instanceCalc.StockReceived = 500;
        }
        stockCountText.text = "" + ParameterCalc.instanceCalc.StockReceived;
        dNeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";

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
        ParameterCalc.instanceCalc.StockReceived -= 10;
        if (ParameterCalc.instanceCalc.StockReceived < 0)
        {
            ParameterCalc.instanceCalc.StockReceived = 0;
        }
        stockCountText.text = "" + ParameterCalc.instanceCalc.StockReceived;
        dNeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";

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

        //必要価格と商品名と販売価格の入れ替え
        switch (selectItem_D)
        {
            case 0: //剣強化
                dealItem.text = dItemName[selectItem_D] + " Lv." + ParameterCalc.instanceCalc.BrSwordUpCount; //商品名
                dNeedPrice.text = ParameterCalc.instanceCalc.BrSwordUp[ParameterCalc.instanceCalc.BrSwordUpCount] + "z必要";
                dOfferPrice.text = "売値 : " + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount] + " → " + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount + 1];
                dButton.text = "改良する";

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
                dNeedPrice.text = ParameterCalc.instanceCalc.PotionUp[ParameterCalc.instanceCalc.PotionUpCount] + "z必要";
                dOfferPrice.text = "売値 : " + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount] + " → " + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount + 1];
                dButton.text = "改良する";
                
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
                dNeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";
                dOfferPrice.text = "売値 : " + ParameterCalc.instanceCalc.StockSell + "z / かぶ";
                dButton.text = "仕入れる";
                stockCountPanel.SetActive(true); //株専用パネルオン
                stockCountText.text = ""+ ParameterCalc.instanceCalc.StockReceived;
                break;

            case 3: //薬入手
                dealItem.text = dItemName[selectItem_D]; //商品名
                dNeedPrice.text = ParameterCalc.instanceCalc.PotionGet + "z必要";
                dOfferPrice.text = " ";
                dButton.text = "解放する";

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
                dNeedPrice.text = ParameterCalc.instanceCalc.StockOpen + "z必要";
                dOfferPrice.text = " ";
                dButton.text = "解放する";
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
                foxySay = " こ の 地 域 で は よ く 売 れ る . . . ";
                break;

            case 1:
                foxySay = " 自 分 に 使 う の は や め て お こ う . . . ";
                break;

            case 2:
                //株を所有していないとき
                if (ParameterCalc.instanceCalc.StockQuantity == 0)
                {
                    foxySay = " 在 庫 . . . . . . な い や . . . ";
                }
                else
                {
                    foxySay = " 在 庫 . . . . . . " + ParameterCalc.instanceCalc.StockQuantity + " か ぶ . . . ";
                }

                break;

            case 3:
                if(pushReple)
                {
                    foxySay = " 部 下 の 交 渉 を 待 と う . . . . . . ";
                    dDoneButton.SetActive(false);
                }
                else
                {
                    foxySay = " キ ケ ン な に お い . . . . . . ";
                }
                break;

            case 4:
                if (pushReple)
                {
                    foxySay = " 部 下 の 交 渉 を 待 と う . . . . . . ";
                    dDoneButton.SetActive(false);
                }
                else
                {
                    foxySay = " 仕 入 れ よ う か な . . . . . . ";
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

        // 半角スペースで文字を分割する。
        words = itemExp[selectItem_D].Split(' ');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            dExpText.text = dExpText.text + word;
            yield return new WaitForSeconds(0.02f);

        }

        //主人公のセリフ
        words = foxySay.Split(' ');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            foxyText.text = foxyText.text + word;
            yield return new WaitForSeconds(0.08f);
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

        runDispo = false;
        foxyText.text = "";

        words = foxySay.Split(' ');

        foreach (var word in words)
        {
            foxyText.text = foxyText.text + word;
            yield return new WaitForSeconds(0.08f);
        }
        ReadNow = false; //文字表示用SE終了
        runDispo = true; 
    }

    /*-----商品選択UI-----*/

    //表示制御
    public void ForDrawPanel()
    {
        //アイテム選択のコンポーネント取得
        Image spritePotion = potionFlame.GetComponent<Image>(); //薬
        Image spriteStock = stockFlame.GetComponent<Image>(); //株

        //アイテム選択画面制御
        if (havePotionN)         
        {
            spritePotion.sprite = havePotion;
            potionName.text = "こうかなくすり";
        }

        if (haveStockN)          
        {
            spriteStock.sprite = haveStock;
            stockName.text = "まぼろしのかぶ";
        }
    }
    
    public void BrSword()
    {
        ParameterCalc.instanceCalc.ToolType = 0;
        itemName.text = "どうのつるぎ     売値:" + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount] +"z";
        buttonSelectItem.interactable = true;
    }

    public void HiPotion()
    {
        ParameterCalc.instanceCalc.ToolType = 1;

        if (havePotionN)    //解放しているか否か
        {
            itemName.text = "こうかなくすり     売値:" + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount] + "z";
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
            itemName.text = "まぼろしのかぶ × "+ ParameterCalc.instanceCalc.StockQuantity + "  設定価格:" + ParameterCalc.instanceCalc.StockSell +" / かぶ";
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

        //アイテム獲得パネルのコンポーネント取得
        Image getSpriteItem = getItemFlame.GetComponent<Image>(); //スプライト入れ替え用
        mainUI.gameObject.SetActive(true);
        strUI.gameObject.SetActive(true);
        getItemPanel.gameObject.SetActive(true);
        strPanel.gameObject.SetActive(false);
        //狐の会話をオフ、処理の終わりにオン
        clickJudge = false;

        if (potionRepleIvent && havePotionN)
        {
            getItemPanel.gameObject.SetActive(true);
            getSpriteItem.sprite = havePotion; //画像入れ替え
            getPorS = " こ う か な く す り の 　　　　　　交 易 ル ー ト が 確 立 し た よ ! ! ";
            StartCoroutine(Get_Goods());
            potionRepleIvent = false;
            //アイテム選択画面用
            havePotionN = true;
        }

        if (stockRepleIvent && haveStockN)
        {
            getItemPanel.gameObject.SetActive(true); //株だけ
            getSpriteItem.sprite = haveStock; //画像入れ替え
            getPorS = " ま ぼ ろ し の か ぶ の 　　　　　　交 易 ル ー ト が 確 立 し た よ ! ! ";
            StartCoroutine(Get_Goods());
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
        getExpText.text = "";
        runDispo = false;//処理中に他のパネルの選択を出来なくする
        ReadNow = true; //文字表示用SE開始

        // 半角スペースで文字を分割する。
        words = getPorS.Split(' ');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する
            getExpText.text = getExpText.text + word;
            yield return new WaitForSeconds(0.1f);
        }
        runDispo = true; //他のパネルの選択を可能にする
        ReadNow = false; //文字表示用SE終了  
    }

    /*----------チュートリアル----------*/
    private void Tutorial()
    {
        firstESC = true;
        switch(pushNtutorial)
        {
            case 1:
                foxDia1.gameObject.SetActive(true);
                foxDia1_Text.text = "おかしら！\n今日もいい朝ですね！";
                clickJudge = true;
                break;

            case 2:
                foxDia1_Text.text = "ジャンジャン稼ぎましょう！";
                clickJudge = true;
                break;

            case 3:
                foxDia1_Text.text = "え！稼ぎかた\n忘れたんですか！";
                clickJudge = true;
                break;

            case 4:
                foxDia1_Text.text = "困ったらこれを見てください！";
                clickJudge = true;
                break;

            case 5:
                tutrialExp.gameObject.SetActive(true);
                mainUI.gameObject.SetActive(true);
                foxDia1.gameObject.SetActive(false);
                haveMoneyPanel.gameObject.SetActive(false);
                slaveTurnPanel.gameObject.SetActive(false);
                break;

            case 6:
                foxDia1.gameObject.SetActive(true);
                mainUI.gameObject.SetActive(false);
                foxDia1_Text.text = "あとESC？ってキーを\n押してみてください！";
                clickJudge = true;
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    firstESC = false;
                    pushNtutorial ++;
                }
                break;

            case 7:
                foxDia1_Text.text = "これでバッチリですね！";
                clickJudge = true;
                break;

            case 8:
                foxDia1_Text.text = "目標は・・・\n１０万Zです！";
                clickJudge = true;
                break;

            case 9:
                haveMoneyPanel.gameObject.SetActive(true);
                slaveTurnPanel.gameObject.SetActive(true);
                foxDia1_Text.text = "早速稼ぎましょう！";
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

        switch(PushN)
        {
            case 1:
                foxDia1.gameObject.SetActive(true);
                foxDia1_Text.text = "やったー！！";
                clickJudge = true;
                break;
        
            case 2:
                foxDia1_Text.text = "おかしら！\n目標達成だ！";
                clickJudge = true;
                break;

            case 3:
                foxDia1.gameObject.SetActive(false);
                foxDia2.gameObject.SetActive(true);
                foxDia2_Text.text = "おかしら！\nおつかれさま！";
                clickJudge = true;
                break;

            case 4:
                foxDia2.gameObject.SetActive(false);
                foxDia3.gameObject.SetActive(true);
                foxDia3_Text.text = "おかしら\n次の町にいこう。";
                clickJudge = true;
                break;
        
            case 5:
                foxDia3.gameObject.SetActive(false);
                foxDia2.gameObject.SetActive(true);
                foxDia2_Text.text = "おかしら！\nいこう！";
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
            gameClearPanel.SetActive(true);
            clearResultText.text = ParameterCalc.instanceCalc.OutPutResult;   
        }
    }

    private void GameOverEvent()
    {
        switch(PushN)
        {
            case 1:
                foxDia1.gameObject.SetActive(true);
                foxDia1_Text.text = "おかしら...";
                clickJudge = true;
                break;

            case 2:
                foxDia1_Text.text = "お金...\nなくなったんですか";
                clickJudge = true;
                break;
        
            case 3:
                foxDia1.gameObject.SetActive(false);
                foxDia2.gameObject.SetActive(true);
                foxDia2_Text.text = "おかしら！\n報酬ないの...";
                clickJudge = true;
                break;
        
            case 4:
                foxDia2.gameObject.SetActive(false);
                foxDia3.gameObject.SetActive(true);
                foxDia3_Text.text = "おかしら\n解散しよう";
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

