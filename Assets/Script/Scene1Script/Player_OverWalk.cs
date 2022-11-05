using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_OverWalk : MonoBehaviour
{
    private Animator animator;
    private float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UIContA.doOverAnim)
        {
            StartFinAnim();
        }         
    }

    private void StartFinAnim()
    {
        Vector2 position = transform.position;
        position.x += speed + Time.deltaTime;
        transform.position = position;
        if(position.x >2500)
        {
            Destroy(this.gameObject);
        }
    }
}
