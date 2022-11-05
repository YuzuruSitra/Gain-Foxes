using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PV_hinmin : MonoBehaviour
{
    private Animator animator;
    public Transform goala;//�ړI�n�ƂȂ�I�u�W�F�N�g�̃g�����X�t�H�[���i�[�p
    public Transform goalb;

    private NavMeshAgent agent;     //�G�[�W�F���g�ƂȂ�I�u�W�F�N�g��NavMeshAgent�i�[�p 

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        //�G�[�W�F���g��NaveMeshAgent���擾����
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //�ړI�n�ƂȂ���W��ݒ肷��
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
    //�l�Ɠ����������̏���
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
