using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//計算処理管理用
public class ParameterCalc : MonoBehaviour
{
    //他スクリプトでも呼べるようにインスタンス化
    public static ParameterCalc instanceCalc;
    //セーブ管理用
    [SerializeField] 
    private SaveControl saveControl; 
    
    /* パラメータ宣言 */

    //初プレイ管理
    public bool InitialPlay;

    //ターンカウント
    public int TurnCount = 0;
    //イベント施行タイミング用変数
    public int PopTurnEvent;

    //お金周り
    public int TargetAmount;
    public bool GameClear = false;
    public bool GameOver = false;
    public int HaveMoney;
    public float SelectItem = 0.0f;

    //民衆倍率
    public float Poor;
    public bool PoorDebt; //貧民破産判定
    public float General;
    public float Millionaire;
    public float Noble;

    //イベント管理
    public float CrimeRate; //犯罪率
    public float TodayCrime; //今日の犯罪率
    public float[] PoorMoney = new float[100]; //貧民所持金
    public float nowPoorMoney;
    public int PoorCount = 0;
    public int RebellionGeneralCount; //反乱する市民のカウント
    public bool DoRebellionGeneral;
    public int RebellionEarnedMoney; //反乱時の獲得金額
    public int Slave; //奴隷数
    public int TodaySlave; //ログシステム用カウント
    public int ToolType; //どの商品を選んだか

    //銅の剣
    public float[] BrSwordSell = new float[100];
    public float[] BrSwordUp = new float[100];
    public int BrSwordUpCount; //強化回数_強化のメソッドに追加する

    //麻薬
    public int PotionGet;
    public float[] PotionSell = new float[100];
    public float[] PotionUp = new float[100];
    public int PotionUpCount; //強化回数
    public bool HavePotionJ; //所持チェック用
    public float TodayPotionCrime;

    //株
    public int StockOpen;
    public int StockGet;
    public int StockSell;
    public bool HaveStockJ; //所持チェック用
    public int StockQuantity; //株所有数
    public int StockReceived; //入荷用
    private bool UseStock;
    public float StockOutLogSystem;

    //戦略用変数
    private int useGossip;
    public bool ExeKill;
    public int killNumber;
    public int SelectKillPanel;  //キル画面のUI操作用
    public int SelectRepleItem;  //アイテム強化対象選択
    private int usePray; 
    public int TodayPrayValue;
    //交渉
    public int PubliWay;
    public int PubliWayPay;
    public float[] PubliRisk = new float[] { 0.2f, 0.5f, 0.8f };
    public bool usePubli;
    public bool PubliSuccess;   //交渉が成功したか否か

    /* メイン計算用変数 */
    public int GenePeopleCount; //民衆生成数
    public int[] GenePeopleType = new int[5]; //民衆の種類
    public float[] ReceiveMoney = new float[5]; //受け取った金額
    public int TotalReceiveMoney;

    /* 金額天引き用変数 */
    public int[] Paycheck = new int[100];
    public int TaxCount; //貴族がくると税率が上昇
    public float TaxRate;
    public int HaveTaxPay; //天引き金額
    public int StealPeoplePay;
    public bool StealTaxjuge;
    private int turnEndHaveMoney;
    public int TotalPayment;
    public bool FineMoney;
    public int FineMoneyInt;

    //Scoreテキスト
    public int[] ResultScore = new int[] {0,0,0};
    public string[] ResultScoreStr = new string[3];
    public string OutPutResult;

    public enum SellItem {
        BronzeSword,
        HighPotion,
        RareTurnips,
        StockHighPotion,
        StockRareTurnips
    }

    public void Awake()
    {
        //初見プレイ初期化
        InitialPlay = false;
        
        if (instanceCalc == null)
        {
            instanceCalc = this;
        }

        saveControl = GameObject.Find("SaveManager").GetComponent<SaveControl> ();
        
        //初期化チェック
        saveControl.LoadNewGame();
        
        if (!saveControl.NewGame)
        {
            //データのロード
            LoadData();
            //イベント判定計算処理
            EventsJuge();
        }
        else
        {
            //初プレイは初期値をロード
            InitialPlay = true;
            saveControl.FirstLunch();
        }

        //ターン追加UI制御の都合でAwakeで処理
        TurnCount++;
    }

    void Start()
    {
        //戦略用変数リセット
        useGossip = 0;
        ExeKill = false;
        SelectKillPanel = 0;
        SelectRepleItem = 0;
        UseStock = false;
        StockReceived = 30;
        usePubli = false;
        PubliWay = 1;
        usePray = 0;
        TodayPrayValue = 0;
        PoorDebt = false;
        DoRebellionGeneral = false;

        //天引き計算用
        StealTaxjuge = false;
        TotalPayment = 0;
        FineMoney = false;

        //商品選択
        ToolType = 0;
    }

    //データをロード
    private void LoadData()
    {
        saveControl.Doload();
    }

