using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//忍者Bの演出管理
public class Scene1_NinB : MonoBehaviour
{
    //UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    private bool onetime2;
    private Animator animator;
    [SerializeField]
    private Transform s1Tb1;     //目的地格納用
    [SerializeField]
    private Transform s1Tb2;
    private NavMeshAgent agent;  

    void Start()
    {
        //コンポーネント取得
        uiCont1 = GameObject.Find("UICont").GetComponent<UICont1> ();
        onetime2 = true;
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (ParameterCalc.instanceCalc.GameClear)
        {
            if (uiCont1.ClearAnim && onetime2)
            {
                AwayStr2();
                onetime2 = false;
            }
        }
        else if (ParameterCalc.instanceCalc.GameOver)
        {
            if (uiCont1.ClearAnim && onetime2)
            {
                AwayStr2();
                onetime2 = false;
            }
        }
        else
        {
            if (uiCont1.PushN == 8 && onetime2)
            {
                AwayStr2();
                onetime2 = false;
            }
        }
    }

    void AwayStr2()
    {
        agent.destination = s1Tb1.position;
        animator.SetBool("Go",true);
        animator.SetBool("FallDown",false);
    }

    void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("S1Tb1"))
       {
        agent.destination = s1Tb2.position;
        animator.SetBool("Go",false);
        animator.SetBool("FallDown",true);
       }
        if(other.CompareTag("S1Tb2"))
       {
        Destroy(this.gameObject);
       }
    }

}
