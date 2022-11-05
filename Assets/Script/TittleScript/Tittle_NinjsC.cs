using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tittle_NinjsC : MonoBehaviour
{
    private bool C;
    private Animator animator;
    SpriteRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
    animator = GetComponent<Animator>();
    InvokeRepeating("ChangeAnim",1.0f,3.0f);
    mesh = GetComponent<SpriteRenderer>();
    mesh.material.color = mesh.material.color - new Color32(0,0,0,0);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeAnim()
    {
        if(C)
        {
        animator.SetBool("Appear",false);
        animator.SetBool("disApear",true);
        C = false;
        }
        else
        {
        animator.SetBool("Appear",true);
        animator.SetBool("disApear",false);
        C = true;
        }
    }

}
