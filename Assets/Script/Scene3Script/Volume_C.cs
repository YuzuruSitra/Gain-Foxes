using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Volume_C : MonoBehaviour
{
    //表示するログの種類の列挙型
    public enum LogType
    {
        All,
        Time,
        Event
    }

    //ログ出力先テキスト
    [SerializeField]
    private Text logText;
    //全データ
    private List<string> allLogs;
    //時間のデータ
    private List<string> timerLogs;
    //イベントのデータ
    private List<string> eventLogs;
    //現在表示するログの種類
    [SerializeField]
    private LogType logTypeToDisplay = LogType.All;
    //ログを保存する数
    [SerializeField]
    private int allLogDataNum = 10;
    [SerializeField]
    private int timerLogDataNum = 10;
    [SerializeField]
    private int eventLogDataNum = 10;
    //縦のスクロールバー
    [SerializeField]
    private Scrollbar verticalScrollbar;
    private StringBuilder logTextStringBuilder;
    private int i;


    // Start is called before the first frame update
    void Start()
    {
        allLogs = new List<string>();
        timerLogs = new List<string>();
        eventLogs = new List<string>();
        logTextStringBuilder = new StringBuilder();
        i = 0;
        StartCoroutine("nunu");
    }

    //ログテキストの追加
    public void AddLogText(string logText, LogType logType)
    {
        //ログテキストの追加
        allLogs.Add(logText);
        if (logType == LogType.Event)
        {
            eventLogs.Add(logText);
        }
        else if (logType == LogType.Time)
        {
            timerLogs.Add(logText);
        }
        //ログの最大保存数を超えたら古いログを削除
        if (allLogs.Count > allLogDataNum)
        {
            allLogs.RemoveRange(0, allLogs.Count - allLogDataNum);
        }
        if (timerLogs.Count > timerLogDataNum)
        {
            timerLogs.RemoveRange(0, timerLogs.Count - timerLogDataNum);
        }
        if (eventLogs.Count > eventLogDataNum)
        {
            eventLogs.RemoveRange(0, eventLogs.Count - eventLogDataNum);
        }
        //ログテキストの表示
        if (logTypeToDisplay == LogType.All || logTypeToDisplay == logType)
        {
            ViewLogText();
        }
    }

    //下からログを追加するバージョン
    public void ViewLogText()
    {
        logTextStringBuilder.Clear();
        List<string> selectedLogs = new List<string>();

        if (logTypeToDisplay == LogType.All)
        {
            selectedLogs = allLogs;
        }
        else if (logTypeToDisplay == LogType.Event)
        {
            selectedLogs = eventLogs;
        }
        else if (logTypeToDisplay == LogType.Time)
        {
            selectedLogs = timerLogs;
        }

        foreach (var log in selectedLogs)
        {
            logTextStringBuilder.Append(Environment.NewLine + log);
        }
        logText.text = logTextStringBuilder.ToString().TrimStart();
        UpdateScrollBar();
    }

    public void UpdateScrollBar()
    {
        verticalScrollbar.value = 0f;
    }

    void Update()
    {

    }

    IEnumerator nunu()
    {
        AddLogText(" ", LogType.Event);
        AddLogText("----------------------------", LogType.Event);

        while (i < ParameterCalc.GenePeopleCount)
        {
            switch (ParameterCalc.GenePeopleType[i])
            {
                case 0: //貧民
                    AddLogText("貧民は" + ParameterCalc.ReceiveMoney[i] + "z支払った。", LogType.Event);
                    break;

                case 1: //市民
                    AddLogText("市民は" + ParameterCalc.ReceiveMoney[i] + "z支払った。", LogType.Event);
                    break;

                case 2: //富豪
                    AddLogText("富豪は" + ParameterCalc.ReceiveMoney[i] + "z支払った。", LogType.Event);
                    break;

                case 3: //貴族
                    AddLogText("貴族は" + ParameterCalc.ReceiveMoney[i] + "z支払った。", LogType.Event);
                    break;
            }
            
            yield return new WaitForSeconds(1.0f);
            i++;
        }
        AddLogText("土地代として" + ParameterCalc.RandTaxPay + "z差し引かれた。", LogType.Event);
        AddLogText("所得税として" + ParameterCalc.HaveTaxPay + "z差し引かれた。", LogType.Event);
        if (ParameterCalc.StealTaxjuge)
        {
            AddLogText("盗賊に" + ParameterCalc.StealPeoplePay + "z奪われてしまった。", LogType.Event);
        }
        AddLogText("子分たちに" + ParameterCalc.Paycheck[ParameterCalc.TurnCount] + "z支払った。", LogType.Event);
        AddLogText("収入：" + ParameterCalc.TotalReceiveMoney + "z     支出：" + ParameterCalc.TotalPayment + "z", LogType.Event);
        AddLogText("----------------------------", LogType.Event);

    }
}

/*
 * 
 *���O�V�X�e��
 *
 */
