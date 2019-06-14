using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorConversacion : MonoBehaviour
{
    //Variables
    public Animator panel;
    Animator anim;
    public Animator finJuego;

    ////Función Start en la que se inicializan las variables que dependan de componentes de objetos
    void Start()
    {
        //Esto solo ocurre en el caso de Corvus, ya que los aldeanos no tienen un componente Animator
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Al entrar el personaje en el área definida con un trigger
    //se activa la animación que hace aparecer la caja de texto correspondiente a cada personaje
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //En caso de Corvus también se activa su animación de aparición
            if (anim != null)
            {
                anim.SetBool("Aparece", true);
            }
            panel.SetBool("activo", true);
        }
    }
    //Al salir el personaje del área definida con un trigger
    //se activa la animación que hace desaparecer la caja de texto correspondiente a cada personaje
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            //En caso de Corvus también se activa su animación de desaparición
            if (anim != null)
            {
                anim.SetBool("Aparece", false);
            }
            panel.SetBool("activo", false);
        }
    }

    //En el caso de Corvus, al terminar su animación de desaparición
    //se activa esta función que borra el objeto para que no vuelva a aparecer
    void Destruye()
    {
        //En caso de que sea el último Corvus, que aparece una vez el personaje ha derrotado a Aries
        //Se para el juego y aparece la pantalla de agradecimientos
        if (finJuego != null)
        {
            Time.timeScale = 0;
            finJuego.SetTrigger("activa");
            
        }
        Destroy(gameObject);
    }
    
}
