using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleSystem;

public class BattleLaunchCharacter : MonoBehaviour {

    [SerializeField]
    private List<BattleCharacter> enemies;
    
    public void PrepareBattle(Player player, BattleLauncher launcher)
    {
        player.gameObject.SetActive(false);
        launcher.PrepareBattle(enemies, player.team, player.transform.position);
    }
}
