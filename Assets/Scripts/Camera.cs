using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public Transform player;
    public float smooth = 0.3f;
    public float height;
    public float distance;

    private Vector3 velocity = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 pos = new Vector3();
       pos.x = player.position.x;
       pos.z = player.position.z  - distance;
       pos.y = player.position.y + height;

       transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smooth);


    }
}
