using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ClearWalk : MonoBehaviour
{
    private Animator animator;
    public Transform PlayerTa;//目的地となるオブジェクトのトランスフォーム格納用
    private UnityEngine.AI.NavMeshAgent agent;  //エージェントとなるオブジェクトのNavMeshAgent格納用 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetBool("IsIdole", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(UIContA.PlClearDoAnim)
        {
            AwayPlayer();

            UIContA.PlClearDoAnim = false;
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
            UIContA.FinClearAnim = true;
            Destroy(this.gameObject);
        }
    }
}
