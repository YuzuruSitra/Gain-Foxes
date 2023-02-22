using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトル忍者B演出管理
public class Title_NinjaB : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform ninjaB;    //目的地となるオブジェクトのトランスフォーム格納用
    private UnityEngine.AI.NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用
    
    void Start()
    {
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //目的地となる座標を設定する
        agent.destination = ninjaB.position;
        animator.SetBool("Go",true);
        animator.SetBool("FallDown",false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("NinjaCGoal"))
        {
            animator.SetBool("Go",false);
            animator.SetBool("FallDown",true);
        }
    }

}
