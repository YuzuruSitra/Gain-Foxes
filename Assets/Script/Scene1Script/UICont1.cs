using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//シーンAのUI、演出管理
public class UICont1 : MonoBehaviour
{
    //インスタンス化
    public static UICont1 instanceUI1;

    //チュートリアル用
    [SerializeField]
    private GameObject TutrialExp;
    [SerializeField]
    private GameObject HaveMoneyPanel;
    [SerializeField]
    private GameObject SlaveTurnPanel;
    private bool firstESC;

    [SerializeField]
    private Text SlaveTx;
    [SerializeField]
    private Text CrimeRateTx;
    [SerializeField]
    private Text HavemoneyT;
    [SerializeField]
    private Text TurnCountText;

    //子分テキスト用
    private bool textChange;
    private int textChangeInt;

    public int PushN;
    public int PushNtutorial;
    [SerializeField]
    private GameObject FoxDia1;
    [SerializeField]
    private GameObject FoxDia2;
    [SerializeField]
    private Text FoxDia1_Text;
    [SerializeField]
    private GameObject FoxDia3;
    [SerializeField]
    private GameObject FoxDia4;
    [SerializeField]
    private Text FoxDia2_Text;
    [SerializeField]
    private GameObject FoxDia5;
    [SerializeField]
    private GameObject FoxDia6;
    [SerializeField]
    private Text FoxDia3_Text;
    [SerializeField]
    private GameObject mainUI;
    [SerializeField]
    private GameObject StrUI;
    [SerializeField]
    private GameObject ItemSelectUI;
    [SerializeField]
    private Text itemName;
    //戦略Doneボタン
    [SerializeField]
    private GameObject DoneButton;

    //暗殺パネル
    [SerializeField]
    private GameObject KillPanel;
    [SerializeField]
    private GameObject STRpanel;
    [SerializeField]
    private GameObject STRDoneButton;
    private bool OpenPanel; //ケア
    private int SelectKill; //暗殺対象
    public string[] PeoName = { "貧民", "市民", "富豪", "貴族" }; //名前
    [SerializeField]
    private Text PeoNameText; //名前を入れる箱
    private Animator animator; //アニメーター
    [SerializeField]
    private GameObject PeopleAnim; //アニメーションさせるオブジェクト

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
    private GameObject publiDoneButton;
    [SerializeField]
    private Button publiDoneBt;
    private string publiPlayerSay;
    private string publiOtherSay;

    //入荷パネル
    [SerializeField]
    private GameObject DealPanel;
    [SerializeField]
    private Button DealButton;
    [SerializeField]
    private Text DealItem;
    private int SelectItem_D;
    private string[] D_ItemName = new string[5]; //名前
    [SerializeField]
    private Text D_NeedPrice; //必要価格
    [SerializeField]
    private Text D_OfferPrice; //販売価格
    [SerializeField]
    private Text D_Button; //ボタン
    [SerializeField]
    private Text D_ExpText; //説明文入れ替え↓
    public string[] ItemExp = new string[5];
    private string[] words;
    private bool RunDispo;
    [SerializeField]
    private GameObject FoxyPanel;
    [SerializeField]
    private Text FoxyText; //主人公一言
    private string FoxySay; //主人公セリフ格納
    private bool PushReple = false;

    [SerializeField]
    private GameObject D_DoneButton; //入荷待ちに非アクティブ化

    //入荷後の画像入れ替え用
    [SerializeField]
    private GameObject D_Potionflame;
    [SerializeField]
    private GameObject D_Stockflame;

    private Image D_SpritePotion;
    private Image D_SpriteStock;
    //株専用パネル
    [SerializeField]
    private GameObject StockCountPanel;
    [SerializeField]
    private Text StockCountText;

    //アイテムゲットUI
    [SerializeField]
    private GameObject Get_ItemPanel;
    [SerializeField]
    private GameObject Get_ItemFlame;
    private Image Get_SpriteItem;
    [SerializeField]
    private Text GetExpText; //一言
    private string GetPorS; //ぽーしょんorかぶ格納


    //アイテム選択
    [SerializeField]
    private Button ButtonSelectItem;
    [SerializeField]
    private Button ButtonKill;
    [SerializeField]
    private Button ButtonPubliUp;
    [SerializeField]
    private Button ButtonPubliDown;

    //イベント発生フラグ
    private bool PotionRepleIvent = false;
    private bool StockRepleIvent = false;
    private bool GeneRepleEvent = false; //次のターンにイベント発生フラグ
    public bool ClearAnim;
    public bool PlClearDoAnim = false;
    public bool FinClearAnim = false;
    [SerializeField]
    private GameObject GameClearPanel;
    [SerializeField]
    private Text ClearResult_Text;
    //クリア時のescキー無効判定
    public bool nowClear;
    //ゲームオーバー
    public bool doOverAnim = false;
    public bool GameOverFade = false;
    [SerializeField]
    private GameObject GameOverPanel;

    //サウンド用スクリプト取得
	[SerializeField] 
    private soundCnt soundA;
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
    public bool readNow = false;
    private bool delayTextSE = true;

