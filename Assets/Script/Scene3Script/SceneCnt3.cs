using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//シーン3の遷移管理
public class SceneCnt3 : MonoBehaviour
{
    //セーブ管理用
    [SerializeField] 
    private SaveControl saveControl; 
	private float fadeSpeed = 0.8f;        //透明度が変わるスピードを管理
	private float red, green, blue, alfa;   //パネルの色、不透明度を管理
	public bool isFadeOut3 = false;  //フェードアウト処理の開始、完了を管理するフラグ
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
	[SerializeField]
	private GameObject fadePanel_C;
	private Image fadeImage_C;                //透明度を変更するパネルのイメージ
    
	//サウンド用スクリプト取得
	[SerializeField] 
    private SoundCnt soundC;
	public AudioClip sceneC_BGM;
	[SerializeField] 
    private AudioClip pushButtonSE;

	void Start()
	{
        /*---bgm設定---*/
        soundC = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
        soundC.PlayBgm(sceneC_BGM);

		fadePanel_C.gameObject.SetActive(true);
		isFadeIn = true;
		//コンポーネント取得・利用
        saveControl = GameObject.Find("SaveManager").GetComponent<SaveControl> ();
		fadeImage_C = fadePanel_C.GetComponent<Image>();

		red = fadeImage_C.color.r;
		green = fadeImage_C.color.g;
		blue = fadeImage_C.color.b;
		alfa = fadeImage_C.color.a;

		//セーブ処理
		saveControl.NewGame = false;
		saveControl.Dosave();
	}

	void Update()
	{
		if (isFadeIn)
		{
			StartFadeIn();
		}

		if (isFadeOut3)
		{
			StartFadeOut();
		}
	}

	void StartFadeIn()
	{
		alfa -= fadeSpeed * Time.deltaTime;
		SetAlpha();                      
		if (alfa <= 0)
		{                    
			isFadeIn = false;
			fadeImage_C.enabled = false;    
		}
	}

	public void StartFadeOut()
	{
		fadeImage_C.enabled = true;  
		alfa += fadeSpeed * Time.deltaTime;         
		SetAlpha();               
		if (alfa >= 1)
		{             
			isFadeOut3 = false;

			// //セーブ処理
			// saveControl.NewGame = false;
			// saveControl.Dosave();
			SceneManager.LoadScene("Scene1");
		}
	}

	void SetAlpha()
	{
		fadeImage_C.color = new Color(red, green, blue, alfa);
	}

	//シーン遷移管理
	public void NextScene()
	{
		isFadeOut3 = true;
	}

	/*--------------SE----------------*/

    //ボタン押したときの音
	public void PushButtonSE_C()
	{
		soundC.PlaySe(pushButtonSE);
	}
}