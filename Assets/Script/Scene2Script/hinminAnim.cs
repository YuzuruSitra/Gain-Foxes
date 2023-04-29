using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//貧民演出管理
public class HinminAnim : MonoBehaviour
{
    //シーン２演出管理用
    [SerializeField] 
    private Volume2 volume2; 
    [SerializeField]
    private AchievementManager _achievementManager;
    private Animator animator;
    [SerializeField]
    private Transform hinminGoalA;  //目的地格納用
    [SerializeField]
    private Transform hinminGoalB; 

    private NavMeshAgent hinminAgent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
  	//サウンド用スクリプト取得
	[SerializeField] 
    private SoundCnt soundBhin;
    [SerializeField] 
    private AudioClip dropMoneySE;

	void Start () 
    {
        /*---bgm設定---*/
        soundBhin = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
		//コンポーネント取得・利用
		volume2 = GameObject.Find("SceneManager_B").GetComponent<Volume2> ();
        animator = GetComponent<Animator>();
        // 実績用クラス
        _achievementManager = AchievementManager.Instance;
        //エージェントのNaveMeshAgentを取得する
        hinminAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //目的地となる座標を設定する
        hinminAgent.destination = hinminGoalA.position;
	}

    void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("GoalA"))
       {
            StartCoroutine("HinAnimCont");
       }

        if (other.CompareTag("GoalB"))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator HinAnimCont()
    {
        volume2.ShuAnim++;
        GetComponent<NavMeshAgent>().isStopped = true;
        animator.SetBool("isFront", true);
        yield return new WaitForSeconds(0.5f);
        soundBhin.PlaySe(dropMoneySE);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isFront",false);
        GetComponent<NavMeshAgent>().isStopped = false;
        yield return new WaitForSeconds(1.0f);
        if(volume2.Scene2SlaveCount > 0)
        {
            volume2.Scene2SlaveCount--;
            _achievementManager.statsAPIs["debtorCount"] += 1;
            animator.SetBool("isFall",true);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool("isFall",false);
        }
        GetComponent<NavMeshAgent>().isStopped = false;
        hinminAgent.destination = hinminGoalB.position;
    }
}
