using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Tr : StreamableObject
{
    public string tr;
}



public class Test : MonoBehaviour
{

    public GameObject poolPre;
  
    // Start is called before the first frame update
    void Start()
    {
      ObjectEventPool.CreatePool(2.0f, poolPre);
    }

}
