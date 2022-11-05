using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_NinjaB : MonoBehaviour
{
    private Animator animator;
    public Transform NinjaB;//目的地となるオブジェクトのトランスフォーム格納用
    private UnityEngine.AI.NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //目的地となる座標を設定する
        agent.destination = NinjaB.position;
        animator.SetBool("Go",true);
        animator.SetBool("FallDown",false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
