using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chao_Infinito : MonoBehaviour
{
    public float difx;
    public float momentoDesaparecer1;
    


    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.x < momentoDesaparecer1)
        {
            transform.position = new Vector3(2 * difx + transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
