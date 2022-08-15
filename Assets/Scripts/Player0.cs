using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player0 : MonoBehaviour
{   

    public float Speed;
    public float JumpForce;
    public bool isJumping;
    public bool doubleJump;
    public Color corPadrao;

    
    private Rigidbody2D rig;
    private Animator anim;
    private Vector3 PosicaoIncial;

    // Start is called before the first frame update
    void Start()
    {
        
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        corPadrao = spriteRenderer.color;
        PosicaoIncial = transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    // Função de movimento, horizontal libera os inputs das teclas awsd e setas
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("Correndo", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);

        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("Correndo", true);
            transform.eulerAngles = new Vector3(0f,180f,0f);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Correndo", false);
        }
        


    }

    
    
    // Pular
    void  Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse );
                doubleJump = true;
                anim.SetBool("Pulando", true);
            }
            else
            {
                
                if(doubleJump)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse );
                    doubleJump = false;
                    
                }
            
            }
        }
    }



    // Morte do personagem
    void Morte()
    {
        anim.SetBool("Morte", true);
        StartCoroutine(Dano());


        
        

    }
    

    // gerar mudança de cor ao levar dano; fazer hit kill ao cair na areia movediça;
    IEnumerator Dano()
    {

        playerSprite.color = new Color(255f, 0f,0f);
        
        yield return new WaitForSeconds(0.1f);
        playerSrite.color = corPadrao;
        anim.SetBool("Morte", false);
        transform.position = PosicaoIncial;

        

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 4)
        {
            isJumping = false;
            anim.SetBool("Pulando", false);
        }

        if(collision.gameObject.CompareTag("AreiaMove"))
        {
            
            Morte();

        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 4 )
        {
            isJumping = true;
        }
    }
}
