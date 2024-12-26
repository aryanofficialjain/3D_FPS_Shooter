using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRefrence : MonoBehaviour
{
    public static GlobalRefrence Instance {get; set;}

    public GameObject bulletImpactEffectPrefab;

    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else {
            Instance = this;
        }
    }
}
