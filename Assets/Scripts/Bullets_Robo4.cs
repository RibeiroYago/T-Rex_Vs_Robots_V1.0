using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_Robo4 : MonoBehaviour
{
    Transform target;
    public GameObject robo;
    public float speed;
    Rigidbody2D bulletRb;
    private Collider2D balaColider;
    private Collider2D coliderInimigo1;
    private Collider2D anotherBala;
    SpriteRenderer sp;

    public bool trigger = false;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        balaColider = GetComponent<PolygonCollider2D>();
        coliderInimigo1 = GameObject.FindGameObjectWithTag("Robo_1_Inimigo").GetComponent<PolygonCollider2D>();
        //anotherBala = GameObject.FindGameObjectWithTag("Bala1").GetComponent<PolygonCollider2D>();

        Physics2D.IgnoreCollision(balaColider, coliderInimigo1, true);
        //Physics2D.IgnoreCollision(balaColider, anotherBala, true);
    }

    private void Update()
    {
        trigger = false;

        if (transform.position.x > robo.transform.position.x + robo.GetComponent<Robo_Tipo_4>().gapAtaque || transform.position.x < robo.transform.position.x - robo.GetComponent<Robo_Tipo_4>().gapAtaque)
        {
            transform.position = Vector2.MoveTowards(transform.position, robo.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        if (robo.GetComponent<Robo_Tipo_4>().look_direction == "d")
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            //Debug.Log("Chao");
            //transform.position = Vector2.MoveTowards(transform.position, robo.transform.position, speed * Time.deltaTime);
            //Invoke("BulletIN", 1f);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            if (robo.GetComponent<Robo_Tipo_4>().look_direction == "d")
            {
                collision.gameObject.GetComponent<Player0>().rb.AddForce(new Vector2(1, -1) * 250);
            }
            else
            {
                collision.gameObject.GetComponent<Player0>().rb.AddForce(new Vector2(1, -1) * 250);
            }

            collision.gameObject.GetComponent<Player0>().pular_AudioSource.Play();

            //transform.position = Vector2.MoveTowards(transform.position, robo.transform.position, speed * Time.deltaTime);
        }
    }

}
