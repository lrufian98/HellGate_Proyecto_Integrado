using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorConversacion : MonoBehaviour
{
    public Animator panel;
    Animator anim;
    public Animator finJuego;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (anim != null)
            {
                anim.SetBool("Aparece", true);
            }
            panel.SetBool("activo", true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (anim != null)
            {
                anim.SetBool("Aparece", false);
            }
            panel.SetBool("activo", false);
        }
    }

    void Destruye()
    {
        if (finJuego != null)
        {
            Time.timeScale = 0;
            finJuego.SetTrigger("activa");
            
        }
        Destroy(gameObject);
    }
    
}
