using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    int coinsLevel;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    float timer;

    [SerializeField]
    PlayerController player;
    [SerializeField]
    AudioSource music;
    [SerializeField]
    AudioSource ambientMusic;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void Timer()
    {
        //timer -= Time.deltaTime;
        timer += Time.deltaTime;

        if (timer < 10f)
        {
            timerText.text = "0" +  timer.ToString("F0");
        }else if (timer >= 10f)
        {
            timerText.text = timer.ToString("F0");
        }

        if (timer <= 0)
        {
            Debug.Log("Se termino el tiempo");
        }

        if (player.coinsAmount == coinsLevel)
        {
            Debug.Log("Recolectaste todas las monedas");
        }
    }
}
