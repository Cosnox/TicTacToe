using UnityEngine;
using UnityEngine.UI;

public class FreindMode : MonoBehaviour
{
    private FriendNode[,] grid = new FriendNode[3, 3];
    private float nodeDiameter = 0.5f;
    Vector3 gridStartingPoint = new Vector3(-1, -2, 0);
    Vector3 gridrndPoint = new Vector3(2, 1, 0);

    int playerTurn = 0;

    public FriendChannel[] allChannels;

    public GameObject xPrefab, oPrefab;

    public Image player1TurnIndicator;
    public Image player2TurnIndicator;

    private bool isGameOver;
    int TotalCo;

    int player1Score, player2Score;
    public TMPro.TextMeshProUGUI scoreText1, scoreText2;


    public Transform parentxo;
    void Start()
    {
        MakeGrid();
        Assignchannels();

        playerTurn = Random.Range(1, 3);
        SwitchPlayer();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameOver)
        {
            Vector3 mousePosInWorlCordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isMouseClikcInBoard(mousePosInWorlCordinates))
            {
                FriendNode retrivedNode = GetNodeBasedOffClickPos(mousePosInWorlCordinates);
                if (retrivedNode.nodeType == FriendNodeType.def)
                {
                    AssignNodeToCorrespondingPlayer(retrivedNode);
                    UpdateChannelNodesAmount(retrivedNode);
                    InstatiateXO(retrivedNode);
                    SwitchPlayer();
                    TotalCo++;


                    if (TotalCo == 9)
                    {
                        isGameOver = true;
                        Debug.LogError("Full");
                    }
                    else
                    {
                        foreach (FriendChannel c in allChannels)
                        {
                            if (c.player1NodeAmount == 3)
                            {
                                isGameOver = true;
                                player1Score++;
                                UpdateScoreBoarf();
                            }
                            else if (c.player2NodeAmount == 3)
                            {
                                isGameOver = true;
                                player2Score++;
                                UpdateScoreBoarf();
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
            ResetGame();
    }

    private void ResetGame()
    {
        MakeGrid();
        Assignchannels();
        SwitchPlayer();
        foreach (Transform child in parentxo)
        {
            Destroy(child.gameObject);
        }
        TotalCo = 0;
        isGameOver = false;
    }

    private void UpdateScoreBoarf()
    {
        scoreText1.text = player1Score.ToString();
        scoreText2.text = player2Score.ToString();
    }

    private void AssignNodeToCorrespondingPlayer(FriendNode frr)
    {
        if (playerTurn == 1)
        {
            frr.nodeType = FriendNodeType.player1;
        }
        else
        {
            frr.nodeType = FriendNodeType.player2;
        }
    }

    private void InstatiateXO(FriendNode n)
    {
        if (playerTurn == 1)
        {
            Instantiate(xPrefab, n.position, Quaternion.Euler(0, 0, 45), parentxo);
        }
        else
        {
            Instantiate(oPrefab, n.position, Quaternion.Euler(0, 0, 45), parentxo);
        }
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

    private void Assignchannels()
    {
        allChannels = new FriendChannel[8];

        allChannels[0] = new FriendChannel(new FriendNode[3]);
        allChannels[0].nodes[0] = grid[0, 0];
        allChannels[0].nodes[1] = grid[1, 0];
        allChannels[0].nodes[2] = grid[2, 0];

        allChannels[1] = new FriendChannel(new FriendNode[3]);
        allChannels[1].nodes[0] = grid[0, 1];
        allChannels[1].nodes[1] = grid[1, 1];
        allChannels[1].nodes[2] = grid[2, 1];

        allChannels[2] = new FriendChannel(new FriendNode[3]);
        allChannels[2].nodes[0] = grid[0, 2];
        allChannels[2].nodes[1] = grid[1, 2];
        allChannels[2].nodes[2] = grid[2, 2];

        allChannels[3] = new FriendChannel(new FriendNode[3]);
        allChannels[3].nodes[0] = grid[0, 0];
        allChannels[3].nodes[1] = grid[0, 1];
        allChannels[3].nodes[2] = grid[0, 2];

        allChannels[4] = new FriendChannel(new FriendNode[3]);
        allChannels[4].nodes[0] = grid[1, 0];
        allChannels[4].nodes[1] = grid[1, 1];
        allChannels[4].nodes[2] = grid[1, 2];

        allChannels[5] = new FriendChannel(new FriendNode[3]);
        allChannels[5].nodes[0] = grid[2, 0];
        allChannels[5].nodes[1] = grid[2, 1];
        allChannels[5].nodes[2] = grid[2, 2];


        allChannels[6] = new FriendChannel(new FriendNode[3]);
        allChannels[6].nodes[0] = grid[0, 0];
        allChannels[6].nodes[1] = grid[1, 1];
        allChannels[6].nodes[2] = grid[2, 2];


        allChannels[7] = new FriendChannel(new FriendNode[3]);
        allChannels[7].nodes[0] = grid[2, 0];
        allChannels[7].nodes[1] = grid[1, 1];
        allChannels[7].nodes[2] = grid[0, 2];
    }

    private void SwitchPlayer()
    {
        if (playerTurn == 1)
        {
            playerTurn = 2;

            player1TurnIndicator.color = Color.gray;
            player2TurnIndicator.color = Color.green;
        }
        else
        {
            playerTurn = 1;

            player1TurnIndicator.color = Color.red;
            player2TurnIndicator.color = Color.gray;
        }
    }

    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
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


    private void UpdateChannelNodesAmount(FriendNode pickedNode)
    {
        foreach (FriendChannel cha in allChannels)
        {
            foreach (FriendNode n in cha.nodes)
            {
                if (n == pickedNode)
                {
                    if (playerTurn == 1)
                        cha.player1NodeAmount++;
                    else
                        cha.player2NodeAmount++;
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
[System.Serializable]
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
    public int player1NodeAmount;
    public int player2NodeAmount;

    public FriendChannel(FriendNode[] nodes)
    {
        this.nodes = nodes;
    }
}