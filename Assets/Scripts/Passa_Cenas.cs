﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Passa_Cenas : MonoBehaviour
{
    public bool TriggerPassagem = false;

    private string cenaAtual;
    private List<string> ordemCenas = new List<string>();

    private int numeroLista, tamanhoLista;

    string[] fases = new string[] {"Menu_Principal", "Preludio", "CutScene1", "Fase1", "CutScene2", "Fase2", "CutScene3", "Fase3", "CutScene4", "Fase4", "CutScene5"};


    // Start is called before the first frame update
    void Start()
    {
        ordemCenas.AddRange(fases);
        cenaAtual = SceneManager.GetActiveScene().name;
        numeroLista = ordemCenas.IndexOf(cenaAtual);
        tamanhoLista = ordemCenas.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (TriggerPassagem == true || cenaAtual == "Menu_Principal")
        {
            if (Input.GetKeyDown("space"))
            {
                Debug.Log("Passa_Level");
                PASSA_LEVEL();
            }
        }

        
    }

    public void INICIA_JOGO()
    {
        SceneManager.LoadScene(ordemCenas[1]);
    }

    public void FECHA_JOGO()
    {
        Application.Quit();
    }

    public void PASSA_LEVEL()
    {
        if(tamanhoLista != numeroLista)
        {
            Debug.Log("Passa_If");
            SceneManager.LoadScene(ordemCenas[numeroLista + 1]);
        }
        else
        {
            SceneManager.LoadScene(ordemCenas[0]);
        }
    }

}
