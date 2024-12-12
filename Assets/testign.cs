using System.Collections.Generic;
using UnityEngine;

public class testign : MonoBehaviour
{
    private float nodeDia = 0.5f;

    private Node[,] grid = new Node[3, 3];
    Vector3 gridStartingPoint;
    Vector3 gridrndPoint;

    public Color xColor, oColor, defColor;
    [Space(20)]
    public Channel hor0;
    public Channel hor1;
    public Channel hor2;
    [Space(20)]
    public Channel ver0;
    public Channel ver1;
    public Channel ver2;
    [Space(20)]
    public Channel dia0;
    public Channel dia1;


    List<Channel> allChannels = new List<Channel>();

    void Start()
    {
        gridStartingPoint = new Vector3(-1, -2, 0);
        gridrndPoint = new Vector3(2, 1, 0);

        MakeGrid();
        Assignchannels();

    }

    private void MakeGrid()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                grid[x, y] = new Node(new Vector3(gridStartingPoint.x + x + nodeDia, gridStartingPoint.y + y + nodeDia), NodeType.def);
            }
        }
    }

    private void Assignchannels()
    {
        hor0 = new Channel(new Node[3]);
        hor0.nodes[0] = grid[0, 0];
        hor0.nodes[1] = grid[1, 0];
        hor0.nodes[2] = grid[2, 0];
        allChannels.Add(hor0);

        hor1 = new Channel(new Node[3]);
        hor1.nodes[0] = grid[0, 1];
        hor1.nodes[1] = grid[1, 1];
        hor1.nodes[2] = grid[2, 1];
        allChannels.Add(hor1);

        hor2 = new Channel(new Node[3]);
        hor2.nodes[0] = grid[0, 2];
        hor2.nodes[1] = grid[1, 2];
        hor2.nodes[2] = grid[2, 2];
        allChannels.Add(hor2);



        ver0 = new Channel(new Node[3]);
        ver0.nodes[0] = grid[0, 0];
        ver0.nodes[1] = grid[0, 1];
        ver0.nodes[2] = grid[0, 2];
        allChannels.Add(ver0);

        ver1 = new Channel(new Node[3]);
        ver1.nodes[0] = grid[1, 0];
        ver1.nodes[1] = grid[1, 1];
        ver1.nodes[2] = grid[1, 2];
        allChannels.Add(ver1);


        ver2 = new Channel(new Node[3]);
        ver2.nodes[0] = grid[2, 0];
        ver2.nodes[1] = grid[2, 1];
        ver2.nodes[2] = grid[2, 2];
        allChannels.Add(ver2);



        dia0 = new Channel(new Node[3]);
        dia0.nodes[0] = grid[0, 0];
        dia0.nodes[1] = grid[1, 1];
        dia0.nodes[2] = grid[2, 2];
        allChannels.Add(dia0);

        dia1 = new Channel(new Node[3]);
        dia1.nodes[0] = grid[2, 0];
        dia1.nodes[1] = grid[1, 1];
        dia1.nodes[2] = grid[0, 2];
        allChannels.Add(dia1);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosinWrlDpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (isClikcInBoard(mousePosinWrlDpace))
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

                Node pickedNode = grid[x, y];
                if (pickedNode.nodeType == NodeType.def)
                {
                    pickedNode.nodeType = NodeType.x;
                    foreach (Channel cha in allChannels)
                    {
                        foreach (Node n in cha.nodes)
                        {
                            if (n == pickedNode)
                            {
                                cha.human++;
                            }
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateChannelImportance();
            Channel bst = RetrieveBestChannel();

            print(allChannels.IndexOf(bst));
            Node pickedNode = null;

            foreach (Node n in bst.nodes)
            {
                if (n.nodeType == NodeType.def)
                {
                    n.nodeType = NodeType.o;
                    pickedNode = n;

                    break;
                }
            }
            print(pickedNode);

            foreach (Channel cha in allChannels)
            {
                foreach (Node n in cha.nodes)
                {
                    if (n == pickedNode)
                    {
                        cha.bot++;
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            int randomChannelIndex = Random.Range(0, 8);
            Node pickedNode = null;

            foreach (Node n in allChannels[randomChannelIndex].nodes)
            {
                if (n.nodeType == NodeType.def)
                {
                    n.nodeType = NodeType.o;
                    pickedNode = n;

                    break;
                }
            }

            foreach (Channel cha in allChannels)
            {
                foreach (Node n in cha.nodes)
                {
                    if (n == pickedNode)
                    {
                        cha.bot++;
                    }
                }
            }
        }

    }

    private Channel RetrieveBestChannel()
    {
        int importance = -1;
        Channel bestChannel = null;

        foreach (Channel cha in allChannels)
        {
            if (cha.importance > importance)
            {
                importance = cha.importance;
                bestChannel = cha;
            }
        }

        return bestChannel;
    }

    private void CalculateChannelImportance()
    {
        foreach (Channel cha in allChannels)
        {
            if (cha.bot == 2 && cha.human == 0)
            {
                cha.importance = 5;
            }
            else if (cha.bot == 0 && cha.human == 2)
            {
                cha.importance = 4;
            }
            else if (cha.bot == 1 && cha.human == 0)
            {
                cha.importance = 3;
            }
            else if (cha.bot == 0 && cha.human == 0)
            {
                cha.importance = 2;
            }
            else if (cha.bot == 1 && cha.human == 1)
            {
                cha.importance = 1;
            }
            else
            {
                cha.importance = 0;
            }
        }
    }

    private bool isClikcInBoard(Vector3 clickWorldPos)
    {
        if (clickWorldPos.x > gridStartingPoint.x && clickWorldPos.x < gridrndPoint.x)
        {
            if (clickWorldPos.y > gridStartingPoint.y && clickWorldPos.y < gridrndPoint.y)
                return true;
            else return false;
        }
        else return false;
    }

    private void OnDrawGizmos()
    {

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.nodeType == NodeType.x)
                {
                    Gizmos.color = xColor;

                }
                else if (node.nodeType == NodeType.o)
                {
                    Gizmos.color = oColor;
                }
                else
                    Gizmos.color = defColor;

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

public enum NodeType { def, x, o };

[System.Serializable]
public class Channel
{
    public Node[] nodes;
    public int human;
    public int bot;
    public int importance;

    public Channel(Node[] nodes)
    {
        this.nodes = nodes;
    }
}
