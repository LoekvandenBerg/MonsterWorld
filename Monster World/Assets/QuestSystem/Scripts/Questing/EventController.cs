using System;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour {
    public static event Action<int> OnEnemyDied = delegate { };
    public static event Action<int> OnItemFound = delegate { };
    public static event Action<QuestSystem.Quest> OnQuestProgressChanged = delegate { };
    public static event Action<QuestSystem.Quest> OnQuestCompleted = delegate { };
    public static event Action OnBattleCompleted = delegate { };
    public static event Action<int> OnReturnFromBattle = delegate { };


    public static void EnemyDied(int enemyID)
    {
        OnEnemyDied?.Invoke(enemyID);
    }

    public static void ItemFound(int itemID)
    {
        OnItemFound?.Invoke(itemID);
    }

    public static void QuestProgressChanged(QuestSystem.Quest quest)
    {
        OnQuestProgressChanged?.Invoke(quest);
    }

    public static void QuestCompleted(QuestSystem.Quest quest)
    {
        OnQuestCompleted?.Invoke(quest);
    }

    public static void BattleCompleted()
    {
        OnBattleCompleted?.Invoke();
    }

    public static void ReturnedFromBattle(int launchcharId)
    {
        OnReturnFromBattle?.Invoke(launchcharId);
    }
}
