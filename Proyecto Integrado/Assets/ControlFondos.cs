using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFondos : MonoBehaviour
{

    public Animator animFondo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
