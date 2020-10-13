using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace QuestSystem
{
    public class QuestController : MonoBehaviour
    {
        public List<Quest> assignedQuests = new List<Quest>();

        [SerializeField]
        private QuestUIItem questUIItem;
        [SerializeField]
        private Transform questUIParent;

        private QuestDatabase questDatabase;

        private void Start()
        {
            if (FindObjectsOfType<QuestController>().Length > 1)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += Populate;
            EventController.OnQuestCompleted += RemoveCompletedQuest;
            questDatabase = GetComponent<QuestDatabase>();
        }

        private void Update()
        {
           
        }

        public Quest AssignQuest(string questName)
        {
            Quest questToAdd = null;
            if (FindActiveQuest(questName) == null)
            {
                questToAdd = (Quest)gameObject.AddComponent(Type.GetType(questName));
                assignedQuests.Add(questToAdd);
                questDatabase.AddQuest(questToAdd);
            }
            else
            {
                questToAdd = (Quest)gameObject.GetComponent(Type.GetType(questName));
            }

            QuestUIItem questUI = Instantiate(questUIItem, questUIParent);
            questUI.Setup(questToAdd);
            return questToAdd;
        }

        public Quest FindActiveQuest(string questSlug)
        {
            return (Quest)GetComponent(Type.GetType(questSlug));
        }

        private void Populate(Scene scene, LoadSceneMode sceneMode)
        {
            questUIParent = GameObject.FindGameObjectWithTag("UI/Quest Panel").transform;
            if (assignedQuests.Count > 0)
            {
                for(int i = 0; i < assignedQuests.Count; i++)
                {
                    AssignQuest(assignedQuests[i].slug);
                }
            }
        }

        private void RemoveCompletedQuest(Quest quest)
        {
            //Animation of Reward here?

            assignedQuests.Remove(quest);
            Destroy(quest);
        }
    }
}
