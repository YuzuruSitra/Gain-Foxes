using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//シーン2演出管理
public class Volume2 : MonoBehaviour
{
    //インスタンス化
    public static Volume2 instanceVolume2;
    //額縁の中身変更用
    [SerializeField]
    private Sprite BrSword;
    [SerializeField]
    private Sprite Potion;
    [SerializeField]
    private Sprite Stock;
    //フレーム
    [SerializeField]
    private GameObject Toolflame;
    private SpriteRenderer ToolFlame;

    //民衆生成
    [SerializeField]
    private GameObject[] PeopleIns = new GameObject[4];
    private int Cp;

    //フィネアニメーション用
    public int ShuAnim;
    [SerializeField]
    private GameObject TalkPanel1;
    [SerializeField]
    private GameObject TalkPanel2;
    //画面遷移
    private bool trnOne;

    //サウンド用スクリプト取得
	[SerializeField] 
    private soundCnt soundB;
    [SerializeField]
    private AudioClip sceneB_BGM;
    
    [SerializeField] 
    private AudioClip pushButtonSE;

    //シーン遷移
    public bool isNextScene = false;

	void Awake()
	{
		if (instanceVolume2 == null)
        {
            instanceVolume2 = this;
        }
	}

    void Start()
    {
        /*---bgm設定---*/
        soundB = GameObject.Find("SoundManager").GetComponent<soundCnt> ();
        soundB.PlayBgm(sceneB_BGM);

        trnOne = true;

        ToolFlame = Toolflame.GetComponent<SpriteRenderer>();

        //販売商品に合わせる
        switch (ParameterCalc.instanceCalc.ToolType)
        {
            case 0: //銅剣
                ToolFlame.sprite = BrSword;
                break;

            case 1: //薬
                ToolFlame.sprite = Potion;
                break;

            case 2: //株
                ToolFlame.sprite = Stock;
                break;
            
        }
        Cp = 0;
        StartCoroutine("GenePeople");

        //フィネアニメーション用
        TalkPanel1.gameObject.SetActive(false);
        TalkPanel2.gameObject.SetActive(false);
        ShuAnim = 0;
    }

    void Update()
    {
        //フィネアニメーション用
        if (ShuAnim > ParameterCalc.instanceCalc.GenePeopleCount)
        {
            TalkPanel1.gameObject.SetActive(false);
            TalkPanel2.gameObject.SetActive(false);

            //画面遷移
            if (trnOne)
            {
                Invoke("GoFade", 6.0f);
                trnOne = false;
            }
        }
        else if(ShuAnim > 0)
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
        while (Cp <= ParameterCalc.instanceCalc.GenePeopleCount)
        {
            
            Instantiate(PeopleIns[ParameterCalc.instanceCalc.GenePeopleType[Cp]], this.transform.position, Quaternion.identity);
            Cp++;
            yield return new WaitForSeconds(2.5f);
        }
        
    }

    //フェード
    void GoFade()
    {
        isNextScene = true;
    }

    /*--------------SE----------------*/

    //ボタン押したときの音
	public void PushButtonSE_B()
	{
		soundB.PlaySe(pushButtonSE);
	}
}