    /*---------商品選択用----------*/

    //入手後の素材
    [SerializeField]
    private Sprite HavePotion;
    [SerializeField]
    private Sprite HaveStock;
    //コンポーネント取得先
    [SerializeField]
    private GameObject Potionflame;
    [SerializeField]
    private GameObject Stockflame;

    private Image SpritePotion;
    private Image SpriteStock;

    //持っているか否か
    private static bool HavePotionN = false;
    private static bool HaveStockN = false;

    //テキスト入れ替え
    [SerializeField]
    private Text PotionName;
    [SerializeField]
    private Text StockName;

    //購入パネル初期テキスト
    private bool drawSelectItem;

    //クリック重複ケア
    private bool ClickJudge;

    //戦略選択チェック
    public int SelectStr;

	void Awake()
	{
		if (instanceUI1 == null)
        {
            instanceUI1 = this;
        }
	}

    void Start()
    {
        PushN = 0;
        PushNtutorial = 0;
        FoxDia1.gameObject.SetActive(false);
        FoxDia2.gameObject.SetActive(false);
        FoxDia3.gameObject.SetActive(false);
        FoxDia4.gameObject.SetActive(false);
        FoxDia5.gameObject.SetActive(false);
        FoxDia6.gameObject.SetActive(false);
        mainUI.gameObject.SetActive(false);
        ItemSelectUI.gameObject.SetActive(false);
        StrUI.gameObject.SetActive(false);
        KillPanel.gameObject.SetActive(false);
        DealPanel.gameObject.SetActive(false);
        STRDoneButton.gameObject.SetActive(false);
        Get_ItemPanel.gameObject.SetActive(false);
        GameClearPanel.SetActive(false);
        GameOverPanel.gameObject.SetActive(false);
        publicityPanel.SetActive(false);
        FoxyPanel.SetActive(false);

        ClearAnim = false;
        textChange = true;
        textChangeInt = 0;
        ClickJudge = true;
        OpenPanel = false;
        PushReple = false;
        nowClear = false;
        drawSelectItem = true;
        SelectStr = 0;
        SelectKill = 0; //暗殺対象リセット
        SelectItem_D = 0; //取引のアイテム選択リセット

        //アイテム入荷のコンポーネント取得
        D_SpritePotion = D_Potionflame.GetComponent<Image>(); //薬
        D_SpriteStock = D_Stockflame.GetComponent<Image>(); //株

        //アイテム獲得パネルのコンポーネント取得
        Get_SpriteItem = Get_ItemFlame.GetComponent<Image>(); //スプライト入れ替え用

        //アイテム選択のコンポーネント取得
        SpritePotion = Potionflame.GetComponent<Image>(); //薬
        SpriteStock = Stockflame.GetComponent<Image>(); //株

        TutrialExp.gameObject.SetActive(false);

        //アイテム入荷用
        D_ItemName[0] = "どうのつるぎ";
        D_ItemName[1] = "こうかなくすり";
        D_ItemName[2] = "まぼろしのかぶ";
        D_ItemName[3] = "？？？";
        D_ItemName[4] = "？？？";

        RunDispo = true;
        ItemExp[0] = " い た っ て ふ つ う の ど う の つ る ぎ 。 　　　　お 客 さ ん の 心 を つ か も う . ";
        ItemExp[1] = " 危 険 な に お い が す る や く ひ ん 。 　　　　　す こ し う し ろ め た い が 、 高 く 売 れ そ う だ 。 ";
        ItemExp[2] = " 市 場 価 格 が 安 定 し な い 株 。 　　　　　　　大 き く 稼 げ そ う だ が そ の 裏 で は 多 く の リ ス ク を は ら ん で い る 。 ";
        ItemExp[3] = "     ";
        ItemExp[4] = "     ";

        /*---bgm設定---*/
        soundA = GameObject.Find("SoundManager").GetComponent<soundCnt> ();

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
    }

