using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigos : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D head;

    public float velInimigo;
    public int gapMovimentacao;

    private float posicaoPlayer;
    private float posicaoInicialInimigo;
    public string look_direction = "d";

    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        posicaoInicialInimigo = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        posicaoPlayer = player.GetComponent<Player0>().transform.position.x;
        //posicaoPlayer[1] = player.GetComponent<Player0>().transform.position.y;

        if (transform.position.x >= posicaoInicialInimigo + gapMovimentacao)
        {
            look_direction = "e";
        }
        else if (transform.position.x <= posicaoInicialInimigo - gapMovimentacao)
        {
            look_direction = "d";
        }

        if (posicaoInicialInimigo - gapMovimentacao <= posicaoPlayer && posicaoPlayer <= posicaoInicialInimigo + gapMovimentacao)
        {
            Perseguir_Player();
        }
        else
        {
            if (look_direction == "d")
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(posicaoInicialInimigo + gapMovimentacao, transform.position.y, 0), velInimigo * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(posicaoInicialInimigo - gapMovimentacao, transform.position.y, 0), velInimigo * Time.deltaTime);
            }
        }

        if (look_direction == "d")
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        //Perseguir_Player();
    }

    void Perseguir_Player()
    {
        if(posicaoPlayer <= transform.position.x)
        {
            look_direction = "e";
        }
        else if(posicaoPlayer >= transform.position.x)
        {
            look_direction = "d";
        }
       
        transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Player0>().transform.position, velInimigo * Time.deltaTime * 2);
    }
}
