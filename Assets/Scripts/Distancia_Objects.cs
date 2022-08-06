using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distancia_Objects : MonoBehaviour
{

    public GameObject Player;
    public GameObject Enimy;
    public GameObject Msg_Entrar;

    private float distancia;

    // Start is called before the first frame update
    void Start()
    {
        distancia = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        distancia = Vector3.Distance(Player.transform.position, Enimy.transform.position);

        if (Enimy.CompareTag("Finish") && distancia < 2)
        {
            Player.GetComponent<Player>().movimentacao = false;
            Instantiate(Msg_Entrar, new Vector3(15.2f, 1.8f, 80), Quaternion.identity);
        }
    }
}
