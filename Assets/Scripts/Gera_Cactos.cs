﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gera_Cactos : MonoBehaviour
{

    public GameObject[] cactoPrefabs;
    public float delay;
    public float delayEntreCactos;
    

    void Start()
    {
        InvokeRepeating("Gera_Cacto", delay, delayEntreCactos);
    }

    private void Gera_Cacto()
    {
        var qtdCactos = cactoPrefabs.Length;
        var indiceCacto = Random.Range(0, qtdCactos);
        var cactoPrefab = cactoPrefabs[indiceCacto];

        Instantiate(cactoPrefab, transform.position, Quaternion.identity);
    }
    
    void Update()
    {
        
    }
}
