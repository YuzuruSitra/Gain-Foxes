using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrExpText : MonoBehaviour
{
    public string[] talks = new string[6];

    private string[] words;
    public Text textLabel;
    //テキストが流れているか判定
    private bool RunDispo;

    void Start()
    {
        //ボタンを押せるように
        RunDispo = true;

        //戦略説明をセット

        //デフォ
        talks[0] = "     今 日 の 戦 略 を 子 分 た ち に 伝 え よ う 。 ";
        //噂
        talks[1] = "     　　噂 を 流 す　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 民 衆 に 噂 を 流 し 、 貧 民 を 出 現 さ せ や す く す る 。";
        //祈り
        talks[2] = "     　　祈 る　　　　　　　　　　　　　　　　　　　　　　　　　　  　　　　　　　　　　　　 祈 り を 捧 げ 、 犯 罪 度 を 下 げ る 。";
        //交渉
        talks[3] = "     　　交 渉 す る　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 負 債 者 を 1 人 利 用 し 、 業 者 と 交 渉 す る 。\n  民 衆 の 来 店 率 を あ げ る 。 \n 成 功 率 は 支 払 う 金 額 し だ い だ 。 ";
        //暗殺
        talks[4] = "     　　暗 殺 す る　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 負 債 者 を 1 人 利 用 し 事 件 を 起 こ す 。\n  今 日 は 対 象 の 民 衆 が 出 現 し な く な る 。 ";
        //入荷
        talks[5] = "     　　仕 入 れ る　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　　 新 た な 商 品 を 仕 入 れ る 。 ";

        // 会話フィールドをリセットする。
        textLabel.text = "";
        StartCoroutine(Dialogue());
    }

    // ボタンを押すと会話スタート
    public void OnButtonClicked()
    {
        // 会話フィールドをリセットする。
        if (RunDispo)
        {
            textLabel.text = "";
            StartCoroutine(Dialogue());
        }
    }

    // コルーチンを使って、１文字ごと表示する。
    IEnumerator Dialogue()
    {
        RunDispo = false;//処理中に他のパネルの選択を出来なくする

        // 半角スペースで文字を分割する。
        words = talks[UIContA.SelectStr].Split(' ');

        foreach (var word in words)
        {
 
            // 0.1秒刻みで１文字ずつ表示する。
            textLabel.text = textLabel.text + word;
            yield return new WaitForSeconds(0.02f);

        }
        RunDispo = true; //他のパネルの選択を可能にする
    }
}
