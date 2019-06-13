using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoBolaFuego : MonoBehaviour
{
    public int dano;
    public float orientacion;
    SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 1*orientacion,transform.position.y,transform.position.z), 0.1f);

        if (orientacion > 0)
        {
            spr.flipX = true;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Estadisticas>().RecibeDano(dano, transform.position);
            Destroy(gameObject);
        }
        
    }
}
