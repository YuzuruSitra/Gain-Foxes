using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenePeople_eveni : MonoBehaviour
{
    //商品入れ替え素材
    public Sprite BrSword;
    public Sprite Potion;
    public Sprite Stock;
    //商品格納先
    public GameObject Toolflame;
    private SpriteRenderer ToolFlame;

    //民衆生成用
    public GameObject[] PeopleIns = new GameObject[4];
    private int Cp;

    //主人公アニメ用
    public static int ShuAnim;
    public GameObject TalkPanel1;
    public GameObject TalkPanel2;


    // Start is called before the first frame update
    void Start()
    {
        /* 額縁の商品入れ替え */

        //コンポーネント取得
        ToolFlame = Toolflame.GetComponent<SpriteRenderer>();
        //出力画像選定
        switch (ParameterCalc.ToolType)
        {
            case 0: //剣
                ToolFlame.sprite = BrSword;
                break;

            case 1: //薬
                ToolFlame.sprite = Potion;
                break;

            case 2: //株
                ToolFlame.sprite = Stock;
                break;

        }

        InvokeRepeating("AnimCnt",1.0f,1.0f);
        /*--デバッグ用--*/
        
        ParameterCalc.GenePeopleCount = 4;
        ParameterCalc.GenePeopleType[0] = 0;
        ParameterCalc.GenePeopleType[1] = 1;
        ParameterCalc.GenePeopleType[2] = 2;
        ParameterCalc.GenePeopleType[3] = 3;
        

        /*-------------------------*/

        Cp = 0;
        //StartCoroutine("GenePeople");

        //主人公会話アニメ用
        TalkPanel1.gameObject.SetActive(false);
        TalkPanel2.gameObject.SetActive(false);
        ShuAnim = 0;
    }

    void AnimCnt()
    {
        ShuAnim++;
        if(ShuAnim > 4)
        {
            ShuAnim = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //主人公会話アニメ用
        if (ShuAnim > 3)
        {
            TalkPanel1.gameObject.SetActive(false);
            TalkPanel2.gameObject.SetActive(false);

        }
        else if (ShuAnim > 0)
        {
            if (ShuAnim == 2)
            {
                TalkPanel2.gameObject.SetActive(true);
                TalkPanel1.gameObject.SetActive(true);
            }
            else if (ShuAnim % 2 == 1)
            {
                TalkPanel2.gameObject.SetActive(false);
                TalkPanel1.gameObject.SetActive(true);
            }
            else
            {
                TalkPanel1.gameObject.SetActive(false);
                TalkPanel2.gameObject.SetActive(true);
            }
        }
    }
    private IEnumerator GenePeople()
    {
        while (Cp <= 3)
        {
            //生成
            Instantiate(PeopleIns[Cp], this.transform.position, Quaternion.identity);
            Cp++;
            yield return new WaitForSeconds(2.5f);
        }

    }

    //シーン遷移用
    void GoFade()
    {
        SceneCnt_B.isFadeOut_B = true;
    }
}