    void Update()
    {
        //UI更新
        SlaveTx.text = ParameterCalc.instanceCalc.Slave + "";
        CrimeRateTx.text = ParameterCalc.instanceCalc.CrimeRate + "/100";
        HavemoneyT.text = ParameterCalc.instanceCalc.HaveMoney + "";
        TurnCountText.text = ParameterCalc.instanceCalc.TurnCount + " 日目";

        //Doneボタン管理
        if (SelectStr == 0)
        {
            DoneButton.gameObject.SetActive(false);
        }
        else
        {
            DoneButton.gameObject.SetActive(true);
        }

        //戦略ボタンDoneを押せなくする
        if (SelectStr == 3 || SelectStr == 4) //窃盗か暗殺の時
        {
            if (ParameterCalc.instanceCalc.Slave > 0)
            {
                DoneButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                DoneButton.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            DoneButton.GetComponent<Button>().interactable = true;
        }

        //狐の会話
        if (Input.GetMouseButtonDown(0) && ClickJudge && !menuCnt.ESCnow)
        {
            if(ParameterCalc.instanceCalc.initialPlay)
            {
                //チュートリアル用ESC説明
                if(firstESC && PushNtutorial != 6)
                {
                    PushNtutorial += 1;
                }
                ClickJudge = false;
            }
            else
            {
                //狐の会話用荒療治
                if(textChange)
                {
                    PushN += 1;
                    ClickJudge = false;
                }
                else
                {
                    textChangeInt ++;
                    ClickJudge = false;
                }
            }
        }

        //チュートリアル
        if(ParameterCalc.instanceCalc.initialPlay)
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

        else if(GeneRepleEvent && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent) //前ターンの入荷イベント発生
        {
            GetFirst();
            GeneRepleEvent = false;
        }

        else    //通常処理
        {
            FoxDia2_Text.text = "おかしら！\n次はどんな工作を？";
            FoxDia3_Text.text = "おかしら。\n今日は何を売る？";
            if (PushN == 1)
            {
                switch(textChangeInt)
                {
                    
                    case 0:
                        textChange = false; //PushNオフ
                        FoxDia1_Text.text = "おかしら！\n" + ParameterCalc.instanceCalc.TurnCount + "日目ですね！";
                        FoxDia1.gameObject.SetActive(true);
                        ClickJudge = true;
                        break;
                    case 1:
                        FoxDia1_Text.text = "今日はどんな工作を？";
                        textChange = true; //PushNオン
                        ClickJudge = true;
                        break;
                }
                
            }
            else if (PushN == 2)
            {
                FoxDia1.gameObject.SetActive(false);
                mainUI.gameObject.SetActive(true);
                StrUI.gameObject.SetActive(true);
                if (!OpenPanel)
                {
                    STRDoneButton.gameObject.SetActive(true);
                }
            }
            else if (PushN == 3)
            {
                mainUI.gameObject.SetActive(false);
                FoxDia2.gameObject.SetActive(true);
                ClickJudge = true;
            }
            else if (PushN == 4)
            {
                FoxDia2.gameObject.SetActive(false);
                FoxDia3.gameObject.SetActive(true);
                ClickJudge = true;
            }
            else if (PushN == 5)
            {
                FoxDia3.gameObject.SetActive(false);
                mainUI.gameObject.SetActive(true);
                StrUI.gameObject.SetActive(true);
                if (!OpenPanel)
                {
                    STRDoneButton.gameObject.SetActive(true);
                }
            }
            else if (PushN == 6)
            {
                mainUI.gameObject.SetActive(false);
                StrUI.gameObject.SetActive(false);
                FoxDia4.gameObject.SetActive(true);
                ClickJudge = true;
            }
            else if (PushN == 7)
            {
                FoxDia4.gameObject.SetActive(false);
                FoxDia5.gameObject.SetActive(true);
                ClickJudge = true;
            }
            else if (PushN == 8)
            {
                FoxDia5.gameObject.SetActive(false);
                mainUI.gameObject.SetActive(true);
                ItemSelectUI.gameObject.SetActive(true);
                if (drawSelectItem)
                {
                    BrSword();
                    drawSelectItem = false;
                }
                //アイテム選択画面UI更新
                ForDrawPanel();
            }
            else if (PushN == 9)
            {
                FoxDia6.gameObject.SetActive(true);
                mainUI.gameObject.SetActive(false);
                ItemSelectUI.gameObject.SetActive(false);
                ClickJudge = true;
            }
            else if (PushN == 10)
            {
                FoxDia6.gameObject.SetActive(false);
                ClickJudge = true;
            }
            else if (PushN == 11)
            {
                //シーンコントロールにお任せ
                SceneCnt1.instanceCnt1.isFadeOut_A = true;
            }
        }

        /*--------SE--------*/

        if(readNow)
        {
            if(delayTextSE) //1.045秒毎に再生
            {
                TextFlowSE();
                delayTextSE = false;
                Invoke("DelayTextSE",1.045f);
            }
        }
    }

    private void DelayTextSE()
    {
        delayTextSE = true;
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
                drowPubli();  //戦略パネル更新
                STRDoneButton.gameObject.SetActive(false);
                STRpanel.gameObject.SetActive(false);
                publicityPanel.SetActive(true);
                OpenPanel = true;
                FoxyPanel.SetActive(true);//画面下部フィネ

                //交渉済みかどうか
                if (!ParameterCalc.instanceCalc.usePubli)
                {
                    FoxySay = " 少 し う さ ん く さ い な . . . ";
                }
                else
                {
                    FoxySay = " 忙 し そ う だ . . . ";
                }

                StartCoroutine(Publi_Log());

                break;

            case 4:
                STRDoneButton.gameObject.SetActive(false);
                STRpanel.gameObject.SetActive(false);
                KillPanel.gameObject.SetActive(true);
                OpenPanel = true;
                FoxyPanel.SetActive(true);//画面下部フィネ

                //暗殺パネル
                if (!ParameterCalc.instanceCalc.ExeKill)
                {
                    FoxySay = " 裏 工 作 . . . あ ま り 気 が 乗 ら な い 。 ";
                    ButtonKill.interactable = true;
                }
                else
                {
                    FoxySay = " こ れ 以 上 騒 ぎ は 起 こ せ な い . . . ";
                    ButtonKill.interactable = false;
                }
                StartCoroutine(Publi_Log());

                break;
                
            case 5:
                SelectItem_D = 0;
                D_Apparent(); //戦略パネル更新
                STRDoneButton.gameObject.SetActive(false);
                STRpanel.gameObject.SetActive(false);
                DealPanel.gameObject.SetActive(true);
                OpenPanel = true;
                FoxyPanel.SetActive(true);

                break;
        }
        SelectStr = 0;
    }

