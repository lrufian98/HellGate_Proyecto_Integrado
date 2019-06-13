using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCraneo : MonoBehaviour
{
    public int vida;
    public int dano;
    bool invulnerable = false;

    GameObject player;
    bool persiguePlayer;

    SpriteRenderer spr;
    Animator anim;
    Rigidbody2D rb;
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
