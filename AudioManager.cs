/*
     DESCRIPTION :

     Sert à Référencer et à contrôler toutes les pistes audios qui interviennent dans les différentes scènes

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;


public class AudioManager : MonoBehaviour {

    //liste des différents clips audio
    public AudioClip[] clips;

    //booléen pour savoir si un audio est déjà en train d'être joué ou non
    [HideInInspector]
    public bool isFinishedPlaying = true;
    [HideInInspector]
    public GameEventsManager gameEventsManager;

    //NPCs
    public GameObject nPC01;
    public GameObject nPC02;
    public GameObject nPC03;
    public GameObject nPC04;
    public GameObject player;
    AudioSource nPC01AudioSource;
    AudioSource nPC02AudioSource;
    AudioSource nPC03AudioSource;
    AudioSource nPC04AudioSource;
    AudioSource playerAudioSource;


    //booléens pour savoir si l'audio a déjà été joué une fois et pour ne plus le rejouer
    
    bool[] hasBeenPlayedTuto = new bool[11];
     
    bool[] hasBeenPlayed = new bool[18];
    

    void Start()
    {
        GameManager.Instance.GameEventsManagerP = gameObject;
        
        //sert à s'inscrire sur la liste des Event subscribed scripts 
        GameManager.Instance.SubscribeScriptToGameEventUpdates(this);

        gameEventsManager = FindObjectOfType<GameEventsManager>();

        nPC01AudioSource = nPC01.GetComponentInChildren<AudioSource>();
        nPC02AudioSource = nPC02.GetComponent<AudioSource>();
        nPC03AudioSource = nPC03.GetComponentInChildren<AudioSource>();
        nPC04AudioSource = nPC04.GetComponentInChildren<AudioSource>();
        playerAudioSource = player.GetComponentInChildren<AudioSource>();
        
        for (int i = 0; i < 18; i++)
        {
            hasBeenPlayed[i] = false;
        }
        for (int i = 0; i < 11; i++)
        {
            hasBeenPlayedTuto[i] = false;
        }

    }
    
    private void OnDestroy()
    {
        //sert à se désinscrire sur la liste des Event subscribed scripts 
        GameManager.Instance.DeSubscribeScriptToGameEventUpdates(this);
    }

    void GameEventUpdated()
    {
       // Debug.Log("                                     Notre Méthode GameEventUpdated() de l'AudioManager est appelée                                      ");


        if (GameManager.Instance.gameEventID == 1 && hasBeenPlayedTuto[0] == false)
        {
            Debug.Log(" Debut Game Event 01 ");
            hasBeenPlayedTuto[0] = true;
            isFinishedPlaying = false;            
            
            StartCoroutine(AttendreAudioTerminé1(clips[0].length));
        }
        
        else
        {
           // Debug.Log("<b><color=#8A29B3>                                               Rien ne se passe                                        </color></b>");
        }


    }
    
    


    private void Update()
    {
        if (GameManager.Instance.gameEventID == 2 && isFinishedPlaying == true && hasBeenPlayedTuto[1] == false)
        {            
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[1].length,1)); 
        }
        if (GameManager.Instance.gameEventID == 3 && isFinishedPlaying == true && hasBeenPlayedTuto[2] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[2].length, 2));            
        }
        if (GameManager.Instance.gameEventID == 4 && isFinishedPlaying == true && hasBeenPlayedTuto[3] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[3].length, 3));
           
        }
        if (GameManager.Instance.gameEventID == 5 && isFinishedPlaying == true && hasBeenPlayedTuto[4] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[4].length, 4));
        }
        if (GameManager.Instance.gameEventID == 6 && isFinishedPlaying == true && hasBeenPlayedTuto[5] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[5].length, 5));            
        }
        if (GameManager.Instance.gameEventID == 7 && isFinishedPlaying == true && hasBeenPlayedTuto[6] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[6].length, 6));
        }
        if (GameManager.Instance.gameEventID == 8 && isFinishedPlaying == true && hasBeenPlayedTuto[7] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[7].length, 7));
        }
        if (GameManager.Instance.gameEventID == 9 && isFinishedPlaying == true && hasBeenPlayedTuto[8] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[8].length, 8));            
        }
        if (GameManager.Instance.gameEventID == 10 && isFinishedPlaying == true && hasBeenPlayedTuto[9] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[25].length, 25));
        }
        if (GameManager.Instance.gameEventID == 11 && isFinishedPlaying == true && hasBeenPlayedTuto[10] == false)
        {
            isFinishedPlaying = false;
            StartCoroutine(AttendreAudioTerminé2(clips[26].length, 26));            
        }

        if (GameManager.Instance.gameEventID == 102 && isFinishedPlaying == true )
        {
            Debug.Log("dans GEID 102 AudioManager");
            if (hasBeenPlayed[0] == false)
            {
                Debug.Log("D0101 doit être joué");
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(1.0f, nPC01AudioSource,9,0));
               
            }
            else if (hasBeenPlayed[0] == true && hasBeenPlayed[1] == false && isFinishedPlaying == true)
            {
                Debug.Log("D0102 doit être joué");
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.5f, playerAudioSource, 10,1));
               
            }
            else if (hasBeenPlayed[1] == true && hasBeenPlayed[2] == false && isFinishedPlaying == true)
            {
                Debug.Log("D0103 doit être joué");
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.5f, nPC01AudioSource, 11,2));
                playerAudioSource.enabled = false;
            }
            
        }
        if (GameManager.Instance.gameEventID == 103 && isFinishedPlaying == true)
        {
            Debug.Log("dans GEID 103 AudioManager");
            if (hasBeenPlayed[3] == false)
            {
                Debug.Log("D0201 doit être joué");
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.8f, nPC03AudioSource, 12, 3));
                hasBeenPlayed[3] = true;
            }
            else if (hasBeenPlayed[3] == true && hasBeenPlayed[4] == false && isFinishedPlaying == true)
            {
                Debug.Log("D0202 doit être joué");
                isFinishedPlaying = false;
                playerAudioSource.enabled = true;
                StartCoroutine(NPCDialogue00(0.5f, playerAudioSource, 13,4));
                hasBeenPlayed[4] = true;
            }
            else if (hasBeenPlayed[4] == true && hasBeenPlayed[5] == false && isFinishedPlaying == true)
            {
                Debug.Log("D0203 doit être joué");
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.5f, nPC03AudioSource, 14, 5));
                hasBeenPlayed[5] = true;
            }
            else if (hasBeenPlayed[5] == true && hasBeenPlayed[6] == false && isFinishedPlaying == true)
            {
                Debug.Log("D0204 doit être joué");
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.5f, playerAudioSource, 15, 6));
                hasBeenPlayed[6] = true;
            }
            else if (hasBeenPlayed[6] == true && hasBeenPlayed[7] == false && isFinishedPlaying == true)
            {
                Debug.Log("D0205 doit être joué");
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.5f, nPC03AudioSource, 16, 7));
                playerAudioSource.enabled = false;
                hasBeenPlayed[7] = true;
            }
        }
        if (GameManager.Instance.gameEventID == 103 && isFinishedPlaying == true && gameEventsManager.audioManagerPeutLancerAudio == true)
        {
            Debug.Log("dans GEID 103 AudioManager deuxieme partie");
            if (hasBeenPlayed[8] == false)
            {
                isFinishedPlaying = false;
                playerAudioSource.enabled = true;
                StartCoroutine(NPCDialogue00(1.0f, nPC02AudioSource, 17, 8));
                hasBeenPlayed[8] = true;
            }
            else if (hasBeenPlayed[9] == true && hasBeenPlayed[8] == false && isFinishedPlaying == true)
            {
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.5f, playerAudioSource, 18, 9));
                playerAudioSource.enabled = false;
                hasBeenPlayed[9] = true;
            }
            
        }
        if (GameManager.Instance.gameEventID == 104 && isFinishedPlaying == true)
        {
            Debug.Log("dans GEID 104 AudioManager");
            if (hasBeenPlayed[10] == false)
            {
                Debug.Log("D0401 doit être joué");
                playerAudioSource.enabled = true;
                isFinishedPlaying = false;
                StartCoroutine(NPCDialogue00(0.8f, playerAudioSource, 19, 10));                
                hasBeenPlayed[10] = true;
            }
            if (hasBeenPlayed[10] == true)
            {
                playerAudioSource.enabled = false;
            }
        }
            if (GameManager.Instance.gameEventID == 107 && isFinishedPlaying == true)
        {
            Debug.Log("dans GEID 107 AudioManager");
            if (gameEventsManager.choixPatientAAider02 == true)
            {
                if (hasBeenPlayed[11] == false)
                {
                    Debug.Log("D0501 doit être joué");
                    playerAudioSource.enabled = true;
                    isFinishedPlaying = false;
                    StartCoroutine(NPCDialogue00(0.8f, playerAudioSource, 20, 11));
                    hasBeenPlayed[11] = true;
                }
                else if (hasBeenPlayed[11] == true && hasBeenPlayed[12] == false && isFinishedPlaying == true)
                {
                    Debug.Log("D0502 doit être joué");
                    isFinishedPlaying = false;
                    StartCoroutine(NPCDialogue00(0.5f, nPC04AudioSource, 21, 12));
                    hasBeenPlayed[12] = true;
                }
                else if (hasBeenPlayed[12] == true && hasBeenPlayed[13] == false && isFinishedPlaying == true)
                {
                    Debug.Log("D0503 doit être joué");
                    isFinishedPlaying = false;
                    StartCoroutine(NPCDialogue00(0.5f, playerAudioSource, 22, 13));
                    hasBeenPlayed[13] = true;
                }
                else if (hasBeenPlayed[13] == true && hasBeenPlayed[14] == false && isFinishedPlaying == true)
                {
                    Debug.Log("D0504 doit être joué");
                    isFinishedPlaying = false;
                    StartCoroutine(NPCDialogue00(0.5f, nPC04AudioSource, 23, 14));
                    playerAudioSource.enabled = false;
                    hasBeenPlayed[14] = true;
                }
            }
            else if (gameEventsManager.choixPatientAAider01 == true)
            {
                if (hasBeenPlayed[15] == false)
                {
                    Debug.Log("D1501 doit être joué");
                    playerAudioSource.enabled = true;
                    isFinishedPlaying = false;
                    StartCoroutine(NPCDialogue00(0.8f, playerAudioSource,22 , 15));
                    hasBeenPlayed[15] = true;
                }
                else if (hasBeenPlayed[15] == true && hasBeenPlayed[16] == false && isFinishedPlaying == true)
                {
                    Debug.Log("D1502 doit être joué");
                    isFinishedPlaying = false;
                    StartCoroutine(NPCDialogue00(0.5f, nPC04AudioSource, 23, 16));
                    hasBeenPlayed[16] = true;
                }
                else if (hasBeenPlayed[16] == true && hasBeenPlayed[17] == false && isFinishedPlaying == true)
                {
                    Debug.Log("D1503 doit être joué");
                    isFinishedPlaying = false;
                    StartCoroutine(NPCDialogue00(0.5f, playerAudioSource, 24, 17));
                    playerAudioSource.enabled = false;
                    hasBeenPlayed[17] = true;
                }
                
            }

        }

    }
    IEnumerator AttendreAudioTerminé1(float temps)
    {

        yield return new WaitForSeconds(3.0f);

        playerAudioSource.clip = clips[0];
        playerAudioSource.Play();
        yield return new WaitForSeconds(temps + 1.5f);
        isFinishedPlaying = true;
        Debug.Log("                                                 Game Event 01 terminé                                       ");
        GameManager.Instance.PlayerPassedEvent();

    }                                           // pour scene du tutoriel, le GE est terminé a la fin de l'audio
    IEnumerator AttendreAudioTerminé2(float temps, int clipID)
    {
        //AudioSource.PlayClipAtPoint(clips[clipID], transform.position);
        playerAudioSource.clip = clips[clipID];
        playerAudioSource.Play();
        yield return new WaitForSeconds(temps + 1.5f);
        hasBeenPlayedTuto[clipID] = true;
        isFinishedPlaying = true;        
    }                               // pour tuto, lance pour donner chaque instruction
    IEnumerator NPCDialogue(AudioSource audioSource, float temps, int clipID)
    {

        audioSource.clip = clips[clipID];
        audioSource.Play();
        yield return new WaitForSeconds(temps);
        isFinishedPlaying = true;

    }
    IEnumerator NPCDialogue00(float attente, AudioSource audioSource, int clipID, int j)
    {
        yield return new WaitForSeconds(attente);
        StartCoroutine(NPCDialogue(audioSource, clips[clipID].length, clipID));
        hasBeenPlayed[j] = true;
    }                   
    
}
