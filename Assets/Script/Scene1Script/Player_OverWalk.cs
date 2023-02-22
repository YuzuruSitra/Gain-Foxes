using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ゲームオーバー時のアニメーション管理
public class Player_OverWalk : MonoBehaviour
{
    //UI管理スクリプトの取得
    [SerializeField]
    private UICont1 uiCont1;
    private Animator animator;
    private float speed = 0.9f;

    void Start()
    {
        //コンポーネント取得
        uiCont1 = GameObject.Find("UICont").GetComponent<UICont1> ();
    }

    void Update()
    {
        if(uiCont1.DoOverAnim)
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
