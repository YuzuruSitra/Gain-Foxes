using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1_NinC : MonoBehaviour
{
    private Animator animator;
    private bool onetime3;
    // Start is called before the first frame update
    void Start()
    {
        onetime3 = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ParameterCalc.GameClear && ParameterCalc.TurnCount == ParameterCalc.PopTurnEvent)
        {
            animator.SetBool("Appear", true);
            animator.SetBool("disApear", false);
            

            if (UIContA.ClearAnim)
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);
                
            }
        }
        else if (ParameterCalc.GameOver && ParameterCalc.TurnCount == ParameterCalc.PopTurnEvent)
        {
            animator.SetBool("Appear", true);
            animator.SetBool("disApear", false);
            

            if (UIContA.ClearAnim )
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);
               
            }
        }
        else
        {
            if (UIContA.PushN == 7 && onetime3)
            {
                animator.SetBool("Appear", true);
                animator.SetBool("disApear", false);
                onetime3 = false;
            }
            else if (UIContA.PushN == 10 && !onetime3)
            {
                animator.SetBool("Appear", false);
                animator.SetBool("disApear", true);
                onetime3 = true;
            }
        }
    }
}
