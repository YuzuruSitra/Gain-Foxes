using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//計算処理管理用
public class ParameterCalc : MonoBehaviour
{
    //他スクリプトでも呼べるようにインスタンス化
    public static ParameterCalc instanceCalc;
    
    /* パラメータ宣言 */

    //初プレイ管理
    public bool initialPlay;

    //ターンカウント
    public int TurnCount = 0;

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
    public float toDayCrime; //今日の犯罪率
    public float PoorMoney; //貧民所持金
    public int Slave; //奴隷数
    public int ToolType; //どの商品を選んだか

    //銅の剣
    public float[] BrSwordSell = new float[100];
    public float[] BrSwordUp = new float[100];
    public int BrSwordUpCount; //強化回数_強化のメソッドに追加する

    //麻薬
    public int PotionGet;
    public float[] PotionSell = new float[100];
    public float[] PotionUp = new float[100];
    private float potionCrime = 7.0f;
    public int PotionUpCount; //強化回数
    public bool HavePotionJ; //所持チェック用

    //株
    public int StockOpen;
    public int StockGet;
    public int StockSell;
    private float StockEnlarge; //倍率管理
    private int StockCrash; //価格暴落
    public bool HaveStockJ; //所持チェック用
    public int StockQuantity; //株所有数
    public int StockReceived; //入荷用
    private bool UseStock;

    //戦略用変数
    private int UseGossip;
    public bool ExeKill;
    private int killNumber;
    public int SelectKillPanel;  //キル画面のUI操作用
    public int SelectRepleItem;  //アイテム強化対象選択
    private int usePray; 
    //交渉
    public int publiWay;
    public int publiWayPay;
    public float[] publiRisk = new float[] { 0.2f, 0.5f, 0.8f };
    public bool usePubli;
    public bool publiSuccess;   //交渉が成功したか否か

    /* メイン計算用変数 */
    public int GenePeopleCount; //民衆生成数
    public int[] GenePeopleType = new int[5]; //民衆の種類
    public float[] ReceiveMoney = new float[5]; //受け取った金額
    public int TotalReceiveMoney;

    /* 金額天引き用変数 */
    public int[] Paycheck = new int[100];
    public int Income;
    public int RandTaxPay; //天引き金額
    private float HaveTax = 0.05f; //行商税
    public int TaxCount; //貴族がくると税率が上昇
    public int HaveTaxPay; //天引き金額
    public int StealPeoplePay;
    public bool StealTaxjuge;
    private int TurnEndHaveMoney;
    public int TotalPayment;
    public bool fineMoney;
    public int fineMoneyInt;

    //Scoreテキスト
    public int[] ResultScore = new int[] {0,0,0};
    public string[] ResultScoreStr = new string[3];
    public string OutPutResult;

    //イベント施行タイミング用変数
    public int PopTurnEvent;

    public void Awake()
    {
        //初見プレイ初期化
        initialPlay = false;
        
        if (instanceCalc == null)
        {
            instanceCalc = this;
        }
    }

    void Start()
    {
        if (!SaveControl.instanceSave.NewGame)
        {
            //データのロード
            LoadData();
            //イベント判定計算処理
            EventsJuge();
        }
        else
        {
            //初プレイは初期値をロード
            initialPlay = true;
            SaveControl.instanceSave.firstLunch();
        }

        //ターン追加
        TurnCount++;

        //戦略用変数リセット
        UseGossip = 0;
        ExeKill = false;
        SelectKillPanel = 0;
        SelectRepleItem = 0;
        UseStock = false;
        StockEnlarge = 1.0f;
        StockCrash = 0;
        StockReceived = 30;
        usePubli = false;
        publiWay = 1;
        usePray = 0;
        PoorDebt = false;

        //天引き計算用
        Income = 0;
        StealTaxjuge = false;
        TotalPayment = 0;
        fineMoney = false;

        //商品選択
        ToolType = 0;
    }

    //データをロード
    private void LoadData()
    {
        SaveControl.instanceSave.Doload();
    }

