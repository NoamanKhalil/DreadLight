using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static Pathfinder _instance;
    public bool showGraph;

    private void Awake()
    {
        _instance = this;
        Initialize();
    }

    Vector3[] NodesPositions;

    float[][] Distances;    //2D array for distance to and from the nodes

    void Initialize()   //Function used to set up the graph
    {
        NodesPositions = new Vector3[transform.childCount];
        Distances = new float[transform.childCount][];
        for (int i = 0; i < transform.childCount; i ++)
        {
            NodesPositions[i] = transform.GetChild(i).position;
            transform.GetChild(i).name = "Waypoint " + i;   //Renames the points
            Distances[i] = new float[transform.childCount];
        }

        for (int i = 0; i < transform.childCount; i ++)
        {
            for (int j = 0; j < transform.childCount; j ++)
            {
                if (i == j)
                {
                    Distances[i][j] = -1;   //Will not connect to itself
                }

                else
                {
                    Vector3 dir = NodesPositions[j] - NodesPositions[i];
                    if (!Physics.Raycast(NodesPositions[i], dir, dir.magnitude))
                    {
                        Distances[i][j] = dir.magnitude;    //If there is no obstacles between two points
                    }
                    else
                    {
                        Distances[i][j] = -1;
                    }
                }
            }
        }
    }

    public List<Vector3> GetPath(Vector3 start, Vector3 target) //FIRST HALF OF CODE: Calculates which node is closest to the enemy, and which node is closest to the player
    {
        float shortestDistance = float.MaxValue;
        int startNode = 0;
        int targetNode = 0;

        for (int i = 0; i < NodesPositions.Length; i ++)
        {
            if (Vector3.Distance(start, NodesPositions[i]) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(start, NodesPositions[i]);
                startNode = i;
            }
        }
        shortestDistance = float.MaxValue;

        for (int i = 0; i < NodesPositions.Length; i ++)
        {
           if (Vector3.Distance(target, NodesPositions[i]) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(target, NodesPositions[i]);
                targetNode = i;
            }
        }

        //SECOND HALF OF CODE: Dijkstra Algorithm, calculating shortest route

        //Defines all the default values for the arrays
        Queue<int> waitingList = new Queue<int>();
        float[] AccumulatedDistance = new float[NodesPositions.Length];
        bool[] isVisited = new bool[NodesPositions.Length];
        int[] fromNode = new int[NodesPositions.Length];

        for (int i = 0; i < NodesPositions.Length; i ++)
        {
            AccumulatedDistance[i] = float.MaxValue;
            isVisited[i] = false;
            fromNode[i] = -1;
        }
        AccumulatedDistance[startNode] = 0;
        waitingList.Enqueue(startNode);

        //Dijkstra Algorithm
        while (waitingList.Count != 0)
        {
            int curNode = waitingList.Dequeue();
            for (int c = 0; c < NodesPositions.Length; c ++)
            {
                if (Distances[curNode][c] != - 1 && !isVisited[c])
                {
                    waitingList.Enqueue(c);
                    if (AccumulatedDistance[curNode] + Distances[curNode][c] < AccumulatedDistance[c])  //Checks which route has the shortest distance
                    {
                        AccumulatedDistance[c] = AccumulatedDistance[curNode] + Distances[curNode][c];
                        fromNode[c] = curNode;  //Keeps which node it came from
                    }
                }
            }

            isVisited[curNode] = true;  //Once visited, check the bool
        }

        int tNode = targetNode;
        List<Vector3> path = new List<Vector3>();

        path.Add(NodesPositions[targetNode]);
        while (tNode != startNode)
        {
            tNode = fromNode[tNode];    //Get tNode's fromNode
            path.Add(NodesPositions[tNode]);    //Adds all the nodes where the algorithm took
        }

        path.Add(NodesPositions[startNode]);
        path.Reverse(); //Backtracking process

        for(int i = 0; i < path.Count - 1; i++)
        {
            Debug.DrawLine(path[i] + Vector3.up * 2, path[i + 1] + Vector3.up * 2, Color.yellow);   //Draws a yellow line to see the path
        }
        return path;
    }

    private void Update()
    {
        if (!showGraph)
        {
            return;
        }

        for (int i = 0; i < NodesPositions.Length; i ++)
        {
            for (int j = 0; j < NodesPositions.Length; j++)
            {
                if (Distances[i][j] != -1)
                {
                    Debug.DrawLine(NodesPositions[i], NodesPositions[j], Color.blue);   //A debug check to see if the lines or the graph's points are all connected
                }
            }
        }
    }
}
