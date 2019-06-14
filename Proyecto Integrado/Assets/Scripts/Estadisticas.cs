using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Estadisticas : MonoBehaviour
{
    //Variables
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



    //Al aparecer, los corazones de vida de la interfaz se llenan
    void Awake()
    {
        corazon1.sprite = fullHeart;
        corazon2.sprite = fullHeart;
        corazon3.sprite = fullHeart;
        corazon4.sprite = fullHeart;
        corazon5.sprite = fullHeart;

    }
    //Al empezar se inicializan las variables que dependen de componentes del objeto
    private void Start()
    {
        animPj = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Función que vuelve al personaje invulnerable al recibir un golpe, y activa la animación de recibir daño del personaje
    public void TiempoInvulnerable()
    {
        invulnerable = true;
        animPj.SetTrigger("golpe");
    }
    //Función que hace que el personaje deje de ser invulnerable, que se ejecuta al terminar la animación de recibir daño
    public void Mortal()
    {
        invulnerable = false;
        Debug.Log("mortal");
    }

    //Funcion de recibir daño del personaje, los enemigos acceden a la función y le dicen el daño que hacen y la posicion del enemigo
    public void RecibeDano(int a, Vector3 enemigo)
    {
        //Si el personaje no es invulnerable
        if (invulnerable == false)
        {
            if (enemigo.x - transform.position.x < 0)
            {
                rb.AddForce(new Vector2(200f, 200f));
                Debug.Log(transform.name + " Vuela Derecha");
            }                                                               //se lanza al personaje hacia un lado o hacia otro
                                                                            //en funcion de donde se encuentre el enemigo
            else
            {
                rb.AddForce(new Vector2(-200f, 200f));
                Debug.Log(transform.name + " Vuela Izquierda");

            }
            //Al recibir daño se comprueba la vida del personaje y si es mayor que cero, se resta vida
            //se actualiza la interfaz para que la cantidad de vida esté acorde con los corazones que la representan en la interfaz
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
                //Tras cambiar la interfaz, se ejecuta la funcion comentada antes
                TiempoInvulnerable();
            }
            //Si al recibir el golpe el personaje tiene menos de 1 de vida,
            //se cambian los corazones de la interfaz a corazones vacíos y se ejecuta la animacion de muerte
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
    //Cuando termina la animación de muerte se ejecuta esta funcion que destruye el objeto y hace aparecer la pantalla de muerte
    void Muerte()
    {
        Destroy(gameObject);
        animMuerte.SetTrigger("activa");
        Time.timeScale = 0;
    }


}
