using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Scene1_NinA : MonoBehaviour
{
    private bool onetime1;
    private Animator animator;
    public Transform S1Ta;//目的地となるオブジェクトのトランスフォーム格納用
    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
 
    // Start is called before the first frame update
    void Start()
    {
    onetime1 = true;
    animator = GetComponent<Animator>();
    //エージェントのNaveMeshAgentを取得する
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    animator.SetBool("isLeft",false);
    animator.SetBool("isRight",false);
    }

    // Update is called once per frame
    void Update()
    {
        if (ParameterCalc.GameClear)
        {
            if(UIContA.ClearAnim && onetime1)
            {
                //UIContA.ClearAnim = false;
                AwayStr();
                animator.SetBool("isRight", true);
            }
        }
        else if (ParameterCalc.GameOver)
        {
            if (UIContA.ClearAnim && onetime1)
            {
                //UIContA.ClearAnim = false;
                AwayStr();
                animator.SetBool("isRight", true);
                onetime1 = false;
            }
        }
        else
        {
            if (UIContA.PushN == 4 && onetime1)
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
