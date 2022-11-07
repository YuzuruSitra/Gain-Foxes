using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;

public class SaveControl : MonoBehaviour
{
    public static bool NewGame;
    //public static bool LoadJ;
    private bool NewGame1;

    //���̃X�N���v�g���烁�\�b�h���Ăׂ�悤��
    public static SaveControl instanceSave;
    private int i;

    public void Awake()
    {
        if (instanceSave == null)
        {
            instanceSave = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // �f�[�^�̕ۑ����Application.dataPath�ɕύX
        QuickSaveGlobalSettings.StorageLocation = Application.dataPath;

        DoEncryption();
        CheckSave();
    }


    void DoEncryption()//�Í����p
    {
        // QuickSaveSettings�̃C���X�^���X���쐬-�Í����p
        QuickSaveSettings settings = new QuickSaveSettings();

        // �Í����̕��@ 
        settings.SecurityMode = SecurityMode.None;
        // Aes�̈Í����L�[
        settings.Password = "Pass";
        // ���k�̕��@
        settings.CompressionMode = CompressionMode.Gzip;
    }

    
    //���N�����ۂ��̏���
    void CheckSave()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬-�Í����p
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveReader�̃C���X�^���X���쐬-load�p
        QuickSaveReader reader = QuickSaveReader.Create("Player", settings);

        // �f�[�^��ǂݍ���


        NewGame = reader.Read<bool>("newGame");
    }


    //�^�[���I���Ɏ��s
    public void Dosave()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬-�Í����p
        QuickSaveSettings settings = new QuickSaveSettings();

        // QuickSaveWriter�̃C���X�^���X���쐬-save�p
        QuickSaveWriter writer = QuickSaveWriter.Create("Player",settings);


        // �f�[�^����������
        writer.Write("newGame", NewGame);
        writer.Write("turnCount", ParameterCalc.TurnCount);
        writer.Write("haveMoney", ParameterCalc.HaveMoney);
        writer.Write("crimeRate", ParameterCalc.CrimeRate);
        writer.Write("slave", ParameterCalc.Slave);
        writer.Write("poorMoney", ParameterCalc.PoorMoney);

        writer.Write("brSwordSell", ParameterCalc.BrSwordSell);
        writer.Write("brSwordUp", ParameterCalc.BrSwordUp);
        writer.Write("brSwordUpCount", ParameterCalc.BrSwordUpCount);

        writer.Write("potionGet", ParameterCalc.PotionGet);
        writer.Write("potionSell", ParameterCalc.PotionSell);
        writer.Write("potionUp", ParameterCalc.PotionUp);
        writer.Write("potionUpCount", ParameterCalc.PotionUpCount);
        writer.Write("havePotionJ", ParameterCalc.HavePotionJ);

        writer.Write("stockOpen", ParameterCalc.StockOpen);
        writer.Write("haveStockJ", ParameterCalc.HaveStockJ);
        writer.Write("stockQuantity", ParameterCalc.StockQuantity);

        writer.Write("resultScore0", ParameterCalc.ResultScore[0]);
        writer.Write("resultScore1", ParameterCalc.ResultScore[1]);
        writer.Write("resultScore2", ParameterCalc.ResultScore[2]);

