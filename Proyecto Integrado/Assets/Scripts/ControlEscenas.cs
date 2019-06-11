using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEscenas : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void ComienzaPartida()
    {
        SceneManager.LoadScene(1);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }
    public void CerrarJuego()
    {
        Application.Quit();
    }

}
