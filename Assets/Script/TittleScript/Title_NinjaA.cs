using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Title_NinjaA : MonoBehaviour
{
    private Animator animator;
    public Transform Ta;//目的地となるオブジェクトのトランスフォーム格納用
    public Transform Tb; 

    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
 
	// Use this for initialization
	void Start () 
    {
    animator = GetComponent<Animator>();
    //エージェントのNaveMeshAgentを取得する
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    //目的地となる座標を設定する
    agent.destination = Ta.position;
    animator.SetBool("isLeft",false);
    animator.SetBool("isRight",true);
	}

    void update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("GoalA"))
       {
        animator.SetBool("isRight",false);
        animator.SetBool("isLeft",true);
        agent.destination = Tb.position;
       }

        if(other.CompareTag("GoalB"))
       {
        animator.SetBool("isLeft",false);
        animator.SetBool("isRight",true);
        agent.destination = Ta.position;
       }

    }

}
