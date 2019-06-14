using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    //VARIABLES

    Rigidbody2D rbPj;                                   
    Animator animPj;

    public float fuerza = 10;
    public float velocidadMax = 7;
    public float fuerzaSalto = 300;
    public bool miraDerecha = true;

    Vector2 direction = Vector2.down;
    public LayerMask layerMask;
    

    bool tocaSuelo = false;

    
    public Vector2 mirando = Vector2.right;

    Estadisticas statsPj;

    // Funcion Start en la que se inicializan las variables 
    // que dependen de componentes del objeto
    void Start()
    {
        rbPj = GetComponent<Rigidbody2D>();
        animPj = GetComponent<Animator>();
        statsPj = GetComponent<Estadisticas>();
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //Si el personaje mira hacia un lado su sprite se gira
        //para que mire hacia ese lado
        if (!statsPj.invulnerable)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MueveIzquierda();
                if (miraDerecha) { GiraSprite(); }
            }
                                                            

            if (Input.GetKey(KeyCode.RightArrow))
            {
                MueveDerecha();
                if (!miraDerecha) { GiraSprite(); }

            }
        }


        //Si se pulsa la tecla asignada al salto y el personaje
        //está tocando el suelo, se aplica una fuerza vertical al personaje para que salte
        if (Input.GetButtonDown("Salto") && tocaSuelo)
        {
            rbPj.AddForce(Vector2.up * fuerzaSalto);        
            
        }

        //Se llama a la función que gestiona las animaciones del personaje
        GestionAnimacion();

        
        //Raycast que detecta que el personaje esté tocando el suelo para evitar que el personaje pueda saltar infinitamente hacia arriba sin estar tocando el suelo
        RaycastHit2D hit = Physics2D.Raycast(new Vector3(transform.position.x, (transform.position.y -1.2f), transform.position.z), direction, 0.1f, layerMask);
        
        if (hit)
        {
            Debug.DrawRay(new Vector3(transform.position.x, (transform.position.y -1.2f), transform.position.z), direction * 0.1f, Color.yellow);
            Debug.Log("Did Hit " + hit.transform.name);
            tocaSuelo = true;
        }
        else
        {
            Debug.DrawRay(new Vector3(transform.position.x, (transform.position.y -1.2f), transform.position.z), direction * 0.1f, Color.white);
            Debug.Log("Did not Hit");
            tocaSuelo = false;
        }

        
    }
    //Funcion que hace que el personaje se desplace hacia la izquierda
    void MueveIzquierda()
    {
        mirando = Vector2.left;
        if (rbPj.velocity.x > -velocidadMax)
            rbPj.AddForce(Vector2.left * fuerza);
    }

    //Funcion que hace que el personaje se desplace hacia la derecha
    void MueveDerecha()
    {
        mirando = Vector2.right; 
        if (rbPj.velocity.x < velocidadMax)
            rbPj.AddForce(Vector2.right * fuerza);
    }

    //Funcion que invierte el eje de rotación Y del personaje para que esté orientado
    void GiraSprite()
    {
        transform.Rotate(0f, 180f, 0f);
        miraDerecha = !miraDerecha;
    }

    //Función que gestiona las animaciones
    void GestionAnimacion()
    {
        //Cuando el personaje se está moviendo o se está pulsando la tecla de movimiento se activa la animación de andar
        if (!statsPj.invulnerable)
        {
            if (Input.GetButton("Horizontal") || rbPj.velocity.x != 0)
            {
                animPj.SetBool("andando", true);

            }
            else
            {
                animPj.SetBool("andando", false);

            }
        }
        
        // Cuando se pulsa el botón den agacharse se ejecuta la animación de agacharse
        if (Input.GetButton("Agachar"))
        {
            animPj.SetBool("agachado", true);
        }
        else
        {
            animPj.SetBool("agachado", false);
        }

        //Cuando el jugador pulsa el boton de salto y el personaje está tocando el suelo, se ejecuta la animación de saltar
        if (Input.GetButtonDown("Salto") && tocaSuelo)
        {
            animPj.SetTrigger("salto");            
        }

        animPj.SetBool("tocandoSuelo", tocaSuelo);

    }

    

    


}
