using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

    grid Grid;
    public Transform seeker;
    public Transform target;



    void Awake()
    {
        Grid = GetComponent<grid>();

    }

    void Update()
    {
        FindPath(seeker.position, target.position);
    }



    //path calculation
    void FindPath(Vector3 startPos, Vector3 TargetPos) {

        node startNode = Grid.NodeFromWorldPoint(startPos);      // where i stand 
        node targetNode = Grid.NodeFromWorldPoint(TargetPos);   // where i wanna get
        List<node> openSet = new List<node>();                  //the set of nodes to be evaluated
        HashSet<node> closedSet = new HashSet<node>();          // the set of nodes already evaluated
        openSet.Add(startNode);                                 // add a evalutaed node to the start node
        
        while (openSet.Count > 0)
        {
            node CurrentNode = openSet[0];
            // loop through all the nodes in the openSet
            for(int i=1; i<openSet.Count; i++)
            {   // if the 2 nodes have equal f.cost we take the closeset one to the open set with the lowest f.cost
                if(openSet[i].fCost() < CurrentNode.fCost()|| openSet[i].fCost() == CurrentNode.fCost() && openSet[i].hCost  < CurrentNode.hCost )
                {   
                    CurrentNode = openSet[i];            
                }
            }
            openSet.Remove(CurrentNode);
            closedSet.Add(CurrentNode);
            // we found our path if this is true 
            if(CurrentNode == targetNode)
            {
                RetracePath(startNode, targetNode);     // give vlaue to the RetracePathe mathod
                return;
            }
            //check if the nieghbour node is walkable or not walkable 
            foreach(node niebghbour in Grid.GetNeighbours(CurrentNode))
            {

            if(!niebghbour.walkable || closedSet.Contains(niebghbour))
                {
                    continue;
                }
                // move from one node to the other by calculation in GetDistance 
                int newMovmentCostToNieghbour = CurrentNode.gCost + GetDistance(CurrentNode, niebghbour);
                if(newMovmentCostToNieghbour < niebghbour.gCost || !openSet.Contains(niebghbour))
                {
                    niebghbour.gCost = newMovmentCostToNieghbour;
                    niebghbour.hCost = GetDistance(niebghbour, targetNode);
                    niebghbour.parent = CurrentNode;

                    if (!openSet.Contains(niebghbour))
                        openSet.Add(niebghbour);
                }
            }

        }

    }

    void RetracePath(node startNode , node endNode)
    {
        List<node> path  = new List<node>();
        node currentNode = endNode;

        while(currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

       Grid.path = path;
    }

    int GetDistance(node nodeA , node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }else{
            return 14 * dstX + 10 * (dstY - dstX);
        }

    }
}