    /*----------キルパネル----------*/
    
    //実行処理
    public void KillDone()
    {
        ParameterCalc.instanceCalc.SelectKillPanel = SelectKill;
        PushN++;
        ParameterCalc.instanceCalc.StrKill();

        SelectStr = 4;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        KillPanel.gameObject.SetActive(false);
        FoxyPanel.SetActive(false);
        OpenPanel = false;
    }

    public void KillChoseDown() //民衆選択↓
    {
        //0-3を循環
        SelectKill -= 1;
        if(SelectKill < 0)
        {
            SelectKill = 3;
        }
        KillPanelAnim();
    }

    public void KillChoseUp() //民衆選択↑
    {
        //0-3を循環
        SelectKill += 1;
        if (SelectKill > 3)
        {
            SelectKill = 0;
        }
        KillPanelAnim();
    }

    //演出処理
    public void KillPanelAnim()
    {
        animator = PeopleAnim.GetComponent<Animator>();

        PeoNameText.text = PeoName [SelectKill];
        animator.SetInteger("SelectPeo", SelectKill);

    }

    //戻る
    public void KillSTRback()
    {
        SelectStr = 4;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        KillPanel.gameObject.SetActive(false);
        FoxyPanel.SetActive(false);
        OpenPanel = false;
    }

    /*----------取引パネル----------*/

    //交渉金額
    public void publiWayUp()
    {
        if(RunDispo)
        {
            ParameterCalc.instanceCalc.publiWay += 1;
            ButtonPubliDown.interactable = true;
            if (ParameterCalc.instanceCalc.publiWay > 1)
            {
                ButtonPubliUp.interactable = false;
            }
            drowPubli();
        }
    }

    public void publiWayDown()
    {
        if(RunDispo){
            ParameterCalc.instanceCalc.publiWay -= 1;
            ButtonPubliUp.interactable = true;
            if (ParameterCalc.instanceCalc.publiWay < 1)
            {
                ButtonPubliDown.interactable = false;
            }
            drowPubli();
        }
    }

    public void publiDone()
    {
        ParameterCalc.instanceCalc.publiCalc();
        SelectStr = 2;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        publicityPanel.SetActive(false);
        OpenPanel = false;
        FoxyPanel.SetActive(false);
        PushN++;
        Debug.Log("こしょう");
    }

    //交渉パネルセリフ更新
    private void drowPubli()
    {
        RunDispo = false;
        publiDoneBt = publiDoneButton.GetComponent<Button>();
        //計算処理
        double Conv = ParameterCalc.instanceCalc.HaveMoney * ParameterCalc.instanceCalc.publiRisk[ParameterCalc.instanceCalc.publiWay];
        ParameterCalc.instanceCalc.publiWayPay = (int)Conv;
        publiMoneyText.text = ParameterCalc.instanceCalc.publiWayPay + "";
        if (!ParameterCalc.instanceCalc.usePubli)
        {
            publiPlayerSay = " . . . " + ParameterCalc.instanceCalc.publiWayPay + " z で ど う だ ？ ";
            publiOtherSay = " そ の 報 酬 だ と 成 功 率 は "+ ParameterCalc.instanceCalc.publiRisk[ParameterCalc.instanceCalc.publiWay] * 100 +" % っ て と こ だ 。 ";
        }
        else
        {
            publiPlayerSay = " . . . . . . ";
            publiOtherSay = " 大 人 し く 結 果 を 待 つ ん だ な 。 ";
            publiDoneBt.interactable = false;
        }
        StartCoroutine(publiSay());
    }

    //交渉パネル二人の会話処理
    IEnumerator publiSay()
    {
        readNow = true; //文字表示用SE開始

        publiPlayerText.text = "";
        publiOtherText.text = "";

        words = publiPlayerSay.Split(' ');
        foreach (var word in words)
        {
            publiPlayerText.text = publiPlayerText.text + word;
            yield return new WaitForSeconds(0.05f);

        }
        words = publiOtherSay.Split(' ');
        
        /*--SE用--*/
        int finSE = words.Length - 20; //文字の長さを取得用
        int finTime = 0; //再生終了タイミング用
        if(finSE < 0)
        {
            finSE = 1;
        }
        /*--------*/
        
        foreach (var word in words)
        {
            finTime ++;
            publiOtherText.text = publiOtherText.text + word;
            yield return new WaitForSeconds(0.05f);
            if(finSE == finTime)
            {
                readNow = false; //文字表示用SE終了
            }
        }
        readNow = false; //文字表示用SE終了
        RunDispo = true; 
        
    }

