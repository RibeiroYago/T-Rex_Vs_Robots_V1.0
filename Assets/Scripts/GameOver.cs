using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameOver : MonoBehaviour
{
    public bool Gameover;
    public string scenereload;
    public RectTransform PanelGameOver;
    public Canvas canva;
    public SpriteRenderer btn;

    private void Update()
    {   
        if (Gameover)
        {
            canva.sortingOrder = 10;
            btn.GetComponent<SpriteRenderer>().sortingOrder = 10;

            if (Input.GetKeyDown("space"))
            {
                Gameover = false;
                SceneManager.LoadScene(scenereload);
            }
        }
    }

}
