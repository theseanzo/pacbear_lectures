using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacBear : BaseUnit
{
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        //let's set up movement for PacBear to simply use WASD to set directions
        //alternatively: how could we have this based on a controller?
        CheckForInput();
        Move();//recall this is part of the BaseUnit class and so we have access to it
    }

    //void CheckForKeyBoardInput()
    //{
    //    if (Input.GetKey(KeyCode.A))
    //        direction = new Vector2Int(-1, 0);
    //    if (Input.GetKey(KeyCode.D))
    //        direction = new Vector2Int(1, 0);
    //    if (Input.GetKey(KeyCode.W))
    //        direction = new Vector2Int(0, 1);
    //    if (Input.GetKey(KeyCode.S))
    //        direction = new Vector2Int(0, -1);
    //}
    void CheckForInput()
    {
        if (Input.GetAxis("Horizontal") < 0)
            direction = new Vector2Int(-1, 0);
        if (Input.GetAxis("Horizontal") > 0)
            direction = new Vector2Int(1, 0);
        if (Input.GetAxis("Vertical") < 0)
            direction = new Vector2Int(0, -1);
        if (Input.GetAxis("Vertical") > 0)
            direction = new Vector2Int(0, 1);
    }
}
