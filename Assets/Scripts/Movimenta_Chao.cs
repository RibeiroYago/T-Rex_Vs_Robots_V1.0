using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimenta_Chao : MonoBehaviour
{
    public Vector2 direcao;

    public GameObject pontuacaoPlayer;

    public float velocidade;
    
    void Start()
    {
        
    }

    void Update()
    {

        if (pontuacaoPlayer.GetComponent<Player>().pontuacao >= 999999 || pontuacaoPlayer.GetComponent<Player>().vida == 0)
        {
            velocidade = 0;
        }

        transform.Translate(direcao * velocidade * Time.deltaTime);
    }
}
