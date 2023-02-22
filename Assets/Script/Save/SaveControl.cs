using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

//ゲームデータのセーブ管理
public class SaveControl : MonoBehaviour
{
    public bool NewGame;

    void Start()
    {
        // データの保存先をApplication.dataPathに変更
        QuickSaveGlobalSettings.StorageLocation = Application.dataPath;

        DoEncryption();
        CheckSave();
    }

    private void DoEncryption()//暗号化処理
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings settings = new QuickSaveSettings();
        // 暗号化の方法
        settings.SecurityMode = SecurityMode.None;
        // 暗号化キー
        settings.Password = "Pass";
        // 圧縮の方法
        settings.CompressionMode = CompressionMode.Gzip;
    }
    
    //セーブデータチェック
    void CheckSave()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("Player", settings);

        // 新規ゲーム判定の読み込み
        NewGame = reader.Read<bool>("newGame");
    }

    //基本セーブ処理
    public void Dosave()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Player",settings);

        // データを書き込む
        writer.Write("newGame", NewGame);
        writer.Write("turnCount", ParameterCalc.instanceCalc.TurnCount);
        writer.Write("popTurnEvent", ParameterCalc.instanceCalc.PopTurnEvent);
        writer.Write("targetAmount", ParameterCalc.instanceCalc.TargetAmount);
        writer.Write("haveMoney", ParameterCalc.instanceCalc.HaveMoney);
        writer.Write("poor", ParameterCalc.instanceCalc.Poor);
        writer.Write("general", ParameterCalc.instanceCalc.General);
        writer.Write("millionaire", ParameterCalc.instanceCalc.Millionaire);
        writer.Write("noble", ParameterCalc.instanceCalc.Noble);

        writer.Write("crimeRate", ParameterCalc.instanceCalc.CrimeRate);
        writer.Write("slave", ParameterCalc.instanceCalc.Slave);
        writer.Write("poorMoney", ParameterCalc.instanceCalc.PoorMoney);

        writer.Write("brSwordUpCount", ParameterCalc.instanceCalc.BrSwordUpCount);

        writer.Write("potionGet", ParameterCalc.instanceCalc.PotionGet);
        writer.Write("potionUpCount", ParameterCalc.instanceCalc.PotionUpCount);
        writer.Write("havePotionJ", ParameterCalc.instanceCalc.HavePotionJ);

        writer.Write("stockGet", ParameterCalc.instanceCalc.StockGet);
        writer.Write("stockSell", ParameterCalc.instanceCalc.StockSell);
        writer.Write("stockOpen", ParameterCalc.instanceCalc.StockOpen);
        writer.Write("haveStockJ", ParameterCalc.instanceCalc.HaveStockJ);
        writer.Write("stockQuantity", ParameterCalc.instanceCalc.StockQuantity);

        writer.Write("resultScore0", ParameterCalc.instanceCalc.ResultScore[0]);
        writer.Write("resultScore1", ParameterCalc.instanceCalc.ResultScore[1]);
        writer.Write("resultScore2", ParameterCalc.instanceCalc.ResultScore[2]);

        writer.Write("taxCount",ParameterCalc.instanceCalc.TaxCount);

        // 変更を反映
        writer.Commit();
    }

    // データを読み込む
    public void Doload()
    {        
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("Player", settings);
        
        ParameterCalc.instanceCalc.TurnCount = reader.Read<int>("turnCount");
        ParameterCalc.instanceCalc.PopTurnEvent = reader.Read<int>("popTurnEvent");
        ParameterCalc.instanceCalc.TargetAmount = reader.Read<int>("targetAmount");
        ParameterCalc.instanceCalc.HaveMoney = reader.Read<int>("haveMoney");
        ParameterCalc.instanceCalc.Poor = reader.Read<float>("poor");
        ParameterCalc.instanceCalc.General = reader.Read<float>("general");
        ParameterCalc.instanceCalc.Millionaire = reader.Read<float>("millionaire");
        ParameterCalc.instanceCalc.Noble = reader.Read<float>("noble");
        ParameterCalc.instanceCalc.CrimeRate = reader.Read<float>("crimeRate");
        ParameterCalc.instanceCalc.Slave = reader.Read<int>("slave");
        ParameterCalc.instanceCalc.PoorMoney = reader.Read<float>("poorMoney");

        ParameterCalc.instanceCalc.BrSwordUpCount = reader.Read<int>("brSwordUpCount");

        ParameterCalc.instanceCalc.PotionGet = reader.Read<int>("potionGet");
        ParameterCalc.instanceCalc.PotionUpCount = reader.Read<int>("potionUpCount");
        ParameterCalc.instanceCalc.HavePotionJ = reader.Read<bool>("havePotionJ");

        ParameterCalc.instanceCalc.StockGet = reader.Read<int>("stockGet");
        ParameterCalc.instanceCalc.StockSell = reader.Read<int>("stockSell");
        ParameterCalc.instanceCalc.StockOpen = reader.Read<int>("stockOpen");
        ParameterCalc.instanceCalc.HaveStockJ = reader.Read<bool>("haveStockJ");
        ParameterCalc.instanceCalc.StockQuantity = reader.Read<int>("stockQuantity");

        ParameterCalc.instanceCalc.ResultScore[0] = reader.Read<int>("resultScore0");
        ParameterCalc.instanceCalc.ResultScore[1] = reader.Read<int>("resultScore1");
        ParameterCalc.instanceCalc.ResultScore[2] = reader.Read<int>("resultScore2");

        ParameterCalc.instanceCalc.TaxCount = reader.Read<int>("taxCount");

        //商品の価格を初期値から計算しセット
        int i = 0;
        int[] brSwordSell = new int[100];
        int[] brSwordUp = new int[100];
        int[] potionSell = new int[100];
        int[] potionUp = new int[100];

        while (i < 100)
        {
            if(i < 2)
            {
                brSwordSell[i] = reader.Read<int>("brSwordSell");
                brSwordUp[i] = reader.Read<int>("brSwordUp");
                potionSell[i] = reader.Read<int>("potionSell");
                potionUp[i] = reader.Read<int>("potionUp");
            }
            else
            {
                brSwordSell[i] = brSwordSell[i-1] + brSwordSell[i-2];
                brSwordUp[i] = brSwordUp[i-1] + brSwordUp[i-2];
                potionSell[i] = potionSell[i-1] + potionSell[i-2];
                potionUp[i] = potionUp[i-1] + potionUp[i-2];
            }
            //計算結果の格納
            ParameterCalc.instanceCalc.BrSwordSell[i] = brSwordSell[i];
            ParameterCalc.instanceCalc.BrSwordUp[i] = brSwordUp[i];
            ParameterCalc.instanceCalc.PotionSell[i] = potionSell[i];
            ParameterCalc.instanceCalc.PotionUp[i] = potionUp[i];
            i++;
        }

        //子分の給料を初期値から計算しセット
        i = 0;
        int[] payCheck = new int[100];
        while (i < 100)
        {
            if(i < 1)
            {
                payCheck[i] = reader.Read<int>("payCheck");
            }
            else
            {
                float tmpCalc = (float)payCheck[i - 1] * 1.2f + 50.0f * i;
                payCheck[i] = (int)tmpCalc;
            }
            ParameterCalc.instanceCalc.Paycheck[i] = payCheck[i];
            i++;
        }
    }

    //データ削除
    public void DeleteDate()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Player", settings);

        // ニューゲーム
        NewGame = true;
        writer.Write("newGame", NewGame);
        //リザルト削除処理
        writer.Write("resultScore0", 0);
        writer.Write("resultScore1", 0);
        writer.Write("resultScore2", 0);

        // データを書き込む
        writer.Commit();
    }
    
    //初期化確認読み込み処理
    public void LoadNewGame()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("Player", settings);
        NewGame = reader.Read<bool>("newGame");
    }

    //初立ち上げ初期値セット
    public void FirstLunch()
    {
        NewGame = true;
        ClearDateSave();
        Doload();
    }
    
    //クリア後のセーブ処理<初期値セット>
    public void ClearDateSave()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("Player", settings);

        //クリア済みなので新規データはfalse
        writer.Write("newGame", NewGame);

        //初期値をセット
        writer.Write("turnCount", 0);
        writer.Write("popTurnEvent",0);
        writer.Write("targetAmount",100000);
        writer.Write("haveMoney", 4000);

        writer.Write("poor", 0.7);
        writer.Write("general", 1.0);
        writer.Write("millionaire", 1.3);
        writer.Write("noble", 1.6);

        writer.Write("crimeRate", 0.0);
        writer.Write("slave", 0);
        writer.Write("poorMoney", 1000.0);

        writer.Write("brSwordSell", 400);
        writer.Write("brSwordUp", 800);
        writer.Write("brSwordUpCount", 1);

        writer.Write("potionGet", 3000);
        writer.Write("potionSell", 800);
        writer.Write("potionUp", 1000);
        writer.Write("potionUpCount", 0);
        writer.Write("havePotionJ", false);

        writer.Write("stockGet", 100);
        writer.Write("stockSell", 300);
        writer.Write("stockOpen", 5000);
        writer.Write("haveStockJ", false);
        writer.Write("stockQuantity", 0);
        writer.Write("payCheck",100);

        writer.Write("taxCount",0.0);

        //スコアの書き込み
        writer.Write("resultScore0", ParameterCalc.instanceCalc.ResultScore[0]);
        writer.Write("resultScore1", ParameterCalc.instanceCalc.ResultScore[1]);
        writer.Write("resultScore2", ParameterCalc.instanceCalc.ResultScore[2]);

        // 変更を反映
        writer.Commit();
    }

}
