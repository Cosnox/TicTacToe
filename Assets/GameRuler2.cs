using System.Collections.Generic;
using UnityEngine;

public class GameRuler2 : MonoBehaviour
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

    bool humanTurn = false;
    bool botTurn = false;

    public Human2 h2;
    public Robot2 b2;

    int isRandomChoose = -1;

    public GameObject xPrefab, oPrefab;


    void Start()
    {
        grid = new Node[3, 3];
        gridStartingPoint = new Vector3(-1, -2, 0);
        gridrndPoint = new Vector3(2, 1, 0);

        MakeGrid();
        Assignchannels();

        int roundDecsion = Random.Range(0, 2); // 0 being bot, 1 being player


        if (roundDecsion == 0)
            humanTurn = true;
        if (roundDecsion == 1)
            botTurn = true;

    }


    private bool isGameOver1()
    {
        foreach (Channel c in allChannels)
        {
            if (c.humanNodesAmount == 3)
            {
                return true;
            }
        }
        return false;
    }

    private bool isGameOver2()
    {
        foreach (Channel c in allChannels)
        {
            if (c.robotNodesAmount == 3)
            {
                return true;
            }
        }
        return false;
    }

    public void LoadHome()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");

    }
    private void Update()
    {
        if (humanTurn)
        {
            //print("listenng for human");
            if (Input.GetMouseButtonDown(0) && !isGameOver1())
            {
                h2.HumanChoose(xPrefab);
                if (!isGameOver1())
                    SwitchPlayer();
                else
                    print("player 1 made it");
            }
        }
        else if (botTurn && !isGameOver2())
        {
            isRandomChoose = Random.Range(0, 2);
            //print("listenng for bot");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isRandomChoose == 0)
                {
                    b2.RobotRandomChoose(oPrefab);
                }
                else if (isRandomChoose == 1)
                {
                    b2.RobotLogicalChoose(oPrefab);
                }

                if (!isGameOver2())
                    SwitchPlayer();
                else
                    print("player 2 made it");
            }
        }
    }

    private void SwitchPlayer()
    {
        humanTurn = !humanTurn;
        botTurn = !botTurn;
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


    //private void OnDrawGizmos()
    //{
    //    if (grid != null)
    //    {
    //        foreach (Node node in grid)
    //        {
    //            if (node.nodeType == NodeType.human)
    //            {
    //                Gizmos.color = Color.red;

    //            }
    //            else if (node.nodeType == NodeType.robot)
    //            {
    //                Gizmos.color = Color.green;
    //            }
    //            else
    //                Gizmos.color = Color.black;

    //            Gizmos.DrawWireCube(node.position, Vector3.one * .9f);
    //        }
    //    }
    //}
}
