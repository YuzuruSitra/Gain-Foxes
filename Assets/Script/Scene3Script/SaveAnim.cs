using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveAnim : MonoBehaviour
{
    private string Saving;
    public Text Savetext;
    public GameObject SaveText;
    private int iSave;
    private int goBreak = 0;
    // Start is called before the first frame update
    void Start()
    {
        SaveText.gameObject.SetActive(true);
        InvokeRepeating("ChangeText", 0.1f, 0.6f);
        Saving = "Saving";
        iSave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Savetext.text = Saving;
    }
    void ChangeText()
    {
        switch (iSave)
        {
            case 0:
                Saving = "Saving";
                iSave++;
                break;

            case 1:
                Saving = "Saving .";
                iSave++;
                break;

            case 2:
                Saving = "Saving ..";
                iSave++;
                break;

            case 3:
                Saving = "Saving ...";
                iSave = 0;
                goBreak++;
                if(goBreak == 2)
                {
                    SaveText.gameObject.SetActive(false);
                }
                break;
            
        }
    }
}
