/*

     DESCRIPTION :

     Sert à centraliser l'information du GameEventID et à la renvoyer à tous les scripts inscrits 

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {
    [Header("temps total")]
    public float totalTime;
    public List<float> timeBetweenEvents;

    //pointeur vers le script GameEventsManager
    public GameObject GameEventsManagerP;
    //Liste des scripts attachés au gameObject GameManager
    public List<MonoBehaviour> eventSubscribedScripts = new List<MonoBehaviour>();

    public AudioSource audioSource;
    public AudioClip[] clips;

    //Pour identifier les évènements
    public int gameEventID = 0;

    public GameObject fadeInOutCanvas;
    public CSVReadWrite cSVReadWrite;

    //int score;
    //int lifePoints;

   //pour check qu'il n'y ait qu'un seul GameManager dans le jeu et qu'il ne soit accessible qu'en ReadOnly
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
#if UNITY_EDITOR
              if(FindObjectsOfType<GameManager>().Length > 1)
                {
                    Debug.LogError("<b>Il y a plus d'un GameManager dans la scene</b");
                }
#endif
            }
            return instance;
        }
    }

    private void Awake()
    {
        //pour conserver le GameManager entre les différentes scenes
        DontDestroyOnLoad(gameObject);
    }
    void Start ()
    {
        //le nombre (et du coup l'id) des évènements qui vont être appelés au démarrage du jeu
        Invoke("PlayerPassedEvent", 0f);

        audioSource.clip = clips[0];
        cSVReadWrite = GameObject.Find("DataStorage").GetComponent<CSVReadWrite>();
               
    }
    private void Update()
    {
        totalTime += Time.deltaTime;
    }

    //pour ajouter et retirer des éléments à la liste des scripts attachés au gameObject
    public void SubscribeScriptToGameEventUpdates(MonoBehaviour pScript)
    {
        eventSubscribedScripts.Add(pScript);
    }
    public void DeSubscribeScriptToGameEventUpdates(MonoBehaviour pScript)
    {
        while (eventSubscribedScripts.Contains(pScript))
        {
            eventSubscribedScripts.Remove(pScript);
        }
    }

    //quand le joueur a fini un evenement important
    public void PlayerPassedEvent()
    {
        gameEventID++;
        timeBetweenEvents.Add(Time.realtimeSinceStartup);
        

        foreach(MonoBehaviour _script in eventSubscribedScripts)
        {
            _script.Invoke("GameEventUpdated", 0f);
        }
        cSVReadWrite.Save();
    }

    public void PlayerPassedEventWithSound()
    {
        gameEventID++;
        timeBetweenEvents.Add(Time.realtimeSinceStartup);
        
        audioSource.Play();

        foreach (MonoBehaviour _script in eventSubscribedScripts)
        {
            _script.Invoke("GameEventUpdated", 0f);
        }
        cSVReadWrite.Save();
    }

    public void Sound()
    {
        audioSource.Play();
               
    }
    public void Sound1()
    {
        audioSource.clip = clips[1];
        audioSource.Play();

    }
    public void Sound2()
    {
        audioSource.clip = clips[2];
        audioSource.Play();

    }


    public void SkipToNextLevel(int caseSwitch)
    {
        switch (caseSwitch)
        {
            case 1:
                gameEventID = 101;
                fadeInOutCanvas.GetComponent<Animator>().Play("fadeInOut");
                foreach (MonoBehaviour _script in eventSubscribedScripts)
                {
                    _script.Invoke("GameEventUpdated", 1.5f);
                }
                break;

            case 2:
                gameEventID = 201;
                fadeInOutCanvas.GetComponent<Animator>().Play("fadeInOut");
                foreach (MonoBehaviour _script in eventSubscribedScripts)
                {
                    _script.Invoke("GameEventUpdated", 1.5f);
                }
                break;

            case 3:
                gameEventID = 301;
                fadeInOutCanvas.GetComponent<Animator>().Play("fadeInOut");
                foreach (MonoBehaviour _script in eventSubscribedScripts)
                {
                    _script.Invoke("GameEventUpdated", 1.5f);
                }
                break;
            case 4:
                gameEventID = 101;
                fadeInOutCanvas.GetComponent<Animator>().Play("fadeInOut");
                foreach (MonoBehaviour _script in eventSubscribedScripts)
                {
                    _script.Invoke("GameEventUpdated", 1.5f);
                }
                break;
            case 5:
                gameEventID = 1;                                           //On Commence le Tuto
                fadeInOutCanvas.GetComponent<Animator>().Play("fadeInOut");
                foreach (MonoBehaviour _script in eventSubscribedScripts)
                {
                    _script.Invoke("GameEventUpdated", 1.5f);
                }
                break;
            case 6:
                gameEventID = 201;                                         //On commence la Scene 01
                fadeInOutCanvas.GetComponent<Animator>().Play("fadeInOut");
                foreach (MonoBehaviour _script in eventSubscribedScripts)
                {
                    _script.Invoke("GameEventUpdated", 1.5f);
                }
                break;
            case 7:
                gameEventID = 1000;
                fadeInOutCanvas.GetComponent<Animator>().Play("fadeInOut");
                foreach (MonoBehaviour _script in eventSubscribedScripts)
                {
                    _script.Invoke("GameEventUpdated", 1.5f);
                }
                break;

        }
        
    }

    public void OnApplicationQuit()
    {
        cSVReadWrite.Save();
    }
}
