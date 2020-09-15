using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

public class NPC : Character {
    private Vector2[] movementDirections = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    [SerializeField]
    private DialogueData dialogueData;
    [SerializeField]
    private Dialogue dialogue;
    private Vector2 spawnPosition;
    [SerializeField]
    private bool wander;

    [SerializeField]
    private string questName;
    private Quest quest;
    private QuestController questController;

	void Start () {
        questController = FindObjectOfType<QuestController>();
        spawnPosition = transform.position;

        if (wander)
        {
            Wander();
        }
	}

    public void Interact(Player player)
    {
        if (GetComponent<BattleLaunchCharacter>() != null)
        {
            GetComponent<BattleLaunchCharacter>().PrepareBattle(player, player.launcher);
        }

        if (questName != "")
        {
            if (quest == null)
            {
                dialogue.StartDialogue(dialogueData.dialogue);
                quest = questController.AssignQuest(questName);
            }
        }
    }

    public void Wander()
    {
        Vector2 currentPosition = transform.position;
        if (currentPosition == spawnPosition)
        {
            int roll = Random.Range(0, 3);
            Vector2 destination = currentPosition + movementDirections[roll];
            StartCoroutine(this.MoveTo(destination, Wander, Random.Range(2, 5)));
        }
        else
        {
            StartCoroutine(this.MoveTo(spawnPosition, Wander, Random.Range(2, 5)));
        }
    }
}
