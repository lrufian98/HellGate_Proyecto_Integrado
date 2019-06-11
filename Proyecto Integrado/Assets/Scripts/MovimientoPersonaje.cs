using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{
    Rigidbody2D rbPj;
    Animator animPj;

    public float fuerza = 10;
    public float velocidadMax = 5;
    public float fuerzaSalto = 300;
    public bool miraDerecha = true;

    Vector2 direction = Vector2.down;
    public LayerMask layerMask;
    

    bool tocaSuelo = false;

    
    public Vector2 mirando = Vector2.right;

    Estadisticas statsPj;

    // Use this for initialization
    void Start()
    {
        rbPj = GetComponent<Rigidbody2D>();
        animPj = GetComponent<Animator>();
        statsPj = GetComponent<Estadisticas>();
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
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
        


        if (Input.GetButtonDown("Salto") && tocaSuelo)
        {
            rbPj.AddForce(Vector2.up * fuerzaSalto);
            
        }

        GestionAnimacion();


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

    void MueveIzquierda()
    {
        mirando = Vector2.left;
        if (rbPj.velocity.x > -velocidadMax)
            rbPj.AddForce(Vector2.left * fuerza);
    }

    void MueveDerecha()
    {
        mirando = Vector2.right; 
        if (rbPj.velocity.x < velocidadMax)
            rbPj.AddForce(Vector2.right * fuerza);
    }

    void GiraSprite()
    {
        transform.Rotate(0f, 180f, 0f);
        miraDerecha = !miraDerecha;
    }

    void GestionAnimacion()
    {
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
        if (Input.GetButton("Horizontal") || rbPj.velocity.x != 0)
        {
            animPj.SetBool("andando", true);

        }
        else
        {
            animPj.SetBool("andando", false);

        }

        if (Input.GetButton("Agachar"))
        {
            animPj.SetBool("agachado", true);
        }
        else
        {
            animPj.SetBool("agachado", false);
        }
        if (Input.GetButtonDown("Salto") && tocaSuelo)
        {
            animPj.SetTrigger("salto");            
        }

        animPj.SetBool("tocandoSuelo", tocaSuelo);

    }

    

    


}