    /*-------戦略メソッド-------*/

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
            if(killNumber == 3)TaxCount = 0;
        }
    }

    //交渉
    public void publiCalc()
    {
        Slave -= 1;
        HaveMoney -= publiWayPay;
        usePubli = true;
    }

    //商品強化
    public void StrReple()
    {
        switch (SelectRepleItem)
        {
            case 0: //銅の剣
                HaveMoney -= (int)BrSwordUp[BrSwordUpCount];
                BrSwordUpCount += 1;
                break;

            case 1: //ハイポーション
                HaveMoney -= (int)PotionUp[PotionUpCount];
                PotionUpCount += 1;
                break;

            case 2: //株
                HaveMoney -= StockGet * StockReceived;
                StockQuantity += StockReceived;
                break;

            case 3: //薬入荷
                HaveMoney -= PotionGet;
                PotionUpCount += 1;
                HavePotionJ = true;
                break;

            case 4: //株入荷
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
        toDayCrime = 0.0f;
        TotalReceiveMoney = 0;

        //商品をセット
        switch (ToolType)
        {
            case 0: //剣
                SelectItem = BrSwordSell[BrSwordUpCount];
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
            toDayCrime += 10.0f; 
            //抽選用乱数生成
            int publiI = Random.Range(1, 101);
            //交渉
            switch (publiWay)
            {
                case 0:
                    if (publiI < 21)    //20%
                    {
                        publiSuccess = true;    //交渉成功
                        GenePeopleCount = 4;
                    }
                    else
                    {
                        publiSuccess = false;   //交渉失敗
                    }
                    break;

                case 1:
                    if (publiI < 51)    //50%
                    {
                        publiSuccess = true;    //交渉成功
                        GenePeopleCount = 4;
                    }
                    else
                    {
                        publiSuccess = false;   //交渉失敗
                    }
                    break;
                    
                case 2:
                    if (publiI < 81)    //80%
                    {
                        publiSuccess = true;    //交渉成功
                        GenePeopleCount = 4;
                    }
                    else
                    {
                        publiSuccess = false;   //交渉失敗
                    }
                    break;
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
            toDayCrime += 50.0f;
        }

        //麻薬の犯罪率処理
        if(ToolType == 1)
        {
            toDayCrime += potionCrime * GenePeopleCount + 1; 
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
            if (i < UseGossip)
            {
                GenePeopleType[i] = 0;
            }

            //祈り
            if(i < usePray)
            {
                CrimeRate -= 25.0f;
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
                        PoorMoney -= ReceiveMoney[i];
                        if (PoorMoney < 0)
                        {
                            //お金をリセットし奴隷追加
                            PoorMoney = 400 * TurnCount;
                            PoorDebt = true;
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
                        TaxCount++; //行商税率加算
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
                            PoorMoney = 400 * TurnCount;
                            PoorDebt = true;
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
                        StockEnlarge += 0.2f;
                        ReceiveMoney[i] = SelectItem * Millionaire;
                        break;
                    //貴族
                    case 3:
                        StockEnlarge += 0.2f;
                        ReceiveMoney[i] = SelectItem * Noble;
                        break;
                }
                //株の崩壊処理
                if (StockCrash >= 2)
                {
                    // 取得金額のクリア
                    for (int n = 0; n < GenePeopleCount; n++)
                    {
                        ReceiveMoney[n] = 0.0f;
                    }
                    TotalReceiveMoney = 0;
                }
                else
                {
                    TotalReceiveMoney += (int)ReceiveMoney[i];
                }

            }
        }
        //犯罪率を加算
        CrimeRate += toDayCrime;
        //金額を所持金に追加
        HaveMoney += (int)TotalReceiveMoney;
        //支出計算を実行
        ExpenseCalc();
    }

    /*---------支出計算処理---------*/

    void ExpenseCalc()
    {
        //得たお金を天引き計算用代入
        TurnEndHaveMoney = HaveMoney;

        //罰金
        if(CrimeRate >= 100)
        {
            CrimeRate -= 100.0f;
            float tmp = (float)HaveMoney * 0.7f;
            fineMoneyInt = (int)tmp;
            TotalPayment += fineMoneyInt;
            fineMoney = true;
        }

        //行商税
        int tmpTax = TaxCount / 3 + 1; //貴族３人につき行商税の倍率が上がる
        if(tmpTax > 8) tmpTax = 8;
        float Convert = HaveMoney * HaveTax * tmpTax;
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
        TurnEndHaveMoney -= TotalPayment;

        //所持金にセット
        HaveMoney = TurnEndHaveMoney;
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
