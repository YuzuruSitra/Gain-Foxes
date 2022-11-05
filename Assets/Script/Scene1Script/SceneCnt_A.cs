using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneCnt_A : MonoBehaviour
{
	public float fadeSpeed = 0.8f;        //透明度が変わるスピードを管理
	public static float red, green, blue, alfa;   //パネルの色、不透明度を管理
	public static bool isFadeOut_A = false;  //フェードアウト処理の開始、完了を管理するフラグ
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
	public GameObject fadePanel_A;
	public GameObject NowLoadUI_A;

	private Image fadeImage_A;                 //透明度を変更するパネルのイメージ

	void Start()
	{
		fadePanel_A.gameObject.SetActive(true);
		isFadeIn = true;
		//コンポーネント取得・利用
		fadeImage_A = fadePanel_A.GetComponent<Image>();

		//シーン1のロードパネル用
		NowLoadUI_A.gameObject.SetActive(false);

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

		if (isFadeOut_A)
		{
			if (UIContA.GameOverFade)
			{
				GameOverFin();
				Debug.Log("aaaaaaaa");
			}
			else
            {
				StartFadeOut();
			}
		}
	}

	void StartFadeIn()
	{
		alfa -= fadeSpeed * Time.deltaTime;                //a)不透明度を徐々に下げる
		SetAlpha();                      //b)変更した不透明度パネルに反映する
		if (alfa <= 0)
		{                    //c)完全に透明になったら処理を抜ける
			isFadeIn = false;
			fadeImage_A.enabled = false;    //d)パネルの表示をオフにする
		}
	}

	void StartFadeOut()
	{
		fadeImage_A.enabled = true;  // a)パネルの表示をオンにする
		alfa += fadeSpeed * Time.deltaTime;         // b)不透明度を徐々にあげる
		SetAlpha();               // c)変更した透明度をパネルに反映する
		if (alfa >= 1)
		{             // d)完全に不透明になったら処理を抜ける
			isFadeOut_A = false;
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
		NowLoadUI_A.gameObject.SetActive(true);
		yield return new WaitForSeconds(3.0f);
		NextScene();
	}

	//シーン遷移管理
	public void NextScene()
	{
		SceneManager.LoadScene("Scene_B");
	}


	//ゲームクリア時のタイトル移動
	public void ClearReturnTitle()
	{
		SceneManager.LoadScene("Title");
		ParameterCalc.GameClear = false;
		ParameterCalc.GameOver = false;
		//クリアアニメ用
		UIContA.PlClearDoAnim = false;
		UIContA.ClearAnim = false;
	}

	//ゲームオーバーの処理
	private void GameOverFin()
    {
		fadeImage_A.enabled = true;  // a)パネルの表示をオンにする
		alfa += fadeSpeed * Time.deltaTime;         // b)不透明度を徐々にあげる
		SetAlpha();               // c)変更した透明度をパネルに反映する
		if (alfa >= 1)
		{             // d)完全に不透明になったら処理を抜ける
			isFadeOut_A = false;
			//戦略シーンの画面遷移のみロードを挟む
			UIContA.doOverAnim = true;
		}
	}

}

