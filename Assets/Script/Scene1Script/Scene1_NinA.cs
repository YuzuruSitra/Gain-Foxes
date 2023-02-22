using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//忍者Aの演出管理
public class Scene1_NinA : MonoBehaviour
{
    //UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    private bool onetime1;
    private Animator animator;
    [SerializeField]
    private Transform s1Ta; //目的地となるオブジェクトのトランスフォーム格納用
    private NavMeshAgent agent; //エージェントとなるオブジェクトのNavMeshAgent格納用 

    void Start()
    {
        //コンポーネント取得
        uiCont1 = GameObject.Find("UICont").GetComponent<UICont1> ();

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
            if(uiCont1.ClearAnim && onetime1)
            {
                AwayStr();
                animator.SetBool("isRight", true);
            }
        }
        else if (ParameterCalc.instanceCalc.GameOver)
        {
            if (uiCont1.ClearAnim && onetime1)
            {
                AwayStr();
                animator.SetBool("isRight", true);
                onetime1 = false;
            }
        }
        else
        {
            if (uiCont1.PushN == 5 && onetime1)
            {
                AwayStr();
                onetime1 = false;
                animator.SetBool("isRight", true);
            }
        }
    }

    void AwayStr()
    {
        agent.destination = s1Ta.position;
    }


    void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("S1TA"))
       {
        Destroy(this.gameObject);
       }
    }

}