        i = 0;
        //�����x��
        while (i <= ParameterCalc.TurnCount)
        {
            writer.Write("payCheck" + i, ParameterCalc.Paycheck[i]);
            i++;
        }
        // �ύX�𔽉f
        writer.Commit();
    }

    //�X�^�[�g��ʂŎ��s
    public void Doload()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬-�Í����p
        QuickSaveSettings settings = new QuickSaveSettings();
        // QuickSaveReader�̃C���X�^���X���쐬-load�p
        QuickSaveReader reader = QuickSaveReader.Create("Player", settings);
        
        ParameterCalc.TurnCount = reader.Read<int>("turnCount");
        ParameterCalc.HaveMoney = reader.Read<int>("haveMoney");
        ParameterCalc.CrimeRate = reader.Read<double>("crimeRate");
        ParameterCalc.Slave = reader.Read<int>("slave");
        ParameterCalc.PoorMoney = reader.Read<double>("poorMoney");

        ParameterCalc.BrSwordSell = reader.Read<int>("brSwordSell");
        ParameterCalc.BrSwordUp = reader.Read<int>("brSwordUp");
        ParameterCalc.BrSwordUpCount = reader.Read<int>("brSwordUpCount");

        ParameterCalc.PotionGet = reader.Read<int>("potionGet");
        ParameterCalc.PotionSell = reader.Read<int>("potionSell");
        ParameterCalc.PotionUp = reader.Read<int>("potionUp");
        ParameterCalc.PotionUpCount = reader.Read<int>("potionUpCount");
        ParameterCalc.HavePotionJ = reader.Read<bool>("havePotionJ");

        ParameterCalc.StockOpen = reader.Read<int>("stockOpen");
        ParameterCalc.HaveStockJ = reader.Read<bool>("haveStockJ");
        ParameterCalc.StockQuantity = reader.Read<int>("stockQuantity");

        ParameterCalc.ResultScore[0] = reader.Read<int>("resultScore0");
        ParameterCalc.ResultScore[1] = reader.Read<int>("resultScore1");
        ParameterCalc.ResultScore[2] = reader.Read<int>("resultScore2");

        i = 0;
        //�����x��
        while (i <= ParameterCalc.TurnCount)
        {
            ParameterCalc.Paycheck[i] = reader.Read<int>("payCheck" + i);
            i++;
        }
        
    }

    //�f�[�^�폜����
    public void DeleteDate()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬-�Í����p
        QuickSaveSettings settings = new QuickSaveSettings();

        // QuickSaveWriter�̃C���X�^���X���쐬-save�p
        QuickSaveWriter writer = QuickSaveWriter.Create("Player", settings);


        NewGame = true;

        // �f�[�^����������
        writer.Write("newGame", NewGame);

        //�N���A�X�R�A�̓��Z�b�g
        writer.Write("resultScore0", 0);
        writer.Write("resultScore1", 0);
        writer.Write("resultScore2", 0);

        // �ύX�𔽉f
        writer.Commit();
    }
    
    //�N���A�f�[�^�ۑ�����
    public void ClearDateSave()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬-�Í����p
        QuickSaveSettings settings = new QuickSaveSettings();

        // QuickSaveWriter�̃C���X�^���X���쐬-save�p
        QuickSaveWriter writer = QuickSaveWriter.Create("Player", settings);


        //�j���[�Q�[���ł͂Ȃ�
        NewGame = false;
        writer.Write("newGame", NewGame);

        // �f�[�^�������ɖ߂��ĕۑ�
        writer.Write("turnCount", 0);
        writer.Write("haveMoney", 1000);
        writer.Write("crimeRate", 0.0);
        writer.Write("slave", 0);
        writer.Write("poorMoney", 2000.0);

        writer.Write("brSwordSell", 400);
        writer.Write("brSwordUp", 800);
        writer.Write("brSwordUpCount", 1);

        writer.Write("potionGet", 500);
        writer.Write("potionSell", 1000);
        writer.Write("potionUp", 1500);
        writer.Write("potionUpCount", 0);
        writer.Write("havePotionJ", false);

        writer.Write("stockOpen", 10000);
        writer.Write("haveStockJ", false);
        writer.Write("stockQuantity", 0);

        //�N���A�X�R�A�͕ۑ�
        writer.Write("resultScore0", ParameterCalc.ResultScore[0]);
        writer.Write("resultScore1", ParameterCalc.ResultScore[1]);
        writer.Write("resultScore2", ParameterCalc.ResultScore[2]);

        // �ύX�𔽉f
        writer.Commit();
    }
}
