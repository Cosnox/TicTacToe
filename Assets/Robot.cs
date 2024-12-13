using UnityEngine;

public class Robot : MonoBehaviour
{
    public GameRuler gameRuler;

    private void RobotRandomChoose()
    {
        int randomChannelIndex = Random.Range(0, 8);
        Node pickedNode = null;

        foreach (Node n in gameRuler.allChannels[randomChannelIndex].nodes)
        {
            if (n.nodeType == NodeType.defaultt)
            {
                n.nodeType = NodeType.robot;
                pickedNode = n;

                break;
            }
        }

        UpdateRobotNodesAmountAterRobotPicksNode(pickedNode);
    }

    private void RobotLogicalChoose()
    {
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
