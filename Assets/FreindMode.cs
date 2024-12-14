using System.Collections.Generic;
using UnityEngine;

public class FreindMode : MonoBehaviour
{
    public FriendNode[,] grid;
    private float nodeDiameter = 0.5f;
    Vector3 gridStartingPoint = new Vector3(-1, -2, 0);
    Vector3 gridrndPoint = new Vector3(2, 1, 0);


    bool player1Turn = true;
    bool player2Turn = false;


    private FriendChannel horizontalChannel_1;
    private FriendChannel horizontalChannel_2;
    private FriendChannel horizontalChannel_3;

    private FriendChannel verticalChannel_1;
    private FriendChannel verticalChannel_2;
    private FriendChannel verticalChannel_3;

    private FriendChannel diagonalChannel_1;
    private FriendChannel diagonalChannel_2;


    public List<FriendChannel> allChannels = new List<FriendChannel>();




    public GameObject xPrefab, oPrefab;




    void Start()
    {
        grid = new FriendNode[3, 3];
        MakeGrid();
        Assignchannels();
    }

    private bool isGameOver1()
    {
        foreach (FriendChannel c in allChannels)
        {
            if (c.player1Amount == 3)
            {
                return true;
            }
        }
        return false;
    }

    private bool isGameOver2()
    {
        foreach (FriendChannel c in allChannels)
        {
            if (c.player2Amount == 3)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Turn)
        {
            if (Input.GetMouseButtonDown(0) && !isGameOver1())
            {
                Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (isMouseClikcInBoard(mp))
                {
                    FriendNode freindMode = GetNodeBasedOffClickPos(mp);
                    if (freindMode.nodeType == FriendNodeType.def)
                    {
                        freindMode.nodeType = FriendNodeType.player1;

                        Update1(freindMode);

                        if (!isGameOver1())
                            SwitchPlayer();
                        else
                            print("player 1 done it");

                        Instantiate(xPrefab, freindMode.position, Quaternion.Euler(0, 0, 45));

                    }
                }
            }
        }
        else if (player2Turn)
        {
            if (Input.GetMouseButtonDown(0) && !isGameOver2())
            {
                Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (isMouseClikcInBoard(mp))
                {
                    FriendNode freindMode = GetNodeBasedOffClickPos(mp);
                    if (freindMode.nodeType == FriendNodeType.def)
                    {
                        freindMode.nodeType = FriendNodeType.player2;

                        Update2(freindMode);

                        if (!isGameOver2())
                            SwitchPlayer();
                        else
                            print("player 2 done it");

                        Instantiate(oPrefab, freindMode.position, Quaternion.identity);


                    }
                }
            }
        }
    }

