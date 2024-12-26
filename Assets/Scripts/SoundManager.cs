using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; set;}

    public AudioSource AKM;

    public AudioSource emptyMagazine;

    public AudioSource ReloadingSound;

    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }
}
