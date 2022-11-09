using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParameterCalc : MonoBehaviour
{
    //他のスクリプトからメソッドを呼べるように
    public static ParameterCalc instance;

    /* パラメータ宣言 */

    //初見プレイか
    public static bool initialPlay;

    //ターンカウント
    public static int TurnCount = 0;

    //お金周り
    private int TargetAmount = 1000000;
    public static bool GameClear = false;
    public static bool GameOver = false;
    public static int HaveMoney = 1000;
    public static double SelectItem = 0.0;

    //民衆倍率
    private static double Poor = 0.7;
    public static bool PoorDebt; //貧民破産判定
    private static double General = 1.0;
    private static double Millionaire = 1.3;
    private static double Noble = 1.6;

    //イベント管理
    public static double CrimeRate = 0.0;
    public static double PoorMoney = 100.0;
    public static int Slave = 0;
    public static int ToolType; //どの商品を選んだか

    //銅の剣
    public static double BrSwordSell = 400;
    public static double BrSwordUp = 2000;
    public static int BrSwordUpCount = 1; //強化回数_強化のメソッドに追加する

    //麻薬
    public static int PotionGet = 3000;
    public static double PotionSell = 1000;
    public static double PotionUp = 2000;
    public static int PotionUpCount = 0; //強化回数
    public static bool HavePotionJ = false; //所持チェック用

    //株
    public static int StockOpen = 30000;
    public static int StockGet = 1000;
    public static int StockSell = 3000;
    private static double StockEnlarge; //倍率管理
    private static int StockCrash; //価格暴落
    public static bool HaveStockJ = false; //所持チェック用
    public static int StockQuantity; //株所有数
    public static int StockReceived; //入荷用
    private bool UseStock;

    //戦略用変数
    private int UseGossip = 0;
    public static bool ExeKill;
    private int killNumber;
    public static int SelectKillPanel;  //キル画面のUI操作用
    public static int SelectRepleItem;  //アイテム強化対象選択
    private int usePray; 
    //����
    public static int publiWay;
    public static int publiWayPay;
    public static double[] publiRisk = new double[] { 0.2, 0.5, 0.8 };
    public static bool usePubli;

    //民衆選定用
    //public static int[] PeopleType = new int[4];
    private static int Cushion; //計算用クッション



    /* メイン計算用変数 */
    public static int GenePeopleCount; //民衆生成数
    public static int[] GenePeopleType = new int[5]; //民衆の種類
    public static double[] ReceiveMoney = new double[5]; //受け取った金額
    public static int TotalReceiveMoney;

    /* 金額天引き用変数 */
    public static int[] Paycheck = new int[100];
    public static int Income;
    private double Convert;
    private double RandTax = 0.1; //土地代
    public static int RandTaxPay; //天引き金額
    private double HaveTax = 0.05; //所得税
    public static int HaveTaxPay; //天引き金額
    public static int StealPeoplePay;
    public static bool StealTaxjuge;
    private int TurnEndHaveMoney;
    public static int TotalPayment;

    //Scoreテキスト
    public static int[] ResultScore = new int[] {0,0,0};
    public static string[] ResultScoreStr = new string[3];
    public static string OutPutResult;

    //イベント発動タイミング用変数
    public static int PopTurnEvent;

    public void Awake()
    {
        //初見プレイ初期化
        initialPlay = false;
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!SaveControl.NewGame)
        {
            if (GameClear || GameOver)
            {

            }
            else
            {
                LoadData();
            }
        }
        else
        {
            //初見
            initialPlay = true;
        }

        //ターン追加
        TurnCount++;

        //戦略用変数リセット
        UseGossip = 0;
        ExeKill = false;
        SelectKillPanel = 0;
        SelectRepleItem = 0;
        UseStock = false;
        StockEnlarge = 1.0;
        StockCrash = 0;
        StockReceived = 30;
        usePubli = false;
        publiWay = 1;
        usePray = 0;
        PoorDebt = false;

        //天引き計算用
        Income = 0;
        Paycheck[0] = 100;
        Paycheck[1] = 100;
        StealTaxjuge = false;
        TotalPayment = 0;

        //商品選択
        ToolType = 0;
    }

    //データをロード
    private void LoadData()
    {
        Debug.Log("aaa");
        SaveControl.instanceSave.Doload();
    }

    /*------------------------------------------------------------*/

    /*戦略メソッドたち*/

    //噂話
    public void StrGossip()
    {
        UseGossip++;
    }

    //暗殺
    public void StrKill()
    {
        if (Slave > 0)
        {
            Slave -= 1;
            ExeKill = true;

            //UIで選択した民衆の種類を処刑対象にセット
            killNumber = SelectKillPanel;
        }
    }

    //交渉
    public void publiCalc()
    {
        HaveMoney -= publiWayPay;
        usePubli = true;
    }

    //商品強化
    public void StrReple()
    {
        switch (SelectRepleItem)
        {
            case 0: //銅の剣
                //if (HaveMoney >= BrSwordUp)
                {
                    HaveMoney -= (int)BrSwordUp;
                    BrSwordUp *= 1.5;
                    BrSwordSell *= 1.5;
                    BrSwordUpCount += 1;
                }

                break;

            case 1: //ハイポーション

                //if (HaveMoney >= PotionUp)
                {
                    HaveMoney -= (int)PotionUp;
                    PotionUp *= 1.5;
                    PotionSell *= 1.5;
                    PotionUpCount += 1;
                }

                break;

            case 2: //株
                //if (HaveMoney >= StockGet * StockReceived)
                {
                    HaveMoney -= StockGet * StockReceived;
                    StockQuantity += StockReceived;
                }

                break;

            case 3: //薬入荷
                //if (HaveMoney >= PotionGet)
                {
                    HaveMoney -= PotionGet;
                    HavePotionJ = true;
                }
                break;

            case 4: //株入荷
                //if (HaveMoney >= StockOpen)
                {
                    HaveMoney -= StockOpen;
                    HaveStockJ = true;
                }
                break;
        }
    }


    //祈る
    public void StrPray()
    {
        usePray += 1;
    }


    /*------------------------------------------------------------*/

    //実際の収入計算処理
    public void GeneCalc()
    {
        //商品をセット
        switch (ToolType)
        {
            case 0: //剣
                SelectItem = BrSwordSell;
                break;

            case 1: //薬
                SelectItem = PotionSell;
                break;

            case 2: //株
                SelectItem = StockSell * StockQuantity;
                StockQuantity = 0;
                UseStock = true;
                break;

        }


        TotalReceiveMoney = 0;

        if (usePubli)
        {
            int publiI = Random.Range(1, 101);
            //交渉
            switch (publiWay)
            {
                case 0:
                    if (publiI < 21)
                    {
                        GenePeopleCount = 4;
                    }
                    break;
                case 1:
                    if (publiI < 51)
                    {
                        GenePeopleCount = 4;
                    }
                    break;
                case 2:
                    if (publiI < 81)
                    {
                        GenePeopleCount = 4;
                    }
                    break;
            }
        }
        else
        {
             //1以上6未満
            GenePeopleCount = Random.Range(0, 5);   //何人生成するか
            //PV用
            GenePeopleCount = 3;
        }
        for (int i = 0; i <= GenePeopleCount; ++i)
        {
            //民衆の生成数設定
            Cushion = Random.Range(0, 101);

            if (0 <= Cushion && Cushion < 21)
            {
                Cushion = 0;
            }
            else if (21 <= Cushion && Cushion < 61)
            {
                Cushion = 1;
            }
            else if (61 <= Cushion && Cushion < 91)
            {
                Cushion = 2;
            }
            else if (91 <= Cushion && Cushion < 101)
            {
                Cushion = 3;
            }

            //民衆の種類を0-3に変換して格納
            GenePeopleType[i] = Cushion;

            //PV用
            GenePeopleType[0] = 0;
            GenePeopleType[1] = 1;
            GenePeopleType[2] = 2;
            GenePeopleType[3] = 3;


            //噂話
            if (i < UseGossip)
            {
                GenePeopleType[i] = 0;
            }

            //祈り
            if(i < usePray)
            {
                CrimeRate -= 25.0;
                if(CrimeRate < 0)
                {
                    CrimeRate = 0.0;
                }
            }


            //暗殺
            if (ExeKill)
            {
                //暗殺対象は再抽選
                if (GenePeopleType[i] == killNumber)
                {
                    GenePeopleType[i] += 3;
                    if (GenePeopleType[i] > 3)
                    {
                        GenePeopleType[i] -= 4;
                    }
                }

            }
            //支払処理

            if (!UseStock)//株未使用
            {
                switch (GenePeopleType[i])
                {
                    //貧民
                    case 0:
                        ReceiveMoney[i] = SelectItem * Poor;
                        //貧民所持金処理
                        PoorMoney -= ReceiveMoney[i];
                        if (PoorMoney < 0)
                        {
                            //お金をリセットし奴隷追加
                            PoorMoney = HaveMoney * 0.3f;
                            PoorDebt = true;
                            Debug.Log("ひんみん");
                            Slave++;
                        }
                        break;
                    //市民
                    case 1:
                        ReceiveMoney[i] = SelectItem * General;
                        break;
                    //富豪
                    case 2:
                        ReceiveMoney[i] = SelectItem * Millionaire;
                        break;
                    //貴族
                    case 3:
                        ReceiveMoney[i] = SelectItem * Noble;
                        break;
                }

                TotalReceiveMoney += (int)ReceiveMoney[i];

            }
            else //株使用
            {
                switch (GenePeopleType[i])
                {
                    //貧民
                    case 0:
                        StockCrash++;
                        ReceiveMoney[i] = SelectItem * Poor;
                        //貧民所持金処理
                        PoorMoney -= ReceiveMoney[i];
                        if (PoorMoney < 0)
                        {
                            //お金をリセットし奴隷追加
                            PoorMoney = HaveMoney * 0.3f;
                            PoorDebt = true;
                            Debug.Log("ひんみん");
                            Slave++;
                        }
                        break;
                    //市民
                    case 1:
                        StockCrash++;
                        ReceiveMoney[i] = SelectItem * General;
                        break;
                    //富豪
                    case 2:
                        StockEnlarge += 0.2;
                        ReceiveMoney[i] = SelectItem * Millionaire;
                        break;
                    //貴族
                    case 3:
                        StockEnlarge += 0.2;
                        ReceiveMoney[i] = SelectItem * Noble;
                        break;
                }
                if (StockCrash >= 2)
                {
                    // 配列をクリア
                    for (int n = 0; n < GenePeopleCount; n++)
                    {
                        ReceiveMoney[n] = 0.0;
                    }
                    TotalReceiveMoney = 0;
                }
                else
                {
                    TotalReceiveMoney += (int)ReceiveMoney[i];
                }

            }
        }

        if(ExeKill)
        {
            CrimeRate += 50.0;
        }

        //金額を所持金に追加
        HaveMoney += (int)TotalReceiveMoney;
        //支出計算を実行
        ExpenseCalc();
    }

    /*------------------------------------------------------------*/


    //支出計算処理

    void ExpenseCalc()
    {
        //得たお金を天引き計算用代入
        TurnEndHaveMoney = HaveMoney;

        Convert = HaveMoney * RandTax;
        RandTaxPay = (int)Convert;

        Convert = HaveMoney * HaveTax;
        HaveTaxPay = (int)Convert;

        Convert = HaveMoney * 0.3;
        StealPeoplePay = (int)Convert;

        //従業員の給料
        if (TurnCount > 1)
        {
            //配列に追加する
            Paycheck[TurnCount] = Paycheck[TurnCount - 2] + Paycheck[TurnCount - 1];
        }
        //セーブ且つ

        //土地代、所得税
        TotalPayment += RandTaxPay;
        TotalPayment += HaveTaxPay;

         //盗賊判定
        if (StealTaxjuge)
        {
            TotalPayment += StealPeoplePay;
        }
        //給料
        TotalPayment += Paycheck[TurnCount];

        //天引き
        TurnEndHaveMoney -= TotalPayment;
        Debug.Log("" + TurnEndHaveMoney);

        //所持金にセット
        HaveMoney = TurnEndHaveMoney;

        //クリア判定
        EventsJuge();
    }

    void EventsJuge()
    {
        //クリア処理
        if (HaveMoney > TargetAmount)
        {
            GameClear = true;
            ClearMethod();
            PopTurnEvent = TurnCount + 1;
        }
        
        //ゲームオーバー処理
        if (HaveMoney < 0)
        {
            GameOver = true;
            //SaveControl.instanceSave.ClearDateSave();
            PopTurnEvent = TurnCount + 1;
        }
   
    }


    void ClearMethod()
    {

        //resultを変数に代入
        if (ResultScore[0] == 0)
        {
            ResultScore[0] = TurnCount;
        }
        else if (ResultScore[1] == 0)
        {
            ResultScore[1] = TurnCount;
        }
        else if (ResultScore[2] == 0)
        {
            ResultScore[2] = TurnCount;
        }
        else
        {
            ResultScore[2] = TurnCount;
        }

        //0番目と1番目を比較
        if (ResultScore[0] < ResultScore[1])
        {
            swap(ResultScore[0], ResultScore[1]);
        }

        //0番目と2番目を比較
        if (ResultScore[0] < ResultScore[2])
        {
            swap(ResultScore[0], ResultScore[2]);
        }

        //1番目と2番目を比較
        if (ResultScore[1] < ResultScore[2])
        {
            swap(ResultScore[1], ResultScore[2]);
        }

        ResultScore[2] = TurnCount - 1;

        //実際のリザルトテキスト入れ替え
        for(int i = 0; i < 3; i++ )
        {
            if (ResultScore[i] == 0)
            {
                ResultScoreStr[i] = "     No Date.";
            }
            else
            {
                ResultScoreStr[i] = i+1 + " - Clear: " + ResultScore[i] + " days.";
            }

            OutPutResult = ResultScoreStr[0] + "\n" + ResultScoreStr[1] + "\n" + ResultScoreStr[2];
        }


    }

    //スコア並べ替え用関数
    void swap(int a, int b)
    {   
        int temp;
        temp = a;
        a = b;
        b = temp;
        return;
    }
}
