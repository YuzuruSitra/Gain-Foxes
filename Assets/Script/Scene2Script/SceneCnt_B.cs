using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneCnt_B: MonoBehaviour
{
	public float fadeSpeed = 0.4f;        //透明度が変わるスピードを管理
	public static float red, green, blue, alfa;   //パネルの色、不透明度を管理
	public static bool isFadeOut_B = false;  //フェードアウト処理の開始、完了を管理するフラグ
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
	public GameObject fadePanel_B;

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

		if (isFadeOut_B)
		{
			StartFadeOut();
		}
	}

	void StartFadeIn()
	{
		alfa -= fadeSpeed * Time.deltaTime;                //a)不透明度を徐々に下げる
		SetAlpha();                      //b)変更した不透明度パネルに反映する
		if (alfa <= 0)
		{                    //c)完全に透明になったら処理を抜ける
			isFadeIn = false;
			fadeImage_B.enabled = false;    //d)パネルの表示をオフにする
		}
	}

	public void StartFadeOut()
	{
		fadeImage_B.enabled = true;  // a)パネルの表示をオンにする
		alfa += fadeSpeed * Time.deltaTime;         // b)不透明度を徐々にあげる
		SetAlpha();               // c)変更した透明度をパネルに反映する
		if (alfa >= 1)
		{             // d)完全に不透明になったら処理を抜ける
			isFadeOut_B = false;
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
		isFadeOut_B = true;
	}
}