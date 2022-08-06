using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public Movimenta_Chao movimenta_Chao;

    public GameObject player;
    public GameObject cameras;
    public GameObject PortalFinal;
    public GameObject Chao1;
    public GameObject Chao2;
    public GameObject Movimenta;

    public Rigidbody2D rb;
    public float forcaPulo;
    public LayerMask layerChao;

    public int multiplicadorPontos;
    public int pontuacaoInicial;

    public Text PontosText;

    public Animator animatorComponent;
    public SpriteRenderer spriteRenderer;

    public AudioSource pular_AudioSource;
    public AudioSource FimDeJogo_AudioSource;

    public float playerVelocity;
    public int vida = 100;
    
    private bool isOnFloor;

    public int pontuacao;
    private int crescente = 0;

    public bool movimentacao = false;
    public bool trava = false;
    private bool camera = false;
    private string look_direction;


    // Start is called before the first frame update
    void Start()
    {
        pontuacaoInicial = 999500;
    }

    // Update is called once per frame
    void Update()
    {
        if(vida == 0)
        {
            return;
        }

        crescente = crescente + 1;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pular();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animatorComponent.SetBool("Pulando", false);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Abaixar();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animatorComponent.SetBool("Abaixando", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (movimentacao)
            {
                Esquerda();
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (movimentacao)
            {
                Direita();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animatorComponent.SetBool("Correndo", false);
            animatorComponent.SetBool("Pulando", true);
        }

        //pontuacao = pontuacao + int.Parse(Time.time.ToString()); --> Outra Opcao
        //pontuacao = Mathf.FloorToInt(Time.time); 
        if (!movimentacao && trava == false)
        {
            pontuacao = crescente / 5 * multiplicadorPontos + pontuacaoInicial; // Mudar depois
            PontosText.text = pontuacao.ToString();
        }
        
        if(pontuacao >= 999999)
        {
            if(movimentacao == false && trava == false)
            {
                look_direction = "Direita";
                animatorComponent.SetBool("Correndo", false);
                animatorComponent.SetBool("Pulando", true);
                movimentacao = true;
                Mostra_Portal();
                //SceneManager.LoadScene(0);
            }
            
            if((int)(player.transform.position.x) == 0 && camera == false)
            {
                cameras.transform.parent = player.transform;
                cameras.transform.position = new Vector3(player.transform.position.x, 1f, -10);
                camera = true;
            }
        }
    }

    void Pular()
    {
        if (isOnFloor)
        {
            animatorComponent.SetBool("Pulando", true);
            rb.AddForce(Vector2.up * forcaPulo);
            pular_AudioSource.Play();
        }   
    }

    void Abaixar()
    {        
        animatorComponent.SetBool("Abaixando", true);
    }

    void Direita()
    {
        animatorComponent.SetBool("Correndo", true);
        animatorComponent.SetBool("Pulando", false);
        transform.position = transform.position + new Vector3(1f * playerVelocity * Time.deltaTime, 0, 0);

        if(look_direction == "Esquerda")
        {
            spriteRenderer.flipX = false;
        }

        look_direction = "Direita";
    }

    void Esquerda()
    {
        animatorComponent.SetBool("Correndo", true);
        animatorComponent.SetBool("Pulando", false);
        transform.position = transform.position + new Vector3(-1f * playerVelocity * Time.deltaTime, 0, 0);

        if (look_direction == "Direita")
        {
            spriteRenderer.flipX = true;
        }

        look_direction = "Esquerda";
    }

    void Mostra_Portal()
    {
        var posicaoChao1 = Chao1.GetComponent<Chao_Infinito>().transform.position.x;
        var posicaoChao2 = Chao2.GetComponent<Chao_Infinito>().transform.position.x;
        

        if (posicaoChao1 > posicaoChao2)
        {
            Debug.Log("Entrou");
            PortalFinal.transform.position = new Vector3(posicaoChao1 + 10, 1.3f, -10);
            Instantiate(PortalFinal, new Vector3(posicaoChao1 + 10, 1.3f, -10), Quaternion.identity);
        }
        else
        {
            Debug.Log("Entrou");
            PortalFinal.transform.position = new Vector3(posicaoChao2 + 10, 1.3f, -10);
            Instantiate(PortalFinal, new Vector3(posicaoChao2 + 10, 1.3f, -10), Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        isOnFloor = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, layerChao);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Inimigo_Cacto"))
        {
            pontuacao = 0;
            crescente = 0;

            animatorComponent.SetBool("Morrendo", true);
            vida = 0;
            Movimenta.GetComponent<Movimenta>().direcao.x = 0;
            Movimenta.GetComponent<Movimenta>().velocidade = 0;


            //SceneManager.LoadScene(0);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//-- Pega e reinicia o nivel atual

            FimDeJogo_AudioSource.Play();
        }
    }
}
