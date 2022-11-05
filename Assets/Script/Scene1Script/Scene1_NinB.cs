using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scene1_NinB : MonoBehaviour
{
    private bool onetime2;
    private Animator animator;
    public Transform S1Tb1;//目的地となるオブジェクトのトランスフォーム格納用
    public Transform S1Tb2;
    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
 
    // Start is called before the first frame update
    void Start()
    {
    onetime2 = true;
    animator = GetComponent<Animator>();
    //エージェントのNaveMeshAgentを取得する
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ParameterCalc.GameClear)
        {
            if (UIContA.ClearAnim && onetime2)
            {
                //UIContA.ClearAnim = false;
                AwayStr2();
                onetime2 = false;
            }
        }
        else if (ParameterCalc.GameOver)
        {
            if (UIContA.ClearAnim && onetime2)
            {
                //UIContA.ClearAnim = false;
                AwayStr2();
                onetime2 = false;
            }
        }
        else
        {
            if (UIContA.PushN == 7 && onetime2)
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
