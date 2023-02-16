using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//忍者Aの演出管理
public class Scene1_NinA : MonoBehaviour
{
    private bool onetime1;
    private Animator animator;
    [SerializeField]
    private Transform S1Ta;//目的地となるオブジェクトのトランスフォーム格納用
    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 

    void Start()
    {
        onetime1 = true;
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator.SetBool("isLeft",false);
        animator.SetBool("isRight",false);
    }

    void Update()
    {
        if (ParameterCalc.instanceCalc.GameClear)
        {
            if(UICont1.instanceUI1.ClearAnim && onetime1)
            {
                AwayStr();
                animator.SetBool("isRight", true);
            }
        }
        else if (ParameterCalc.instanceCalc.GameOver)
        {
            if (UICont1.instanceUI1.ClearAnim && onetime1)
            {
                AwayStr();
                animator.SetBool("isRight", true);
                onetime1 = false;
            }
        }
        else
        {
            if (UICont1.instanceUI1.PushN == 4 && onetime1)
            {
                AwayStr();
                onetime1 = false;
                animator.SetBool("isRight", true);
            }
        }
    }

    void AwayStr()
    {
        agent.destination = S1Ta.position;
    }


    void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("S1TA"))
       {
        Destroy(this.gameObject);
       }
    }

}