    /*----------取引パネル----------*/

    //実行処理
    public void DealDone()
    {
        //商品強化処理
        ParameterCalc.instanceCalc.SelectRepleItem = SelectItem_D;
        ParameterCalc.instanceCalc.StrReple();
        SelectStr = 5;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        DealPanel.gameObject.SetActive(false);
        FoxyPanel.SetActive(false);
        OpenPanel = false;
        PushN++;

        if (SelectItem_D == 3) //ポーション初契約
        {
            GeneRepleEvent = true; //次のターンイベント発生
            ParameterCalc.instanceCalc.PopTurnEvent = ParameterCalc.instanceCalc.TurnCount + 1; //次ターン制限
            PotionRepleIvent = true;
            PushReple = true; //１ターンに入荷は一度まで
        }
        else if (SelectItem_D == 4) //株初契約
        {
            GeneRepleEvent = true; //次のターンイベント発生
            ParameterCalc.instanceCalc.PopTurnEvent = ParameterCalc.instanceCalc.TurnCount + 1; //次ターン制限
            StockRepleIvent = true;
            PushReple = true; //１ターンに入荷は一度まで
        }
    }

    //戻る
    public void Publi_STRback()
    {
        SelectStr = 3;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        publicityPanel.SetActive(false);
        OpenPanel = false;
        FoxyPanel.SetActive(false);
    }

    //アイテム選択
    public void D_DelectSwords() //剣
    {
        if (RunDispo)
        {
            SelectItem_D = 0;
            D_Apparent();
        }
    }

    public void D_DelectPotion() //薬
    {
        if (RunDispo)
        {
            if (HavePotionN)
            {
                SelectItem_D = 1;
            }
            else
            {
                SelectItem_D = 3;
            }

            D_Apparent();
        }
    }
    
    public void D_DelectStock() //株
    {
        if (RunDispo)
        {
            if (HaveStockN)
            {
                SelectItem_D = 2;
            }
            else
            {
                SelectItem_D = 4;
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
        StockCountText.text = "" + ParameterCalc.instanceCalc.StockReceived;
        D_NeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";

        //0個以上かつ買える時はインタラクティブ
        if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived && 0 < ParameterCalc.instanceCalc.StockReceived)
        {
            DealButton.interactable = true;
        }
        else
        {
            DealButton.interactable = false;
        }
    }

    public void StockCountDown()
    {
        ParameterCalc.instanceCalc.StockReceived -= 10;
        if (ParameterCalc.instanceCalc.StockReceived < 0)
        {
            ParameterCalc.instanceCalc.StockReceived = 0;
        }
        StockCountText.text = "" + ParameterCalc.instanceCalc.StockReceived;
        D_NeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";

        //0個以上かつ買える時はインタラクティブ
        if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived && 0 < ParameterCalc.instanceCalc.StockReceived)
        {
            DealButton.interactable = true;
        }
        else
        {
            DealButton.interactable = false;
        }
    }

