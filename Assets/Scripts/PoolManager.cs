using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The purpose for this is to store objects that have been used and keep track of resources
public class PoolManager : Singleton<PoolManager>

{

    private string folderPath = "PoolObjects";
    private Dictionary<string,Stack<GameObject>> nameToObjects = new Dictionary<string, Stack<GameObject>>();
    // Start is called before the first frame update
    void Awake()
    {
        //we want to load all gameobjects from the resources folder
        GameObject[] resources = Resources.LoadAll<GameObject>(folderPath);
        foreach (GameObject objPrefab in resources)
        {
            Stack<GameObject> objStack = new Stack<GameObject>();//create a stack
            objStack.Push(objPrefab);//add our prefab to the stack
            nameToObjects.Add(objPrefab.name, objStack);//add this stack with the objPrefab name to the dictionary
        }
    }
    public GameObject Spawn(string name)
    {
        //recall: we want to spawn or create a new item if our stack is running out of items
        Stack<GameObject> objStack = nameToObjects[name]; //instead of using an array index, we use a string value. This is because dictionaries are key/value pairs, and the key for our dictionary is a string.
        //if only 1 element is left in our stack, we need to create/instantiate a new object
        
        if(objStack.Count == 1) //we are left with our original prefab
        {
            GameObject newObject = Instantiate(objStack.Peek()); //to peek is to look at the item that would be popped
            newObject.name = name;
            return newObject;
        }

        //if we have more than one item, we don't want to instantiate an item in the stack, and instead we want to take the top object of the stack
        GameObject topObject = objStack.Pop();
        topObject.SetActive(true);
        return topObject;
    }
    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        nameToObjects[obj.name].Push(obj); //we set an item to be inactive when we despawn it, and then we push it into our stack
    }
    // Update is called once per frame

}
