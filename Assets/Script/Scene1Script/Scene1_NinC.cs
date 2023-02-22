using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//忍者Aの演出管理
public class Scene1_NinC : MonoBehaviour
{
    //UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    private Animator animator;
    private bool onetime3;

    void Start()
    {
        //コンポーネント取得
        uiCont1 = GameObject.Find("UICont").GetComponent<UICont1> ();
        onetime3 = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ParameterCalc.instanceCalc.GameClear && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent)
        {
            animator.SetBool("Appear", true);
            animator.SetBool("disApear", false);
            if (uiCont1.ClearAnim)
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);   
            }
        }
        else if (ParameterCalc.instanceCalc.GameOver && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent)
        {
            animator.SetBool("Appear", true);
            animator.SetBool("disApear", false);
            if (uiCont1.ClearAnim )
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);       
            }
        }
        else
        {
            if (uiCont1.PushN == 8 && onetime3)
            {
                animator.SetBool("Appear", true);
                animator.SetBool("disApear", false);
                onetime3 = false;
            }
            else if (uiCont1.PushN == 11 && !onetime3)
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);
                onetime3 = true;
            }
        }
    }
}
