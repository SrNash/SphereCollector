using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[Header("Generales")]      //Variables Generales
    [SerializeField]
    float h, v; //Variables de desplazamiento del Player
    float turnSpeed;    //Variable de velocidad de rotación
    [SerializeField]
    float speed = .125f;

    //[Header("Colliders")]

    //[Header("Audios")]

    //[Header("Otros Componentes")]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Configuración movimiento
        h = Input.GetAxis("Horizontal") * speed;
        v = Input.GetAxis("Vertical") * speed;
    }
}
