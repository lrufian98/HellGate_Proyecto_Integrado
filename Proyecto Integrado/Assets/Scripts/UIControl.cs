using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    bool pausa = false;
    public Animator menuPausa;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
