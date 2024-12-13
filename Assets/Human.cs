using UnityEngine;

public class Human : MonoBehaviour
{
    public GameRuler gameRuler;

    private void UpdateHumanNodesAmountAterHumanPicksNode(Node pickedNode)
    {
        foreach (Channel cha in gameRuler.allChannels)
        {
            foreach (Node n in cha.nodes)
            {
                if (n == pickedNode)
                {
                    cha.humanNodesAmount++;
                }
            }
        }
    }

    private void HumanChoose()
    {
        Vector3 mousePosinWrlDpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (isMouseClikcInBoard(mousePosinWrlDpace))
        {
            Node pickedNode = GetNodeBasedOffClickPos(mousePosinWrlDpace);
            if (pickedNode.nodeType == NodeType.defaultt)
            {
                pickedNode.nodeType = NodeType.human;

                UpdateHumanNodesAmountAterHumanPicksNode(pickedNode);
            }
        }
    }

    private Node GetNodeBasedOffClickPos(Vector3 mousePosinWrlDpace)
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


        return gameRuler.grid[x, y];
    }

    private bool isMouseClikcInBoard(Vector3 clickWorldPos)
    {
        if (clickWorldPos.x > gameRuler.gridStartingPoint.x && clickWorldPos.x < gameRuler.gridrndPoint.x)
        {
            if (clickWorldPos.y > gameRuler.gridStartingPoint.y && clickWorldPos.y < gameRuler.gridrndPoint.y)
                return true;
            else return false;
        }
        else return false;
    }

}
