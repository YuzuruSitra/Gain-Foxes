using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//シーン2の遷移管理
public class SceneCnt2: MonoBehaviour
{
	private float fadeSpeed = 0.4f;        //透明度が変わるスピードを管理
	private float red, green, blue, alfa;   //パネルの色、不透明度を管理
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
	[SerializeField]
	private GameObject fadePanel_B;
	private Image fadeImage_B;                //透明度を変更するパネルのイメージ


	void Start()
	{
		fadePanel_B.gameObject.SetActive(true);
		isFadeIn = true;
		//コンポーネント取得・利用
		fadeImage_B = fadePanel_B.GetComponent<Image>();

		red = fadeImage_B.color.r;
		green = fadeImage_B.color.g;
		blue = fadeImage_B.color.b;
		alfa = fadeImage_B.color.a;
	}

	void Update()
	{
		if (isFadeIn)
		{
			StartFadeIn();
		}
		if (Volume2.instanceVolume2.isNextScene)
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
			fadeImage_B.enabled = false;  
		}
	}

	public void StartFadeOut()
	{
		fadeImage_B.enabled = true;
		alfa += fadeSpeed * Time.deltaTime; 
		SetAlpha();   
		if (alfa >= 1)
		{           
			Volume2.instanceVolume2.isNextScene = false;
			//シーンのロードを挟む
			SceneManager.LoadScene("Scene_C");
		}
	}

	void SetAlpha()
	{
		fadeImage_B.color = new Color(red, green, blue, alfa);
	}

	//シーン遷移管理
	public void NextScene()
	{
		Volume2.instanceVolume2.isNextScene = true;
	}
}