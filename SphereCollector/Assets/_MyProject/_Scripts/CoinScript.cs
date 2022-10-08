using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    [SerializeField]
    float rotSpeed;
    [SerializeField]
    Transform coinGO;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();  //Buscaremos el script PlayerController en la escena

        if(player == null)  //Comprobamos si este componente está asignado o no
        {
            player = FindObjectOfType<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 turnSpeed = new Vector3 (0f, 0f, rotSpeed * Time.deltaTime);
        coinGO.Rotate(turnSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player.coinsAmount += 1;
            Destroy(this.gameObject);
        }
    }
}
