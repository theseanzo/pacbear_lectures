using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillEffect : MonoBehaviour
{
    // Start is called before the first frame update
    //our pill effect will last 2s
    float lifeTime = 2;
    private void OnEnable() //this is set when an object is made active
    {
        Invoke("Despawn", lifeTime);
    }
    void Despawn()
    {
        PoolManager.instance.Despawn(this.gameObject);

    }
    // Update is called once per frame

}
