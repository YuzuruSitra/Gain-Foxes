using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//シーン2の遷移管理
public class SceneCnt2: MonoBehaviour
{
    //シーン２演出管理用
    [SerializeField] 
    private Volume2 volume2; 
	private float fadeSpeed = 0.4f;        //透明度が変わるスピードを管理
	private float red, green, blue, alfa;   //パネルの色、不透明度を管理
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
	//サウンド用スクリプト取得
	[SerializeField] 
    private SoundCnt sound2;
	//メニュー管理スクリプトの取得
    [SerializeField]
    private MenuCnt2 _menuCnt2;
	[SerializeField]
	private GameObject fadePanelB;
	private Image fadeImageB;                //透明度を変更するパネルのイメージ
	[SerializeField] 
    private AudioClip pushButtonSE;


	void Start()
	{
		fadePanelB.gameObject.SetActive(true);
		isFadeIn = true;
		//コンポーネント取得・利用
		volume2 = GameObject.Find("SceneManager_B").GetComponent<Volume2> ();
		/*---bgm設定---*/
        sound2 = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
		fadeImageB = fadePanelB.GetComponent<Image>();

		red = fadeImageB.color.r;
		green = fadeImageB.color.g;
		blue = fadeImageB.color.b;
		alfa = fadeImageB.color.a;
	}

	void Update()
	{
		if (isFadeIn)
		{
			StartFadeIn();
		}
		if (volume2.IsNextScene)
		{
			StartFadeOut();
		}
	}

	void StartFadeIn()
	{
		_menuCnt2.FadeNow2 = true;
		alfa -= fadeSpeed * Time.deltaTime;
		SetAlpha();      
		if (alfa <= 0)
		{         
			_menuCnt2.FadeNow2 = false;         
			isFadeIn = false;
			fadeImageB.enabled = false;  
		}
	}

	public void StartFadeOut()
	{
		_menuCnt2.FadeNow2 = true;
		fadeImageB.enabled = true;
		alfa += fadeSpeed * Time.deltaTime; 
		SetAlpha();   
		if (alfa >= 1)
		{           
			_menuCnt2.FadeNow2 = false;
			volume2.IsNextScene = false;
			//シーンのロードを挟む
			SceneManager.LoadScene("Scene3");
		}
	}

	void SetAlpha()
	{
		fadeImageB.color = new Color(red, green, blue, alfa);
	}

	//シーン遷移管理
	public void NextScene()
	{
		volume2.IsNextScene = true;
	}
	//ボタン押したときの音
	public void PushButtonSE_2()
	{
		sound2.PlaySe(pushButtonSE);
	}
}