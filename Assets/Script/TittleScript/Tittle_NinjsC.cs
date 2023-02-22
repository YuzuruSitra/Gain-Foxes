using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//タイトル忍者C演出管理
public class Tittle_NinjsC : MonoBehaviour
{
    private bool change;
    private Animator animator;
    private SpriteRenderer mesh;

    void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("ChangeAnim",1.0f,3.0f);
        mesh = GetComponent<SpriteRenderer>();
        mesh.material.color = mesh.material.color - new Color32(0,0,0,0);
    }

    void ChangeAnim()
    {
        if(change)
        {
            animator.SetBool("Appear",false);
            animator.SetBool("disApear",true);
            change = false;
        }
        else
        {
            animator.SetBool("Appear",true);
            animator.SetBool("disApear",false);
            change = true;
        }
    }

}
