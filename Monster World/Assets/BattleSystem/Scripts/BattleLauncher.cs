using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace BattleSystem
{
    public class BattleLauncher : MonoBehaviour
    {
        public List<BattleCharacter> Players { get; set; }
        public List<BattleCharacter> Enemies { get; set; }
        private int launchCharacterId;
        private Vector2 worldPosition;
        private int worldSceneIndex;
        private Player player;

        void Awake()
        {
            if (FindObjectsOfType<BattleLauncher>().Length > 1)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this);
            player = FindObjectOfType<Player>();
            EventController.OnBattleCompleted += ReturnToWorld;
            EventController.OnReturnFromBattle += DestroyLastBattleLaunchCharacter;
        }

        public void PrepareBattle(List<BattleCharacter> enemies, List<BattleCharacter> players, Vector2 position, int launchCharId)
        {
            worldPosition = position;
            worldSceneIndex = SceneManager.GetActiveScene().buildIndex;
            Players = players;
            Enemies = enemies;
            SceneManager.LoadScene("Battle");
            launchCharacterId = launchCharId;
        }

        private void ReturnToWorld()
        { 
            player.gameObject.SetActive(true);
            GatewayManager.Instance.SetSpawnPosition(worldPosition);
            GatewayManager.Instance.SetLastBattledCharacterId(launchCharacterId);
            GatewayManager.Instance.SetLastScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(worldSceneIndex);
        }

        private void DestroyLastBattleLaunchCharacter(int launchCharId)
        {
            BattleLaunchCharacter launchCharacter = FindObjectsOfType<BattleLaunchCharacter>().FirstOrDefault(x => x.id == launchCharId);
            Destroy(launchCharacter.gameObject);
        }

        public void Launch()
        {
            BattleController.Instance.StartBattle(Players, Enemies);
        }
    }
}
