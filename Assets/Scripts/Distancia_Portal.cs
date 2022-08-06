﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


        if (distancia < 5f && entrou == false)
        {
            Player.GetComponent<Player>().movimentacao = false;
            Player.GetComponent<Player>().trava = true;

            Instantiate(Msg_Entrar, new Vector3(Player.transform.position.x - 5, 1.8f, 80), Quaternion.identity);
            entrou = true;
        }
    }
}