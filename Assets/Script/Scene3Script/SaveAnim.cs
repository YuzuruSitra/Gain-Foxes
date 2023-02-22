using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//セーブ演出用
public class SaveAnim : MonoBehaviour
{
    private string saving;
	[SerializeField] 
    private Text saveText;
	[SerializeField] 
    private GameObject saveTextPanel;
    private int iSave;
    private int goBreak = 0;

    void Start()
    {
        saveTextPanel.gameObject.SetActive(true);
        InvokeRepeating("ChangeText", 0.1f, 0.6f);
        saving = "Saving";
        iSave = 0;
    }

    void Update()
    {
        saveText.text = saving;
    }

    void ChangeText()
    {
        switch (iSave)
        {
            case 0:
                saving = "Saving";
                iSave++;
                break;

            case 1:
                saving = "Saving .";
                iSave++;
                break;

            case 2:
                saving = "Saving ..";
                iSave++;
                break;

            case 3:
                saving = "Saving ...";
                iSave = 0;
                goBreak++;
                if(goBreak == 2)
                {
                    saveTextPanel.gameObject.SetActive(false);
                }
                break;    
        }
    }
}