    /*-------戦略メソッド-------*/

    //噂話
    public void StrGossip()
    {
        useGossip++;
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
            if(killNumber == 3)TaxCount = 0;
        }
    }

    //交渉
    public void PubliCalc()
    {
        Slave -= 1;
        HaveMoney -= PubliWayPay;
        usePubli = true;
    }

    //商品強化
    public void StrReple()
    {
        SellItem _sellItem = (SellItem)SelectRepleItem;

        switch (_sellItem)
        {
            case SellItem.BronzeSword : //銅の剣
                HaveMoney -= (int)BrSwordUp[BrSwordUpCount];
                BrSwordUpCount += 1;
                break;

            case SellItem.HighPotion: //ハイポーション
                HaveMoney -= (int)PotionUp[PotionUpCount];
                PotionUpCount += 1;
                break;

            case SellItem.RareTurnips: //株
                HaveMoney -= StockGet * StockReceived;
                StockQuantity += StockReceived;
                break;

            case SellItem.StockHighPotion: //薬入荷
                HaveMoney -= PotionGet;
                PotionUpCount += 1;
                HavePotionJ = true;
                break;

            case SellItem.StockRareTurnips: //株入荷
                HaveMoney -= StockOpen;
                HaveStockJ = true;
                break;
        }
    }

    //祈る
    public void StrPray()
    {
        usePray += 1;
    }

    /*---------収入計算処理---------*/

    public void GeneCalc()
    {
        //初期化
        TodayCrime = 0.0f;
        TotalReceiveMoney = 0;
        TodaySlave = 0;
        float stockEnlarge = 1.0f; //株の倍率管理
        int stockCrash = 0; //価格暴落
        bool sellBrswords = false;

        //商品をセット
        switch (ToolType)
        {
            case 0: //剣
                SelectItem = BrSwordSell[BrSwordUpCount];
                sellBrswords = true;
                break;

            case 1: //薬
                SelectItem = PotionSell[PotionUpCount];
                break;

            case 2: //株
                SelectItem = StockSell * StockQuantity;
                StockQuantity = 0;
                UseStock = true;
                break;
        }

        //交渉
        if (usePubli)
        {
            //犯罪度上昇
            TodayCrime += 10.0f; 
            //抽選用乱数生成
            int publiI = Random.Range(1, 101);
            //交渉
            if (publiI < PubliRisk[PubliWay] * 100)    //publi...0-20%,1-50%,2-80%
            {
                PubliSuccess = true;    //交渉成功
                GenePeopleCount = 4;
            }
            else
            {
                PubliSuccess = false;   //交渉失敗
            }
        }
        else
        {
            //1以上6未満
            GenePeopleCount = Random.Range(0, 5);   //何人生成するか
        }

        //暗殺の犯罪率
        if(ExeKill)
        {
            TodayCrime += 50.0f;
        }

        //麻薬の犯罪率処理
        if(ToolType == 1)
        {
            const float potionCrime = 10.0f;
            TodayPotionCrime = potionCrime * GenePeopleCount + 1;
            TodayCrime += TodayPotionCrime; 
        }

        int Cushion; //民衆選定用クッション

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

            //噂話
            if (i < useGossip)
            {
                GenePeopleType[i] = 0;
            }

            //祈り
            if(i < usePray)
            {
                const float PlayValue = 25.0f;
                if(TodayCrime > 0)
                {
                    TodayCrime -= PlayValue;
                    TodayPrayValue += (int)PlayValue;
                }
                else if(CrimeRate > 0)
                {
                    CrimeRate -= PlayValue;
                    TodayPrayValue += (int)PlayValue;
                }
    
                if(TodayCrime < 0)
                {
                    TodayCrime = 0.0f;
                    TodayPrayValue -= (int)PlayValue;
                }
    
                if(CrimeRate < 0)
                {
                    CrimeRate = 0.0f;
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
                        PoorMoney[PoorCount] -= ReceiveMoney[i];
                        if (PoorMoney[PoorCount] <= 0)
                        {
                            //お金をリセットし奴隷追加
                            PoorMoneyCalc();
                        }
                        break;
                    //市民
                    case 1:
                        ReceiveMoney[i] = SelectItem * General;
                        //銅の剣を売っていた場合反乱ポイントを加算
                        if(sellBrswords)
                        {
                            RebellionGeneralCount++;
                        }
                        break;
                    //富豪
                    case 2:
                        ReceiveMoney[i] = SelectItem * Millionaire;
                        break;
                    //貴族
                    case 3:
                        ReceiveMoney[i] = SelectItem * Noble;
                        TaxCount++; //行商税率加算
                        break;
                }
            }
            else //株使用
            {
                const float StockEnlargeAmount = 0.1f;
                switch (GenePeopleType[i])
                {
                    //貧民
                    case 0:
                        stockCrash++;
                        ReceiveMoney[i] = SelectItem * Poor;
                        //貧民所持金処理
                        PoorMoney[PoorCount] -= ReceiveMoney[i];
                        if (PoorMoney[PoorCount] <= 0)
                        {
                            //お金をリセットし奴隷追加
                            PoorMoneyCalc();
                        }
                        break;
                    //市民
                    case 1:
                        stockCrash++;
                        ReceiveMoney[i] = SelectItem * General;
                        break;
                    //富豪
                    case 2:
                        stockEnlarge += StockEnlargeAmount;
                        ReceiveMoney[i] = SelectItem * Millionaire;
                        break;
                    //貴族
                    case 3:
                        stockEnlarge += StockEnlargeAmount;
                        ReceiveMoney[i] = SelectItem * Noble;
                        break;
                }
            }
        }
        
        //プラスなら価格崩壊
        int stockCrashCalc = GenePeopleCount + 1 - stockCrash;

        //株の崩壊処理
        if (UseStock)
        {
            if (stockCrashCalc <= stockCrash)stockEnlarge = 0;
            StockOutLogSystem = StockSell * stockEnlarge;
        }
        //お金の合算処理
        for (int i = 0; i <= GenePeopleCount; ++i)
        {
            if(UseStock) //株の倍率反映
            {
                ReceiveMoney[i] = ReceiveMoney[i] * stockEnlarge;
            }
            TotalReceiveMoney += (int)ReceiveMoney[i];
        }

        // 市民の反乱処理
        const int consumptionCount = 10;
        if(RebellionGeneralCount >= consumptionCount)
        {
            RebellionGeneralCount -= consumptionCount;
            const float earnedEnf = 0.5f;
            float earnedMoney = HaveMoney * earnedEnf;
            RebellionEarnedMoney = (int)earnedMoney;
            TotalReceiveMoney += RebellionEarnedMoney;
            DoRebellionGeneral = true;
        }

        //貧民のお金保持用処理
        nowPoorMoney = PoorMoney[PoorCount];
        //犯罪率を加算
        CrimeRate += TodayCrime;
        //金額を所持金に追加
        HaveMoney += TotalReceiveMoney;
        //支出計算を実行
        ExpenseCalc();
    }

    /*---------支出計算処理---------*/

    void ExpenseCalc()
    {
        //得たお金を天引き計算用代入
        turnEndHaveMoney = HaveMoney;

        //罰金
        if(CrimeRate >= 100)
        {
            CrimeRate -= 100.0f;
            float tmp = (float)HaveMoney * 0.7f;
            FineMoneyInt = (int)tmp;
            TotalPayment += FineMoneyInt;
            FineMoney = true;
        }

        //行商税
        float haveTax = 0.05f; //行商税倍率
        int tmpTax = TaxCount / 3 + 1; //貴族３人につき行商税の倍率が上がる
        if(tmpTax > 8) tmpTax = 8;
        TaxRate = haveTax * tmpTax;
        float Convert = HaveMoney * TaxRate;
        HaveTaxPay = (int)Convert;
        TotalPayment += HaveTaxPay;

        //従業員の給料
        TotalPayment += Paycheck[TurnCount];

        //盗賊処理
        int exeSteal = Random.Range(1, 101); //5%で実行
        if(exeSteal == 1) StealTaxjuge = true;

        if (StealTaxjuge)
        {
            float tmpSteal = HaveMoney * 0.5f;;
            StealPeoplePay =  (int)tmpSteal;
            TotalPayment += StealPeoplePay;
        }

        //天引き
        turnEndHaveMoney -= TotalPayment;

        //所持金にセット
        HaveMoney = turnEndHaveMoney;
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
            bool noDateJude = false;
            if(ResultScore[0] == 0)noDateJude = true;
            swap(ResultScore[0], ResultScore[1]);
            if(noDateJude)ResultScore[1] = 0;
        }

        //0番目と2番目を比較
        if (ResultScore[0] < ResultScore[2])
        {
            bool noDateJude = false;
            if(ResultScore[0] == 0)noDateJude = true;
            swap(ResultScore[0], ResultScore[2]);
            if(noDateJude)ResultScore[2] = 0;
        }

        //1番目と2番目を比較
        if (ResultScore[1] < ResultScore[2])
        {
            bool noDateJude = false;
            if(ResultScore[1] == 0)noDateJude = true;
            swap(ResultScore[1], ResultScore[2]);
            if(noDateJude)ResultScore[2] = 0;
        }

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
        }
        OutPutResult = ResultScoreStr[0] + "\n" + ResultScoreStr[1] + "\n" + ResultScoreStr[2];
    }
    //貧民の所持金処理
    void PoorMoneyCalc()
    {
        PoorCount += 1;
        if(PoorCount > 99)PoorCount = 99;
        PoorDebt = true;
        Slave++;
        TodaySlave ++;
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
