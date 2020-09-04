using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class BattleController : MonoBehaviour
    {
        public static BattleController Instance { get; set; }

        public Dictionary<int, List<BattleCharacter>> characters = new Dictionary<int, List<BattleCharacter>>();
        public int characterTurnIndex;
        public Spell playerSelectedSpell;
        public bool playerIsAttacking;

        [SerializeField] private BattleSpawnPoint[] spawnPoints;

        [SerializeField] private BattleUIController uiController;

        private int actTurn;

        private void Start()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
            characters.Add(0, new List<BattleCharacter>());
            characters.Add(1, new List<BattleCharacter>());
            FindObjectOfType<BattleLauncher>().Launch();
            uiController.UpdateCharacterUI();
        }

        public BattleCharacter GetRandomPlayer()
        {
            return characters[0][Random.Range(0, characters[0].Count - 1)];
        }

        public BattleCharacter GetWeakestEnemy()
        {
            BattleCharacter weakestEnemy = characters[1][0];
            foreach (BattleCharacter character in characters[1])
            {
                if (character.health < weakestEnemy.health)
                {
                    weakestEnemy = character;
                }
            }
            return weakestEnemy;
        }

        private void NextTurn()
        {
            actTurn = actTurn == 0 ? 1 : 0;
        }

        private void NextAct()
        {
            if (characters[0].Count > 0 && characters[1].Count > 0)
            {
                if (characterTurnIndex < characters[actTurn].Count - 1)
                {
                    characterTurnIndex++;
                }
                else
                {
                    NextTurn();
                    characterTurnIndex = 0;
                    Debug.Log("turn: " + actTurn);
                }

                switch (actTurn)
                {
                    case 0:
                        uiController.ToggleActionState(true);
                        uiController.BuildSpellList(GetCurrentCharacter().spells);
                        break;
                    case 1:
                        StartCoroutine(PerformAct());
                        uiController.ToggleActionState(false);
                        break;
                }
            }
            else
            {
                Debug.Log("Battle over!");
                EventController.BattleCompleted();
            }
        }

        IEnumerator PerformAct()
        {
            yield return new WaitForSeconds(.75f);
            if (GetCurrentCharacter().health > 0)
            {
                GetCurrentCharacter().GetComponent<Enemy>().Act();
            }
            uiController.UpdateCharacterUI();
            yield return new WaitForSeconds(1f);
            NextAct();
        }

        public void SelectCharacter(BattleCharacter character)
        {
            if (playerIsAttacking)
            {
                DoAttack(GetCurrentCharacter(), character);
            }
            else if (playerSelectedSpell != null)
            {
                if (GetCurrentCharacter().CastSpell(playerSelectedSpell, character))
                {
                    uiController.UpdateCharacterUI();
                    NextAct();
                }
                else
                {
                    Debug.LogWarning("Not enough mana to cast that spell!");
                }
            }
        }

        public void DoAttack(BattleCharacter attacker, BattleCharacter target)
        {
            Debug.Log("do attack.");
            target.Hurt(attacker.attackPower);
            NextAct();
        }

        public void StartBattle(List<BattleCharacter> players, List<BattleCharacter> enemies)
        {
            Debug.Log("Setup battle!");
            for (int i = 0; i < players.Count; i++)
            {
                characters[0].Add(spawnPoints[i + 3].Spawn(players[i]));
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                characters[1].Add(spawnPoints[i].Spawn(enemies[i]));
            }
        }

        public BattleCharacter GetCurrentCharacter()
        {
            return characters[actTurn][characterTurnIndex];
        }
    }
}