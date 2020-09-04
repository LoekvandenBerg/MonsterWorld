using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    public class Item : MonoBehaviour
    {
        private int itemID;
        // Use this for initialization
        void Start()
        {
            itemID = 0;
        }

        public void Get()
        {
            EventController.ItemFound(itemID);
        }
    }
}
