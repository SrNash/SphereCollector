using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    private void Awake() {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.SelectSound(1,.25f);
    }
}
