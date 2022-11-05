using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneCnt_C : MonoBehaviour
{
	public float fadeSpeed = 0.8f;        //透明度が変わるスピードを管理
	public static float red, green, blue, alfa;   //パネルの色、不透明度を管理
	public static bool isFadeOut_C = false;  //フェードアウト処理の開始、完了を管理するフラグ
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
	public GameObject fadePanel_C;

	private Image fadeImage_C;                //透明度を変更するパネルのイメージ


	void Start()
	{
		fadePanel_C.gameObject.SetActive(true);
		isFadeIn = true;
		//コンポーネント取得・利用
		fadeImage_C = fadePanel_C.GetComponent<Image>();

		red = fadeImage_C.color.r;
		green = fadeImage_C.color.g;
		blue = fadeImage_C.color.b;
		alfa = fadeImage_C.color.a;
	}

	void Update()
	{

		if (isFadeIn)
		{
			StartFadeIn();
		}

		if (isFadeOut_C)
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
			fadeImage_C.enabled = false;    //d)パネルの表示をオフにする
		}
	}

	public void StartFadeOut()
	{
		fadeImage_C.enabled = true;  // a)パネルの表示をオンにする
		alfa += fadeSpeed * Time.deltaTime;         // b)不透明度を徐々にあげる
		SetAlpha();               // c)変更した透明度をパネルに反映する
		if (alfa >= 1)
		{             // d)完全に不透明になったら処理を抜ける
			isFadeOut_C = false;

			//セーブ処理
			SaveControl.NewGame = false;
			if (ParameterCalc.GameClear || ParameterCalc.GameOver)
			{
				SaveControl.instanceSave.ClearDateSave();
			}
			else
            {
				SaveControl.instanceSave.Dosave();
			}
			SceneManager.LoadScene("Scene_A");
		}
	}

	void SetAlpha()
	{
		fadeImage_C.color = new Color(red, green, blue, alfa);
	}

	//シーン遷移管理
	public void NextScene()
	{
		isFadeOut_C = true;
	}
}