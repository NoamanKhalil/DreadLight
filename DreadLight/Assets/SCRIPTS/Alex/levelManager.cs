using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : MonoBehaviour {

    public GameObject initialSpawnLocation;
    public GameObject[] player1Spawns;
    public GameObject[] player2Spawns;
    public GameObject[] levels;

    public environmentController environmentController;
    public EnvironmentSettings[] environment;

    public Text curStageText;
    public int currentStage = 0;
    int currentStageTextVersion = 0;
    public int totalStages = 3;

    public GameObject player1;
    public GameObject player2;

    private void Awake()
    {
        UpdateEnvironment(0);
    }

    void Start () 
    {
        RestartLevels();
        UpdateStageText();
	}

    void UpdateStageText()
    {
        currentStageTextVersion++;
        curStageText.text = "Stage: " + currentStageTextVersion.ToString() + "/" + totalStages.ToString();
    }
    	
	public void LoadNextLevel()
    {
        currentStage++;
        UpdateStageText();

        //teleporting players to next level spawn points
        TeleportPlayersToSpawn(currentStage);
        UpdateEnvironment(currentStage);
    }

    public void UpdateEnvironment(int index)
    {
        environmentController.settings = environment[index];
        environmentController.UpdateEnvironment(); // testing fog
    }

    public void DisableLastLevel(int index)
    {
        index--;
        if(index >= 0)
        {
            levels[index].SetActive(false);
        }
        else
        {
            Debug.LogWarning("CANNOT DISABLE LAST LEVEL, INDEX IS < 0");
        }
    }

    public void RestartLevels()
    {
        currentStage = 0;
        //UpdateEnvironment(currentStage);
        TeleportPlayersToSpawn(currentStage);

        for (int i = 1; i < levels.Length - 1; i++)
        {
            levels[i].SetActive(false);
        }
    }

    public void TeleportPlayersToSpawn(int index)
    {
        player1.transform.position = player1Spawns[index].transform.position;
        player2.transform.position = player2Spawns[index].transform.position;
    }
}
