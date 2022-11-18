using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogSystem : MonoBehaviour
{
    public Text ChangeLogTex; //テキストを替えるオブジェクト
    private string LogText; //代入するテキストの中身
    private string[] PeopleKindText = new string[5]; //種類ごとに出力するテキストを変更
    private string[] words; //テキスト処理用

	[SerializeField] 
    private soundCnt soundCLog;
	[SerializeField] 
    private AudioClip LogFlowSE;
    private bool readNowLog = false;
    private bool delayTextSELog = true;

    void Start()
    {
        /*---bgm設定---*/
        soundCLog = GameObject.Find("SoundManager").GetComponent<soundCnt> ();

        LogText = "";
        GenerateLog();
    }

    void Update()
    {
        /*--------SE--------*/
        if(readNowLog)
        {
            Debug.Log("aaaaaa");
            if(delayTextSELog)//1.045秒毎に再生
            {
                TextFlowSELog();
                delayTextSELog = false;
                Invoke("DelayTextSE",1.045f);
            }
        }
    }

    void GenerateLog()
    {
        //何を売ったか
        switch (ParameterCalc.ToolType)
        {
            case 0: //剣
                LogText += " 今 日 は ど う の つ る ぎ を 売 っ た 。\n";
                break;

            case 1: //薬
                LogText += " 今 日 は こ う か な く す り を 売 っ た 。 \n";
                break;

            case 2: //株
                LogText += " 今 日 は ま ぼ ろ し の か ぶ を 売 っ た 。 \n";
                break;
        }
        if (ParameterCalc.PoorDebt)
        {
            LogText += " 貧 民 は 支 払 え ず 、 負 債 者 と な っ た 。 \n";
        }

        if (ParameterCalc.usePubli)
        {
            if(ParameterCalc.publiSuccess)
            {
                LogText += " 業 者 は 成 功 し た よ う だ 。 \n";
            }
            else
            {
                LogText += " 業 者 は 失 敗 し た よ う だ 。 \n";
            }
        }

        //来店
        for (int i = 0; i <= ParameterCalc.GenePeopleCount; i++ )
        {
            switch (ParameterCalc.GenePeopleType[i])
            {
                case 0://貧民
                    PeopleKindText[i] = " 貧 民 か ら " + ParameterCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;

                case 1://市民
                    PeopleKindText[i] = " 市 民 か ら " + ParameterCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;

                case 2://富豪
                    PeopleKindText[i] = " 富 豪 か ら " + ParameterCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;

                case 3://貴族
                    PeopleKindText[i] = " 貴 族 か ら " + ParameterCalc.ReceiveMoney[i] + " z 得 た 。 \n";
                    break;
            }

            LogText += PeopleKindText[i];

        }

        /*
         * 
         * イベント発生内容
         * 
         */

        //犯罪率と罰金
        if(ParameterCalc.toDayCrime > 0 )
        {
            LogText += " 犯 罪 度 が " + ParameterCalc.toDayCrime + " 増 加 し た 。 \n";
        
            if(ParameterCalc.fineMoney)
            {
                LogText += " 犯 罪 度 が 1 0 0 を 超 え た た め \n 罰 金 と し て " + ParameterCalc.fineMoneyInt + " z 差 し 引 か れ た 。 \n";
            }
        }

        LogText += " 行 商 税 と し て " + ParameterCalc.HaveTaxPay + " z 差 し 引 か れ た 。 \n";
        if (ParameterCalc.StealTaxjuge)
        {
            LogText +=  " 盗 賊 に " + ParameterCalc.StealPeoplePay + " z 奪 わ れ て し ま っ た 。 \n";
        }
        LogText += " 子 分 た ち に " + ParameterCalc.Paycheck[ParameterCalc.TurnCount] + " z 支 払 っ た 。\n";
        int earnings = ParameterCalc.TotalReceiveMoney - ParameterCalc.TotalPayment;
        if (earnings > 0) //収支
        {
            LogText += " 収 支 ： +" + earnings + "z 　　　所 持 金 ：" + ParameterCalc.HaveMoney + " z \n";
        }
        else
        {
            LogText += " 収 支 ： " + earnings + "z 　　　所 持 金 ：" + ParameterCalc.HaveMoney + " z \n";
        }
        int target = ParameterCalc.TargetAmount - ParameterCalc.HaveMoney;
        if(target > 0)
        {
            LogText += " 目 標 ま で あ と ：" + target + " z ";
        }
        else if(target >= ParameterCalc.TargetAmount)
        {
            LogText += " 所 持 金 が 無 く な っ て し ま っ た . . . ";
        }
        else
        {
            LogText += " 目 標 金 額 達 成 ! !";
        }
        
        StartCoroutine(WriteResult());
    }

    //テキスト処理
    IEnumerator WriteResult()
    {
        ChangeLogTex.text = "";
        readNowLog = true; //文字表示用SE開始

        // 半角スペースで文字を分割する。
        words = LogText.Split(' ');

        /*--SE用--*/
        int finSE = words.Length - 12; //文字の長さを取得用
        int finTime = 0; //再生終了タイミング用
        if(finSE < 0)
        {
            finSE = 1;
        }
        /*--------*/

        foreach (var word in words)
        {
            finTime++;
            // 0.1秒刻みで１文字ずつ表示する。
            ChangeLogTex.text = ChangeLogTex.text + word;
            yield return new WaitForSeconds(0.04f);
            if(finSE < finTime)
            {
                readNowLog = false; //文字表示用SE終了
            }
        }

        readNowLog = false; //文字表示用SE終了
    }

    /*--------------SE----------------*/
    private void TextFlowSELog()
    {
        soundCLog.PlaySe(LogFlowSE);
    }
    private void DelayTextSE()
    {
        delayTextSELog = true;
    }

    /*--------------------------------*/
}