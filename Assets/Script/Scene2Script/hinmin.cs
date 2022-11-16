using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class hinmin : MonoBehaviour
{
    private Animator animator;
    public Transform goala;//目的地となるオブジェクトのトランスフォーム格納用
    public Transform goalb; 

    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
 
 	//サウンド用スクリプト取得
	[SerializeField] 
    private soundCnt soundB;
    [SerializeField] 
    private AudioClip dropMoneySE;

	// Use this for initialization
	void Start () 
    {
    /*---bgm設定---*/
    soundB = GameObject.Find("SoundManager").GetComponent<soundCnt> ();

    animator = GetComponent<Animator>();
    //エージェントのNaveMeshAgentを取得する
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    //目的地となる座標を設定する
    agent.destination = goala.position;
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
            Debug.Log("a");
        }
    }

    IEnumerator AnimCont()
    {
        //PV用後で消して下の奴に替える
        //GenePeople_eveni.ShuAnim++;
        Volume_B.ShuAnim++;
        GetComponent<NavMeshAgent>().isStopped = true;
        animator.SetBool("isFront", true);
        yield return new WaitForSeconds(0.5f);
        soundB.PlaySe(dropMoneySE);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isFront",false);
        GetComponent<NavMeshAgent>().isStopped = false;
        agent.destination = goalb.position;
    }
	
}

