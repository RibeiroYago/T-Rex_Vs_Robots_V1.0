using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets_Script : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRb;
    private Collider2D balaColider;
    private Collider2D coliderInimigo1;
    private Collider2D anotherBala;

    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 movedir = (target.transform.position - transform.position).normalized * speed;
        bulletRb.velocity = new Vector2(movedir.x, movedir.y);
        Destroy(this.gameObject, 2);

        balaColider = GetComponent<PolygonCollider2D>();
        coliderInimigo1 = GameObject.FindGameObjectWithTag("Robo_1_Inimigo").GetComponent<PolygonCollider2D>();
        anotherBala = GameObject.FindGameObjectWithTag("Bala1").GetComponent<PolygonCollider2D>();

        Physics2D.IgnoreCollision(balaColider, coliderInimigo1, true);
        Physics2D.IgnoreCollision(balaColider, anotherBala, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            Destroy(this.gameObject);
        }
    }
}
