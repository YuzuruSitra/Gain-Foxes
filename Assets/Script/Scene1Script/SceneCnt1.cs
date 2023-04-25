using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//シーン1画面遷移管理
public class SceneCnt1 : MonoBehaviour
{
	//UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    //セーブ管理用
    [SerializeField] 
    private SaveControl saveControl; 
	//メニュー管理スクリプトの取得
    [SerializeField]
    private MenuCnt _menuCnt;

	private float fadeSpeed = 0.8f;        //透明度が変わるスピードを管理
	private float red, green, blue, alfa;   //パネルの色、不透明度を管理
	public bool IsFadeOut_A = false;  //フェードアウト処理の開始、完了を管理するフラグ
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
    [SerializeField]
    private GameObject fadePanel_A;
    [SerializeField]
    private GameObject nowLoadUI_A;

	private Image fadeImage_A;                 //透明度を変更するパネルのイメージ

	void Start()
	{
		fadePanel_A.gameObject.SetActive(true);
		isFadeIn = true;
		//コンポーネント取得・利用
	    saveControl = GameObject.Find("SaveManager").GetComponent<SaveControl> ();
        uiCont1 = GameObject.Find("UICont").GetComponent<UICont1> ();
		fadeImage_A = fadePanel_A.GetComponent<Image>();

		//シーン1のロードパネル用
		nowLoadUI_A.gameObject.SetActive(false);

		red = fadeImage_A.color.r;
		green = fadeImage_A.color.g;
		blue = fadeImage_A.color.b;
		alfa = fadeImage_A.color.a;
	}

	void Update()
	{

		if (isFadeIn)
		{
			StartFadeIn();
		}

		if (IsFadeOut_A)
		{
			if (uiCont1.GameOverFade)
			{
				GameOverFin();
			}
			else
            {
				StartFadeOut();
			}
		}
	}

	void StartFadeIn()
	{
		_menuCnt.FadeNow1 = true;
		alfa -= fadeSpeed * Time.deltaTime;               
		SetAlpha();                 
		if (alfa <= 0)
		{             
			_menuCnt.FadeNow1 = false;
			isFadeIn = false;
			fadeImage_A.enabled = false;   
		}
	}

	void StartFadeOut()
	{
		_menuCnt.FadeNow1 = true;
		fadeImage_A.enabled = true; 
		alfa += fadeSpeed * Time.deltaTime;        
		SetAlpha();            
		if (alfa >= 1)
		{      
			_menuCnt.FadeNow1 = false;
			isFadeIn = false;      
			IsFadeOut_A = false;
			//戦略シーンの画面遷移のみロードを挟む
			StartCoroutine("NowLoad");
		}
	}

	void SetAlpha()
	{
		fadeImage_A.color = new Color(red, green, blue, alfa);
	}

	//ナウローディング描画
	private IEnumerator NowLoad()
	{
		yield return new WaitForSeconds(0.2f);
		nowLoadUI_A.gameObject.SetActive(true);
		yield return new WaitForSeconds(3.0f);
		NextScene();
	}

	//シーン遷移管理
	public void NextScene()
	{
		SceneManager.LoadScene("Scene2");
	}

	//ゲームクリア時のタイトル移動
	public void ClearReturnTitle()
	{
		SceneManager.LoadScene("Title");
		ParameterCalc.instanceCalc.GameClear = false;
		ParameterCalc.instanceCalc.GameOver = false;

		saveControl.ClearDateSave();
		//クリアアニメ用
		uiCont1.PlClearDoAnim = false;
		uiCont1.ClearAnim = false;
	}

	//ゲームオーバーの処理
	private void GameOverFin()
    {
		fadeImage_A.enabled = true;  
		alfa += fadeSpeed * Time.deltaTime;      
		SetAlpha();            
		if (alfa >= 1)
		{             
			IsFadeOut_A = false;
			//戦略シーンの画面遷移のみロードを挟む
			uiCont1.DoOverAnim = true;
		}
	}

}

