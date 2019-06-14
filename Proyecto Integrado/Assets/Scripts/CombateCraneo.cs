using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCraneo : MonoBehaviour
{
    //Variables
    public int vida;
    public int dano;
    bool invulnerable = false;

    GameObject player;

    SpriteRenderer spr;
    Animator anim;
    Rigidbody2D rb;
    //Función Start en la que se inicializan las variables que dependan de componentes del objeto
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    
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
            StartCoroutine("PersiguePlayer");
            Debug.Log("Voy");
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
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
                dano = 0;
                anim.SetTrigger("Muerte");
            }
            
            Invoke("Mortal",0.6f);
        }
        
    }

    void Muerte()
    {
        Destroy(gameObject);
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
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.025f);

                    spr.flipX = true;
                    

                }
                else if (player.transform.position.x - transform.position.x < 0)
                {
                    Debug.Log("izq");

                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.025f);

                    spr.flipX = false;
                    
                }
                yield return null;

            }
            yield return null;
        }

    }
}
