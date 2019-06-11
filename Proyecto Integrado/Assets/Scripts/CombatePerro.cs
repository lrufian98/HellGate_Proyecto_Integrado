using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatePerro : MonoBehaviour
{
    public int vida;
    public int dano;
    bool invulnerable = false;

    GameObject player;
    bool persiguePlayer;

    SpriteRenderer spr;

    Rigidbody2D rb;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        anim.SetFloat("velocidad", Mathf.Abs(rb.velocity.x));
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Estadisticas>().RecibeDano(dano,transform.position);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject;
            persiguePlayer = true;
            StartCoroutine("PersiguePlayer");
            Debug.Log("Voy");
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            persiguePlayer = false;
            StopCoroutine("PersiguePlayer");
        }        
    }
    public void RecibeDano()
    {
        if (invulnerable == false)
        {
            vida--;
            Debug.Log("Golpe Recibido");
            if (vida <= 0)
            {
                Destroy(gameObject);
            }
            invulnerable = true;
            if (player.transform.position.x - transform.position.x < 0)
            {
                rb.AddForce(new Vector2(300f,200f));
                Debug.Log(transform.name + " Vuela Derecha");
            }
            else
            {
                rb.AddForce(new Vector2(-300f, 200f));
                Debug.Log(transform.name + " Vuela Izquierda");

            }
            Invoke("Mortal",0.6f);
        }
        
    }
    
    public void Mortal()
    {
        invulnerable = false;
    }


    IEnumerator PersiguePlayer()
    {
        while (true)
        {
            while (!invulnerable)
            {
                Debug.Log("en camino");
                if (player.transform.position.x - transform.position.x > 0)
                {
                    Debug.Log("dch");
                    if (rb.velocity.x < 4f)
                    {
                        rb.AddForce(Vector2.right * 9f);
                        spr.flipX = true;
                    }

                }
                else if (player.transform.position.x - transform.position.x < 0)
                {
                    Debug.Log("izq");

                    if (rb.velocity.x > -4f)
                    {
                        Debug.Log("fuerza izq");
                        rb.AddForce(Vector2.left * 9f);
                        spr.flipX = false;
                    }
                }
                yield return null;

            }
            yield return null;
        }

    }
}
