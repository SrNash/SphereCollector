using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Generales")]      //Variables Generales
    [SerializeField]
    Transform orientation;
    [SerializeField]
    float h;
    [SerializeField]
    float v; //Variables de desplazamiento del Player
    [SerializeField]
    float turnSpeed;    //Variable de velocidad de rotación
    float turnSmoothSpeed = .1f;
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

    [Header("Otros Elementos")]
    [SerializeField]
    Camera cam;

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Configuración movimiento
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 desireDirection = new Vector3 (h, 0f, v).normalized;
        float magnitude = Mathf.Clamp01(desireDirection.magnitude) * speed;
        //desireDirection.Normalize();    //Suavizaremos el desplazamiento del Player

        if (desireDirection.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(desireDirection.x, desireDirection.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSpeed, turnSmoothSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        ySpeed += Physics.gravity.y * Time.deltaTime;
        Vector3 velocity = new Vector3(0f,0f,0f);
        velocity.y = ySpeed * .125f;
        controller.Move(velocity);
       /* ySpeed += Physics.gravity.y * Time.deltaTime;   //Aplicamos gravedad al Player

        Vector3 velocity = desireDirection * magnitude;
        velocity.y = ySpeed;

        controller.Move(velocity * Time.deltaTime);*/

        ///ERROR - El Player deja de detectar el suelo, metodo para solventarlo: forzar a detectar cuando isGrounded = true
        ///  

        //Detectamos si hay Ground o no
        if (controller.isGrounded)
        {
            controller.stepOffset = originalStepOffset;
            ySpeed = -.5f;
            Debug.Log("LLEGUE!");

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

        //Mostraremos la cantidad de Coins que tiene el Player y los actualizaremos
        if (coinsAmount < 10)
        {
            coinsText.text = "00" + coinsAmount.ToString();
        }else
        {
            coinsText.text = "0" + coinsAmount.ToString();
        }
        Debug.Log(controller.isGrounded);
    }
}
