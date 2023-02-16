using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//忍者Bの演出管理
public class Scene1_NinB : MonoBehaviour
{
    private bool onetime2;
    private Animator animator;
    [SerializeField]
    private Transform S1Tb1;     //目的地格納用
    [SerializeField]
    private Transform S1Tb2;
    private NavMeshAgent agent;  
 
    void Start()
    {
        onetime2 = true;
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (ParameterCalc.instanceCalc.GameClear)
        {
            if (UICont1.instanceUI1.ClearAnim && onetime2)
            {
                AwayStr2();
                onetime2 = false;
            }
        }
        else if (ParameterCalc.instanceCalc.GameOver)
        {
            if (UICont1.instanceUI1.ClearAnim && onetime2)
            {
                AwayStr2();
                onetime2 = false;
            }
        }
        else
        {
            if (UICont1.instanceUI1.PushN == 7 && onetime2)
            {
                AwayStr2();
                onetime2 = false;
            }
        }
    }

    void AwayStr2()
    {
        agent.destination = S1Tb1.position;
        animator.SetBool("Go",true);
        animator.SetBool("FallDown",false);
    }

    void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("S1Tb1"))
       {
        agent.destination = S1Tb2.position;
        animator.SetBool("Go",false);
        animator.SetBool("FallDown",true);
       }
        if(other.CompareTag("S1Tb2"))
       {
        Destroy(this.gameObject);
       }
    }

}
