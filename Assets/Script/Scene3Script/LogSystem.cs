using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//シーン3ログシステム管理
public class LogSystem : MonoBehaviour
{
	[SerializeField] 
    private Text changeLogTex; //テキストを替えるオブジェクト
    private string[] peopleKindText = new string[5]; //種類ごとに出力するテキストを変更

	[SerializeField] 
    private SoundCnt soundCLog;
	[SerializeField] 
    private AudioClip logFlowSE;
    private bool readNowLog = false;
    private bool _openESC = false;
    //言語用のクラス
    [SerializeField]
    private ChangeLanguageScene3 _languageCnt;
    //ログシステムのコルーチン取得
    private IEnumerator _logSystemIEnumerator;
    private Coroutine _logSystemCoroutine;
    private int _corutineCount;
    private string _logTmp;
    private string languageType;
    private const float WaitSecondsJP = 0.03f;
    private const float WaitSecondsEN = 0.007f;

    void Start()
    {
        /*---bgm設定---*/
        soundCLog = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
        //言語設定用
        _languageCnt = GameObject.Find("LanguageUI_Scene3").GetComponent<ChangeLanguageScene3> ();
        languageType = _languageCnt.LanguageState_Scene3;
        //_logSystemIEnumerator = WriteResult();
        _corutineCount = 0;
    }

    void Update()
    {
        //SE処理用
        DoReadSE();
    }

