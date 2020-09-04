using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleSystem
{
    public class BattleCharacter : MonoBehaviour
    {
        public string characterName;
        public int health;
        public int maxHealth;
        public int attackPower;
        public int defencePower;
        public int manaPoints;
        public List<Spell> spells;
        public int enemyID;

        public void Hurt(int amount)
        {
            int damageAmount = amount;
            health = Mathf.Max(health - damageAmount, 0);
            if (health == 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            int healAmount = amount;
            health = Mathf.Min(health + healAmount, maxHealth);
        }

        public void Defend()
        {
            defencePower += (int)(defencePower * .33);
            Debug.Log("Def increased.");
        }

        public bool CastSpell(Spell spell, BattleCharacter targetCharacter)
        {
            bool successful = manaPoints >= spell.manaCost;

            if (successful)
            {
                Spell spellToCast = Instantiate<Spell>(spell, transform.position, Quaternion.identity);
                manaPoints -= spell.manaCost;
                spellToCast.Cast(targetCharacter);
            }

            return successful;
        }

        public virtual void Die()
        {
            EventController.EnemyDied(enemyID);
            Destroy(this.gameObject);
            Debug.LogFormat("{0} has died!", characterName);
        }
    }
}
