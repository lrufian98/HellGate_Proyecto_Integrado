using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatePerro : MonoBehaviour
{
    //Variables
    public int vida;
    public int dano;
    bool invulnerable = false;

    GameObject player;

    SpriteRenderer spr;

    Rigidbody2D rb;
    Animator anim;
    //Función Start en la que se inicializan las variables que dependan de componentes del objeto
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //Cada frame, se pasa el valor absoluto de la velocidad del enemigo a su animator
    //y en caso de que sea superior a 0, ejecuta la animación de correr
    void FixedUpdate()
    {
        anim.SetFloat("velocidad", Mathf.Abs(rb.velocity.x));
    }

    //Cuando el enemigo entra en contacto con el jugador activa la funcion de recibir daño del mismo
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Estadisticas>().RecibeDano(dano,transform.position);
        }
    }
    //Detecta cuando el personaje entra en el área de visión representada por un collider marcado como trigger
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player = col.gameObject;
            StartCoroutine("PersiguePlayer");       ////Activa una función que se ejecuta en paralelo que hace que el enemigo vaya hacia el personaje
            Debug.Log("Voy");
        }
    }
    //Cuando el personaje abandona el área comentada antes, detiene la función de seguimiento
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StopCoroutine("PersiguePlayer");
        }        
    }
    //Función que se ejecuta cada vez que el personaje alcanza con su arma al enemigo
    public void RecibeDano()
    {
        //si el enemigo no es invulnerable, entonces se le resta un punto de vida
        //se le hace invulnerable durante un pequeño intervalo de tiempo para evitar que un solo ataque interactúe varias veces
        //se le aplica una fuerza que empuja al enemigo en dirección contraria al personaje
        if (invulnerable == false)
        {
            vida--;
            Debug.Log("Golpe Recibido");
            //Cuando la vida baja de 0, activa la animación de muerte
            if (vida <= 0)
            {
                
                anim.SetTrigger("Muerte");
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

    //Función que destruye el enemigo al terminar la animación de muerte
    void Muerte()
    {
        Destroy(gameObject);
    }
    
    //Función que hace volver a ser vulnerable al enemigo
    public void Mortal()
    {
        invulnerable = false;
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
