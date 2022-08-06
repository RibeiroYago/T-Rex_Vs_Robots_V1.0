using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gera_Cactos : MonoBehaviour
{

    public GameObject[] cactoPrefabs;
    public GameObject[] dinossauroInimigo;

    public GameObject pontuacaoPlayer;

    public float delay;
    public float delayEntreCactos;

    private float ultimoTempo = 0;

    void Start()
    {
        InvokeRepeating("Gera_Cacto", delay, delayEntreCactos);
    }

    private void Gera_Cacto()
    {
        ultimoTempo = Time.time;
        if (pontuacaoPlayer.GetComponent<Player>().pontuacao + 1 <= 999999)
        {
            var qtdCactos = cactoPrefabs.Length;
            var indiceCacto = Random.Range(0, qtdCactos);
            var cactoPrefab = cactoPrefabs[indiceCacto];

            Instantiate(cactoPrefab, transform.position, Quaternion.identity);
        } 
    }
    
    void Update()
    {
        /*if (pontuacaoPlayer.GetComponent<Player>().pontuacao + 1 <= 999999)
        {
            if (Time.time > delay)
            {
                Debug.Log("Delay: " + delayEntreCactos);
                if (Time.time - ultimoTempo > delayEntreCactos)
                {
                    Debug.Log(Time.time - ultimoTempo);
                    Gera_Cacto();
                    delayEntreCactos = Random.Range(0.5f, 2f);
                }
            }
        }
     */    
    }
}
