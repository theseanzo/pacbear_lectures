using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public BaseObject[] objectPrefabs;
    //this is a jagged array. Which means an array of arrays
    //The difference between a jagged array and a 2D array, is that a jagged array can have multiple sub-arrays with different lengths
    //public int[][] jaggedGrid;
    private int _numPillsLeft = 0;
    public int NumPillsLeft
    {
        get
        {
            return _numPillsLeft;
        }
        set
        {
            _numPillsLeft = value;
            //if we set our numPillsLeft to be 0, we are done our level and we should reload it
            if(_numPillsLeft <= 0) 
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    //This is a 2D array
    //0 = pill, 1 = wall, 2 = ghost, 3 = macman, 4 = specialPill
    public int[,] grid = new int[,]
    {
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
        { 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1 },
        { 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1 },
        { 1, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
        { 1, 1, 0, 1, 0, 0, 0, 1, 1, 3, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1 },
        { 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1 },
        { 1, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
        { 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1 },
        { 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 0, 0, 1, 1, 1, 0, 1, 1 },
        { 1, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1, 4, 1, 0, 0, 0, 4, 1 },
        { 1, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 0, 0, 0, 0, 1, 1, 2, 1 },
        { 1, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
    };
    // Start is called before the first frame update
    
    void Awake()
    {
        //how are we going to generate our level?
        //we need to figure out the width and height of our grid and then iterate over the entire grid, creating objects
        GameManager.instance = this; //assign our game manager instancew that can be referenced in the world

        int gridSizeX = grid.GetLength(0); //when finding the length of a multi-dimensional array, we don't use .Length, we have to get the length of a particular dimension. So GetLength(0) gets the length of our first dimension (our x dimension)
        int gridSizeY = grid.GetLength(1);
        Debug.Log(string.Format("Grid sizes are {0}, {1}", gridSizeX, gridSizeY));
        for(int i = 0; i < gridSizeX; i++)
        {
            for(int j = 0; j < gridSizeY; j++)
            {
                //now we do the magic of creation
                int gridValue = grid[i, j]; //this gives us the ith row by the jth column
                //we saved, in the Unity editor, an array of prefabs called objectPrefabs. How do we use this gridValue to create one of these?
                BaseObject objectClone = Instantiate(objectPrefabs[gridValue], new Vector3(i, 0, j), Quaternion.identity); //how do i create a prefab here?
                objectClone.posInGrid = new Vector2Int(i, j);
                if(gridValue == 0) //since 0 is for pills, every time we find a pill, add it to the list that needs to be collected
                {
                    NumPillsLeft++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
