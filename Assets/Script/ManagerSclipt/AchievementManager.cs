using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class AchievementManager : MonoBehaviour
{
    public Dictionary<string, int> statsAPIs = new Dictionary<string, int>
    {
        { "playTurn", 0 },
        { "debtorCount", 0 },
        { "fullComing", 0 },
        { "getDrugs", 0 },
        { "getStock", 0 },
        { "negotiate", 0 },
        { "penalty", 0 },
        { "beAttacked", 0 },
        { "gameClearCount", 0 }
    };

    private class AchievementCondition
    {
        public string StatKey { get; }
        public int Threshold { get; }
        public string AchievementID { get; }

        public AchievementCondition(string statKey, int threshold, string achievementID)
        {
            StatKey = statKey;
            Threshold = threshold;
            AchievementID = achievementID;
        }
    }

    private List<AchievementCondition> achievementConditions = new List<AchievementCondition>
    {
        new AchievementCondition("playTurn", 1, "Play_Turn_Count"),
        new AchievementCondition("debtorCount", 15, "Debtor_Count"),
        new AchievementCondition("fullComing", 10, "Full_Coming"),
        new AchievementCondition("getDrugs", 1, "Get_Drugs"),
        new AchievementCondition("getStock", 1, "Get_Stocks"),
        new AchievementCondition("negotiate", 1, "Do_Negotiate"),
        new AchievementCondition("penalty", 1, "Receved_Penalty"),
        new AchievementCondition("beAttacked", 1, "Be_Attacked"),
        new AchievementCondition("gameClearCount", 1, "First_Game_Clear"),
        new AchievementCondition("gameClearCount", 3, "Third_Game_Clear")
    };

    /*-----------------------------------------------------------------------------*/

    private void Awake()
    {
        // シングルトン
        GameObject achievementManager = CheckAchievementManager();
        bool checkResult = achievementManager != null && achievementManager != gameObject;

        if (checkResult)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if (!SteamManager.Initialized) return;
    }

    GameObject CheckAchievementManager()
    {
        return GameObject.FindGameObjectWithTag("AchievementManager");
    }

    // Statsの呼び出し
    public void LoadStat()
    {
        List<string> keys = new List<string>(statsAPIs.Keys);
        foreach (string key in keys)
        {
            int tempValue;
            SteamUserStats.GetStat(key, out tempValue);
            statsAPIs[key] = tempValue;
        }
    }

    // Statsの書き込み
    public void PushStat()
    {
        List<string> keys = new List<string>(statsAPIs.Keys);
        foreach (string key in keys)
        {
            var tempValue = 0;
            tempValue = statsAPIs[key];
            SteamUserStats.SetStat(key, tempValue);
        }
    }
    private void Update()
    {
        if (!SteamManager.Initialized) return;

        // 条件を監視し、実績を解除
        CheckAchievement();
    }

    private void CheckAchievement()
    {
        foreach (AchievementCondition condition in achievementConditions)
        {
            if (statsAPIs[condition.StatKey] >= condition.Threshold)
            {
                UnlockAchievement(condition.AchievementID);
            }
        }
    }

    private void UnlockAchievement(string achievementId)
    {
        // If the achievement is already unlocked, skip it
        bool isAchieved;
        SteamUserStats.GetAchievement(achievementId, out isAchieved);
        if (isAchieved) return;

        SteamUserStats.SetAchievement(achievementId);
        SteamUserStats.StoreStats();
    }

}


