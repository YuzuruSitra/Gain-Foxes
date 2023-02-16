using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//クリア時の主人公演出管理
public class Player_ClearWalk : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private Transform PlayerTa;//目的地となるオブジェクトのトランスフォーム格納用
    private UnityEngine.AI.NavMeshAgent agent;  //エージェントとなるオブジェクトのNavMeshAgent格納用 

    void Start()
    {
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetBool("IsIdole", true);
    }

    void Update()
    {
        if(UICont1.instanceUI1.PlClearDoAnim)
        {
            AwayPlayer();
            UICont1.instanceUI1.PlClearDoAnim = false;
        }
    }

    void AwayPlayer()
    {
        agent.destination = PlayerTa.position;
        animator.SetBool("IsIdole", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlTA"))
        {
            UICont1.instanceUI1.FinClearAnim = true;
            Destroy(this.gameObject);
        }
    }
}
