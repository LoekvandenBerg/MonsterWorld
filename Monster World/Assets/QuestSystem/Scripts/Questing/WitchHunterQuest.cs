using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

public class WitchHunterQuest : Quest
{

    void Awake()
    {
        slug = "WitchHunterQuest";
        questName = "Witch Hunter";
        description = "Hunt some witches";
        itemRewards = new List<string>() { "Ruby Talisman" };
        goal = new KillGoal(2, 1, this);
    }

    public override void Complete()
    {
        base.Complete();
    }

}