    public void GenerateLog()
    {
        _logTmp = "";
        //何を売ったか
        switch (ParameterCalc.instanceCalc.ToolType)
        {
            case 0: //剣
                //_logText += " 今 日 は ど う の つ る ぎ を 売 っ た 。\n";
                _logTmp += _languageCnt.Scene3LanguaeData[2];
                break;

            case 1: //薬
                //_logText += " 今 日 は こ う か な く す り を 売 っ た 。 \n";
                _logTmp += _languageCnt.Scene3LanguaeData[3];
                break;

            case 2: //株
                //_logText += " 今 日 は ま ぼ ろ し の か ぶ を 売 っ た 。 \n";
                _logTmp += _languageCnt.Scene3LanguaeData[4];
                int todayStockPrice = (int)ParameterCalc.instanceCalc.StockOutLogSystem;
                if(todayStockPrice == 0)
                {
                    //参入した時期が遅かったようだ、相場が暴落してしまった。
                    _logTmp += _languageCnt.Scene3LanguaeData[44];
                }
                else
                {
                    //貴族が富豪が多いようだ、かぶの相場が０００zになった。
                    _logTmp += _languageCnt.Scene3LanguaeData[45] + todayStockPrice + _languageCnt.Scene3LanguaeData[46];
                }
                break;
        }

        if (ParameterCalc.instanceCalc.usePubli)
        {
            if(ParameterCalc.instanceCalc.PubliSuccess)
            {
                //_logText += " 業 者 は 成 功 し た よ う だ 。 お 客 さ ん が 普 段 よ り 多 く 来 店 し た 。 \n";
                _logTmp += _languageCnt.Scene3LanguaeData[6];
            }
            else
            {
                //_logText += " 業 者 は 失 敗 し た よ う だ 。 お 客 さ ん が 普 段 よ り 多 く 来 店 し た 。  \n";
                _logTmp += _languageCnt.Scene3LanguaeData[7];
            }
        }

        if (ParameterCalc.instanceCalc.ExeKill)
        {
            int killPeople = ParameterCalc.instanceCalc.killNumber;
            switch(killPeople)
            {
                case 0:
                    //_logText += " 貧 民 の 暗 殺 に 成 功 し た 。 \n";
                    //_logText += " 普 段 よ り 貴 族 が 少 し 多 く 来 店 し た よ う だ 。 \n";
                    _logTmp += _languageCnt.Scene3LanguaeData[8];
                    break;
                case 1:
                    //_logText += " 市 民 の 暗 殺 に 成 功 し た 。 \n";
                    _logTmp += _languageCnt.Scene3LanguaeData[9];
                    break;
                case 2:
                    //_logText += " 富 豪 の 暗 殺 に 成 功 し た 。 \n";
                    _logTmp += _languageCnt.Scene3LanguaeData[10];
                    break;
                case 3:
                    //_logText += " 貴 族 の 暗 殺 に 成 功 し た 。 \n";
                    //_logText += " 行 商 税 の 倍 率 が 下 が っ た 。 \n";
                    _logTmp += _languageCnt.Scene3LanguaeData[11];
                    _logTmp += _languageCnt.Scene3LanguaeData[12];
                    break;
            }
        }

        //来店
        for (int i = 0; i <= ParameterCalc.instanceCalc.GenePeopleCount; i++ )
        {
            int[] tmpReceveMoneyInt = new int[5];
            tmpReceveMoneyInt[i] =  (int)ParameterCalc.instanceCalc.ReceiveMoney[i];
            switch (ParameterCalc.instanceCalc.GenePeopleType[i])
            {
                case 0://貧民
                    //peopleKindText[i] = " 貧 民 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    if(_languageCnt.LanguageState_Scene3 == "English")
                    {          
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14] + _languageCnt.Scene3LanguaeData[33];
                    }
                    else
                    {
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[33] + _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14];
                    }
                    break;

                case 1://市民
                    //peopleKindText[i] = " 市 民 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    if(_languageCnt.LanguageState_Scene3 == "English")
                    {          
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14] + _languageCnt.Scene3LanguaeData[34];
                    }
                    else
                    {
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[34] + _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14];
                    }
                    break;

                case 2://富豪
                    //peopleKindText[i] = " 富 豪 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    if(_languageCnt.LanguageState_Scene3 == "English")
                    {          
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14] + _languageCnt.Scene3LanguaeData[35];
                    }
                    else
                    {
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[35] + _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14];
                    }
                    break;

                case 3://貴族
                    //peopleKindText[i] = " 貴 族 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    if(_languageCnt.LanguageState_Scene3 == "English")
                    {          
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14] + _languageCnt.Scene3LanguaeData[36];
                    }
                    else
                    {
                        peopleKindText[i] = _languageCnt.Scene3LanguaeData[36] + _languageCnt.Scene3LanguaeData[13] + tmpReceveMoneyInt[i] +  _languageCnt.Scene3LanguaeData[14];
                    }
                    break;
            }
            _logTmp += peopleKindText[i];
        }

        if (ParameterCalc.instanceCalc.TodaySlave > 0)
        {
            //_logText += " 貧 民 は 支 払 え ず 、〇 人 負 債 者 と な っ た 。 \n";
            if(_languageCnt.LanguageState_Scene3 == "English")
            {          
                _logTmp += "T^h^e^ " + ParameterCalc.instanceCalc.TodaySlave + _languageCnt.Scene3LanguaeData[5];
            }
            else
            {
                _logTmp += ParameterCalc.instanceCalc.TodaySlave + _languageCnt.Scene3LanguaeData[5];
            }
        }

        //薬の売却による犯罪度上昇
        if (ParameterCalc.instanceCalc.ToolType == 1)
        {
            _logTmp += _languageCnt.Scene3LanguaeData[40] + ParameterCalc.instanceCalc.TodayPotionCrime + _languageCnt.Scene3LanguaeData[41];
        }

        //市民の反乱
        if(ParameterCalc.instanceCalc.DoRebellionGeneral)
        {
            //_logText += " 市 民 は 銅 の 剣 を 手 に 取 り 立 ち 上 が っ た 。 \n";
            //_logText += " お 礼 と し て "+ ParameterCalc.instanceCalc.RebellionEarnedMoney + " z 得 た 。 \n";
            _logTmp += _languageCnt.Scene3LanguaeData[15];         
            _logTmp += _languageCnt.Scene3LanguaeData[16] + ParameterCalc.instanceCalc.RebellionEarnedMoney + _languageCnt.Scene3LanguaeData[17];
        }

        //祈り
        if(ParameterCalc.instanceCalc.TodayPrayValue > 0)
        {
            _logTmp += _languageCnt.Scene3LanguaeData[42] + ParameterCalc.instanceCalc.TodayPrayValue + _languageCnt.Scene3LanguaeData[43];
        }

        /*
         * 
         * イベント発生内容
         * 
         */

        //犯罪率と罰金
        if(ParameterCalc.instanceCalc.TodayCrime > 0 )
        {
            //_logText += " 犯 罪 度 が " + ParameterCalc.instanceCalc.TodayCrime + " 増 加 し た 。 \n";
            _logTmp += _languageCnt.Scene3LanguaeData[18] + ParameterCalc.instanceCalc.TodayCrime + _languageCnt.Scene3LanguaeData[19];
            if(ParameterCalc.instanceCalc.FineMoney)
            {
                //_logText += " 犯 罪 度 が 1 0 0 を 超 え た た め \n 罰 金 と し て " + ParameterCalc.instanceCalc.FineMoneyInt + " z 差 し 引 か れ た 。 \n";
                if(_languageCnt.LanguageState_Scene3 == "English")
                {          
                    _logTmp += _languageCnt.Scene3LanguaeData[20] + _languageCnt.Scene3LanguaeData[24] + ParameterCalc.instanceCalc.FineMoneyInt + _languageCnt.Scene3LanguaeData[21];
                }
                else
                {
                    _logTmp += _languageCnt.Scene3LanguaeData[20] + ParameterCalc.instanceCalc.FineMoneyInt + _languageCnt.Scene3LanguaeData[21];
                }
            }
        }

        // 行商税の税率表記
        float outTaxRate = ParameterCalc.instanceCalc.TaxRate * 100;
        _logTmp += _languageCnt.Scene3LanguaeData[38] +  (int)outTaxRate + _languageCnt.Scene3LanguaeData[39];

        //_logText += " 行 商 税 と し て " + ParameterCalc.instanceCalc.HaveTaxPay + " z 支 払 っ た 。\n";
        if(_languageCnt.LanguageState_Scene3 == "English")
        {          
            _logTmp += _languageCnt.Scene3LanguaeData[24] +  ParameterCalc.instanceCalc.HaveTaxPay + _languageCnt.Scene3LanguaeData[22];
        }
        else
        {
            _logTmp += _languageCnt.Scene3LanguaeData[22] +  ParameterCalc.instanceCalc.HaveTaxPay + _languageCnt.Scene3LanguaeData[24];
        }

        if (ParameterCalc.instanceCalc.StealTaxjuge)
        {
            //_logText +=  " 盗 賊 に " + ParameterCalc.instanceCalc.StealPeoplePay + " z 奪 わ れ て し ま っ た 。 \n";
            _logTmp += _languageCnt.Scene3LanguaeData[25] +  ParameterCalc.instanceCalc.StealPeoplePay + _languageCnt.Scene3LanguaeData[26];
        }

        //_logText += " 子 分 た ち に " + ParameterCalc.instanceCalc.Paycheck[ParameterCalc.instanceCalc.TurnCount] + " z 支 払 っ た 。\n";
        if(_languageCnt.LanguageState_Scene3 == "English")
        {          
            _logTmp += _languageCnt.Scene3LanguaeData[24] +  ParameterCalc.instanceCalc.Paycheck[ParameterCalc.instanceCalc.TurnCount] + _languageCnt.Scene3LanguaeData[23];
        }
        else
        {
            _logTmp += _languageCnt.Scene3LanguaeData[23] +  ParameterCalc.instanceCalc.Paycheck[ParameterCalc.instanceCalc.TurnCount] + _languageCnt.Scene3LanguaeData[24];
        }

        // 現在の犯罪度
        
        _logTmp += _languageCnt.Scene3LanguaeData[37] + ParameterCalc.instanceCalc.CrimeRate + "\n";
        
        
        int earnings = ParameterCalc.instanceCalc.TotalReceiveMoney - ParameterCalc.instanceCalc.TotalPayment;
        
        if (earnings > 0) //収支
        {        
            _logTmp += _languageCnt.Scene3LanguaeData[28] +  earnings + "z\n" + _languageCnt.Scene3LanguaeData[29] + ParameterCalc.instanceCalc.HaveMoney + "z \n";
        }
        else
        {         
            _logTmp += _languageCnt.Scene3LanguaeData[27] +  earnings + "z\n" + _languageCnt.Scene3LanguaeData[29] + ParameterCalc.instanceCalc.HaveMoney + "z \n";
        }

        int target = ParameterCalc.instanceCalc.TargetAmount - ParameterCalc.instanceCalc.HaveMoney;

        if(target <= 0)     //ゲームクリア
        {
            //_logText += " 目 標 金 額 達 成 ! !";
            _logTmp += _languageCnt.Scene3LanguaeData[30];
        }
        else if(target >= ParameterCalc.instanceCalc.TargetAmount)   //ゲームオーバー
        {
            //_logText += " 所 持 金 が 無 く な っ て し ま っ た . . . ";
            _logTmp += _languageCnt.Scene3LanguaeData[31];
        }
        else    //通常処理
        {
            // _logText += " 目 標 ま で あ と ：" + target + " z ";
            _logTmp += _languageCnt.Scene3LanguaeData[32] + target + "z";
        }
        _logSystemIEnumerator = WriteResult();
        _logSystemCoroutine = StartCoroutine(_logSystemIEnumerator);
    }

    public void StartTextAnim()
    {
        //言語を変更していた場合
        if(languageType != _languageCnt.LanguageState_Scene3)
        {
            if(readNowLog)StopCoroutine(_logSystemCoroutine);
            GenerateLog();
            return;
        }
        if(!readNowLog)return;
        //処理の重複ケア
        if(1 < _corutineCount)
        {
            StopCoroutine(_logSystemCoroutine);
            _corutineCount = 0;
        }
        _logSystemCoroutine = StartCoroutine(_logSystemIEnumerator);
        _openESC = false;
    }

    public void StopTextAnim()
    {
        StopCoroutine(_logSystemIEnumerator);
        languageType = _languageCnt.LanguageState_Scene3;
        _openESC = true;
    }

    //テキスト処理
    IEnumerator WriteResult()
    {
        _corutineCount ++;
        changeLogTex.text = "";
        readNowLog = true; //文字表示用SE開始

        //改行コード変換
        if (_logTmp.Contains("\\n"))
        {
            _logTmp = _logTmp.Replace(@"\n", Environment.NewLine);
        }

        // 半角スペースで文字を分割する。
        string[] words = _logTmp.Split('^'); //テキスト処理用

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            changeLogTex.text = changeLogTex.text + word;
            switch(_languageCnt.LanguageState_Scene3)
            {
            case "Japanese":
                yield return new WaitForSeconds(WaitSecondsJP);
                break;
            case "English":
                yield return new WaitForSeconds(WaitSecondsEN);
                break;                        
            }
        }
        _corutineCount = 0;
        readNowLog = false; //文字表示用SE終了
    }

    /*--------SE--------*/
    public void DoReadSE()
    {
        if(readNowLog && !_openESC)
        {
            if(!soundCLog.CheckReadSE())
            {
                //テキストを表示している間再生
                soundCLog.PlayReadSe(logFlowSE);
            }
        }
        else
        {
            soundCLog.StopSE();
        }
    }
    /*------------------*/
}