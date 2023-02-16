using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//タイトル画面遷移管理
public class SceneCnt_Title : MonoBehaviour
{
	private float fadeSpeed = 0.8f;        //透明度が変わるスピードを管理
	private float red, green, blue, alfa;   //パネルの色、不透明度を管理
	private bool isFadeOut_Ti = false;  //フェードアウト処理の開始、完了を管理するフラグ
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
	[SerializeField]
	private GameObject fadePanel;
	[SerializeField]
	private GameObject settingPanel;
	[SerializeField]
	private GameObject confirmationPanel;
	[SerializeField]
	private GameObject doneDeletePanel;

	//サウンド用スクリプト取得
	[SerializeField] 
    private soundCnt sound;
	[SerializeField]
	private AudioClip titleBGM;
    [SerializeField] 
    private AudioClip pushButtonSE;

	private Image fadeImage;	//透明度を変更するパネルのイメージ

	void Start()
	{
		fadePanel.gameObject.SetActive(true);
		isFadeIn = true;
		//コンポーネント取得・利用
		fadeImage = fadePanel.GetComponent<Image>();

		red = fadeImage.color.r;
		green = fadeImage.color.g;
		blue = fadeImage.color.b;
		alfa = fadeImage.color.a;

		//パネルを閉じる
		settingPanel.gameObject.SetActive(false);
		confirmationPanel.gameObject.SetActive(false);
		doneDeletePanel.gameObject.SetActive(false);

     	/*---bgm設定---*/
        sound = GameObject.Find("SoundManager").GetComponent<soundCnt> ();

		//bgm再生
		sound.PlayBgm(titleBGM);
	}

	void Update()
	{

		if (isFadeIn)
		{
			StartFadeIn();
		}

		if (isFadeOut_Ti)
		{
			StartFadeOut();
		}
	}

	//ボタン押したときの音
	public void PushButtonSE()
	{
		sound.PlaySe(pushButtonSE);
	}

	void StartFadeIn()
	{
		alfa -= fadeSpeed * Time.deltaTime; 
		SetAlpha();                
		if (alfa <= 0)
		{                 
			isFadeIn = false;
			fadeImage.enabled = false;  
		}
	}

	public void StartFadeOut()
	{
		fadeImage.enabled = true;  
		alfa += fadeSpeed * Time.deltaTime; 
		SetAlpha();  
		if (alfa >= 1)
		{       
			isFadeOut_Ti = false;
			//シーンのロードを挟む
			SceneManager.LoadScene("Scene_A");
		}
	}

	//設定パネルオープン
	public void OpenSetPanel()
	{
		settingPanel.SetActive(true);		
	}
	//設定パネルクローズ
	public void CloseSetPanel()
	{
		settingPanel.SetActive(false);		
	}

	//削除パネルオープン
	public void OpenconfirmationPanel()
	{
		confirmationPanel.SetActive(true);
	}

	//削除パネルクローズ
	public void CloseconfirmationPanel()
	{
		confirmationPanel.SetActive(false);
	}

	//削除完了パネル
	public void OpenDeletePanel()
	{
		doneDeletePanel.SetActive(true);
	}

	//削除完了クローズ
	public void CloseDeletePanel()
	{
		settingPanel.SetActive(false);	
		confirmationPanel.SetActive(false);
		doneDeletePanel.SetActive(false);
	}

	void SetAlpha()
	{
		fadeImage.color = new Color(red, green, blue, alfa);
	}

	//シーン遷移管理
	public void NextScene()
	{
		isFadeOut_Ti = true;
	}
}