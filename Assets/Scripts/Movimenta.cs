using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimenta : MonoBehaviour
{
    public Vector2 direcao;

    public float velocidade;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(direcao * velocidade * Time.deltaTime);
    }
}
