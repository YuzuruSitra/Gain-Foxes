using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class otame : MonoBehaviour
{
    public static int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        i++;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("2");
    }
}
