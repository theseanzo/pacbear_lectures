using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : BaseUnit
{
    // Start is called before the first frame update
    Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
    };
    // Update is called once per frame
    void Update()
    {
        if (moveTimer > 1f)
        {
            List<Vector2Int> options = new List<Vector2Int>(); //we need to figure out all of the possible options for our ghost to move into. Remember the logic: we can go in any of the 3 directions other than backwards, but only if that direction is available (i.e. it's not a wall)
            //we want to find all possible options, and then randomly select one
            foreach (Vector2Int dir in directions)
            {
                //0 = pill, 1 = wall
                int adjacentObjInGrid = GameManager.instance.grid[nextPosInGrid.x + dir.x, nextPosInGrid.y + dir.y];
                if (adjacentObjInGrid != 1 && dir != -direction) //recall that direction is the current direction of our movement
                {
                    options.Add(dir); //if the potential next location isn't a wall, and it isn't the opposite direction, we can have it as a potential option
                }
                //if we have no options, we have to go backwards
                if (options.Count == 0)//.Count is for a list's length
                {
                    Debug.Log("We have no directions");
                    direction = -direction;
                }
                else
                {
                    direction = options[Random.Range(0, options.Count)];
                }
            }
        }
        Move();
    }
}
