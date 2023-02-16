using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームオーバー時のアニメーション管理
public class Player_OverWalk : MonoBehaviour
{
    private Animator animator;
    private float speed = 0.9f;

    void Update()
    {
        if(UICont1.instanceUI1.doOverAnim)
        {
            StartFinAnim();
        }         
    }

    private void StartFinAnim()
    {
        Vector2 position = transform.position;
        position.x += speed + Time.deltaTime;
        transform.position = position;
        if(position.x > 2500)
        {
            Destroy(this.gameObject);
        }
    }
}
