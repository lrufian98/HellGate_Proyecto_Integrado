using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscenas : MonoBehaviour
{
    //Estas funciones están asignadas a los botones
    //que aparecen en los menús

    //Función que carga la escena de Juego    
    public void ComienzaPartida()
    {
        SceneManager.LoadScene(1);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
    //Función que carga la escena del Menú Principal
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
    //Función que cierra el juego
    public void CerrarJuego()
    {
        Application.Quit();
    }

}
