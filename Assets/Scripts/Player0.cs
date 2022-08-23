using UnityEngine;
using UnityEngine.SceneManagement;

public class Player0 : MonoBehaviour
{
    //Publics
    public GameObject dino;
    public GameObject GameOverCanvas;

    public Animator animatorComponent;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;

    public LayerMask layerChao;
    public LayerMask layerArmadilhas;

    public AudioSource pular_AudioSource;
    public AudioSource FimDeJogo_AudioSource;

    public float forcaPulo;
    public float playerVelocity;
    public float vida;

    //Privates
    private bool isOnFloor;
    private string look_direction = "Direita";


    private void Start()
    {
        animatorComponent.SetBool("Correndo", false);
        animatorComponent.SetBool("Abaixando", false);
        animatorComponent.SetBool("Pulando", false);
        animatorComponent.SetBool("Parado", true);
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if(vida <= 0f)
        {
            Morrer();
        }

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
            Esquerda();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Direita();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animatorComponent.SetBool("Correndo", false);
            animatorComponent.SetBool("Parado", true);
        }
    }

    private void FixedUpdate()
    {
        isOnFloor = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, layerChao);
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
        animatorComponent.SetBool("Parado", false);
        transform.position = transform.position + new Vector3(1f * playerVelocity * Time.deltaTime, 0, 0);

        if (look_direction == "Esquerda")
        {
            spriteRenderer.flipX = false;
        }

        look_direction = "Direita";
    }

    void Esquerda()
    {
        animatorComponent.SetBool("Correndo", true);
        animatorComponent.SetBool("Parado", false);
        transform.position = transform.position + new Vector3(-1f * playerVelocity * Time.deltaTime, 0, 0);

        if (look_direction == "Direita")
        {
            spriteRenderer.flipX = true;
        }

        look_direction = "Esquerda";
    }

    public void Morrer()
    {
        if(playerVelocity != 0)
        {
            FimDeJogo_AudioSource.Play();
        }

        playerVelocity = 0;
        forcaPulo = 0;
        PlayerRed(true);

        GameOverCanvas.GetComponent<GameOver>().scenereload = SceneManager.GetActiveScene().name;
        GameOverCanvas.GetComponent<GameOver>().Gameover = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Armadilhas"))
        { 
            animatorComponent.SetBool("Morrendo", true);
            vida = 0;
        }
        else if (other.gameObject.CompareTag("Robo_1_Inimigo"))
        {
            vida = vida - 20;
            PlayerRed(false);
            rb.AddForce(Vector2.up * (forcaPulo/5));
        }
    }

    void PlayerRed(bool trigger)
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
