using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//タイトル忍者A演出管理
public class Title_NinjaA : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform targetA;//目的地となるオブジェクトのトランスフォーム格納用
    [SerializeField]
    private Transform targetB; 
    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
 
	void Start () 
    {
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //目的地となる座標を設定する
        agent.destination = targetA.position;
        animator.SetBool("isLeft",false);
        animator.SetBool("isRight",true);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GoalA"))
        {
            animator.SetBool("isRight",false);
            animator.SetBool("isLeft",true);
            agent.destination = targetB.position;
        }

        if(other.CompareTag("GoalB"))
        {
            animator.SetBool("isLeft",false);
            animator.SetBool("isRight",true);
            agent.destination = targetA.position;
        }
    }
}
