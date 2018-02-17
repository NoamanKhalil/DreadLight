using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;

public class PhotonNetworkManagerCs :Photon.PunBehaviour
{
    [SerializeField]
    private GameObject Camera;
    [SerializeField]
    private GameObject[] Player;
    [SerializeField]
    private GameObject[] spawnPoints;
    [SerializeField]
    private Text networkUpdateText;
    
    private PhotonView photonView;
	// Use this for initialization
	void Start ()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
        photonView = GetComponent<PhotonView>();
	}
    public virtual void OnJoinedLobby()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinOrCreateRoom("New", null, null);
    }

    public virtual void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate(Player[0].name, spawnPoints[0].transform.position, spawnPoints[0].transform.rotation, 0);
        Camera.SetActive(false);
    }
	// Update is called once per frame
	void Update ()
    {
        networkUpdateText.text = PhotonNetwork.connectionStateDetailed.ToString (); 
	}
}
