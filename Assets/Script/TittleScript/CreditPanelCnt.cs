using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPanelCnt : MonoBehaviour
{
    [SerializeField]
    private GameObject _creditPanel;
    private bool _openPanel;
    [SerializeField]

    void Start()
    {
        _openPanel = false;
        _creditPanel.SetActive(false);
    }

    public void SwichCreditPanel()
    {
        if(_openPanel)
        {
            _openPanel = false;
        }
        else
        {
            _openPanel = true;
        }
        _creditPanel.SetActive(_openPanel);
    }

    public void OpenHP()
    {
        Application.OpenURL("https://yuzurusitra.myportfolio.com/");
    }


}
