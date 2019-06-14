using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateAries : MonoBehaviour
{

    //Variables
    int vida = 10;
    int dano = 1;
    bool invulnerable = false;

    bool rageMode;

    GameObject player;
    

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

    //Función Start en la que se inicializan las variables que dependan de componentes de objetos
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        //estas variables se inicializan con los objetos de el nombre indicado que existan en la escena
        puntoIzq = GameObject.Find("PuntoFuegoIzq");
        puntoDch = GameObject.Find("PuntoFuegoDch");
    }

    //Cuando el enemigo entra en contacto con el jugador activa la funcion de recibir daño del mismo
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Estadisticas>().RecibeDano(dano, transform.position);
        }
    }
    //Detecta cuando el personaje entra en el área de visión representada por un collider marcado como trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            borde.enabled = true;          //Activa un borde que evita que el personaje salga del área de combate
            player = col.gameObject;
            StartCoroutine("PersiguePlayer");       //Activa una función que se ejecuta en paralelo que hace que el enemigo vaya hacia el personaje
            StartCoroutine("LanzaBolas");           //Activa una función que se ejecuta en paralelo que hace que el enemigo dispare bolas de fuego
            Debug.Log("Voy");
        }
    }
    //Cuando el personaje abandona el área comentada antes, detiene las funciones de seguimiento y de disparo
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }
    
    //Función que se ejecuta cada vez que el personaje alcanza con su arma al enemigo
    public void RecibeDano()
    {
        //si el enemigo no es invulnerable, entonces se le resta un punto de vida
        //se le hace invulnerable durante un pequeño intervalo de tiempo para evitar que un solo ataque interactúe varias veces
        //se cambia el color a rojo para que el jugador sepa que ha conectado el golpe
        if (invulnerable == false)
        {
            vida--;
            invulnerable = true;
            spr.color = Color.red;
            Debug.Log("Golpe Recibido");
            //Si la vida baja de 0, entonces se ejecuta la animación de muerte
            if (vida <= 0)
            {
                anim.SetTrigger("Muerte");
            }
            
            //Si la vida del enemigo baja hasta 5, activa el modo furia del enemigo
            //aumentando el daño que inflinge a 2 puntos, y activando las animaciones del modo furia
            if (vida <= 5 && !rageMode)
            {
                rageMode = true;
                dano = 2;
                anim.SetBool("RageMode", true);
            }
            Invoke("Mortal", 0.6f);
        }

    }

    //Cuando se termina la animación de muerte, se deshabilita el borde
    //que evita que el personaje salga, activa la posibilidad de interactuar con Corvus,
    //y destruye el objeto
    void Muerte()
    {
        borde.enabled = false;
        corvus.GetComponent<BoxCollider2D>().enabled = true;
        Destroy(gameObject);

    }
   
    //Función que devuelve a la normalidad el enemigo
    //Vuelve a ser vulnerable y el sprite recupera sus colores originales
    public void Mortal()
    {
        spr.color = Color.white;
        invulnerable = false;
    }

    //Función que activa la animación de disparo del enemigo
    void AnimLanzaBolas()
    {
        anim.SetTrigger("Disparo");
    }

    //Función que se ejecuta al final de la animación de disparo
    //que crea una bola de fuego que se desplaza horizontalmente en la dirección hacia la que esté el enemigo mirando
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

    //Corrutina que hace que el enemigo persiga al personaje teniendo en cuenta la posición del mismo
    //para ir en una dirección u otra
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
    //Corrutina que cada intervalo de 3 a 6 segundos activa la animación de disparar
    IEnumerator LanzaBolas()
    {
        while (true)
        {

            AnimLanzaBolas();

            yield return new WaitForSeconds(Random.Range(3, 6));

        }
    }
}
