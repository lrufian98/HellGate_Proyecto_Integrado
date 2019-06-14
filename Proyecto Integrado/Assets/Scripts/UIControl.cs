using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    bool pausa = false;
    public Animator menuPausa;

    //Se detecta cuando se pulsa la tecla "Escape"
    //y activa la función Pausa, que detiene el tiempo
    //y hace aparecer el menú de pausa en pantalla
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pausa();
        }
    }
    public void Pausa()
    {
        pausa = !pausa;
        menuPausa.SetBool("activo", pausa);
        if (pausa)
        {
            Time.timeScale = 0f;
        }
        else if (!pausa)
        {
            Time.timeScale = 1f;
        }
    }
}
