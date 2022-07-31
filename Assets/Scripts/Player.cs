using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //public Movimenta_Chao movimenta_Chao;

    public Rigidbody2D rb;
    public float forcaPulo;
    public LayerMask layerChao;

    public int multiplicadorPontos;
    public int pontuacaoInicial;

    private bool isOnFloor;

    private int pontuacao;
    private int crescente = 0;

    public Text PontosText;

    public Animator animatorComponent;

    public AudioSource pular_AudioSource;
    public AudioSource FimDeJogo_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        pontuacaoInicial = 996000;
    }

    // Update is called once per frame
    void Update()
    {
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

        //pontuacao = pontuacao + int.Parse(Time.time.ToString()); --> Outra Opcao
        //pontuacao = Mathf.FloorToInt(Time.time); 
    
        pontuacao = crescente / 5 * multiplicadorPontos + pontuacaoInicial; // Mudar depois
        PontosText.text = pontuacao.ToString();

        if(pontuacao >= 999999)
        {
            SceneManager.LoadScene(0);
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

            //SceneManager.LoadScene(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//-- Pega e reinicia o nivel atual

            FimDeJogo_AudioSource.Play();
        }
    }
}
