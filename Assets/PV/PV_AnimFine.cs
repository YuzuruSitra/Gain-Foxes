using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PV_AnimFine : MonoBehaviour
{
    public static bool FineWalk = false;
    private Animator animator;
    public Transform DestPos;//目的地となるオブジェクトのトランスフォーム格納用
    private UnityEngine.AI.NavMeshAgent agent1;     //エージェントとなるオブジェクトのNavMeshAgent格納用
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent1 = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(FineWalk)
        {
            agent1.destination = DestPos.position;
            animator.SetBool("isWalk", true);

        }
    }


}
