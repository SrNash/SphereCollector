using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Generales")]      //Variables Generales
    [SerializeField]
    float h;
    [SerializeField]
    float v; //Variables de desplazamiento del Player
    float turnSpeed;    //Variable de velocidad de rotación
    [SerializeField]
    float speed;
    [SerializeField]
    float ySpeed;
    [SerializeField]
    float jumpSpeed;
    float originalStepOffset;

    [Header("CoinsCollector")]
    public int coinsAmount;

    [Header("UI")]
    [SerializeField]
    Canvas playerCanvas;
    [SerializeField]
    TextMeshProUGUI coinsText;


    [Header("Otros Componentes")]
    [SerializeField]
    Rigidbody rg;
    [SerializeField]
    CharacterController controller; //Usaré el componente de CharacterController para usar la gravedad para que el 
                                    //Player caiga cuando no detecte suelo

    // Start is called before the first frame update
    void Start()
    {

        /// <summary>
        /// Obtenemos los componentes de RigidBody y CharacterController
        /// </summary>

        rg = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        originalStepOffset = controller.stepOffset;
        
        //En caso de que no se asignen los componentes los coge automaticamente
        if(rg == null)
        {
            rg = GetComponent<Rigidbody>();
        }
        if (controller == null)
        {
            controller = GetComponent<CharacterController>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Configuración movimiento
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 desireDirection = new Vector3(h, 0, v); //Dirección a la que el Player se desplazará
        float magnitude = Mathf.Clamp01(desireDirection.magnitude) * speed;
        desireDirection.Normalize();    //Suavizaremos el desplazamiento del Player

        ySpeed += Physics.gravity.y * Time.deltaTime;   //Aplicamos gravedad al Player

        //Detectamos si hay Ground o no
        if (controller.isGrounded)
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -9.7f;

            //Saltamos o no
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }
        else
        {
            controller.stepOffset = 0;
        }


        Vector3 velocity = desireDirection * magnitude;
        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);

        if (desireDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(desireDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
        }

        //Mostraremos la cantidad de Coins que tiene el Player y los actualizaremos
        coinsText.text = coinsAmount.ToString();
    }
}
