using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
public class DemonSlayerQuest : Quest
{

    void Awake()
    {
        slug = "DemonSlayerQuest";
        questName = "Demon Hunter";
        description = "slay some demons.";
        itemRewards = new List<string>() { "Diamond Ore" };
        goal = new KillGoal(1, 0, this);
    }

    public override void Complete()
    {
        base.Complete();
    }

}
