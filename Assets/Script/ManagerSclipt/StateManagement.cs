using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

//設定の状態管理
public class StateManagement : MonoBehaviour
{
    //設定済みか否か
    private bool firstSettings;
    //画面サイズの設定
    public string WindowMode = "FullScreen";
    //音量の設定
    public float SetvolumeBGM;
    public float SetvolumeSE;
    //言語の設定
    public string UseLanguage = "Japanese";
    
    void Awake()
    {
        // データの保存先をApplication.dataPathに変更
        QuickSaveGlobalSettings.StorageLocation = Application.dataPath;

        //暗号化処理
        DoEncryption();

        //設定の読み込み
        ReadSettings();
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
    public void ReadSettings()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings gameSettings = new QuickSaveSettings();
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader setReader = QuickSaveReader.Create("Player", gameSettings);

        //設定済み判定
        firstSettings = setReader.Read<bool>("firstSettings");

        if(!firstSettings)   //設定済みの場合
        {
            WindowMode = setReader.Read<string>("WindowMode");
            SetvolumeBGM = setReader.Read<float>("VolumeBGM");
            SetvolumeSE = setReader.Read<float>("VolumeSE");
            UseLanguage = setReader.Read<string>("LanguageMode");
        }
        else    //未設定の場合
        {
            WindowMode = "FullScreen";
            SetvolumeBGM = 0.25f;
            SetvolumeSE = 0.25f;
            UseLanguage = "Japanese";
        }
    }

    //設定書き込み
    public void WriteSettings()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings gameSettings = new QuickSaveSettings();    
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter setWriter = QuickSaveWriter.Create("Player",gameSettings);
        
        //データの書き込み判定
        setWriter.Write("firstSettings", false);

        //ウィンドウモード設定
        setWriter.Write("WindowMode",WindowMode);
        //音量設定
        setWriter.Write("VolumeBGM",SetvolumeBGM);
        setWriter.Write("VolumeSE",SetvolumeSE);
        //言語の設定
        setWriter.Write("LanguageMode",UseLanguage);
        // 変更を反映
        setWriter.Commit();
    }

    //設定の初期化
    public void ResetSettings()
    {
        // QuickSaveSettingsのインスタンスを作成
        QuickSaveSettings gameSettings = new QuickSaveSettings();    
        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter setWriter = QuickSaveWriter.Create("Player",gameSettings);
        
        WindowMode = "FullScreen";
        SetvolumeBGM = 0.25f;
        SetvolumeSE = 0.25f;
        UseLanguage = "Japanese";
        //データの書き込み判定
        setWriter.Write("firstSettings", true);

        //ウィンドウモード設定
        setWriter.Write("FullScreen",WindowMode);
        //音量設定
        setWriter.Write("VolumeBGM",SetvolumeBGM);
        setWriter.Write("VolumeSE",SetvolumeSE);
        //言語設定
        setWriter.Write("LanguageMode",UseLanguage);
        // 変更を反映
        setWriter.Commit();
    }
}
