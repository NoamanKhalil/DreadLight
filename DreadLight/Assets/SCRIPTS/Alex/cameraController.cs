using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public List<Transform> players;

    public Vector3 offSet;

    private void LateUpdate()
    {
        if(players.Count == 0)
        {
            return;
        }

        Vector3 centerPoint = GetCenterPoint();

        Vector3 gotoPos = centerPoint + offSet;

        transform.position = gotoPos;

    }

    Vector3 GetCenterPoint()
    {
        if(players.Count == 1)
        {
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }

        return bounds.center;
    }
}
