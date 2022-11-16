using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class hinminAnim : MonoBehaviour
{
    private Animator animator;
    public Transform Hingoala;//目的地となるオブジェクトのトランスフォーム格納用
    public Transform Hingoalb; 

    private NavMeshAgent Hinagent;     //エージェントとなるオブジェクトのNavMeshAgent格納用 
  	//サウンド用スクリプト取得
	[SerializeField] 
    private soundCnt soundBhin;
    [SerializeField] 
    private AudioClip dropMoneySE;


	// Use this for initialization
	void Start () 
    {
    /*---bgm設定---*/
    soundBhin = GameObject.Find("SoundManager").GetComponent<soundCnt> ();

    animator = GetComponent<Animator>();
    //エージェントのNaveMeshAgentを取得する
    Hinagent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    //目的地となる座標を設定する
    Hinagent.destination = Hingoala.position;
	}

    void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("GoalA"))
       {
            StartCoroutine("HinAnimCont");
       }

        if (other.CompareTag("GoalB"))
        {
            Destroy(this.gameObject);
            Debug.Log("a");
        }
    }

    IEnumerator HinAnimCont()
    {
        Volume_B.ShuAnim++;
        GetComponent<NavMeshAgent>().isStopped = true;
        animator.SetBool("isFront", true);
        yield return new WaitForSeconds(0.5f);
        soundBhin.PlaySe(dropMoneySE);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("isFront",false);
        GetComponent<NavMeshAgent>().isStopped = false;
        yield return new WaitForSeconds(1.0f);
        if(ParameterCalc.PoorDebt)
        {
            animator.SetBool("isFall",true);
            yield return new WaitForSeconds(2.0f);
            animator.SetBool("isFall",false);
        }
        GetComponent<NavMeshAgent>().isStopped = false;
        Hinagent.destination = Hingoalb.position;
    }
}
