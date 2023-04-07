using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguageTitle : MonoBehaviour
{
    [SerializeField]
    private Text[] _UIs = new Text[11];
    
    public void ChangeUI(string[] stringData)
    {
        for(int i= 0; i < 11; i++)
        {
            _UIs[i].text = stringData[i];
        }
    }
}
