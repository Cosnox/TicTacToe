using UnityEngine;

public class Robot2 : MonoBehaviour
{
    public GameRuler2 gameRuler;

    public void RobotRandomChoose(GameObject o)
    {
        Debug.LogWarning("Random");
        Channel randomChannel = gameRuler.allChannels[Random.Range(0, 8)];
        //print(" 1st Random : " + gameRuler.allChannels.IndexOf(randomChannel));

        while (randomChannel.humanNodesAmount + randomChannel.robotNodesAmount == 3)
        {
            randomChannel = gameRuler.allChannels[Random.Range(0, 8)];
           // print("looping :" + gameRuler.allChannels.IndexOf(randomChannel));
        }
        Node pickedNode = null;

        foreach (Node n in randomChannel.nodes)
        {
            if (n.nodeType == NodeType.defaultt)
            {
                n.nodeType = NodeType.robot;
                pickedNode = n;

                break;
            }
        }

        UpdateRobotNodesAmountAterRobotPicksNode(pickedNode);

        Instantiate(o, pickedNode.position, Quaternion.identity);

    }

    public void RobotLogicalChoose(GameObject o)
    {
        Debug.LogWarning("Logical");
        CalculateChannelsImportance();
        Channel bst = GetTheMostImportantChannel();

        Node pickedNode = null;

        foreach (Node n in bst.nodes)
        {
            if (n.nodeType == NodeType.defaultt)
            {
                n.nodeType = NodeType.robot;
                pickedNode = n;

                break;
            }
        }
        UpdateRobotNodesAmountAterRobotPicksNode(pickedNode);
        Instantiate(o, pickedNode.position, Quaternion.identity);

    }

    private void UpdateRobotNodesAmountAterRobotPicksNode(Node pickedNode)
    {
        foreach (Channel cha in gameRuler.allChannels)
        {
            foreach (Node n in cha.nodes)
            {
                if (n == pickedNode)
                {
                    cha.robotNodesAmount++;
                }
            }
        }
    }

    private Channel GetTheMostImportantChannel()
    {
        int importance = -1;
        Channel bestChannel = null;

        foreach (Channel cha in gameRuler.allChannels)
        {
            if (cha.channelImportance > importance)
            {
                importance = cha.channelImportance;
                bestChannel = cha;
            }
        }

        return bestChannel;
    }

    private void CalculateChannelsImportance()
    {
        foreach (Channel cha in gameRuler.allChannels)
        {
            if (cha.robotNodesAmount == 2 && cha.humanNodesAmount == 0)
            {
                cha.channelImportance = 5;
            }
            else if (cha.robotNodesAmount == 0 && cha.humanNodesAmount == 2)
            {
                cha.channelImportance = 4;
            }
            else if (cha.robotNodesAmount == 1 && cha.humanNodesAmount == 0)
            {
                cha.channelImportance = 3;
            }
            else if (cha.robotNodesAmount == 0 && cha.humanNodesAmount == 0)
            {
                cha.channelImportance = 2;
            }
            else if (cha.robotNodesAmount == 1 && cha.humanNodesAmount == 1)
            {
                cha.channelImportance = 1;
            }
            else
            {
                cha.channelImportance = 0;
            }
        }
    }

}
