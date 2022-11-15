using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class StateManagement : MonoBehaviour
{

    //設定済みか否か
    private bool firstSettings;

    //画面サイズの設定
    public string windowMode = "FullScreen";
    //音量の設定
    public float SetvolumeBGM;
    public float SetvolumeSE;
    

    //初起動かチェック
    void Awake()
    {
        // データの保存先をApplication.dataPathに変更
        QuickSaveGlobalSettings.StorageLocation = Application.dataPath;

        //暗号化処理
        DoEncryption();

        //設定の読み込み
        readSettings();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void DoEncryption()//暗号化処理
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings gameSettings = new QuickSaveSettings();
        // 暗号化の方法
        gameSettings.SecurityMode = SecurityMode.None;
        // 暗号化キー
        gameSettings.Password = "Pass";
        // 圧縮の方法
        gameSettings.CompressionMode = CompressionMode.Gzip;
    }

    //設定読み込み
    public void readSettings()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings gameSettings = new QuickSaveSettings();
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader setReader = QuickSaveReader.Create("Player", gameSettings);

        //設定済み判定
        firstSettings = setReader.Read<bool>("firstSettings");

        if(!firstSettings)   //設定済みの場合
        {
            windowMode = setReader.Read<string>("WindowMode");
            SetvolumeBGM = setReader.Read<float>("VolumeBGM");
            SetvolumeSE = setReader.Read<float>("VolumeSE");
            
        }
        else    //未設定の場合
        {
            windowMode = "FullScreen";
            SetvolumeBGM = 0.5f;
            SetvolumeSE = 0.5f;
            
        }
    }

    //設定書き込み
    public void writeSettings()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings gameSettings = new QuickSaveSettings();    
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter setWriter = QuickSaveWriter.Create("Player",gameSettings);
        
        Debug.Log(SetvolumeBGM);
        //データの書き込み判定
        setWriter.Write("firstSettings", false);

        //ウィンドウモード設定
        setWriter.Write("WindowMode",windowMode);
        //音量設定
        setWriter.Write("VolumeBGM",SetvolumeBGM);
        setWriter.Write("VolumeSE",SetvolumeSE);
        // 変更を反映
        setWriter.Commit();
    }

    //設定の初期化
    public void resetSettings()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings gameSettings = new QuickSaveSettings();    
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter setWriter = QuickSaveWriter.Create("Player",gameSettings);
        
        windowMode = "FullScreen";
        SetvolumeBGM = 0.5f;
        SetvolumeSE = 0.5f;
        Debug.Log("a");
        //データの書き込み判定
        setWriter.Write("firstSettings", true);

        //ウィンドウモード設定
        setWriter.Write("FullScreen",windowMode);
        //音量設定
        setWriter.Write("VolumeBGM",SetvolumeBGM);
        setWriter.Write("VolumeSE",SetvolumeSE);
        // 変更を反映
        setWriter.Commit();

        
    }
}