    //表示の処理
    public void D_Apparent()
    {

        D_DoneButton.SetActive(true);

        //画像制御
        if (HavePotionN)
        {
            D_SpritePotion.sprite = HavePotion;
        }

        if (HaveStockN)
        {
            D_SpriteStock.sprite = HaveStock;
            
        }
        //株専用パネルオフ
        StockCountPanel.SetActive(false);

        //必要価格と商品名と販売価格の入れ替え
        switch (SelectItem_D)
        {
            case 0://剣強化
                DealItem.text = D_ItemName[SelectItem_D] + " Lv." + ParameterCalc.instanceCalc.BrSwordUpCount; //商品名
                D_NeedPrice.text = ParameterCalc.instanceCalc.BrSwordUp[ParameterCalc.instanceCalc.BrSwordUpCount] + "z必要";
                D_OfferPrice.text = "売値 : " + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount] + " → " + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount + 1];
                D_Button.text = "改良する";

                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.BrSwordUp[ParameterCalc.instanceCalc.BrSwordUpCount])
                {
                    DealButton.interactable = true;
                }
                else
                {
                    DealButton.interactable = false;
                }
                break;
            case 1: //薬強化
                DealItem.text = D_ItemName[SelectItem_D] + " Lv." + ParameterCalc.instanceCalc.PotionUpCount; //商品名
                D_NeedPrice.text = ParameterCalc.instanceCalc.PotionUp[ParameterCalc.instanceCalc.PotionUpCount] + "z必要";
                D_OfferPrice.text = "売値 : " + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount] + " → " + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount + 1];
                D_Button.text = "改良する";
                
                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.PotionUp[ParameterCalc.instanceCalc.PotionUpCount])
                {
                    DealButton.interactable = true;
                }
                else
                {
                    DealButton.interactable = false;
                }
                break;
            case 2: //株仕入れ
                DealItem.text = D_ItemName[SelectItem_D]; //商品名
                D_NeedPrice.text = ParameterCalc.instanceCalc.StockGet * ParameterCalc.instanceCalc.StockReceived + "z必要";
                D_OfferPrice.text = "売値 : " + ParameterCalc.instanceCalc.StockSell + "z / かぶ";
                D_Button.text = "仕入れる";
                StockCountPanel.SetActive(true); //株専用パネルオン
                StockCountText.text = ""+ ParameterCalc.instanceCalc.StockReceived;
                break;
            case 3: //薬入手
                DealItem.text = D_ItemName[SelectItem_D]; //商品名
                D_NeedPrice.text = ParameterCalc.instanceCalc.PotionGet + "z必要";
                D_OfferPrice.text = " ";
                D_Button.text = "解放する";

                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.PotionGet)
                {
                    DealButton.interactable = true;
                }
                else
                {
                    DealButton.interactable = false;
                }
                break;
            case 4: //株解放
                DealItem.text = D_ItemName[SelectItem_D]; //商品名
                D_NeedPrice.text = ParameterCalc.instanceCalc.StockOpen + "z必要";
                D_OfferPrice.text = " ";
                D_Button.text = "解放する";
                if (ParameterCalc.instanceCalc.HaveMoney >= ParameterCalc.instanceCalc.StockOpen)
                {
                    DealButton.interactable = true;
                }
                else
                {
                    DealButton.interactable = false;
                }
                break;
        }

        //右下セリフのテキスト
        switch (SelectItem_D)
        {
            case 0:
                FoxySay = " こ の 地 域 で は よ く 売 れ る . . . ";
                break;

            case 1:
                FoxySay = " 自 分 に 使 う の は や め て お こ う . . . ";
                break;

            case 2:
                //株を所有していないとき
                if (ParameterCalc.instanceCalc.StockQuantity == 0)
                {
                    FoxySay = " 在 庫 . . . . . . な い や . . . ";
                }
                else
                {
                    FoxySay = " 在 庫 . . . . . . " + ParameterCalc.instanceCalc.StockQuantity + " か ぶ . . . ";
                }

                break;

            case 3:
                if(PushReple)
                {
                    FoxySay = " 部 下 の 交 渉 を 待 と う . . . . . . ";
                    D_DoneButton.SetActive(false);
                }
                else
                {
                    FoxySay = " キ ケ ン な に お い . . . . . . ";
                }
                break;

            case 4:
                if (PushReple)
                {
                    FoxySay = " 部 下 の 交 渉 を 待 と う . . . . . . ";
                    D_DoneButton.SetActive(false);
                }
                else
                {
                    FoxySay = " 仕 入 れ よ う か な . . . . . . ";
                }
                break;
        }
        //説明文入れ替え
        D_ExpText.text = "";
        FoxyText.text = "";
        StartCoroutine(D_ExpLog());
    }

    //コルーチンを使って、１文字ごと表示する。
    IEnumerator D_ExpLog()
    {
        RunDispo = false; //処理中に他のパネルの選択を出来なくする
        readNow = true; //文字表示用SE開始

        // 半角スペースで文字を分割する。
        words = ItemExp[SelectItem_D].Split(' ');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            D_ExpText.text = D_ExpText.text + word;
            yield return new WaitForSeconds(0.02f);

        }

        //主人公のセリフ
        words = FoxySay.Split(' ');

        /*--SE用--*/
        int finSE = words.Length - 13; //文字の長さを取得用
        int finTime = 0; //再生終了タイミング用
        if(finSE < 0)
        {
            finSE = 1;
        }

        /*--------*/

        foreach (var word in words)
        {
            finTime ++;
            // 0.1秒刻みで１文字ずつ表示する。
            FoxyText.text = FoxyText.text + word;
            yield return new WaitForSeconds(0.08f);
            if(finSE == finTime)
            {
                readNow = false; //文字表示用SE終了
            }
        }
        readNow = false; //文字表示用SE終了
        RunDispo = true; //他のパネルの選択を可能にする
       
    }

    //戻る
    public void D_STRback()
    {
        SelectStr = 5;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        DealPanel.gameObject.SetActive(false);
        OpenPanel = false;
        ParameterCalc.instanceCalc.StockReceived = 30;
        FoxyPanel.SetActive(false);
    }

    /*-----交渉パネルのテキスト処理-----*/

    IEnumerator Publi_Log()
    {
        readNow = true; //文字表示用SE開始

        RunDispo = false;
        FoxyText.text = "";

        words = FoxySay.Split(' ');

        /*--SE用--*/
        int finSE = words.Length - 13; //文字の長さを取得用
        int finTime = 0; //再生終了タイミング用
        if(finSE < 0)
        {
            finSE = 1;
        }
        /*--------*/
        foreach (var word in words)
        {
            finTime++;
            FoxyText.text = FoxyText.text + word;
            yield return new WaitForSeconds(0.08f);
            if(finSE == finTime)
            {
                readNow = false; //文字表示用SE終了
            }
        }
        readNow = false; //文字表示用SE終了
        RunDispo = true; 
    }

    /*-----商品選択UI-----*/

    //表示制御
    public void ForDrawPanel()
    {
        //アイテム選択画面制御
        if (HavePotionN)         
        {
            SpritePotion.sprite = HavePotion;
            PotionName.text = "こうかなくすり";
        }

        if (HaveStockN)          
        {
            SpriteStock.sprite = HaveStock;
            StockName.text = "まぼろしのかぶ";
            
        }
    }
    
    public void BrSword()
    {
        ParameterCalc.instanceCalc.ToolType = 0;
        itemName.text = "どうのつるぎ     売値:" + ParameterCalc.instanceCalc.BrSwordSell[ParameterCalc.instanceCalc.BrSwordUpCount] +"z";
        ButtonSelectItem.interactable = true;
    }

    public void HiPotion()
    {
        ParameterCalc.instanceCalc.ToolType = 1;

        if (HavePotionN)    //解放しているか否か
        {
            itemName.text = "こうかなくすり     売値:" + ParameterCalc.instanceCalc.PotionSell[ParameterCalc.instanceCalc.PotionUpCount] + "z";
            ButtonSelectItem.interactable = true;
        }
        else
        {
            itemName.text = "-----";
            ButtonSelectItem.interactable = false;
        }
    }

    public void LuckyTurnip()
    {
        ParameterCalc.instanceCalc.ToolType = 2;

        if (HaveStockN)
        {
            itemName.text = "まぼろしのかぶ × "+ ParameterCalc.instanceCalc.StockQuantity + "  設定価格:" + ParameterCalc.instanceCalc.StockSell +" / かぶ";
            ButtonSelectItem.interactable = true;
        }
        else 
        {
            itemName.text = "-----";
            ButtonSelectItem.interactable = false;
        }
        //株を保有していないときは非インタラクティブ化
        if(ParameterCalc.instanceCalc.StockQuantity > 0)
        {
            ButtonSelectItem.interactable = true;
        }
        else
        {
            ButtonSelectItem.interactable = false;
        }
    }

    public void SelectDone()
    {
        PushN += 1;
        ParameterCalc.instanceCalc.GeneCalc(); //計算開始
        mainUI.gameObject.SetActive(false);
        ItemSelectUI.gameObject.SetActive(false);
    }

    /*----------アイテム初回獲得パネル----------*/

    public void GetFirst()
    {
        mainUI.gameObject.SetActive(true);
        StrUI.gameObject.SetActive(true);
        Get_ItemPanel.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(false);
        //狐の会話をオフ、処理の終わりにオン
        ClickJudge = false;

        if (PotionRepleIvent)
        {
            Get_ItemPanel.gameObject.SetActive(true);
            Get_SpriteItem.sprite = HavePotion; //画像入れ替え
            GetPorS = " こ う か な く す り の 　　　　　　交 易 ル ー ト が 確 立 し た よ ! ! ";
            StartCoroutine(Get_Goods());
            PotionRepleIvent = false;
            //アイテム選択画面用
            HavePotionN = true;
        }
        if (StockRepleIvent)
        {
            Get_ItemPanel.gameObject.SetActive(true); //株だけ
            Get_SpriteItem.sprite = HaveStock; //画像入れ替え
            GetPorS = " ま ぼ ろ し の か ぶ の 　　　　　　交 易 ル ー ト が 確 立 し た よ ! ! ";
            StartCoroutine(Get_Goods());
            StockRepleIvent = false;
            //アイテム選択画面用
            HaveStockN = true;
        }
    }

    //商品獲得UI処理
    public void D_GetItem()
    {
        mainUI.gameObject.SetActive(false);
        StrUI.gameObject.SetActive(false);
        Get_ItemPanel.gameObject.SetActive(false);
        STRpanel.gameObject.SetActive(true);
        ClickJudge = true;
    }

    //アイテム解放のテキスト処理
    IEnumerator Get_Goods()
    {
        GetExpText.text = "";
        RunDispo = false;//処理中に他のパネルの選択を出来なくする
        readNow = true; //文字表示用SE開始

        // 半角スペースで文字を分割する。
        words = GetPorS.Split(' ');

        /*--SE用--*/
        int finSE = words.Length - 10; //文字の長さを取得用
        int finTime = 0; //再生終了タイミング用
        if(finSE < 0)
        {
            finSE = 1;
        }
        /*--------*/

        foreach (var word in words)
        {
            finTime++;
            // 0.1秒刻みで１文字ずつ表示する
            GetExpText.text = GetExpText.text + word;
            yield return new WaitForSeconds(0.1f);
            if(finSE == finTime)
            {
                readNow = false; //文字表示用SE終了
            }
        }
        RunDispo = true; //他のパネルの選択を可能にする
        readNow = false; //文字表示用SE終了  
    }

    /*----------チュートリアル----------*/
    private void Tutorial()
    {
        firstESC = true;
        if (PushNtutorial == 1)
        {
            FoxDia1.gameObject.SetActive(true);
            FoxDia1_Text.text = "おかしら！\n今日もいい朝ですね！";
            ClickJudge = true;
        }
        else if (PushNtutorial == 2)
        {
            FoxDia1_Text.text = "ジャンジャン稼ぎましょう！";
            ClickJudge = true;
        }
        else if (PushNtutorial == 3)
        {
            FoxDia1_Text.text = "え！稼ぎかた\n忘れたんですか！";
            ClickJudge = true;
        }
        else if (PushNtutorial == 4)
        {
            FoxDia1_Text.text = "困ったらこれを見てください！";
            ClickJudge = true;
        }
        else if (PushNtutorial == 5)
        {
            TutrialExp.gameObject.SetActive(true);
            mainUI.gameObject.SetActive(true);
            FoxDia1.gameObject.SetActive(false);
            HaveMoneyPanel.gameObject.SetActive(false);
            SlaveTurnPanel.gameObject.SetActive(false);
        }
        else if (PushNtutorial == 6)
        {
            FoxDia1.gameObject.SetActive(true);
            mainUI.gameObject.SetActive(false);
            FoxDia1_Text.text = "あとESC？ってキーを\n押してみてください！";
            ClickJudge = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                firstESC = false;
                PushNtutorial ++;
            }
        }
        else if (PushNtutorial == 7)
        {
            FoxDia1_Text.text = "これでバッチリですね！";
            ClickJudge = true;
        }
        else if (PushNtutorial == 8)
        {
            FoxDia1_Text.text = "目標は・・・\n１０万Zです！";
            ClickJudge = true;
        }
        else if (PushNtutorial == 9)
        {
            HaveMoneyPanel.gameObject.SetActive(true);
            SlaveTurnPanel.gameObject.SetActive(true);
            FoxDia1_Text.text = "早速稼ぎましょう！";
            ClickJudge = true;
        }
        else if (PushNtutorial >= 10)
        {
            FoxDia1.gameObject.SetActive(false);
            ParameterCalc.instanceCalc.initialPlay = false;
            PushN = 0;
            ClickJudge = true;
        }
    }

    public void CloseTutorial()
    {
        TutrialExp.gameObject.SetActive(false);
        PushNtutorial += 1;
    }

    /*----------クリアイベント----------*/

    private void ClearIvent()
    {
        //menuCntにてescキー無効
        nowClear = true;

        if (PushN == 1)
        {
            FoxDia1.gameObject.SetActive(true);
            FoxDia1_Text.text = "やったー！！";
            ClickJudge = true;
        }
        else if (PushN == 2)
        {
            FoxDia1_Text.text = "おかしら！\n目標達成だ！";
            ClickJudge = true;
        }
        else if (PushN == 3)
        {
            FoxDia1.gameObject.SetActive(false);
            FoxDia3.gameObject.SetActive(true);
            FoxDia2_Text.text = "おかしら！\nおつかれさま！";
            ClickJudge = true;
        }
        else if (PushN == 4)
        {
            FoxDia3.gameObject.SetActive(false);
            FoxDia5.gameObject.SetActive(true);
            FoxDia3_Text.text = "おかしら\n次の町にいこう。";
            ClickJudge = true;
        }
        else if (PushN == 5)
        {
            FoxDia5.gameObject.SetActive(false);
            FoxDia3.gameObject.SetActive(true);
            FoxDia2_Text.text = "おかしら！\nいこう！";
            ClickJudge = true;
        }
        else if (PushN == 6)
        {
            FoxDia3.gameObject.SetActive(false);
            ClearAnim = true;
            ClickJudge = true;
        }
        else if (PushN >= 7)
        {
            PlClearDoAnim = true;
        }
        //主人公のアニメが終わったらパネルを開く
        if(FinClearAnim)
        {
            GameClearPanel.SetActive(true);
            ClearResult_Text.text = ParameterCalc.instanceCalc.OutPutResult;
            
        }
    }

    private void GameOverEvent()
    {
        if (PushN == 1)
        {
            FoxDia1.gameObject.SetActive(true);
            FoxDia1_Text.text = "おかしら...";
            ClickJudge = true;
        }
        else if (PushN == 2)
        {
            FoxDia1_Text.text = "お金...\nなくなったんですか";
            ClickJudge = true;
        }
        else if (PushN == 3)
        {
            FoxDia1.gameObject.SetActive(false);
            FoxDia3.gameObject.SetActive(true);
            FoxDia2_Text.text = "おかしら！\n報酬ないの...";
            ClickJudge = true;
        }
        else if (PushN == 4)
        {
            FoxDia3.gameObject.SetActive(false);
            FoxDia5.gameObject.SetActive(true);
            FoxDia3_Text.text = "おかしら\n解散しよう";
            ClickJudge = true;
        }
        else if (PushN == 5)
        {
            FoxDia5.gameObject.SetActive(false);
            ClearAnim = true;
            ClickJudge = true;
        }
        else if (PushN >= 6)
        {
            SceneCnt1.instanceCnt1.isFadeOut_A = true;
            GameOverFade = true;
            //フェードアウトの後にゲームオーバーパネルオン
            if (doOverAnim)
            {
                GameOverPanel.gameObject.SetActive(true);
            }
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

    //テキストを表示している間再生
	public void TextFlowSE()
	{
		soundA.PlaySe(textFlowSE);
	}
}

