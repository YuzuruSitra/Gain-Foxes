using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PV_hinmin : MonoBehaviour
{
    private Animator animator;
    public Transform goala;//目的地となるオブジェクトのトランスフォーム格納用
    public Transform goalb;

    private NavMeshAgent agent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //エージェントのNaveMeshAgentを取得する
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //目的地となる座標を設定する
        agent.destination = goala.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoalA"))
        {
            StartCoroutine("AnimCont");
        }

        if (other.CompareTag("GoalB"))
        {
            Destroy(this.gameObject);
            Debug.Log("a");
        }
    }

    /*
    //人と当たった時の処理
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("InsPeople"))
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            Debug.Log("aaa");
        }
        Debug.Log("hit");
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("InsPeople"))
        {
            //GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
    */

    IEnumerator AnimCont()
    {

        //GenePeople_eveni.ShuAnim++;
        Volume_B.ShuAnim++;
        GetComponent<NavMeshAgent>().isStopped = true;
        animator.SetBool("isFall", true);
        yield return new WaitForSeconds(5.0f);
        animator.SetBool("isFront", false);
        GetComponent<NavMeshAgent>().isStopped = false;
        agent.destination = goalb.position;
    }

}
