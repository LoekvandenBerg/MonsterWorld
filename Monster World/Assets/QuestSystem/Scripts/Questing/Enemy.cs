using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{
    public class Enemy : MonoBehaviour
    {
        private int enemyID;
        // Use this for initialization
        void Start()
        {
            enemyID = 0;
        }

        public void Die()
        {
            EventController.EnemyDied(enemyID);
        }
    }
}
