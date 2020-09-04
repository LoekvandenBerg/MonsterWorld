using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField] private string questName;
        private QuestController questController;
        private Quest quest;

        // Use this for initialization
        void Start()
        {
            questController = FindObjectOfType<QuestController>();
            EventController.OnQuestCompleted += Completed;
        }

        public void GiveQuest()
        {
            quest = questController.AssignQuest(questName);
            GetComponent<UnityEngine.UI.Button>().image.color = Color.green;
        }

        public void Completed(Quest quest)
        {
            if (this.quest != null && quest == this.quest)
            {
                GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
    }
}
