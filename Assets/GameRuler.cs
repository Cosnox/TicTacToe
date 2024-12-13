using System.Collections.Generic;
using UnityEngine;

public class GameRuler : MonoBehaviour
{
    private float nodeDiameter = 0.5f;
    public Node[,] grid;
    [HideInInspector] public Vector3 gridStartingPoint;
    [HideInInspector] public Vector3 gridrndPoint;


    private Channel horizontalChannel_1;
    private Channel horizontalChannel_2;
    private Channel horizontalChannel_3;

    private Channel verticalChannel_1;
    private Channel verticalChannel_2;
    private Channel verticalChannel_3;

    private Channel diagonalChannel_1;
    private Channel diagonalChannel_2;


    public List<Channel> allChannels = new List<Channel>();

    void Start()
    {
        grid = new Node[3, 3];
        gridStartingPoint = new Vector3(-1, -2, 0);
        gridrndPoint = new Vector3(2, 1, 0);

        MakeGrid();
        Assignchannels();

        Debug.Log(Random.Range(0, 2) + " should start game");
    }

    private void MakeGrid()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                grid[x, y] = new Node(new Vector3(gridStartingPoint.x + x + nodeDiameter, gridStartingPoint.y + y + nodeDiameter), NodeType.defaultt);
            }
        }
    }

    private void Assignchannels()
    {
        horizontalChannel_1 = new Channel(new Node[3]);
        horizontalChannel_1.nodes[0] = grid[0, 0];
        horizontalChannel_1.nodes[1] = grid[1, 0];
        horizontalChannel_1.nodes[2] = grid[2, 0];
        allChannels.Add(horizontalChannel_1);

        horizontalChannel_2 = new Channel(new Node[3]);
        horizontalChannel_2.nodes[0] = grid[0, 1];
        horizontalChannel_2.nodes[1] = grid[1, 1];
        horizontalChannel_2.nodes[2] = grid[2, 1];
        allChannels.Add(horizontalChannel_2);

        horizontalChannel_3 = new Channel(new Node[3]);
        horizontalChannel_3.nodes[0] = grid[0, 2];
        horizontalChannel_3.nodes[1] = grid[1, 2];
        horizontalChannel_3.nodes[2] = grid[2, 2];
        allChannels.Add(horizontalChannel_3);



        verticalChannel_1 = new Channel(new Node[3]);
        verticalChannel_1.nodes[0] = grid[0, 0];
        verticalChannel_1.nodes[1] = grid[0, 1];
        verticalChannel_1.nodes[2] = grid[0, 2];
        allChannels.Add(verticalChannel_1);

        verticalChannel_2 = new Channel(new Node[3]);
        verticalChannel_2.nodes[0] = grid[1, 0];
        verticalChannel_2.nodes[1] = grid[1, 1];
        verticalChannel_2.nodes[2] = grid[1, 2];
        allChannels.Add(verticalChannel_2);


        verticalChannel_3 = new Channel(new Node[3]);
        verticalChannel_3.nodes[0] = grid[2, 0];
        verticalChannel_3.nodes[1] = grid[2, 1];
        verticalChannel_3.nodes[2] = grid[2, 2];
        allChannels.Add(verticalChannel_3);



        diagonalChannel_1 = new Channel(new Node[3]);
        diagonalChannel_1.nodes[0] = grid[0, 0];
        diagonalChannel_1.nodes[1] = grid[1, 1];
        diagonalChannel_1.nodes[2] = grid[2, 2];
        allChannels.Add(diagonalChannel_1);

        diagonalChannel_2 = new Channel(new Node[3]);
        diagonalChannel_2.nodes[0] = grid[2, 0];
        diagonalChannel_2.nodes[1] = grid[1, 1];
        diagonalChannel_2.nodes[2] = grid[0, 2];
        allChannels.Add(diagonalChannel_2);
    }


    private void OnDrawGizmos()
    {
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.nodeType == NodeType.human)
                {
                    Gizmos.color = Color.red;

                }
                else if (node.nodeType == NodeType.robot)
                {
                    Gizmos.color = Color.green;
                }
                else
                    Gizmos.color = Color.black;

                Gizmos.DrawWireCube(node.position, Vector3.one * .9f);
            }
        }
    }
}


public class Node
{
    public Vector3 position;
    public NodeType nodeType;
    public Node(Vector3 pos, NodeType nodeType)
    {
        position = pos;
        this.nodeType = nodeType;
    }
}

public enum NodeType { defaultt, human, robot };

[System.Serializable]
public class Channel
{
    public Node[] nodes;
    public int humanNodesAmount;
    public int robotNodesAmount;
    public int channelImportance;

    public Channel(Node[] nodes)
    {
        this.nodes = nodes;
    }
}
