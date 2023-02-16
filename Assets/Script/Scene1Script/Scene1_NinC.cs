using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//忍者Aの演出管理
public class Scene1_NinC : MonoBehaviour
{
    private Animator animator;
    private bool onetime3;
    
    void Start()
    {
        onetime3 = true;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (ParameterCalc.instanceCalc.GameClear && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent)
        {
            animator.SetBool("Appear", true);
            animator.SetBool("disApear", false);
            if (UICont1.instanceUI1.ClearAnim)
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);   
            }
        }
        else if (ParameterCalc.instanceCalc.GameOver && ParameterCalc.instanceCalc.TurnCount == ParameterCalc.instanceCalc.PopTurnEvent)
        {
            animator.SetBool("Appear", true);
            animator.SetBool("disApear", false);
            if (UICont1.instanceUI1.ClearAnim )
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);       
            }
        }
        else
        {
            if (UICont1.instanceUI1.PushN == 7 && onetime3)
            {
                animator.SetBool("Appear", true);
                animator.SetBool("disApear", false);
                onetime3 = false;
            }
            else if (UICont1.instanceUI1.PushN == 10 && !onetime3)
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);
                onetime3 = true;
            }
        }
    }
}
