using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PV_AnimFine : MonoBehaviour
{
    public static bool FineWalk = false;
    private Animator animator;
    public Transform DestPos;//�ړI�n�ƂȂ�I�u�W�F�N�g�̃g�����X�t�H�[���i�[�p
    private UnityEngine.AI.NavMeshAgent agent1;     //�G�[�W�F���g�ƂȂ�I�u�W�F�N�g��NavMeshAgent�i�[�p
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //�G�[�W�F���g��NaveMeshAgent���擾����
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
