using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//民衆演出管理
public class PeopleAnim : MonoBehaviour
{
    //シーン２演出管理用
    [SerializeField] 
    private Volume2 volume2; 
    private Animator animator;
    [SerializeField]
    private Transform goalA;    //目的地格納用
    [SerializeField]
    private Transform goalB; 

    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
 
 	//サウンド用スクリプト取得
	[SerializeField] 
    private SoundCnt soundB;
    [SerializeField] 
    private AudioClip dropMoneySE;

	// Use this for initialization
	void Start () 
    {
        /*---bgm設定---*/
        soundB = GameObject.Find("SoundManager").GetComponent<SoundCnt> ();
		//コンポーネント取得・利用
		volume2 = GameObject.Find("SceneManager_B").GetComponent<Volume2> ();
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //目的地となる座標を設定する
        agent.destination = goalA.position;
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GoalA"))
        {
            StartCoroutine("AnimCont");
        }

        if (other.CompareTag("GoalB"))
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator AnimCont()
    {
        volume2.ShuAnim++;
        GetComponent<NavMeshAgent>().isStopped = true;
        animator.SetBool("isFront", true);
        yield return new WaitForSeconds(0.5f);
        soundB.PlaySe(dropMoneySE);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isFront",false);
        GetComponent<NavMeshAgent>().isStopped = false;
        agent.destination = goalB.position;
    }
	
}

