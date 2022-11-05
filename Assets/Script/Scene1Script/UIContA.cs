using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContA : MonoBehaviour
{

    //メインUI
    public Text SlaveTx;
    public Text CrimeRateTx;
    public Text HavemoneyT;
    public Text TurnCountText;

    public static int PushN;
    public GameObject FoxDia1;
    public GameObject FoxDia2;
    public Text FoxDia1_Text;
    public GameObject FoxDia3;
    public GameObject FoxDia4;
    public Text FoxDia2_Text;
    public GameObject FoxDia5;
    public GameObject FoxDia6;
    public Text FoxDia3_Text;
    public GameObject mainUI;
    public GameObject StrUI;
    public GameObject ItemSelectUI;
    public Text itemName;
    //戦略Doneボタン
    public GameObject DoneButton;

    //暗殺パネル
    public GameObject KillPanel;
    public GameObject STRpanel;
    public GameObject STRDoneButton;
    private bool OpenPanel; //ケア
    private int SelectKill; //暗殺対象
    public string[] PeoName = { "貧民", "市民", "富豪", "貴族" }; //名前
    public Text PeoNameText; //名前を入れる箱
    private Animator animator; //アニメーター
    public GameObject PeopleAnim; //アニメーションさせるオブジェクト

    //交渉パネル
    public GameObject publicityPanel;
    public Text publiMoneyText;
    public Text publiPlayerText;
    public Text publiOtherText;
    public GameObject publiDoneButton;
    private Button publiDoneBt;
    private string publiPlayerSay;
    private string publiOtherSay;

    //入荷パネル
    public GameObject DealPanel;
    public Text DealItem;
    private int SelectItem_D;
    private string[] D_ItemName = new string[5]; //名前
    public Text D_NeedPrice; //必要価格
    public Text D_OfferPrice; //販売価格
    public Text D_Button; //ボタン
    public Text D_ExpText; //説明文入れ替え↓
    public string[] ItemExp = new string[5];
    private string[] words;
    private bool RunDispo;
    public GameObject FoxyPanel;
    public Text FoxyText; //主人公一言
    private string FoxySay; //主人公セリフ格納
    private bool PushReple = false;

    public GameObject D_DoneButton; //入荷待ちに非アクティブ化

    //入荷後の画像入れ替え用
    public GameObject D_Potionflame;
    public GameObject D_Stockflame;

    private Image D_SpritePotion;
    private Image D_SpriteStock;
   //株専用パネル
    public GameObject StockCountPanel;
    public Text StockCountText;

    //アイテムゲットUI
    public GameObject Get_ItemPanel;
    public GameObject Get_ItemFlame;
    private Image Get_SpriteItem;
    public Text GetExpText; //一言
    private string GetPorS; //ぽーしょんorかぶ格納


    //アイテム選択
    public Button ButtonSelectItem;
    public Button ButtonKill;
    public Button ButtonPubliUp;
    public Button ButtonPubliDown;

    //イベント発生フラグ
    private static bool PotionRepleIvent = false;
    private static bool StockRepleIvent = false;
    private static bool GeneRepleEvent = false; //次のターンにイベント発生フラグ
    public static bool ClearAnim = false;
    public static bool PlClearDoAnim = false;
    public static bool FinClearAnim = false;
    public GameObject GameClearPanel;
    public Text ClearResult_Text;
    //ゲームオーバー
    public static bool doOverAnim = false;
    public static bool GameOverFade = false;
    public GameObject GameOverPanel;

    /*---------商品選択用----------*/

    //入手後の素材
    public Sprite HavePotion;
    public Sprite HaveStock;
    //コンポーネント取得先
    public GameObject Potionflame;
    public GameObject Stockflame;

    private Image SpritePotion;
    private Image SpriteStock;

    //持っているか否か
    private static bool HavePotionN = false;
    private static bool HaveStockN = false;

    //テキスト入れ替え
    public Text PotionName;
    public Text StockName;

    //購入パネル初期テキスト
    private bool drawSelectItem;


    /*------------------------------*/


    //クリック重複ケア
    private bool ClickJudge;

    //戦略選択チェック
    public static int SelectStr;

    // Start is called before the first frame update
    void Start()
    {
        PushN = 0;
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


        ClickJudge = true;
        OpenPanel = false;
        PushReple = false;
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

    }

    // Update is called once per frame
    void Update()
    {
        //UI更新
        SlaveTx.text = ParameterCalc.Slave + "";
        CrimeRateTx.text = ParameterCalc.CrimeRate + "/100";
        HavemoneyT.text = ParameterCalc.HaveMoney + "";
        TurnCountText.text = ParameterCalc.TurnCount + " 日目";


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
            if (ParameterCalc.Slave > 0)
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

        if (Input.GetMouseButtonDown(0) && ClickJudge)
        {
            PushN += 1;
            ClickJudge = false;
        }



        if (ParameterCalc.GameClear && ParameterCalc.TurnCount == ParameterCalc.PopTurnEvent)  //クリア処理
        {
            ClearIvent();
        }

        else if(ParameterCalc.GameOver && ParameterCalc.TurnCount == ParameterCalc.PopTurnEvent) //ゲームオーバー処理
        {
            GameOverEvent();
        }

        else if(GeneRepleEvent && ParameterCalc.TurnCount == ParameterCalc.PopTurnEvent) //前ターンの入荷イベント発生
        {
            GetFirst();
            GeneRepleEvent = false;
        }

        else//通常処理
        {

            if (PushN == 1)
            {
                FoxDia1.gameObject.SetActive(true);
                ClickJudge = true;
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
                SceneCnt_A.isFadeOut_A = true;
            }
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
                ParameterCalc.instance.StrGossip();
                PushN++;
                break;

            case 2:
                ParameterCalc.instance.StrPray();
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
                if (!ParameterCalc.usePubli)
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
                if (!ParameterCalc.ExeKill)
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
        ParameterCalc.SelectKillPanel = SelectKill;
        PushN++;
        ParameterCalc.instance.StrKill();

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

    /*------------------------------*/

    /*----------取引パネル----------*/


    //交渉金額
    public void publiWayUp()
    {
        if(RunDispo){
            ParameterCalc.publiWay += 1;
            ButtonPubliDown.interactable = true;
            if (ParameterCalc.publiWay > 1)
            {
                //ParameterCalc.publiWay = 0;
                ButtonPubliUp.interactable = false;
            }
            drowPubli();
        }
    }

    public void publiWayDown()
    {
        if(RunDispo){
            ParameterCalc.publiWay -= 1;
            ButtonPubliUp.interactable = true;
            if (ParameterCalc.publiWay < 1)
            {
                //ParameterCalc.publiWay = 2;
                ButtonPubliDown.interactable = false;
            }
            drowPubli();
        }
    }

    public void publiDone()
    {
        ParameterCalc.instance.publiCalc();
        SelectStr = 2;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        publicityPanel.SetActive(false);
        OpenPanel = false;
        FoxyPanel.SetActive(false);
        PushN++;
    }

    //交渉パネルセリフ更新

    private void drowPubli()
    {
        publiDoneBt = publiDoneButton.GetComponent<Button>();
        //計算処理
        double Conv = ParameterCalc.HaveMoney * ParameterCalc.publiRisk[ParameterCalc.publiWay];
        ParameterCalc.publiWayPay = (int)Conv;
        publiMoneyText.text = ParameterCalc.publiWayPay + "";
        if (!ParameterCalc.usePubli)
        {
            publiPlayerSay = " . . . " + ParameterCalc.publiWayPay + " z で ど う だ ？ ";
            publiOtherSay = " そ の 報 酬 だ と 成 功 率 は "+ ParameterCalc.publiRisk[ParameterCalc.publiWay] * 100 +" % っ て と こ だ 。 ";
        }
        else
        {
            publiPlayerSay = " . . . . . . ";
            publiOtherSay = " 大 人 し く 結 果 を 待 つ ん だ な 。 ";
            publiDoneBt.interactable = false;
        }
        StartCoroutine(publiSay());
        Debug.Log(ParameterCalc.usePubli);
    }

    //交渉パネル二人の会話処理
    IEnumerator publiSay()
    {
        RunDispo = false;
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

        RunDispo = true; 
    }

    /*------------------------------*/



    //戻る
    public void Publi_STRback()
    {
        SelectStr = 2;
        STRDoneButton.gameObject.SetActive(true);
        STRpanel.gameObject.SetActive(true);
        publicityPanel.SetActive(false);
        OpenPanel = false;
        FoxyPanel.SetActive(false);
    }


    /*----------取引パネル----------*/

    //実行処理
    public void DealDone()
    {
        //商品強化処理
        ParameterCalc.SelectRepleItem = SelectItem_D;
        ParameterCalc.instance.StrReple();
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
            ParameterCalc.PopTurnEvent = ParameterCalc.TurnCount + 1; //次ターン制限
            PotionRepleIvent = true;
            PushReple = true; //１ターンに入荷は一度まで
        }
        else if (SelectItem_D == 4) //株初契約
        {
            GeneRepleEvent = true; //次のターンイベント発生
            ParameterCalc.PopTurnEvent = ParameterCalc.TurnCount + 1; //次ターン制限
            StockRepleIvent = true;
            PushReple = true; //１ターンに入荷は一度まで
        }
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
        ParameterCalc.StockReceived += 10;
        if(ParameterCalc.StockReceived > 500)
        {
            ParameterCalc.StockReceived = 500;
        }
        StockCountText.text = "" + ParameterCalc.StockReceived;
        D_NeedPrice.text = ParameterCalc.StockGet * ParameterCalc.StockReceived + "z必要";
    }

    public void StockCountDown()
    {
        ParameterCalc.StockReceived -= 10;
        if (ParameterCalc.StockReceived < 0)
        {
            ParameterCalc.StockReceived = 0;
        }
        StockCountText.text = "" + ParameterCalc.StockReceived;
        D_NeedPrice.text = ParameterCalc.StockGet * ParameterCalc.StockReceived + "z必要";
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
                DealItem.text = D_ItemName[SelectItem_D] + " Lv." + ParameterCalc.BrSwordUpCount; //商品名
                D_NeedPrice.text = ParameterCalc.BrSwordUp + "z必要";
                D_OfferPrice.text = "売値 : " + ParameterCalc.BrSwordSell + " → " + ParameterCalc.BrSwordSell * 2;
                D_Button.text = "改良する";
                break;
            case 1: //薬強化
                DealItem.text = D_ItemName[SelectItem_D] + " Lv." + ParameterCalc.PotionUpCount; //商品名
                D_NeedPrice.text = ParameterCalc.PotionUp + "z必要";
                D_OfferPrice.text = "売値 : " + ParameterCalc.PotionSell + " → " + ParameterCalc.PotionSell * 2;
                D_Button.text = "改良する";
                break;
            case 2: //株仕入れ
                DealItem.text = D_ItemName[SelectItem_D]; //商品名
                D_NeedPrice.text = ParameterCalc.StockGet * ParameterCalc.StockReceived + "z必要";
                D_OfferPrice.text = "売値 : " + ParameterCalc.StockSell + "z / かぶ";
                D_Button.text = "仕入れる";
                StockCountPanel.SetActive(true); //株専用パネルオン
                StockCountText.text = ""+ ParameterCalc.StockReceived;
                break;
            case 3: //薬入手
                DealItem.text = D_ItemName[SelectItem_D]; //商品名
                D_NeedPrice.text = ParameterCalc.PotionGet + "z必要";
                D_OfferPrice.text = " ";
                D_Button.text = "解放する";
                break;
            case 4: //株解放
                DealItem.text = D_ItemName[SelectItem_D]; //商品名
                D_NeedPrice.text = ParameterCalc.StockOpen + "z必要";
                D_OfferPrice.text = " ";
                D_Button.text = "解放する";
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
                if (ParameterCalc.StockQuantity == 0)
                {
                    FoxySay = " 在 庫 . . . . . . な い や . . . ";
                }
                else
                {
                    FoxySay = " 在 庫 . . . . . . " + ParameterCalc.StockQuantity + " か ぶ . . . ";
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
        foreach (var word in words)
        {

            // 0.1秒刻みで１文字ずつ表示する。
            FoxyText.text = FoxyText.text + word;
            yield return new WaitForSeconds(0.08f);

        }



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
        ParameterCalc.StockReceived = 30;
        FoxyPanel.SetActive(false);
    }


    /*----------------------------------*/

    /*-----交渉パネルのテキスト処理-----*/

    IEnumerator Publi_Log()
    {
        RunDispo = false;
        FoxyText.text = "";

        words = FoxySay.Split(' ');
        foreach (var word in words)
        {

            FoxyText.text = FoxyText.text + word;
            yield return new WaitForSeconds(0.08f);

        }

        RunDispo = true; 
    }

    /*------------------------------*/

    //商品選択UI

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
        ParameterCalc.ToolType = 0;
        itemName.text = "どうのつるぎ     売値:" + ParameterCalc.BrSwordSell +"z";
        ButtonSelectItem.interactable = true;
    }

    public void HiPotion()
    {
        ParameterCalc.ToolType = 1;

        if (HavePotionN)         //解放しているか否か
        {
            itemName.text = "こうかなくすり     売値:" + ParameterCalc.PotionSell + "z";
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
        ParameterCalc.ToolType = 2;

        if (HaveStockN)
        {
            itemName.text = "まぼろしのかぶ × "+ ParameterCalc.StockQuantity + "設定価格:" + ParameterCalc.StockSell +" / かぶ";
            ButtonSelectItem.interactable = true;
        }
        else 
        {
            itemName.text = "-----";
            ButtonSelectItem.interactable = false;
        }
    }

    public void SelectDone()
    {
        PushN += 1;
        ParameterCalc.instance.GeneCalc(); //計算開始
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
        //狐の会話をオフ　処理の終わりに最後にオンにする
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

        // 半角スペースで文字を分割する。
        words = GetPorS.Split(' ');

        foreach (var word in words)
        {

            // 0.1秒刻みで１文字ずつ表示する
            GetExpText.text = GetExpText.text + word;
            yield return new WaitForSeconds(0.1f);

        }
        RunDispo = true; //他のパネルの選択を可能にする
    }

    /*------------------------------------------*/

    /*----------クリアイベント----------*/
    private void ClearIvent()
    {
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
            ClearResult_Text.text = ParameterCalc.OutPutResult;
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
            SceneCnt_A.isFadeOut_A = true;
            GameOverFade = true;
            //フェードアウトの後にゲームオーバーパネルオン
            //タイトルへ
            if (doOverAnim)
            {
                GameOverPanel.gameObject.SetActive(true);
            }
        }
    }

    /*----------------------------------*/
}


/*
 * 
 * 商品選択の株の説明を直す
 * 商品獲得前に狐に喋らせる
 * ゲームクリア、オーバーの処理
 * 
 */
