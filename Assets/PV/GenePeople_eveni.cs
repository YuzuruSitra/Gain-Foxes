using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenePeople_eveni : MonoBehaviour
{
    //���i����ւ��f��
    public Sprite BrSword;
    public Sprite Potion;
    public Sprite Stock;
    //���i�i�[��
    public GameObject Toolflame;
    private SpriteRenderer ToolFlame;

    //���O�����p
    public GameObject[] PeopleIns = new GameObject[4];
    private int Cp;

    //��l���A�j���p
    public static int ShuAnim;
    public GameObject TalkPanel1;
    public GameObject TalkPanel2;


    // Start is called before the first frame update
    void Start()
    {
        /* �z���̏��i����ւ� */

        //�R���|�[�l���g�擾
        ToolFlame = Toolflame.GetComponent<SpriteRenderer>();
        //�o�͉摜�I��
        switch (ParameterCalc.ToolType)
        {
            case 0: //��
                ToolFlame.sprite = BrSword;
                break;

            case 1: //��
                ToolFlame.sprite = Potion;
                break;

            case 2: //��
                ToolFlame.sprite = Stock;
                break;

        }

        InvokeRepeating("AnimCnt",1.0f,1.0f);
        /*--�f�o�b�O�p--*/
        
        ParameterCalc.GenePeopleCount = 4;
        ParameterCalc.GenePeopleType[0] = 0;
        ParameterCalc.GenePeopleType[1] = 1;
        ParameterCalc.GenePeopleType[2] = 2;
        ParameterCalc.GenePeopleType[3] = 3;
        

        /*-------------------------*/

        Cp = 0;
        //StartCoroutine("GenePeople");

        //��l����b�A�j���p
        TalkPanel1.gameObject.SetActive(false);
        TalkPanel2.gameObject.SetActive(false);
        ShuAnim = 0;
    }

    void AnimCnt()
    {
        ShuAnim++;
        if(ShuAnim > 4)
        {
            ShuAnim = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //��l����b�A�j���p
        if (ShuAnim > 3)
        {
            TalkPanel1.gameObject.SetActive(false);
            TalkPanel2.gameObject.SetActive(false);

        }
        else if (ShuAnim > 0)
        {
            if (ShuAnim == 2)
            {
                TalkPanel2.gameObject.SetActive(true);
                TalkPanel1.gameObject.SetActive(true);
            }
            else if (ShuAnim % 2 == 1)
            {
                TalkPanel2.gameObject.SetActive(false);
                TalkPanel1.gameObject.SetActive(true);
            }
            else
            {
                TalkPanel1.gameObject.SetActive(false);
                TalkPanel2.gameObject.SetActive(true);
            }
        }
    }
    private IEnumerator GenePeople()
    {
        while (Cp <= 3)
        {
            //����
            Instantiate(PeopleIns[Cp], this.transform.position, Quaternion.identity);
            Cp++;
            yield return new WaitForSeconds(2.5f);
        }

    }

    //�V�[���J�ڗp
    void GoFade()
    {
        SceneCnt_B.isFadeOut_B = true;
    }
}
