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

    }

    protected void Move()
    {
        
    }
}
