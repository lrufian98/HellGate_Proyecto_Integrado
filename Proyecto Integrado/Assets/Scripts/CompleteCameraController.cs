using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{

    public GameObject player;       //Variable para guardar el gameObject del personaje


    private Vector3 offset;         //Variable para guardar la distancia entre la cámara y el jugador

    // Se inicializa el valor
    void Start()
    {
        //Calcula y guarda la distancia entre el jugador y la cámara
        offset = transform.position - player.transform.position;
    }

    //Función que se repite cadad frame
    void LateUpdate()
    {
        //La cámara sigue al personaje, respetando siempre la distancia guardada con anterioridad
        if (player != null)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
