using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//シーン2演出管理
public class Volume2 : MonoBehaviour
{
    //額縁の中身変更用
    [SerializeField]
    private Sprite BrSword;
    [SerializeField]
    private Sprite Potion;
    [SerializeField]
    private Sprite Stock;
    //フレーム
    [SerializeField]
    private GameObject toolFlame;

    //民衆生成
    [SerializeField]
    private GameObject[] peopleIns = new GameObject[4];

    //フィネアニメーション用
    public int ShuAnim;
    [SerializeField]
    private GameObject talkPanel1;
    [SerializeField]
    private GameObject talkPanel2;
    //画面遷移
    private bool trnOne;

    //サウンド用スクリプト取得
	[SerializeField] 
    private SoundCnt soundB;
    [SerializeField]
    private AudioClip sceneB_BGM;
    
    [SerializeField] 
    private AudioClip pushButtonSE;

    //シーン遷移
    public bool IsNextScene = false;
    public int Scene2SlaveCount;

    void Start()
    {
        /*---bgm設定---*/
        soundB = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
        soundB.PlayBgm(sceneB_BGM);

        trnOne = true;
        SpriteRenderer spriteToolFlame = toolFlame.GetComponent<SpriteRenderer>();

        //販売商品に合わせる
        switch (ParameterCalc.instanceCalc.ToolType)
        {
            case 0: //銅剣
                spriteToolFlame.sprite = BrSword;
                break;

            case 1: //薬
                spriteToolFlame.sprite = Potion;
                break;

            case 2: //株
                spriteToolFlame.sprite = Stock;
                break;
            
        }
        StartCoroutine("GenePeople");

        //フィネアニメーション用
        talkPanel1.gameObject.SetActive(false);
        talkPanel2.gameObject.SetActive(false);
        ShuAnim = 0;
        Scene2SlaveCount = ParameterCalc.instanceCalc.TodaySlave;
    }

    void Update()
    {
        //フィネアニメーション用
        if (ShuAnim > ParameterCalc.instanceCalc.GenePeopleCount)
        {
            talkPanel1.gameObject.SetActive(false);
            talkPanel2.gameObject.SetActive(false);

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
                talkPanel2.gameObject.SetActive(true);
                talkPanel1.gameObject.SetActive(true);
            }
            else if (ShuAnim % 2 == 1)
            {
                talkPanel2.gameObject.SetActive(false);
                talkPanel1.gameObject.SetActive(true);
            }
            else
            {
                talkPanel1.gameObject.SetActive(false);
                talkPanel2.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator GenePeople()
    {
        int cp = 0;

        while (cp <= ParameterCalc.instanceCalc.GenePeopleCount)
        {
            
            Instantiate(peopleIns[ParameterCalc.instanceCalc.GenePeopleType[cp]], this.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            cp++;
        }
        
    }

    //フェード
    void GoFade()
    {
        IsNextScene = true;
    }

    /*--------------SE----------------*/

    //ボタン押したときの音
	public void PushButtonSE_B()
	{
		soundB.PlaySe(pushButtonSE);
	}
}
