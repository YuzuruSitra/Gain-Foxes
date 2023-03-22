using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//シーン3ログシステム管理
public class LogSystem : MonoBehaviour
{
	[SerializeField] 
    private Text changeLogTex; //テキストを替えるオブジェクト
    private string logText; //代入するテキストの中身
    private string[] peopleKindText = new string[5]; //種類ごとに出力するテキストを変更

	[SerializeField] 
    private SoundCnt soundCLog;
	[SerializeField] 
    private AudioClip logFlowSE;
    private bool readNowLog = false;

    void Start()
    {
        /*---bgm設定---*/
        soundCLog = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();

        logText = "";
        GenerateLog();
    }

    void Update()
    {
        //SE処理用
        DoReadSE();
    }

    void GenerateLog()
    {
        //何を売ったか
        switch (ParameterCalc.instanceCalc.ToolType)
        {
            case 0: //剣
                logText += " 今 日 は ど う の つ る ぎ を 売 っ た 。\n";
                break;

            case 1: //薬
                logText += " 今 日 は こ う か な く す り を 売 っ た 。 \n";
                break;

            case 2: //株
                logText += " 今 日 は ま ぼ ろ し の か ぶ を 売 っ た 。 \n";
                break;
        }

        if (ParameterCalc.instanceCalc.PoorDebt)
        {
            logText += " 貧 民 は 支 払 え ず 、 負 債 者 と な っ た 。 \n";
        }

        if (ParameterCalc.instanceCalc.usePubli)
        {
            if(ParameterCalc.instanceCalc.PubliSuccess)
            {
                logText += " 業 者 は 成 功 し た よ う だ 。 お 客 さ ん が 普 段 よ り 多 く 来 店 し た 。 \n";
            }
            else
            {
                logText += " 業 者 は 失 敗 し た よ う だ 。 お 客 さ ん が 普 段 よ り 多 く 来 店 し た 。  \n";
            }
        }

        if (ParameterCalc.instanceCalc.ExeKill)
        {
            int killPeople = ParameterCalc.instanceCalc.killNumber;
            switch(killPeople)
            {
                case 0:
                    logText += " 貧 民 の 暗 殺 に 成 功 し た 。 \n";
                    logText += " 普 段 よ り 貴 族 が 少 し 多 く 来 店 し た よ う だ 。 \n";
                    break;
                case 1:
                    logText += " 市 民 の 暗 殺 に 成 功 し た 。 \n";
                    break;
                case 2:
                    logText += " 富 豪 の 暗 殺 に 成 功 し た 。 \n";
                    break;
                case 3:
                    logText += " 貴 族 の 暗 殺 に 成 功 し た 。 \n";
                    logText += " 行 商 税 の 倍 率 が 下 が っ た 。 \n";
                    break;
            }
        }

        //来店
        for (int i = 0; i <= ParameterCalc.instanceCalc.GenePeopleCount; i++ )
        {
            switch (ParameterCalc.instanceCalc.GenePeopleType[i])
            {
                case 0://貧民
                    peopleKindText[i] = " 貧 民 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;

                case 1://市民
                    peopleKindText[i] = " 市 民 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;

                case 2://富豪
                    peopleKindText[i] = " 富 豪 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;

                case 3://貴族
                    peopleKindText[i] = " 貴 族 か ら " + ParameterCalc.instanceCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;
            }
            logText += peopleKindText[i];
        }

        //市民の反乱
        if(ParameterCalc.instanceCalc.DoRebellionGeneral)
        {
            logText += " 市 民 は 銅 の 剣 を 手 に 取 り 立 ち 上 が っ た 。 \n";
            logText += " お 礼 と し て "+ ParameterCalc.instanceCalc.RebellionEarnedMoney + " z 得 た 。 \n";
        }

        /*
         * 
         * イベント発生内容
         * 
         */

        //犯罪率と罰金
        if(ParameterCalc.instanceCalc.TodayCrime > 0 )
        {
            logText += " 犯 罪 度 が " + ParameterCalc.instanceCalc.TodayCrime + " 増 加 し た 。 \n";
        
            if(ParameterCalc.instanceCalc.FineMoney)
            {
                logText += " 犯 罪 度 が 1 0 0 を 超 え た た め \n 罰 金 と し て " + ParameterCalc.instanceCalc.FineMoneyInt + " z 差 し 引 か れ た 。 \n";
            }
        }

        logText += " 行 商 税 と し て " + ParameterCalc.instanceCalc.HaveTaxPay + " z 支 払 っ た 。\n";

        if (ParameterCalc.instanceCalc.StealTaxjuge)
        {
            logText +=  " 盗 賊 に " + ParameterCalc.instanceCalc.StealPeoplePay + " z 奪 わ れ て し ま っ た 。 \n";
        }

        logText += " 子 分 た ち に " + ParameterCalc.instanceCalc.Paycheck[ParameterCalc.instanceCalc.TurnCount] + " z 支 払 っ た 。\n";
        int earnings = ParameterCalc.instanceCalc.TotalReceiveMoney - ParameterCalc.instanceCalc.TotalPayment;
        
        if (earnings > 0) //収支
        {
            logText += " 収 支 ： +" + earnings + "z 　　　所 持 金 ：" + ParameterCalc.instanceCalc.HaveMoney + " z \n";
        }
        else
        {
            logText += " 収 支 ： " + earnings + "z 　　　所 持 金 ：" + ParameterCalc.instanceCalc.HaveMoney + " z \n";
        }

        int target = ParameterCalc.instanceCalc.TargetAmount - ParameterCalc.instanceCalc.HaveMoney;

        if(target <= 0)     //ゲームクリア
        {
            logText += " 目 標 金 額 達 成 ! !";
        }
        else if(target >= ParameterCalc.instanceCalc.TargetAmount)   //ゲームオーバー
        {
            logText += " 所 持 金 が 無 く な っ て し ま っ た . . . ";
        }
        else    //通常処理
        {
            logText += " 目 標 ま で あ と ：" + target + " z ";
        }
        
        StartCoroutine(WriteResult());
    }

    //テキスト処理
    IEnumerator WriteResult()
    {
        changeLogTex.text = "";
        readNowLog = true; //文字表示用SE開始

        // 半角スペースで文字を分割する。
        string[] words = logText.Split(' ');; //テキスト処理用

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            changeLogTex.text = changeLogTex.text + word;
            yield return new WaitForSeconds(0.04f);
        }

        readNowLog = false; //文字表示用SE終了
    }

    /*--------SE--------*/
    public void DoReadSE()
    {
        if(readNowLog)
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