using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFondos : MonoBehaviour
{

    public Animator animFondo;

    //Cuando el personaje entra en el área del cementerio
    //delimitada por un trigger, se activa la animación
    //que hace aparecer los fondos del cementerio
    //y desaparecer los de la ciudad. Al salir del área
    //ocurre justo lo contrario desaparecen los del cementerio
    //y aparecen los de la ciudad
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animFondo.SetBool("cementerio", true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            animFondo.SetBool("cementerio", false);
        }
    }
}
