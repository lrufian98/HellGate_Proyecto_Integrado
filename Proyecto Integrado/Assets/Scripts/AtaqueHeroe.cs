using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueHeroe : MonoBehaviour
{
    Animator animPj;
    MovimientoPersonaje mov;
    public LayerMask layerMaskAtaque;

    // Start is called before the first frame update
    void Start()
    {
        animPj = GetComponent<Animator>();
        mov = GetComponent<MovimientoPersonaje>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Ataque"))
        {
            animPj.SetTrigger("ataque");

        }

        
    }
    public void AnimAtaque()
    {
        RaycastHit2D ataque = Physics2D.Raycast(transform.position, mov.mirando, 2f, layerMaskAtaque);

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
