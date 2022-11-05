using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class SaveControl : MonoBehaviour
{
    public static bool NewGame;
    //public static bool LoadJ;
    private bool NewGame1;

    //他のスクリプトからメソッドを呼べるように
    public static SaveControl instanceSave;
    private int i;

    public void Awake()
    {
        if (instanceSave == null)
        {
            instanceSave = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // データの保存先をApplication.dataPathに変更
        QuickSaveGlobalSettings.StorageLocation = Application.dataPath;

        DoEncryption();
        CheckSave();
    }


    void DoEncryption()//暗号化用
    {
        // QuickSaveSettingsのインスタンスを作成-暗号化用
        QuickSaveSettings settings = new QuickSaveSettings();

        // 暗号化の方法 
        settings.SecurityMode = SecurityMode.None;
        // Aesの暗号化キー
        settings.Password = "Pass";
        // 圧縮の方法
        settings.CompressionMode = CompressionMode.Gzip;
    }

    
    //初起動か否かの処理
    void CheckSave()
    {
        // QuickSaveSettingsのインスタンスを作成-暗号化用
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveReaderのインスタンスを作成-load用
        QuickSaveReader reader = QuickSaveReader.Create("Player", settings);

        // データを読み込む


        NewGame = reader.Read<bool>("newGame");
    }


    //ターン終わりに実行
    public void Dosave()
    {
        // QuickSaveSettingsのインスタンスを作成-暗号化用
        QuickSaveSettings settings = new QuickSaveSettings();

        // QuickSaveWriterのインスタンスを作成-save用
        QuickSaveWriter writer = QuickSaveWriter.Create("Player",settings);


        // データを書き込む
        writer.Write("newGame", NewGame);
        writer.Write("turnCount", ParameterCalc.TurnCount);
        writer.Write("haveMoney", ParameterCalc.HaveMoney);
        writer.Write("crimeRate", ParameterCalc.CrimeRate);
        writer.Write("slave", ParameterCalc.Slave);
        writer.Write("poorMoney", ParameterCalc.PoorMoney);

        writer.Write("brSwordSell", ParameterCalc.BrSwordSell);
        writer.Write("brSwordUp", ParameterCalc.BrSwordUp);
        writer.Write("brSwordUpCount", ParameterCalc.BrSwordUpCount);

        writer.Write("potionGet", ParameterCalc.PotionGet);
        writer.Write("potionSell", ParameterCalc.PotionSell);
        writer.Write("potionUp", ParameterCalc.PotionUp);
        writer.Write("potionUpCount", ParameterCalc.PotionUpCount);
        writer.Write("havePotionJ", ParameterCalc.HavePotionJ);

        writer.Write("stockOpen", ParameterCalc.StockOpen);
        writer.Write("haveStockJ", ParameterCalc.HaveStockJ);
        writer.Write("stockQuantity", ParameterCalc.StockQuantity);

        writer.Write("resultScore0", ParameterCalc.ResultScore[0]);
        writer.Write("resultScore1", ParameterCalc.ResultScore[1]);
        writer.Write("resultScore2", ParameterCalc.ResultScore[2]);

        i = 0;
        //給料支払
        while (i <= ParameterCalc.TurnCount)
        {
            writer.Write("payCheck" + i, ParameterCalc.Paycheck[i]);
            i++;
        }
        // 変更を反映
        writer.Commit();
        Debug.Log("書き込み");
    }

    //スタート画面で実行
    public void Doload()
    {
        // QuickSaveSettingsのインスタンスを作成-暗号化用
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveReaderのインスタンスを作成-load用
        QuickSaveReader reader = QuickSaveReader.Create("Player", settings);

        // データを読み込む
       

        ParameterCalc.TurnCount = reader.Read<int>("turnCount");
        ParameterCalc.HaveMoney = reader.Read<int>("haveMoney");
        ParameterCalc.CrimeRate = reader.Read<double>("crimeRate");
        ParameterCalc.Slave = reader.Read<int>("slave");
        ParameterCalc.PoorMoney = reader.Read<double>("poorMoney");

        ParameterCalc.BrSwordSell = reader.Read<int>("brSwordSell");
        ParameterCalc.BrSwordUp = reader.Read<int>("brSwordUp");
        ParameterCalc.BrSwordUpCount = reader.Read<int>("brSwordUpCount");

        ParameterCalc.PotionGet = reader.Read<int>("potionGet");
        ParameterCalc.PotionSell = reader.Read<int>("potionSell");
        ParameterCalc.PotionUp = reader.Read<int>("potionUp");
        ParameterCalc.PotionUpCount = reader.Read<int>("potionUpCount");
        ParameterCalc.HavePotionJ = reader.Read<bool>("havePotionJ");

        ParameterCalc.StockOpen = reader.Read<int>("stockOpen");
        ParameterCalc.HaveStockJ = reader.Read<bool>("haveStockJ");
        ParameterCalc.StockQuantity = reader.Read<int>("stockQuantity");

        ParameterCalc.ResultScore[0] = reader.Read<int>("resultScore0");
        ParameterCalc.ResultScore[1] = reader.Read<int>("resultScore1");
        ParameterCalc.ResultScore[2] = reader.Read<int>("resultScore2");

        i = 0;
        //給料支払
        while (i <= ParameterCalc.TurnCount)
        {
            ParameterCalc.Paycheck[i] = reader.Read<int>("payCheck" + i);
            i++;
        }
        Debug.Log("読み込み");
    }

    //データ削除処理
    public void DeleteDate()
    {
        // QuickSaveSettingsのインスタンスを作成-暗号化用
        QuickSaveSettings settings = new QuickSaveSettings();

        // QuickSaveWriterのインスタンスを作成-save用
        QuickSaveWriter writer = QuickSaveWriter.Create("Player", settings);


        NewGame = true;

        // データを書き込む
        writer.Write("newGame", NewGame);

        //クリアスコアはリセット
        writer.Write("resultScore0", 0);
        writer.Write("resultScore1", 0);
        writer.Write("resultScore2", 0);

        // 変更を反映
        writer.Commit();
    }
    
    //クリアデータ保存処理
    public void ClearDateSave()
    {
        // QuickSaveSettingsのインスタンスを作成-暗号化用
        QuickSaveSettings settings = new QuickSaveSettings();

        // QuickSaveWriterのインスタンスを作成-save用
        QuickSaveWriter writer = QuickSaveWriter.Create("Player", settings);


        //ニューゲームではない
        NewGame = false;
        writer.Write("newGame", NewGame);

        // データを初期に戻して保存
        writer.Write("turnCount", 0);
        writer.Write("haveMoney", 1000);
        writer.Write("crimeRate", 0.0);
        writer.Write("slave", 0);
        writer.Write("poorMoney", 2000.0);

        writer.Write("brSwordSell", 400);
        writer.Write("brSwordUp", 800);
        writer.Write("brSwordUpCount", 1);

        writer.Write("potionGet", 500);
        writer.Write("potionSell", 1000);
        writer.Write("potionUp", 1500);
        writer.Write("potionUpCount", 0);
        writer.Write("havePotionJ", false);

        writer.Write("stockOpen", 10000);
        writer.Write("haveStockJ", false);
        writer.Write("stockQuantity", 0);

        //クリアスコアは保存
        writer.Write("resultScore0", ParameterCalc.ResultScore[0]);
        writer.Write("resultScore1", ParameterCalc.ResultScore[1]);
        writer.Write("resultScore2", ParameterCalc.ResultScore[2]);

        // 変更を反映
        writer.Commit();
        Debug.Log("クリア後");
    }
}
