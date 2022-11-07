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

    void Start()
    {
        LogText = "";
        GenerateLog();
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

        //来店
        for (int i = 0; i <= ParameterCalc.GenePeopleCount; i++ )
        {
            switch (ParameterCalc.GenePeopleType[i])
            {
                case 0://貧民
                    PeopleKindText[i] = " 貧 民 は " + ParameterCalc.ReceiveMoney[i] + "z 支 払 っ た 。 \n";
                    break;

                case 1://市民
                    PeopleKindText[i] = " 市 民 は " + ParameterCalc.ReceiveMoney[i] + " z 支 払 っ た 。 \n";
                    break;

                case 2://富豪
                    PeopleKindText[i] = " 富 豪 は " + ParameterCalc.ReceiveMoney[i] + " z 支 払 っ た 。 \n";
                    break;

                case 3://貴族
                    PeopleKindText[i] = " 貴 族 は " + ParameterCalc.ReceiveMoney[i] + " z 支 払 っ た 。 \n";
                    break;
            }

            LogText += PeopleKindText[i];

        }

        /*
         * 
         * イベント発生内容
         * 
         */

        LogText += " 土 地 代 と し て " + ParameterCalc.RandTaxPay + " z 差 し 引 か れ た 。 \n";
        LogText += " 所 得 税 と し て " + ParameterCalc.HaveTaxPay + " z 差 し 引 か れ た 。 \n";
        if (ParameterCalc.StealTaxjuge)
        {
            LogText +=  " 盗 賊 に " + ParameterCalc.StealPeoplePay + " z 奪 わ れ て し ま っ た 。 \n";
        }
        LogText += " 子 分 た ち に " + ParameterCalc.Paycheck[ParameterCalc.TurnCount] + " z 支 払 っ た \n";
        LogText += " 収 入 ： " + ParameterCalc.TotalReceiveMoney + " z 　　　支 出 ：" + ParameterCalc.TotalPayment + " z \n";
        int earnings = ParameterCalc.TotalReceiveMoney - ParameterCalc.TotalPayment;
        if (earnings > 0) //収支
        {
            LogText += " 収支 : +" + earnings + "z \n";
        }
        else
        {
            LogText += " 収支 : " + earnings + "z \n";
        }

        //ChangeLogTex.text = LogText;
        StartCoroutine(WriteResult());
    }

    //テキスト処理
    IEnumerator WriteResult()
    {
        ChangeLogTex.text = "";

        // 半角スペースで文字を分割する。
        words = LogText.Split(' ');

        foreach (var word in words)
        {

            // 0.1秒刻みで１文字ずつ表示する。
            ChangeLogTex.text = ChangeLogTex.text + word;
            yield return new WaitForSeconds(0.04f);

        }
    }
}