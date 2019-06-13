using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estadisticas : MonoBehaviour
{
    public int vidaPj;

    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite voidHeart;

    public Image corazon1;
    public Image corazon2;
    public Image corazon3;
    public Image corazon4;
    public Image corazon5;

    public int dineroPj;

    public bool invulnerable;

    Animator animPj;
    Rigidbody2D rb;

    public Animator animMuerte;



    // Start is called before the first frame update
    void Awake()
    {
        corazon1.sprite = fullHeart;
        corazon2.sprite = fullHeart;
        corazon3.sprite = fullHeart;
        corazon4.sprite = fullHeart;
        corazon5.sprite = fullHeart;

    }
    private void Start()
    {
        animPj = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TiempoInvulnerable()
    {
        invulnerable = true;
        animPj.SetTrigger("golpe");
    }
    public void Mortal()
    {
        invulnerable = false;
    }

    public void RecibeDano(int a, Vector3 enemigo)
    {
        if (invulnerable == false)
        {
            if (enemigo.x - transform.position.x < 0)
            {
                rb.AddForce(new Vector2(200f, 200f));
                Debug.Log(transform.name + " Vuela Derecha");
            }
            else
            {
                rb.AddForce(new Vector2(-200f, 200f));
                Debug.Log(transform.name + " Vuela Izquierda");

            }
            Invoke("Mortal", 0.6f);
            if (vidaPj > 0)
            {
                vidaPj = vidaPj - a;
                if (vidaPj <= 1)
                {
                    corazon1.sprite = halfHeart;
                    corazon2.sprite = voidHeart;
                    corazon3.sprite = voidHeart;
                    corazon4.sprite = voidHeart;
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 2)
                {
                    corazon2.sprite = voidHeart;
                    corazon3.sprite = voidHeart;
                    corazon4.sprite = voidHeart;
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 3)
                {
                    corazon2.sprite = halfHeart;
                    corazon3.sprite = voidHeart;
                    corazon4.sprite = voidHeart;
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 4)
                {
                    corazon3.sprite = voidHeart;
                    corazon4.sprite = voidHeart;
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 5)
                {
                    corazon3.sprite = halfHeart;
                    corazon4.sprite = voidHeart;
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 6)
                {
                    corazon4.sprite = voidHeart;
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 7)
                {
                    corazon4.sprite = halfHeart;
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 8)
                {
                    corazon5.sprite = voidHeart;
                }
                else if (vidaPj <= 9)
                {
                    corazon5.sprite = halfHeart;
                }
                TiempoInvulnerable();
            }
            if (vidaPj < 1)
            {
                corazon1.sprite = voidHeart;
                corazon2.sprite = voidHeart;
                corazon3.sprite = voidHeart;
                corazon4.sprite = voidHeart;
                corazon5.sprite = voidHeart;

                animPj.SetTrigger("Muerte");
            }
        }
        
    }

    void Muerte()
    {
        Destroy(gameObject);
        animMuerte.SetTrigger("activa");
        Time.timeScale = 0;
    }


}
