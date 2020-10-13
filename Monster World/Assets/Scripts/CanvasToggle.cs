using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;

public class CanvasToggle : MonoBehaviour
{
    QuestDatabase questDataBase;
    [SerializeField]
    UIInventory inventoryUI;

    private void Start()
    {
        questDataBase = FindObjectOfType<QuestDatabase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questDataBase.gameObject.SetActive(!questDataBase.gameObject.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
        }
    }
}
