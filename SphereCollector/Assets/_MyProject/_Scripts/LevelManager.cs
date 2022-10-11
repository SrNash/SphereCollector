using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("PlayerController")]
    [SerializeField]
    PlayerController player;
    [Header("Control de Nivel")]
    [SerializeField]
    int coinsLevel;
    [SerializeField]
    float timer;
    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI timerText;
    [Header("VictoryCanvas")]
    [SerializeField]
    Canvas victoryCanvas;
    [SerializeField]
    TextMeshProUGUI pointsText;
    [SerializeField]
    TextMeshProUGUI timerVictoryText;
    [Header("Audio")]
    [SerializeField]
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
        
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        TimerCount();
    }

    private void TimerCount()
    {
        //Comenzamos a contar el tiempo
        //timer+= Time.deltaTime;

        //Dividiremos dicho tiempo en minutos y segundos
        int seconds = (int)(timer % 60);    //Realizamos una operación para comprobar si hemos llegado a contar 60 segundos
        int minutes = (int)(timer / 60) % 60;   //Realizamos una operación para conocer la cantidad de minutos que llebamos

        if (timer <= 9f)
        {
            timerText.text = "0" + minutes.ToString() + ":0" + seconds.ToString();
        }else if (timer >= 10f && timer <= 59.9f)
        {
            timerText.text = "0" + minutes.ToString() + ":" + seconds.ToString();
        }/*else if (timer >= 60f)
        {
            
        }*/

        if (player.coinsAmount != coinsLevel)
        {
            //Comenzamos a contar el tiempo
            timer+= Time.deltaTime;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (player.coinsAmount == coinsLevel)
        {
            pointsText.text = player.coinsAmount.ToString();
            timerVictoryText.text = timerText.text;
            victoryCanvas.gameObject.SetActive(true); 
            
            audioSource.mute = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Debug.Log("Recolectaste todas las monedas");
        }
    }

    public void ClickQuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
    public void ClickReStart()
    {
        SceneManager.LoadScene("Level_0");
        //SceneManager.LoadScene(0);
        Debug.Log("Reseteando el Nivel");
    }
}
