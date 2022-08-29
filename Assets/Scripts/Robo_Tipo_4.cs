using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo_Tipo_4 : MonoBehaviour
{
    public GameObject player;
    public BoxCollider2D head;
    //public Animator Animator;
    public GameObject Bullet1;
    public GameObject Bullet2;
    public GameObject BulletPai;
    public float fireRate = 1f;
    public float nextfireTime;
    public bool BulletIN;
    public bool trigger;


    public float velInimigo;
    public int gapMovimentacao;
    public int gapAtaque;

    private float posicaoPlayer;
    private float posicaoInicialInimigo;
    public string look_direction = "d";

    private int vida = 300;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        posicaoInicialInimigo = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(vida <= 0)
        {
            return;
        }

        posicaoPlayer = player.GetComponent<Player0>().transform.position.x;
        //posicaoPlayer[1] = player.GetComponent<Player0>().transform.position.y;

        if (transform.position.x + 0.2 >= posicaoInicialInimigo + gapMovimentacao)
        {
            look_direction = "e";
        }
        else if (transform.position.x - 0.2 <= posicaoInicialInimigo - gapMovimentacao)
        {
            look_direction = "d";
        }

        if (posicaoInicialInimigo - gapAtaque <= posicaoPlayer && posicaoPlayer <= posicaoInicialInimigo + gapAtaque)
        {
            Atirar();
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Andando", true);
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

    void Atirar()
    {
        gameObject.GetComponent<Animator>().SetBool("Atirando", true);
        if (posicaoPlayer <= transform.position.x)
        {
            look_direction = "e";
        }
        else if (posicaoPlayer >= transform.position.x)
        {
            look_direction = "d";
        }

        //Debug.Log(nextfireTime < Time.time);

        if (BulletIN == true)
        {
            Debug.Log("IN");
            BulletIN = false;
            Instantiate(Bullet1, BulletPai.transform.position, Quaternion.identity);
            Invoke("SegundaMao", 1f); 
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.GetComponent<Player0>().transform.position, velInimigo * Time.deltaTime * 1);
        }
    }

    void SegundaMao()
    {
        Instantiate(Bullet2, BulletPai.transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D Collision2D)
    {
        if (Collision2D.gameObject.transform.position.y > transform.position.y && Collision2D.gameObject.CompareTag("Player"))
        {
            //Morrer();
            //player.GetComponent<Player0>().animatorComponent.SetBool("Pulando", true);

            if(look_direction == "d")
            {
                player.GetComponent<Player0>().rb.AddForce(new Vector2(1, -1) * 250);
            }
            else{
                player.GetComponent<Player0>().rb.AddForce(new Vector2(1, 1) * 250);
            }
            
            player.GetComponent<Player0>().pular_AudioSource.Play();

            vida = vida - 20;
            if(vida <= 0)
            {
                gameObject.GetComponent<Animator>().SetBool("Morrendo", true);
                Invoke("Mata", 1f);
            }
            else
            {
                EnimyRed(false);
            }

            
        }
    }

    /*void Morrer()
    {
        Animator.SetBool("Morrendo", true);

        Invoke("Mata", 0.5f);
    }*/

    void Mata()
    {
        gameObject.GetComponent<Passa_Cenas>().TriggerPassagem = true;
        player.GetComponent<Player0>().vida = player.GetComponent<Player0>().vida + 100;
        Destroy(gameObject);
    }

    void EnimyRed(bool trigger)
    {
        spriteRenderer.color = Color.red;

        if (!trigger)
        {
            Invoke("NormalizaCor", 0.5f);
        }

    }

    void NormalizaCor()
    {
        spriteRenderer.color = Color.white;
    }
}
