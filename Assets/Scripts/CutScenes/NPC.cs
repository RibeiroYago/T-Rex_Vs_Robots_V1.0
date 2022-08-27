using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Sprite profile;
    public string speechText;
    public string actorName;

    private DialogControl dc;

    private void Start()
    {
        dc = FindObjectOfType<DialogControl>();
    }

    public void Interact_Dialogo()
    {

    }
}
