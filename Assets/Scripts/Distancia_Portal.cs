using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Distancia_Portal : MonoBehaviour
{

    public GameObject Player;
    public GameObject Enimy;
    public GameObject Msg_Entrar;

    private float distancia;
    private bool entrou = false;

    // Start is called before the first frame update
    void Start()
    {
        distancia = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Player.GetComponent<Player>().PortalFinal.transform.position.x - Player.transform.position.x;
        //distancia = transform.position.x - Player.transform.position.x;

        //Debug.Log(Player.GetComponent<Player>().PortalFinal.transform.position.x); 


        if (distancia < 5f && entrou == false && SceneManager.GetActiveScene().name == "Preludio")
        {
            Player.GetComponent<Player>().movimentacao = false;
            Player.GetComponent<Player>().trava = true;

            Instantiate(Msg_Entrar, new Vector3(Player.transform.position.x - 5, 1.8f, 80), Quaternion.identity);

            Player.GetComponent<Passa_Cenas>().TriggerPassagem = true;
            entrou = true;
        }
        else if(distancia < 5f && entrou == false)
        {
            Instantiate(Msg_Entrar, new Vector3(transform.position.x, 6f, 80), Quaternion.identity);

            Player.GetComponent<Passa_Cenas>().TriggerPassagem = true;
            entrou = true;
        }
    }
}
