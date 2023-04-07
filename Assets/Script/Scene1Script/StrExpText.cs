using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


//戦略パネルのテキスト管理
public class StrExpText : MonoBehaviour
{
    //UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    //言語用のクラス
    [SerializeField]
    private ChangeLanguageScene1 _languageCnt;
    private string[] talks = new string[6];
    private string[] words;
    [SerializeField]
    private Text textLabel;
    //テキストが流れているか判定
    private bool runDispo;
    public bool _DoSetFirst;
    //コルーチンの取得
    private Coroutine _dialogCoroutine;

    void Start()
    {
        _DoSetFirst = true;
        //コンポーネント取得
        uiCont1 = GameObject.Find("UICont").GetComponent<UICont1> ();
        //ボタンを押せるように
        runDispo = true;

        //言語設定用
        _languageCnt = GameObject.Find("LanguageUI_Scene1").GetComponent<ChangeLanguageScene1> ();
    }

    public void SetFirstExp()
    {
        if(!_DoSetFirst)return;
        //重複処理ケア
        if(!runDispo)StopCoroutine(_dialogCoroutine);
        //戦略説明をセット
        SetExpLanguage();
        textLabel.text = talks[0];
    }

    public void OnButtonClicked()
    {
        if (runDispo)
        {
            textLabel.text = "";
            _dialogCoroutine = StartCoroutine(Dialogue());
        }
        _DoSetFirst = false;
    }

    // コルーチンを使って、１文字ごと表示する。
    IEnumerator Dialogue()
    {
        runDispo = false;//処理中に他のパネルの選択を出来なくする
        uiCont1.ReadNow = true;
        SetExpLanguage();
        //改行コード変換
        if (talks[uiCont1.SelectStr].Contains("\\n"))
        {
            talks[uiCont1.SelectStr] = talks[uiCont1.SelectStr].Replace(@"\n", Environment.NewLine);
        }
        // 半角スペースで文字を分割する。
        words = talks[uiCont1.SelectStr].Split('^');

        foreach (var word in words)
        {
            // 0.1秒刻みで１文字ずつ表示する。
            textLabel.text = textLabel.text + word;
            yield return new WaitForSeconds(0.02f);
        }
        uiCont1.ReadNow = false; //文字表示用SE終了
        runDispo = true; //他のパネルの選択を可能にする
    }

    void SetExpLanguage()
    {
        //戦略説明をセット
        //デフォ
        talks[0] = _languageCnt.Scene1LanguaeData[62];
        //噂
        talks[1] = _languageCnt.Scene1LanguaeData[63] + _languageCnt.Scene1LanguaeData[64];
        //祈り
        talks[2] = _languageCnt.Scene1LanguaeData[65] +_languageCnt.Scene1LanguaeData[66];
        //交渉
        talks[3] = _languageCnt.Scene1LanguaeData[67] +_languageCnt.Scene1LanguaeData[68];
        //暗殺
        talks[4] = _languageCnt.Scene1LanguaeData[69] +_languageCnt.Scene1LanguaeData[70];
        //入荷
        talks[5] = _languageCnt.Scene1LanguaeData[71] +_languageCnt.Scene1LanguaeData[72];
    }
}
