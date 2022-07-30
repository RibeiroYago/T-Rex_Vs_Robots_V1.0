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

    public Text PontosText;

    public Animator animatorComponent;

    // Start is called before the first frame update
    void Start()
    {
        pontuacaoInicial = 999000;
    }

    // Update is called once per frame
    void Update()
    {
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
    
        pontuacao = Mathf.FloorToInt(Time.time) * multiplicadorPontos + pontuacaoInicial; // Mudar depois
        PontosText.text = pontuacao.ToString();

        if(pontuacao >= 999999)
        {
            SceneManager.LoadScene(0);
        }
    }

    void Pular()
    {
        animatorComponent.SetBool("Pulando", true);
        if (isOnFloor)
        {
            rb.AddForce(Vector2.up * forcaPulo);
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
            //SceneManager.LoadScene(0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//-- Pega e reinicia o nivel atual
        }
    }
}
