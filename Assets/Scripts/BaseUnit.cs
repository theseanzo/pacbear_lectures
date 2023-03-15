using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : BaseObject
{
    public float speed;

    protected Vector2Int direction;

    protected Vector2Int nextPosInGrid;

    private Vector3 posInWorld;
    private Vector3 nextPosInWorld;

    protected float moveTimer = Mathf.Infinity;

    private Vector2Int prevDirection;

    // Use this for initialization
    void Start()
    {
        nextPosInGrid = posInGrid; //the initial next position we are going to is going to be our current position
    }

    protected void Move()
    {
        if(moveTimer > 1f) //the reason we check for the value of 1 here is that when we get to a new grid location, we want to do these steps over again (where we find the current grid location and the next one), but we don't want to do this if we are in that new grid location yet
        {
            posInGrid = nextPosInGrid;
            Vector2Int checkPos = posInGrid + direction; //we need to check if our position is valid and we can go there
            //we now need to check if we have gone into a wall
            if (GameManager.instance.grid[checkPos.x, checkPos.y] == 1)
            {
                direction = prevDirection;
            }
            nextPosInGrid = posInGrid + direction;
            //if we're still moving into a wall, just keep our posInGrid the same
            if (GameManager.instance.grid[nextPosInGrid.x, nextPosInGrid.y] == 1)
            {
                nextPosInGrid = posInGrid;
            }

            //convert these to our location in 3d space
            posInWorld = new Vector3(posInGrid.x, 0, posInGrid.y);
            nextPosInWorld = new Vector3(nextPosInGrid.x, 0, nextPosInGrid.y);

            prevDirection = direction;
            moveTimer = 0; //this sets it to 0, so this if statement will not pass until our moveTimer is 1 or greater again

            //lastly, if we are changing direction, we also need to rotate pacbear.
            if (direction.x == -1)
                transform.localEulerAngles = new Vector3(0, -90, 0); //moving left
            if (direction.x == 1)
                transform.localEulerAngles = new Vector3(0, 90, 0); //moving right
            if(direction.y == -1)
                transform.localEulerAngles = new Vector3(0, 180, 0); //moving backwards
            if(direction.y == 1)
                transform.localEulerAngles = new Vector3(0, 0, 0); //moving forwards
        }
        moveTimer += Time.deltaTime * speed;
        //our new position is going to be a blend of current posInwWorld and our nextPosInWorld
        transform.localPosition = Vector3.Lerp(posInWorld, nextPosInWorld, moveTimer);
    }
}