    private void SwitchPlayer()
    {
        player1Turn = !player1Turn;
        player2Turn = !player2Turn;
    }

    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
    private void MakeGrid()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                grid[x, y] = new FriendNode(new Vector3(gridStartingPoint.x + x + nodeDiameter, gridStartingPoint.y + y + nodeDiameter), FriendNodeType.def);
            }
        }
    }
    private bool isMouseClikcInBoard(Vector3 clickWorldPos)
    {
        if (clickWorldPos.x > gridStartingPoint.x && clickWorldPos.x < gridrndPoint.x)
        {
            if (clickWorldPos.y > gridStartingPoint.y && clickWorldPos.y < gridrndPoint.y)
                return true;
            else return false;
        }
        else return false;
    }

    private FriendNode GetNodeBasedOffClickPos(Vector3 mousePosinWrlDpace)
    {
        int x = -1;
        if (mousePosinWrlDpace.x > -1 && mousePosinWrlDpace.x < 0)
            x = 0;
        if (mousePosinWrlDpace.x > 0 && mousePosinWrlDpace.x < 1)
            x = 1;
        if (mousePosinWrlDpace.x > 1 && mousePosinWrlDpace.x < 2)
            x = 2;


        int y = -1;
        if (mousePosinWrlDpace.y > -2 && mousePosinWrlDpace.y < -1)
            y = 0;
        if (mousePosinWrlDpace.y > -1 && mousePosinWrlDpace.y < 0)
            y = 1;
        if (mousePosinWrlDpace.y > 0 && mousePosinWrlDpace.y < 1)
            y = 2;


        return grid[x, y];
    }

    private void Assignchannels()
    {
        horizontalChannel_1 = new FriendChannel(new FriendNode[3]);
        horizontalChannel_1.nodes[0] = grid[0, 0];
        horizontalChannel_1.nodes[1] = grid[1, 0];
        horizontalChannel_1.nodes[2] = grid[2, 0];
        allChannels.Add(horizontalChannel_1);

        horizontalChannel_2 = new FriendChannel(new FriendNode[3]);
        horizontalChannel_2.nodes[0] = grid[0, 1];
        horizontalChannel_2.nodes[1] = grid[1, 1];
        horizontalChannel_2.nodes[2] = grid[2, 1];
        allChannels.Add(horizontalChannel_2);

        horizontalChannel_3 = new FriendChannel(new FriendNode[3]);
        horizontalChannel_3.nodes[0] = grid[0, 2];
        horizontalChannel_3.nodes[1] = grid[1, 2];
        horizontalChannel_3.nodes[2] = grid[2, 2];
        allChannels.Add(horizontalChannel_3);



        verticalChannel_1 = new FriendChannel(new FriendNode[3]);
        verticalChannel_1.nodes[0] = grid[0, 0];
        verticalChannel_1.nodes[1] = grid[0, 1];
        verticalChannel_1.nodes[2] = grid[0, 2];
        allChannels.Add(verticalChannel_1);

        verticalChannel_2 = new FriendChannel(new FriendNode[3]);
        verticalChannel_2.nodes[0] = grid[1, 0];
        verticalChannel_2.nodes[1] = grid[1, 1];
        verticalChannel_2.nodes[2] = grid[1, 2];
        allChannels.Add(verticalChannel_2);


        verticalChannel_3 = new FriendChannel(new FriendNode[3]);
        verticalChannel_3.nodes[0] = grid[2, 0];
        verticalChannel_3.nodes[1] = grid[2, 1];
        verticalChannel_3.nodes[2] = grid[2, 2];
        allChannels.Add(verticalChannel_3);



        diagonalChannel_1 = new FriendChannel(new FriendNode[3]);
        diagonalChannel_1.nodes[0] = grid[0, 0];
        diagonalChannel_1.nodes[1] = grid[1, 1];
        diagonalChannel_1.nodes[2] = grid[2, 2];
        allChannels.Add(diagonalChannel_1);

        diagonalChannel_2 = new FriendChannel(new FriendNode[3]);
        diagonalChannel_2.nodes[0] = grid[2, 0];
        diagonalChannel_2.nodes[1] = grid[1, 1];
        diagonalChannel_2.nodes[2] = grid[0, 2];
        allChannels.Add(diagonalChannel_2);
    }

    private void Update1(FriendNode pickedNode)
    {
        foreach (FriendChannel cha in allChannels)
        {
            foreach (FriendNode n in cha.nodes)
            {
                if (n == pickedNode)
                {
                    cha.player1Amount++;
                }
            }
        }
    }
    private void Update2(FriendNode pickedNode)
    {
        foreach (FriendChannel cha in allChannels)
        {
            foreach (FriendNode n in cha.nodes)
            {
                if (n == pickedNode)
                {
                    cha.player2Amount++;
                }
            }
        }
    }


    //private void OnDrawGizmos()
    //{
    //    if (grid != null)
    //    {
    //        foreach (FriendNode node in grid)
    //        {
    //            if (node.nodeType == FriendNodeType.player1)
    //            {
    //                Gizmos.color = Color.red;

    //            }
    //            else if (node.nodeType == FriendNodeType.player2)
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


public enum FriendNodeType { def, player1, player2 };

public class FriendNode
{
    public Vector3 position;
    public FriendNodeType nodeType;
    public FriendNode(Vector3 pos, FriendNodeType nodeType)
    {
        position = pos;
        this.nodeType = nodeType;
    }
}

[System.Serializable]
public class FriendChannel
{
    public FriendNode[] nodes;
    public int player1Amount;
    public int player2Amount;

    public FriendChannel(FriendNode[] nodes)
    {
        this.nodes = nodes;
    }
}