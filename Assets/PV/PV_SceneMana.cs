using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PV_SceneMana : MonoBehaviour
{
    private int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SceneChange();
        }
    }

    void SceneChange()
    {
        switch (i)
        {
            case 0:
                SceneManager.LoadScene("PV_Title");
                break;

            case 1:
                SceneManager.LoadScene("PV_eveni");
                break;

            case 2:
                SceneManager.LoadScene("PV_night");
                break;
        }
        i++;
    }
}
