using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//クリア時の主人公演出管理
public class Player_ClearWalk : MonoBehaviour
{
    //UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    private Animator animator;
    [SerializeField]
    private Transform playerTa;//目的地となるオブジェクトのトランスフォーム格納用
    private UnityEngine.AI.NavMeshAgent agent;  //エージェントとなるオブジェクトのNavMeshAgent格納用 

    void Start()
    {
        //コンポーネント取得
        uiCont1 = GameObject.Find("UICont").GetComponent<UICont1> ();
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetBool("IsIdole", true);
    }

    void Update()
    {
        if(uiCont1.PlClearDoAnim)
        {
            AwayPlayer();
            uiCont1.PlClearDoAnim = false;
        }
    }

    void AwayPlayer()
    {
        agent.destination = playerTa.position;
        animator.SetBool("IsIdole", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlTA"))
        {
            uiCont1.FinClearAnim = true;
            Destroy(this.gameObject);
        }
    }
}
