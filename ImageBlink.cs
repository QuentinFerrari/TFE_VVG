/*
 
 DESCTIPTION :

 Sert à faire clignoter à une fréquence le nombre d'enfants de l'objet auquel ce script est attaché ( /!\  l'ID commence à 0)
                                  |                     |
                                  |                     |
                                  V                     V
                                Voulue                Voulus
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ImageBlink : MonoBehaviour {

    public bool state;
    [Range(0,2)]
    public float onDuration = 1;
    [Range(0, 2)]
    public float offDuration = 1;

    private float nextSwitch;
    public int[] nombreEnfantsClignottent;
    void Update()
    {
        if (Time.time > nextSwitch)
        {
            state = !state;
            nextSwitch += (state ? onDuration : offDuration);
            for (int i = 0; i < nombreEnfantsClignottent.Length; i++)
            {
                this.transform.GetChild(nombreEnfantsClignottent[i]).gameObject.SetActive(state);
            }
                
        }
    }

}
