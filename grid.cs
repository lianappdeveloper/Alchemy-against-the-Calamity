using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour {

    public GameObject[] allEnemys;
    public Transform unit;
    public LayerMask unWalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;//how much space each individual nodes covers
    node[,] Grid;


    float nodeDiameter;
    int gridSizeX, gridSizeY,gridSizeZ;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
       CreateGrid();


    }

    //if u didnt understand what the method doas fucking read the method name morron -_-" 
    public void CreateGrid()
    {
        Grid = new node[gridSizeX, gridSizeY];
        Vector3 WorldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;//chacks the bottom left corrner of the map
        //deffy the grid and nodes orders on it
        for (int x = 0; x < gridSizeX; x ++)
        {
            for(int y =0; y< gridSizeY; y++)
            {
                Vector3 worldPoint = WorldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unWalkableMask));
                Grid[x, y] = new node(walkable, worldPoint, x, y );
            }

        }
    }

    // will check all the nodes around the place the AI is dynamicly for all the nodes around it 
    public List<node> GetNeighbours(node Node)
    {
        List<node> nieghbours = new List<node>();
        for(int x = -1; x<=1; x++ ){
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                    int checkX = Node.gridX + x;
                    int checkY = Node.gridY + y;
                    if (checkY >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        nieghbours.Add(Grid[checkX, checkY]);
                    }
                }
            }
        }
        return nieghbours;
    }

    public node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float precentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float precentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        precentX = Mathf.Clamp01(precentX);
        precentY = Mathf.Clamp01(precentY);

        int x = Mathf.RoundToInt((gridSizeX-1) * precentX);
        int y = Mathf.RoundToInt((gridSizeY-1) * precentY);
        return Grid[x,y];
    }


    public List<node> path;

    //create zone that the AI will read 
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        if(Grid != null)
        {
           
            allEnemys = GameObject.FindGameObjectsWithTag("Enemy");
            node unitNode = NodeFromWorldPoint(unit.position);
         //   node unitenote = NodeFromWorldPoint(allEnemys);
            // slice this zone to cubes
            foreach (node n in Grid)
            {
               
                Gizmos.color=(n.walkable) ? Color.white : Color.red;
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
                if (unitNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.WorldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
