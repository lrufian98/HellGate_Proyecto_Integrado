using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateAries : MonoBehaviour
{
    int vida = 10;
    int dano = 1;
    bool invulnerable = false;

    bool rageMode;

    GameObject player;
    bool persiguePlayer;

    SpriteRenderer spr;
    Rigidbody2D rb;
    Animator anim;

    float orientacion;

    public GameObject prefabBolaFuego;
    GameObject saleFuego;
    GameObject puntoIzq;
    GameObject puntoDch;

    public GameObject corvus;

    public BoxCollider2D borde;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        puntoIzq = GameObject.Find("PuntoFuegoIzq");
        puntoDch = GameObject.Find("PuntoFuegoDch");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Estadisticas>().RecibeDano(dano, transform.position);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            borde.enabled = true;
            player = col.gameObject;
            persiguePlayer = true;
            StartCoroutine("PersiguePlayer");
            StartCoroutine("LanzaBolas");
            Debug.Log("Voy");
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            persiguePlayer = false;
            StopAllCoroutines();
        }
    }
    
    public void RecibeDano()
    {
        if (invulnerable == false)
        {
            vida--;
            invulnerable = true;
            spr.color = Color.red;
            Debug.Log("Golpe Recibido");
            if (vida <= 0)
            {
                anim.SetTrigger("Muerte");
            }
            
            
            if (vida <= 5 && !rageMode)
            {
                rageMode = true;
                dano = 2;
                anim.SetBool("RageMode", true);
            }
            Invoke("Mortal", 0.6f);
        }

    }

    void Muerte()
    {
        borde.enabled = false;
        corvus.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject);

    }
   

    public void Mortal()
    {
        spr.color = Color.white;
        invulnerable = false;
    }

    void AnimLanzaBolas()
    {
        anim.SetTrigger("Disparo");
    }

    public void DisparaBolas()
    {
        
        GameObject bolaFuego;
        DanoBolaFuego bolaScript;
        bolaFuego = Instantiate(prefabBolaFuego, saleFuego.transform.position, new Quaternion(0,0,0,0));
        bolaScript = bolaFuego.GetComponent<DanoBolaFuego>();
        bolaScript.dano = dano;
        bolaScript.orientacion = orientacion;

        Destroy(bolaFuego, 2f);
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
                    orientacion = 1;
                    saleFuego = puntoDch;
                    Debug.Log("dch");
                    if (rb.velocity.x < 0.8f)
                    {
                        rb.AddForce(Vector2.right * 9f);
                        spr.flipX = true;
                    }

                }
                else if (player.transform.position.x - transform.position.x < 0)
                {
                    Debug.Log("izq");

                    if (rb.velocity.x > -0.8f)
                    {
                        orientacion = -1;
                        saleFuego = puntoIzq;
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
    IEnumerator LanzaBolas()
    {
        while (true)
        {

            AnimLanzaBolas();

            yield return new WaitForSeconds(Random.Range(3, 6));

        }
    }
}
