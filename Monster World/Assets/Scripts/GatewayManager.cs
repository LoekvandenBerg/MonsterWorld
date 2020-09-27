using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GatewayManager : MonoBehaviour 
{
    private Vector2 spawnPosition;
    private bool spawnPrepared;
    private string lastSceneName;
    private int launchCharacterId;
    public static GatewayManager Instance { get; set; }

	// Use this for initialization
	void Start () {
		if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += SceneLoaded;
	}
	
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (spawnPrepared)
        {
            MovePosition();
            if(lastSceneName == "Battle")
                EventController.ReturnedFromBattle(launchCharacterId);
        }
    }

    public void SetLastBattledCharacterId(int launchCharId)
    {
        launchCharacterId = launchCharId;
    }

    public void SetLastScene(string sceneName)
    {
        lastSceneName = sceneName;
    }

    public void SetSpawnPosition(Vector2 spawnPosition)
    {
        spawnPrepared = true;
        this.spawnPosition = spawnPosition;
    }

    private void MovePosition()
    {
        FindObjectOfType<Player>().TeleportTo(spawnPosition);
        spawnPrepared = false;
    }
}
