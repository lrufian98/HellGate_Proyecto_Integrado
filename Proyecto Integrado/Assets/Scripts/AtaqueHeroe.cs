using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueHeroe : MonoBehaviour
{
    //Variables
    Animator animPj;
    MovimientoPersonaje mov;
    public LayerMask layerMaskAtaque;

    // Funcion Start que inicializa las variables que dependen de los componentes del objeto
    void Start()
    {
        animPj = GetComponent<Animator>();
        mov = GetComponent<MovimientoPersonaje>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Si se pulsa el botón de atacar ejecuta la animacion de atacar
        if (Input.GetButtonDown("Ataque"))
        {
            animPj.SetTrigger("ataque");

        }

        
    }
    //Esta es la funcion de ataque, que se ejecuta cada frame de la animación de ataque
    public void AnimAtaque()
    {
        //Raycast que detecta si se toca a un enemigo
        RaycastHit2D ataque = Physics2D.Raycast(transform.position, mov.mirando, 2f, layerMaskAtaque);

        //en caso de que se toque un enemigo, se comprueba que tipo de enemigo es y se ejecuta su función de recibir daño
        if (ataque)
        {
            Debug.DrawRay(transform.position, mov.mirando * 2f, Color.red);
            Debug.Log("Ataque a " + ataque.transform.name);
            if (ataque.transform.gameObject.GetComponent<CombatePerro>())
            {
                ataque.transform.gameObject.GetComponent<CombatePerro>().RecibeDano();      
            }
            else if (ataque.transform.gameObject.GetComponent<CombateCraneo>())
            {
                ataque.transform.gameObject.GetComponent<CombateCraneo>().RecibeDano();     
            }
            else if (ataque.transform.gameObject.GetComponent<CombateAries>())
            {
                ataque.transform.gameObject.GetComponent<CombateAries>().RecibeDano();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, mov.mirando * 2f, Color.white);
            Debug.Log("Ataque Fallado");
        }

    }
    
}
