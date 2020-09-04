using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {
    public static event System.Action<int> OnEnemyDied = delegate { };
    public static event System.Action<int> OnItemFound = delegate { };
    public static event System.Action<QuestSystem.Quest> OnQuestProgressChanged = delegate { };
    public static event System.Action<QuestSystem.Quest> OnQuestCompleted = delegate { };
    public static event System.Action OnBattleCompleted = delegate { };


    public static void EnemyDied(int enemyID)
    {
        OnEnemyDied(enemyID);
    }

    public static void ItemFound(int itemID)
    {
        OnItemFound(itemID);
    }

    public static void QuestProgressChanged(QuestSystem.Quest quest)
    {
        OnQuestProgressChanged(quest);
    }

    public static void QuestCompleted(QuestSystem.Quest quest)
    {
        OnQuestCompleted(quest);
    }

    public static void BattleCompleted()
    {
        OnBattleCompleted();
    }
}
