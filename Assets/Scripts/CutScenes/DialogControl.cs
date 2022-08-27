using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DialogControl : MonoBehaviour
{
    [Header("GameItens")]
    public GameObject JanelaDialogoPlayer;
    public GameObject JanelaDialogoMago;
    public Text TextoPlayer;
    public Text TextoMago;
    public AudioSource Dino_Bravo_Audio;
    public AudioSource Dino_Normal_Audio;

    public GameObject TextoFinal;
    public GameObject PassaFala;

    [Header("Config")]
    public float typeSpeed;

    private string NomeCena;
    
    private bool trigger = false;
    private bool travaSpace = true;
    private bool passou = false;

    private int num_fala = 0;

    private List<string> ordemFalas = new List<string>();
    private List<string> falas = new List<string>();
    private List<string> tonalidade = new List<string>();

    string[] tonalidadeCT1 = new string[] {"b","n","b","n", "n", "n", "n", "n", "n", "n", "n", "b" };
    string[] ordemFalasTxtCT1 = new string[] { "Dino", "Mago", "Dino", "Mago", "Mago", "Mago", "Mago", "Mago", "Dino", "Mago", "Mago", "Dino" };    
    string[] falasTxtCT1 = new string[] {
        "Ué, onde estou?!?!", 
        "Bem vindo última chance dos dinossauros, sou o celestisauro, o dinossauro celestial...",
        "Quer que eu te morda?",
        "Se acalme, vou te explicar tudo...",
        "Daqui muitos anos a terra será ou seria populada por humanos, seres detentores de poderosas tecnologias",
        "Mas isso só aconteceu devido a Elon misk, um magnata, criador da máquina do tempo",
        "Elon foi o responsável pela extinção de nós, dinossauros!",
        "O que ocorre é que nesse espaço tempo ele quer cometer o que fez com outras civilizações",
        "Mas que M#RD@@, e porque eu?",
        "Você é o único capaz de lutar, o único que tentou sobreviver ao meteóro",
        "A terra depende de você!!!",
        "Vamos amassar latinhas!"
    };

    string[] tonalidadeCT2 = new string[] { "b", "n", "n", "n", "n", "n"};
    string[] ordemFalasTxtCT2 = new string[] { "Dino", "Mago", "Mago", "Mago", "Dino", "Mago" };
    string[] falasTxtCT2 = new string[] {
        "Que CALOR celestisauro, @$#$@!!!!",
        "Meus parabéns dino!",
        "Estamos quase lá!",
        "Os robôs dominaram outro tipo de civilização do futuro, preciso que você de um jeito dele para se preparar!",
        "Que tipo de civilização???",
        "Você verá!"
    };

    string[] tonalidadeCT3 = new string[] { "b", "n", "b", "n", "n", "n", "n", "b", "b"};
    string[] ordemFalasTxtCT3 = new string[] { "Mago", "Mago", "Dino", "Mago", "Dino", "Mago", "Mago", "Mago", "Dino" };
    string[] falasTxtCT3 = new string[] {
        "Lindo, Lindo y Lindo Dino! Que aventura!",
        "Você está quase lá...",
        "Eu só quero que essa Me#$a acabe logo, para mim comer alguma coisa que não seja metal...",
        "Atenção com essa próxima viagem, ela será a decisiva e mais perigosa de todas!",
        "Fale mais...",
        "Você tera contato com os robôs mais fortes de lusk, cuidado! No final, acredito que nosso alvo estará te esperando...",
        "Afinal, você já o derrotou em dois marcos históricos da espécie dele...",
        "Agora será tudo ou nada!!!!!!!",
        "UAAAARRRRRRRRRRRRRRR"
    };



    private void Start()
    {
        NomeCena = SceneManager.GetActiveScene().name;

        if(NomeCena == "CutScene1")
        {
            ordemFalas.AddRange(ordemFalasTxtCT1);
            falas.AddRange(falasTxtCT1);
            tonalidade.AddRange(tonalidadeCT1);
        }
        else if (NomeCena == "CutScene2")
        {
            ordemFalas.AddRange(ordemFalasTxtCT2);
            falas.AddRange(falasTxtCT2);
            tonalidade.AddRange(tonalidadeCT2);
        }
        else if (NomeCena == "CutScene3")
        {
            ordemFalas.AddRange(ordemFalasTxtCT3);
            falas.AddRange(falasTxtCT3);
            tonalidade.AddRange(tonalidadeCT3);
        }

        JanelaDialogoMago.SetActive(false);
        JanelaDialogoPlayer.SetActive(false);
        TextoFinal.SetActive(true);
        PassaFala.SetActive(false);
    }

    private void Update()
    {
        if (num_fala - 1 == falas.Count)
        {
            Passar();
        }
     
        if (Input.GetKeyDown("space") && (travaSpace == false || num_fala == 0))
        {
            trigger = true;
            PassaFala.SetActive(false);
            Debug.Log(trigger);
        }

        Fala();
    }

    public void Fala()
    {
        if(trigger == true)
        {
            trigger = false;
            travaSpace = true;
            JanelaDialogoPlayer.SetActive(false);
            JanelaDialogoMago.SetActive(false);

            num_fala = num_fala + 1;

            if (ordemFalas[num_fala - 1] == "Dino")
            {
                JanelaDialogoPlayer.SetActive(true);
                //Invoke("Textos", 1.0f);
                StartCoroutine(Textos());
            }
            else
            {
                JanelaDialogoMago.SetActive(true);
                //Invoke("Textos", 1.0f);
                StartCoroutine(Textos());
            }
        }
    }

    public void Passar()
    {
        if (!passou)
        {
            JanelaDialogoMago.SetActive(false);
            JanelaDialogoPlayer.SetActive(false);
            TextoFinal.SetActive(true);

            passou = true;
        }

        if (Input.GetKeyDown("space"))
        {
            gameObject.GetComponent<Passa_Cenas>().TriggerPassagem = true;
        }  
    }

    IEnumerator Textos()
    {
        if(tonalidade[num_fala - 1] == "b")
        {
            Dino_Bravo_Audio.Play();
        }
        else
        {
            Dino_Normal_Audio.Play();
        }

        if(ordemFalas[num_fala - 1] == "Mago")
        {
            TextoMago.text = "";
            foreach (char character in (falas[num_fala - 1]).ToCharArray())
            {
                TextoMago.text += character;
                yield return new WaitForSeconds(typeSpeed);
            }
        }
        else
        {
            TextoPlayer.text = "";
            foreach (char character in (falas[num_fala - 1]).ToCharArray())
            {
                TextoPlayer.text += character;
                yield return new WaitForSeconds(typeSpeed);
            }
        }

        Invoke("time", 0.5f);
    }

    void time()
    {
        PassaFala.SetActive(true);
        Dino_Bravo_Audio.Stop();
        Dino_Normal_Audio.Stop();
        travaSpace = false;
    }
}
