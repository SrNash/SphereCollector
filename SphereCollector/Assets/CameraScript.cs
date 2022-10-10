using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    SoundManager soundManager;
    private void Awake() {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.SelectSound(0,.2f);
    }
}
