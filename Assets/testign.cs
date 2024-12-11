using UnityEngine;

public class testign : MonoBehaviour
{
    public GameObject oo;
    private float nodeDia = 0.5f;

    private Node[,] grid = new Node[3, 3];
    Vector3 gridStartingPoint;
    Vector3 gridrndPoint;


    void Start()
    {
        gridStartingPoint = new Vector3(-1, -2, 0);
        gridrndPoint = new Vector3(2, 1, 0);

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {                                        //-1             *   + 0.5      
                grid[x, y] = new Node(new Vector3(gridStartingPoint.x + x + nodeDia, gridStartingPoint.y + y + nodeDia));
            }
        }
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

                oo.transform.position = grid[x, y].position;
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

    //    private void OnDrawGizmos()
    //    {
    //        Gizmos.color = Color.yellow;
    //        //Gizmos.DrawWireCube(grid[2, 2].position, Vector3.one);


    //        if (grid != null)
    //        {
    //            foreach (Node node in grid)
    //                Gizmos.DrawWireCube(node.position, Vector3.one);
    //        }
    //    }
    //}
}

public class Node
{
    public Vector3 position;
    public Node(Vector3 pos)
    {
        position = pos;
    }
}