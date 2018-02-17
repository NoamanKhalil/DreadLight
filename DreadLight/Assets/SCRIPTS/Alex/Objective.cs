using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

    public levelManager lvlManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2") 
        {
            lvlManager.LoadNextLevel();
        }
    }
}
