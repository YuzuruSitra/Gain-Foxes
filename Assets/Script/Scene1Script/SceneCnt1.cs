using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//シーン1画面遷移管理
public class SceneCnt1 : MonoBehaviour
{
    //他スクリプトでも呼べるようにインスタンス化
    public static SceneCnt1 instanceCnt1;

	private float fadeSpeed = 0.8f;        //透明度が変わるスピードを管理
	private float red, green, blue, alfa;   //パネルの色、不透明度を管理
	public bool isFadeOut_A = false;  //フェードアウト処理の開始、完了を管理するフラグ
	private bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ
    [SerializeField]
    private GameObject fadePanel_A;
    [SerializeField]
    private GameObject NowLoadUI_A;

	private Image fadeImage_A;                 //透明度を変更するパネルのイメージ

	void Awake()
	{
		if (instanceCnt1 == null)
        {
            instanceCnt1 = this;
        }
	}

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
			if (UICont1.instanceUI1.GameOverFade)
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
		alfa -= fadeSpeed * Time.deltaTime;               
		SetAlpha();                 
		if (alfa <= 0)
		{             
			isFadeIn = false;
			fadeImage_A.enabled = false;   
		}
	}

	void StartFadeOut()
	{
		fadeImage_A.enabled = true; 
		alfa += fadeSpeed * Time.deltaTime;        
		SetAlpha();            
		if (alfa >= 1)
		{            
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
		ParameterCalc.instanceCalc.GameClear = false;
		ParameterCalc.instanceCalc.GameOver = false;

		SaveControl.instanceSave.ClearDateSave();
		//クリアアニメ用
		UICont1.instanceUI1.PlClearDoAnim = false;
		UICont1.instanceUI1.ClearAnim = false;
	}

	//ゲームオーバーの処理
	private void GameOverFin()
    {
		fadeImage_A.enabled = true;  
		alfa += fadeSpeed * Time.deltaTime;      
		SetAlpha();            
		if (alfa >= 1)
		{             
			isFadeOut_A = false;
			//戦略シーンの画面遷移のみロードを挟む
			UICont1.instanceUI1.doOverAnim = true;
		}
	}

}

