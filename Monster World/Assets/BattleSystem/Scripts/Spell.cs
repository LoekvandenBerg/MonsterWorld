using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BattleSystem
{
    public class Spell : MonoBehaviour
    {
        public string spellName;
        public int power;
        public int manaCost;
        public enum SpellType { Attack, Heal }
        public SpellType spellType;

        private Vector3 targetPosition;

        private void Update()
        {
            if (targetPosition != Vector3.zero)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, .15f);
                if (Vector3.Distance(transform.position, targetPosition) < .25f)
                {
                    Destroy(this.gameObject, 1);
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void Cast(BattleCharacter target)
        {
            targetPosition = target.transform.position;
            Debug.Log(spellName + " was cast on " + target.name + "!");
            if (spellType == SpellType.Attack)
            {
                target.Hurt(power);
            }
            else if (spellType == SpellType.Heal)
            {
                target.Heal(power);
            }
        }
    }
}
