using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PV_NinBAnim : MonoBehaviour
{

    private Animator animator;
    public Transform NinjaB1;//�ړI�n�ƂȂ�I�u�W�F�N�g�̃g�����X�t�H�[���i�[�p
    public Transform NinjaB2;//�ړI�n�ƂȂ�I�u�W�F�N�g�̃g�����X�t�H�[���i�[�p
    public Transform NinjaB3;//�ړI�n�ƂȂ�I�u�W�F�N�g�̃g�����X�t�H�[���i�[�p
    private UnityEngine.AI.NavMeshAgent agent;     //�G�[�W�F���g�ƂȂ�I�u�W�F�N�g��NavMeshAgent�i�[�p 
    public GameObject NinBSayBox;
    private string[] NinBsay = new string[2];
    public Text NinBtextBox;
    private int i;
    bool fa = true;
    public Image Title;
    public Image Titleback;
    float red1, green1, blue1, alfa1;   //�p�l���̐F�A�s�����x���Ǘ�
    float red2, green2, blue2, alfa2;   //�p�l���̐F�A�s�����x���Ǘ�
    bool isFadeIn1 = false;
    bool isFadeIn2 = false;

    //public Camera pvCam;
    //float maxFOV = 60;
    //float minFOV = 35;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //�G�[�W�F���g��NaveMeshAgent���擾����
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Invoke("PVstar", 10.0f);
        animator.SetBool("Go", true);
        animator.SetBool("pvFall", false);
        NinBsay[0] = " お か し ら ！ \n 商 い お つ か れ さ ま ！ ";
        NinBsay[1] = " あ っ ！ \n 待 っ て く だ さ い よ ～ ";


        red1 = Title.color.r;
        green1 = Title.color.g;
        blue1 = Title.color.b;
        alfa1 = 0;


        red2 = Titleback.color.r;
        green2 = Titleback.color.g;
        blue2 = Titleback.color.b;
        alfa2 = 0;

    
    }

    void PVstar()
    {
        //�ړI�n�ƂȂ���W��ݒ肷��
        agent.destination = NinjaB1.position;
    }


    // Update is called once per frame
    void Update()
    {
        if(isFadeIn1)
        {
            StartTitle();
        }

        if(isFadeIn2)
        {
            StartTitleBack();
        }
    }
    //��ʐ���
    IEnumerator NinBanim()
    {

        yield return new WaitForSeconds(1.0f);
        animator.SetBool("pvStay", true);
        yield return new WaitForSeconds(0.5f);
        NinBSayBox.gameObject.SetActive(true);
        NinBtextBox.text = "";
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ChangeSay());
        i++;
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(ChangeSay());
        yield return new WaitForSeconds(2.0f);
        agent.destination = NinjaB2.position;
        NinBSayBox.gameObject.SetActive(false);
        animator.SetBool("Go", true);
        animator.SetBool("pvStay", false);

        yield return new WaitForSeconds(4.5f);
        animator.SetBool("Go", true);
        animator.SetBool("pvFall", false);
        agent.destination = NinjaB3.position;
        yield return new WaitForSeconds(2.0f);
        isFadeIn1 = true;
        isFadeIn2 = true;
    }

    //�e�L�X�g����ւ�
    IEnumerator ChangeSay()
    {
        NinBtextBox.text = "";

        string[] words;
        // ���p�X�y�[�X�ŕ����𕪊�����B
        words = NinBsay[i].Split(' ');

        foreach (var word in words)
        {

            // 0.1�b���݂łP�������\������B
            NinBtextBox.text = NinBtextBox.text + word;
            yield return new WaitForSeconds(0.08f);

        }
        PV_AnimFine.FineWalk = true;
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("GoalA"))
        {
            if (fa)
            {
                StartCoroutine(NinBanim());
                fa = false;
            }
        }

        if (other.CompareTag("GoalB"))
        {
            animator.SetBool("Go", false);
            animator.SetBool("pvFall", true);

        }
    }

    void StartTitle()
    {
        alfa1 += 0.25f * Time.deltaTime;                //a)�s�����x�����X�ɉ�����
        SetAlpha1();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa1 >= 0.84)
        {                    //c)���S�ɓ����ɂȂ����珈���𔲂���
            isFadeIn1 = false ;
        }
    }

    void StartTitleBack()
    {
        alfa2 += 0.3f * Time.deltaTime;                //a)�s�����x�����X�ɉ�����
        SetAlpha2();                      //b)�ύX�����s�����x�p�l���ɔ��f����
        if (alfa2 >= 1)
        {                    //c)���S�ɓ����ɂȂ����珈���𔲂���
            isFadeIn2 = false;
        }
    }

    void SetAlpha1()
    {
        Title.color = new Color(red1, green1, blue1, alfa1);
    }

    void SetAlpha2()
    {
        Titleback.color = new Color(red2, green2, blue2, alfa2);
    }

}
