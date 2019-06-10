/*
     DESCRIPTION :

     Dans ce script On retrouve toutes les étapes des différents scénarios
     Ce script est abonné au GameManager et reçoit le GameEventID

     La fonction GameEventUpdated sert au setup du gameEvent
     La fonction Update sert à valider des check pour pouvoir passer au gameEventID suivant

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
using VRTK.Controllables.PhysicsBased;
using UnityEngine.AI;


public class GameEventsManager : MonoBehaviour
{
    //Pour toutes les scenes
    [Header("-------------------------Toutes les scènes----------------------------- ")]
    public GameObject environnementTutoriel; //les gameObjects propres à la scene du tuto et qui sont désactivés dans les autres scènes
    public GameObject environnementScene01;
    public GameObject environnementScene02;
    public GameObject controllerDroit;
    public GameObject controllerGauche;
    public GameObject CameraRig;
    public string texteADeployer;
    public Vector3 positionLobby;
    [Range(0, 100)]
    public int lifePoints = 100;
    public int score = 0;
    AudioManager audioManager;
    NPCsBehaviourManager npcsBehaviourManager;
    bool isFinishedPlaying = true;
    public int nombreObjetsRangés;
    public int nombreChambresVerifiées;

    //POUR LA SCENE DU TUTORIEL
    [Header("-------------------------Scène du tutoriel----------------------------- ")]

    public GameObject tutorielCanvas01;
    public GameObject tutorielCanvas11;
    public GameObject tutorielCanvas21;
    public GameObject tutorielCanvas31;
    public GameObject tutorielCanvas41;
    public GameObject tutorielCanvas51;
    public GameObject tutorielCanvas02;

    public GameObject chaiseRoulanteASnapDrop;
    public GameObject smartphone;
    public GameObject porteAOuvrir;
    public GameObject chaiseRoulanteADeplacer;
    public GameObject litADeplacer;
    
    public GameObject murInvisible;
    public GameObject menu;
    public GameObject extincteur;
    //public GameObject collisionBox;
   
    public GameObject zoneChaiseRoulante;
    public GameObject zoneLitHopital;
    
    

    //Bool pour tester les boutons de la manette
    public  bool isButton01PressedD = false;
    public bool isButton01PressedG = false;
    public bool isButton02PressedD = false;
    public bool isButton02PressedG = false;
    public bool isButton03PressedD = false;
    public bool isButton03PressedG = false;
    public bool isButton04PressedD = false;
    public bool isButton04PressedG = false;


    //les vecteurs position et  rotation pour téléporter le joueur au début d'une nouvelle scene
    private Quaternion rotationZero; //ne fonctionne pas pour tourner le cameraRig, mais nécessaire à l'appel de la fonction SetPositionAndRotation

    public Vector3 positionSceneTutoriel;
    public Vector3 rotationSceneTutoriel;

    

    [Header("-------------------------Scène 01----------------------------- ")]         //POUR LA SCENE 01 ---- commence à GameEventID = 100 

    public Vector3 positionScene01; //les vecteurs position et  rotation pour téléporter le joueur au début d'une nouvelle scene
    public Vector3 rotationScene01;

    [Header("       ·triggers")]
    public GameObject collisionObject101;// boite collision chambre X1
    public GameObject collisionObject102;// boite collision bureau
    public GameObject collisionObject103;// boite collision chambre X2
    public GameObject collisionObject104;// boite collision chambre X3 
    public GameObject safeZoneTrigger101; //safezonetrigger01
    public GameObject safeZoneTrigger102;
    public GameObject safeZoneTrigger103;
    public GameObject endSceneTrigger;

    [Header("       ·canvas")]
    public GameObject scene01Canvas01;
    public GameObject scene01Canvas02;
    public GameObject scene01Canvas03;
    public GameObject scene01Canvas04;
    public GameObject scene01Canvas05;
    public GameObject scene01Canvas06;

    [Header("       ·NPCs")]
    public GameObject NPC01;
    public GameObject NPC02;
    public GameObject NPC03;
    public GameObject NPC04;
    GameObject[] NNPCs;

    [Header("       ·Autres")]
    public GameObject chaiseRoulante;
    public GameObject litHopital;
    public GameObject alarmes;
    public GameObject feu;
    public GameObject boutonAAppuyer;
    public GameObject litHopitalAnim;
    public GameObject litHopitalInteractable;
    public GameObject gsm;
    public GameObject smoke;
    public bool aPrisLeGSM;
    public bool audioManagerPeutLancerAudio = false;
    public GameObject porteAVerifier;
    public bool porteAEteVerifiee;
    public bool porteAEteBloquee;
    public GameObject questionnaire;
    public QuestionnaireManager questionnaireManager;
    bool lancerLeQuestionnaireUneSeuleFois = false;


    public GameObject triggerObjetsEncombrants;
    [HideInInspector]
    public ObjetsEncombrants triggerLeave01;
    [HideInInspector]
    public ObjetsEncombrants triggerLeave02;
    [HideInInspector]
    public List<bool> decomptePoints01; //sert à ne compter les points qu'une seule fois
    [HideInInspector]
    public List<bool> decomptePoints02;
    public GameObject[] snapDropZones;

    [Header(" -------------------------Scène 02------------------------- ")]

    public Vector3 positionScene02;
    public Vector3 rotationScene02;

    [Header("       ·Canvas")]
    public GameObject scene02Canvas01;
    public GameObject scene02Canvas02;
    public GameObject scene02Canvas03;
    public GameObject scene02Canvas04;
    [Header("       ·NPCs")]
    public GameObject NINPC201;
    [Header("       ·Triggers")]
    public GameObject trigger201;
    public GameObject trigger202;
    [Header("       ·Autres")]
    public GameObject snapDropZones2;
    public GameObject[] chaiseADeplacer;
    public GameObject waypoints;

    [HideInInspector]
    public bool choixPatientAAider01;
    [HideInInspector]
    public bool choixPatientAAider02;
    [HideInInspector]
    public int choixPatientAAider00 = 0;


    //fonctions

    void Start()
    {
        GameManager.Instance.GameEventsManagerP = gameObject;                                               //sert à s'inscrire sur la liste des Event subscribed scripts 

        controllerDroit = GameObject.FindGameObjectWithTag("RightController");
        controllerGauche = GameObject.FindGameObjectWithTag("LeftController");
               

        GameManager.Instance.SubscribeScriptToGameEventUpdates(this);
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        triggerLeave01 = triggerObjetsEncombrants.transform.GetChild(0).GetComponent<ObjetsEncombrants>();
        triggerLeave02 = triggerObjetsEncombrants.transform.GetChild(1).GetComponent<ObjetsEncombrants>();
        GameObject snapDropZoneTuto = chaiseRoulanteASnapDrop.transform.parent.GetChild(1).gameObject; ;
        for (int i = 0; i < triggerLeave01.objetsADeplacer.Length; i++)
        {
            decomptePoints01.Add(false);
        }                                   //instantie le liste des bool qui vérifient qu'on ne compte les points qu'une fois
        for (int i = 0; i < triggerLeave02.objetsADeplacer.Length; i++)
        {
            decomptePoints02.Add(false);
        }
        npcsBehaviourManager = GameObject.Find("NNpcsBehaviourManager").GetComponent<NPCsBehaviourManager>();
        NNPCs = GameObject.FindGameObjectsWithTag("NINPC");
        for (int i = 0; i < snapDropZones.Length; i++)
        {
            snapDropZones[i].GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnappedToDropZone);
        }
        for (int i = 0; i < snapDropZones2.transform.childCount; i++)
        {
            snapDropZones2.transform.GetChild(i).GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnappedToDropZone);
        }

        snapDropZoneTuto.GetComponent<VRTK_SnapDropZone>().ObjectSnappedToDropZone += new SnapDropZoneEventHandler(ObjectSnappedToDropZone);




    }    
    private void OnDestroy()
    {
        //sert à se désinscrire sur la liste des Event subscribed scripts 
        GameManager.Instance.DeSubscribeScriptToGameEventUpdates(this);
    }
    
    void GameEventUpdated()
    {


        //-------------------------------------------------------------------------------------------------------------------------------
        //                                                                                              ID Events tutoriel commencent à 1    
        //                                                                                              ---------------------------------

        if (GameManager.Instance.gameEventID == 1)
        {

            //on positionne le joueur au bon endroit
            CameraRig.transform.SetPositionAndRotation(positionSceneTutoriel, rotationZero);
            CameraRig.transform.Rotate(rotationScene01);

            //on active l'evenementTutoriel
            environnementTutoriel.SetActive(true);

            //on cache les toggle des 2 manettes
            controllerDroit.transform.GetChild(1).gameObject.SetActive(false);
            controllerGauche.transform.GetChild(1).gameObject.SetActive(false);          

            //on active le mur invisible
            murInvisible.SetActive(true);

            //on désactive la touche menu
            menu.SetActive(false);
            controllerDroit.GetComponent<MenuToggle>().enabled = false;
            controllerGauche.GetComponent<MenuToggle>().enabled = false;


            tutorielCanvas01.SetActive(true);
            tutorielCanvas01.transform.GetChild(0).gameObject.SetActive(true);
            tutorielCanvas01.transform.GetChild(1).gameObject.SetActive(true);


        }
        else if (GameManager.Instance.gameEventID == 2)
        {
            Debug.Log("Debut de Game Event ID 02");
            texteADeployer = "Appuyez sur les boutons Gachette";
            tutorielCanvas01.transform.GetChild(1).gameObject.SetActive(false);
            tutorielCanvas01.transform.GetChild(2).gameObject.SetActive(true);
                        

            controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                   //On allume controller tooltip Droit
            controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                  //On allume controller tooltip Gauche
            
            controllerDroit.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);       
            controllerGauche.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);      //on montre le bouton Gâchette à Gauche        
        }
        else if (GameManager.Instance.gameEventID == 3)
        {
            Debug.Log("Debut de Game Event ID 03");
            texteADeployer = "Appuyez sur les boutons Saisie";
            tutorielCanvas01.transform.GetChild(2).gameObject.SetActive(false);
            tutorielCanvas01.transform.GetChild(3).gameObject.SetActive(true);


            
            //----------------------------------------------------------------------------------------------------------------------
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.black;              //UPDATE COULEUR
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.white;                   //UPDATE COULEUR
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.white;                   //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.black;             //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.white;                  //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.white;                  //UPDATE COULEUR
            controllerDroit.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
            controllerGauche.transform.GetChild(1).gameObject.SetActive(false);                                     //UPDATE COULEUR
            controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR
            controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                                      //UPDATE COULEUR
            //----------------------------------------------------------------------------------------------------------------------


            controllerDroit.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);                  //on éteint le bouton Gâchette à Droite   
            controllerDroit.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);                   //on montre le bouton Grip à Droite   
            controllerGauche.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);                 //on éteint le bouton Gâchette à Droite   
            controllerGauche.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);                  //on montre le bouton Grip à Droite   


        }
        else if (GameManager.Instance.gameEventID == 4)
        {
            Debug.Log("Debut de Game Event ID 04");
            texteADeployer = "Appuyez sur les paves tactiles";
            tutorielCanvas01.transform.GetChild(3).gameObject.SetActive(false);
            tutorielCanvas01.transform.GetChild(4).gameObject.SetActive(true);

            //----------------------------------------------------------------------------------------------------------------------
            
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.black;              //UPDATE COULEUR
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.white;                   //UPDATE COULEUR
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.white;                   //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.black;             //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.white;                  //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.white;                  //UPDATE COULEUR
            controllerDroit.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
            controllerGauche.transform.GetChild(1).gameObject.SetActive(false);                                     //UPDATE COULEUR
            controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR
            controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                                      //UPDATE COULEUR
            //----------------------------------------------------------------------------------------------------------------------

            controllerDroit.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);                  //on éteint le bouton Grip à Droite   
            controllerDroit.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);                   //on montre le bouton touchpad à Droite   
            controllerGauche.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);                 //on éteint le bouton Gâchette à Droite   
            controllerGauche.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);                  //on montre le bouton touchpad à Droite   


        }
        else if (GameManager.Instance.gameEventID == 5)
        {
            Debug.Log("Debut de Game Event ID 05");
            texteADeployer = "Appuyez sur les boutons menus";
            tutorielCanvas01.transform.GetChild(4).gameObject.SetActive(false);
            tutorielCanvas01.transform.GetChild(5).gameObject.SetActive(true);



            //----------------------------------------------------------------------------------------------------------------------
            
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.black;              //UPDATE COULEUR
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.white;                   //UPDATE COULEUR
            controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.white;                   //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.black;             //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.white;                  //UPDATE COULEUR
            controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.white;                  //UPDATE COULEUR
            controllerDroit.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
            controllerGauche.transform.GetChild(1).gameObject.SetActive(false);                                     //UPDATE COULEUR
            controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR
            controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                                      //UPDATE COULEUR
            //----------------------------------------------------------------------------------------------------------------------

            controllerDroit.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);                  //on éteint le bouton touchpad à Droite   
            controllerDroit.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);                   //on montre le bouton menu à Droite   
            controllerGauche.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);                 //on éteint le bouton touchpad à Droite   
            controllerGauche.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);                  //on montre le bouton menu à Droite   

        }
        else if (GameManager.Instance.gameEventID == 6)
        {
            texteADeployer = "Deposez la chaise roulante dans la zone prevue";

            //on eteint le canvas01 du tuto
            tutorielCanvas01.SetActive(false);
            
            //on désactive le mur invisible
            murInvisible.SetActive(false);

            //on réactive le bouton menu
            menu.SetActive(true);
            controllerDroit.GetComponent<MenuToggle>().enabled = true;
            controllerGauche.GetComponent<MenuToggle>().enabled = true;            

            //on ne montre  plus les tooltips
            
            controllerDroit.transform.GetChild(1).gameObject.SetActive(false); 
            controllerGauche.transform.GetChild(1).gameObject.SetActive(false);

            //on allume les canvas 02.0 et 11
            tutorielCanvas02.SetActive(true);
            tutorielCanvas02.transform.GetChild(0).gameObject.SetActive(true);
            tutorielCanvas11.SetActive(true);


        }                   // on va pres de la chaise roulante et SnapDropZone
        else if (GameManager.Instance.gameEventID == 7)
        {
            texteADeployer = "Attrapez et rangez le gsm";
            // on désactive le canvas 02.0
            tutorielCanvas02.transform.GetChild(0).gameObject.SetActive(false);
            tutorielCanvas11.SetActive(false);


            // on active le canvas 02.1 et 21
            tutorielCanvas02.transform.GetChild(1).gameObject.SetActive(true);
            tutorielCanvas21.SetActive(true);

        }                   // on va pres du smartphone et on l'attrape
        else if (GameManager.Instance.gameEventID == 8)
        {
            texteADeployer = "Ouvrez et passez la porte";
            tutorielCanvas02.transform.GetChild(2).gameObject.SetActive(true);      //dactive canvas
            tutorielCanvas02.transform.GetChild(1).gameObject.SetActive(false);     //desactive canvas
            tutorielCanvas31.SetActive(true);
            tutorielCanvas21.SetActive(false);


        }                   // on va pres de la porte et on l'ouvre
        else if (GameManager.Instance.gameEventID == 9)
        {
            texteADeployer = "Deposez la chaise roulante dans la zone";
            tutorielCanvas02.transform.GetChild(3).gameObject.SetActive(true);      //dactive canvas
            tutorielCanvas02.transform.GetChild(5).gameObject.SetActive(true);      //dactive canvas
            tutorielCanvas02.transform.GetChild(2).gameObject.SetActive(false);     //desactive canvas
            tutorielCanvas41.SetActive(true);
            tutorielCanvas31.SetActive(false);

        }                   //on va  pres de la CR et on l'amene au bon endroit
        else if (GameManager.Instance.gameEventID == 10)
        {
            texteADeployer = "Deposez le lit dans la zone";
            tutorielCanvas02.transform.GetChild(4).gameObject.SetActive(true);      //dactive canvas
            tutorielCanvas02.transform.GetChild(6).gameObject.SetActive(true);      //dactive canvas
            tutorielCanvas02.transform.GetChild(3).gameObject.SetActive(false);     //desactive canvas
            tutorielCanvas02.transform.GetChild(5).gameObject.SetActive(true);      //dactive canvas
            tutorielCanvas51.SetActive(true);
            tutorielCanvas41.SetActive(false);
        }                  //on va pres du lit d'hopital et on l'amene au bon endroit
        else if (GameManager.Instance.gameEventID == 11)
        {
            texteADeployer = "Bravo ! Essayez le Scenario 1 maintenant";
            tutorielCanvas02.transform.GetChild(5).gameObject.SetActive(false);     //desactive canvas            
            tutorielCanvas51.SetActive(false);
            GameManager.Instance.SkipToNextLevel(7);                                //on est teleporte au Lobby
        }                  
        //-------------------------------------------------------------------------------------------------------------------------------




        //-------------------------------------------------------------------------------------------------------------------------------
        //                                                                                            ID Events scene 01 commencent à 100
        //                                                                                            -----------------------------------

        else if (GameManager.Instance.gameEventID == 101)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 101                                        </color></b>");
            texteADeployer = "Discutez avec votre collegue";            
            new WaitForSeconds(2);
            NINPC201.GetComponent<AudioSource>().enabled = false; //son ambiance cafetaria
            scene02Canvas04.transform.gameObject.SetActive(false);
            GameManager.Instance.PlayerPassedEvent();

        }
        else if (GameManager.Instance.gameEventID == 102)
        {
            Debug.Log("<b><color=#8A29B3>Début évent 102</color></b>");
            texteADeployer = "Allez voir votre patient <color=green>chambre 552</color>";

            scene01Canvas01.SetActive(true);
            collisionObject101.transform.GetChild(0).gameObject.SetActive(true);

        }
        else if (GameManager.Instance.gameEventID == 103)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 103                                         </color></b>");
            texteADeployer = "Allez chercher les lunettes dans le bureau du personnel";

            scene01Canvas01.SetActive(false);
            scene01Canvas02.SetActive(true);
            collisionObject101.transform.GetChild(0).gameObject.SetActive(false);
            collisionObject102.transform.GetChild(0).gameObject.SetActive(true);
           
            //on lance la discussion avec la personne
            //on retourne dans le bureau

        }
        else if (GameManager.Instance.gameEventID == 104)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 104                                         </color></b>");
            texteADeployer = "Allez evacuer le patient <color=green>chambre 562</color>";

            //Modifier l'état du feu
            npcsBehaviourManager.fireState = NPCsBehaviourManager.FireState.fireIsOn;

            //alarmes.SetActive(true);
            //feu.gameObject.SetActive(true);
            scene01Canvas02.SetActive(false);
            scene01Canvas03.SetActive(true);

            collisionObject102.transform.GetChild(0).gameObject.SetActive(false);
            collisionObject103.transform.GetChild(0).gameObject.SetActive(true);
        }           //l'alarme se lance et on doit aller aider dans la chambre 02
        else if (GameManager.Instance.gameEventID == 105)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 105                                         </color></b>");

            scene01Canvas03.SetActive(false);
            safeZoneTrigger101.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            safeZoneTrigger102.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            safeZoneTrigger103.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }           //On doit deposer la chaise Roulante et on active les SafeZoneREnderer jaunes
        else if (GameManager.Instance.gameEventID == 106)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 106                                         </color></b>");

            scene01Canvas03.SetActive(false);
            scene01Canvas04.SetActive(true);
            collisionObject102.transform.GetChild(0).gameObject.SetActive(true);
            collisionObject103.transform.GetChild(0).gameObject.SetActive(false);
            safeZoneTrigger101.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            safeZoneTrigger102.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            safeZoneTrigger103.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            safeZoneTrigger101.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            safeZoneTrigger102.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            safeZoneTrigger103.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;


        }           //On change la couleur des SafeZones et on active les fleches vers la chambre
        else if (GameManager.Instance.gameEventID == 107)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 107                                         </color></b>");
            //proposer le choix entre prendre le lit ou aider la personne
            litHopital.transform.GetChild(1).gameObject.SetActive(true);
            NPC04.transform.GetChild(0).gameObject.SetActive(true);
            scene01Canvas04.SetActive(false);
            scene01Canvas05.SetActive(true);
            scene01Canvas06.SetActive(true);
            safeZoneTrigger101.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;//on éteint en vert
            safeZoneTrigger102.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            safeZoneTrigger103.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            safeZoneTrigger101.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;//on allume en jaune
            safeZoneTrigger102.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            safeZoneTrigger103.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            collisionObject102.transform.GetChild(0).gameObject.SetActive(false);
            collisionObject104.transform.GetChild(0).gameObject.SetActive(true);
        }           //L'autre infirmier nous suit et on doit aller dans la 3e chambre, les SafeZoneRenderer sontdésactivés pour ce GEID ci
        else if (GameManager.Instance.gameEventID == 108)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 108                                         </color></b>");

            if (choixPatientAAider00 == 1)
            {
                NPC01.GetComponent<BasicAI>().enabled = true;                             
                NPC01.GetComponent<BasicAI>().walkDistance = 2.0f;
                NPC01.GetComponent<BasicAI>().walkTarget = NPC04; 
            }
            else if (choixPatientAAider00 == 2)
            {
                NPC01.GetComponent<BasicAI>().enabled = true;
               
                NPC01.GetComponent<BasicAI>().walkDistance = 2.0f;
                NPC01.GetComponent<BasicAI>().walkTarget = waypoints.transform.GetChild(1).gameObject;
                NPC04.GetComponent<BasicAI>().stateAfter = BasicAI.StateAfter.FOLLOW;
                NPC04.GetComponent<BasicAI>().fireState = NPCsBehaviourManager.FireState.fireIsOn;
                NPC04.GetComponent<FollowerScript>().enabled = true;

            }

        }        
        else if (GameManager.Instance.gameEventID == 109)
        {
            Debug.Log("<b><color=#8A29B3>                                               Début évent 109                                         </color></b>");
            endSceneTrigger.SetActive(true);
            //on doit mettre le message d'aller chercher le gsm;
            if (choixPatientAAider00 == 1)
            {
                NPC04.GetComponentInChildren<FollowerScript>().enabled = false;
                NPC04.GetComponentInChildren<BasicAI>().stateAfter = BasicAI.StateAfter.IDLE;
                NPC01.GetComponent<BasicAI>().stateAfter = BasicAI.StateAfter.IDLE;
                Debug.Log("LE PATIENT EST SAIN ET SAUF");
                // faire dire à NPC01 - voila vous êtes sain et sauf monsieur -
            }
            
            if (choixPatientAAider00 == 2 && safeZoneTrigger102.GetComponent<CollisionBox>().hasEnteredNPC == true)
            {
                NPC04.GetComponentInChildren<FollowerScript>().enabled = false;
                NPC04.GetComponentInChildren<BasicAI>().stateAfter = BasicAI.StateAfter.IDLE;
                NPC01.GetComponent<BasicAI>().stateAfter = BasicAI.StateAfter.IDLE;
                Debug.Log("LE PATIENT EST SAIN ET SAUF");
            }
            
            scene01Canvas05.SetActive(false);
            collisionObject102.transform.GetChild(0).gameObject.SetActive(true);
            collisionObject104.transform.GetChild(0).gameObject.SetActive(false);

            safeZoneTrigger101.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;// on allume en vert
            safeZoneTrigger102.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            safeZoneTrigger103.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            safeZoneTrigger101.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;//on éteint en jaune
            safeZoneTrigger102.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            safeZoneTrigger103.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }           // on active les SafeZoneRenderer, il faut déplacer le patient dans une SafeZone
        //-------------------------------------------------------------------------------------------------------------------------------




        //-------------------------------------------------------------------------------------------------------------------------------
        //                              ID Events Scene 2 commencent à 200; même si c'est le debut de la scene 01 car ça a été fait après  
        //                              -------------------------------------------------------------------------------------------------

        else if (GameManager.Instance.gameEventID == 201)
        {
            Debug.Log("<b><color=#FF4500>                                               Début évent 201                                        </color></b>");
            texteADeployer = "Rendez-vous dans le bureau";
            CameraRig.transform.SetPositionAndRotation(positionScene02, rotationZero);
            CameraRig.transform.Rotate(rotationScene02);
            new WaitForSeconds(2);
            NINPC201.GetComponent<AudioSource>().enabled = true;
            GameManager.Instance.PlayerPassedEvent();

        }           //On téléporte le joueur dans le réfectoire et on attend 2 sec
        else if (GameManager.Instance.gameEventID == 202)
        {
            Debug.Log("<b><color=#FF4500>                                               Début évent 202                                        </color></b>");
            scene02Canvas01.transform.gameObject.SetActive(true);



        }           //On indique au joueur de se rendre dans l'autre bloc
        else if (GameManager.Instance.gameEventID == 203)
        {
            Debug.Log("<b><color=#FF4500>                                               Début évent 203                                        </color></b>");
            texteADeployer = "Rangez les chaises sous une table";
            scene02Canvas01.transform.gameObject.SetActive(false);
            scene02Canvas02.transform.gameObject.SetActive(true);



        }
        else if (GameManager.Instance.gameEventID == 204)
        {
            Debug.Log("<b><color=#FF4500>                                               Début évent 204                                        </color></b>");
            scene02Canvas02.transform.gameObject.SetActive(false);
            scene02Canvas03.transform.gameObject.SetActive(true);
            snapDropZones2.transform.GetChild(0).GetComponent<VRTK_SnapDropZone>().highlightAlwaysActive = true;
            snapDropZones2.transform.GetChild(1).GetComponent<VRTK_SnapDropZone>().highlightAlwaysActive = true;


        }
        else if (GameManager.Instance.gameEventID == 205)
        {
            Debug.Log("<b><color=#FF4500>                                               Début évent 205                                        </color></b>");
            texteADeployer = "Rendez-vous dans le bureau en suivant les <color=green>fleches vertes </color> ";
            scene02Canvas04.transform.gameObject.SetActive(true);
            scene02Canvas03.transform.gameObject.SetActive(false);



        }           //On indique au joueur de se rendre dans l'autre bloc
        //-------------------------------------------------------------------------------------------------------------------------------




        else if (GameManager.Instance.gameEventID == 1000)
        {
            CameraRig.transform.SetPositionAndRotation(positionLobby, rotationZero);
            CameraRig.transform.Rotate(rotationScene01);
        }          //pour mettre dans le lobby

        else
        {
            Debug.Log("<b><color=#8A29B3>                                               Rien ne se passe                                        </color></b>");
        }


    }           //Sert à mettre à jour chaque élément du jeu dans chaque nouveau Game Event : FONCTION PRINCIPALE AVEC UPDATE

    private void Update()
    {
        //-------------------------------------------------------------------------------------------------------------------------------
        //                                                                                        ID Events Scene Tutoriel commencent à 0
        //                                                                                        ---------------------------------------

        if (GameManager.Instance.gameEventID == 2)
        {
            if (isButton01PressedD == false || isButton01PressedG == false)
            {
                if (controllerDroit.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress))
                {
                    isButton01PressedD = true;

                    //----------------------------------------------------------------------------------------------------------------------                    
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR   
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------                    
                    Debug.Log("<b><color=green>                 Le bouton droit est validé                                      </color></b>");

                }
                if (controllerGauche.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress))
                {
                    isButton01PressedG = true;

                    //----------------------------------------------------------------------------------------------------------------------                   
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR   
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------

                    Debug.Log("<b><color=green>             Le bouton gauche est validé                                             </color></b>");
                    //change couleur tooltip;
                }
            }
            if (isButton01PressedD == true && isButton01PressedG == true && audioManager.isFinishedPlaying == true)
            {
                Debug.Log("<b><color=blue>                                              game Event 02 terminé                                       </color></b>");
                GameManager.Instance.PlayerPassedEventWithSound();

            }

        }           //OK        il faut avoir appuye sur les 2 gachettes
        if (GameManager.Instance.gameEventID == 3)
        {
            if (isButton02PressedD == false || isButton02PressedG == false)
            {

                if (controllerDroit.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
                {
                    isButton02PressedD = true;

                    //----------------------------------------------------------------------------------------------------------------------                    
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR   
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------                    

                    Debug.Log("<b><color=green>Le bouton droit est validé</color></b>");
                    //change couleur tooltip;
                }
                if (controllerGauche.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
                {
                    isButton02PressedG = true;
                    //----------------------------------------------------------------------------------------------------------------------                    
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR  
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------                    

                    Debug.Log("<b><color=green>Le bouton gauche est validé</color></b>");
                    //change couleur tooltip;
                }
            }
            if (isButton02PressedD == true && isButton02PressedG == true && audioManager.isFinishedPlaying == true)
            {
                Debug.Log("<b><color=blue>                                              game Event 03 terminé                                       </color></b>");

                GameManager.Instance.PlayerPassedEvent();
            }

        }           //OK        il faut avoir appuye sur les 2 grip             
        if (GameManager.Instance.gameEventID == 4)
        {
            if (isButton03PressedD == false || isButton03PressedG == false)
            {

                if (controllerDroit.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TouchpadTouch))
                {
                    isButton03PressedD = true;
                    //----------------------------------------------------------------------------------------------------------------------                    
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR   
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------                    

                    Debug.Log("<b><color=green>                                             Le bouton droit est validé                                      </color></b>");
                    //change couleur tooltip;
                }
                if (controllerGauche.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TouchpadTouch))
                {
                    isButton03PressedG = true;

                    //----------------------------------------------------------------------------------------------------------------------                    
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR   
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------                    

                    Debug.Log("<b><color=green>                                             Le bouton gauche est validé                                     </color></b>");
                    //change couleur tooltip;
                }
            }
            if (isButton03PressedD == true && isButton03PressedG == true && audioManager.isFinishedPlaying == true)
            {
                Debug.Log("<b><color=blue>                                              Game Event 04 terminé                                       </color></b>");

                GameManager.Instance.PlayerPassedEvent();
            }

        }           //OK        il faut avoir appuye sur les 2 pad
        if (GameManager.Instance.gameEventID == 5)
        {
            if (isButton04PressedD == false || isButton04PressedG == false)
            {

                if (controllerDroit.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress))
                {
                    isButton04PressedD = true;

                    //----------------------------------------------------------------------------------------------------------------------                    
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerDroit.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR   
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerDroit.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------                    


                    Debug.Log("<b><color=green>                                             Le bouton droit est validé                                      </color></b>");
                    //change couleur tooltip;
                }
                if (controllerGauche.GetComponent<VRTK_ControllerEvents>().IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress))
                {
                    isButton04PressedG = true;

                    //----------------------------------------------------------------------------------------------------------------------                    
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().containerColor = Color.green;              //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().lineColor = Color.green;                   //UPDATE COULEUR
                    controllerGauche.GetComponentInChildren<VRTK_ObjectTooltip>().fontColor = Color.black;                   //UPDATE COULEUR   
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(false);                                      //UPDATE COULEUR
                    controllerGauche.transform.GetChild(1).gameObject.SetActive(true);                                       //UPDATE COULEUR                    
                    //----------------------------------------------------------------------------------------------------------------------                    

                    Debug.Log("<b><color=green>                                             Le bouton gauche est validé                                     </color></b>");
                    //change couleur tooltip;
                }
            }
            if (isButton04PressedD == true && isButton04PressedG == true && audioManager.isFinishedPlaying == true)
            {
                Debug.Log("<b><color=blue>                                              game Event 05 terminé                                       </color></b>");
                GameManager.Instance.PlayerPassedEvent();

            }

        }           //OK        il faut avoir appuye sur les 2 menu
        if (GameManager.Instance.gameEventID == 6)
        {
            if (chaiseRoulanteASnapDrop.GetComponent<ComptagePoints>().aEteCompte == true )
            {
                GameManager.Instance.PlayerPassedEvent();
            }
        }           //OK     pour passer GE06 il faut mettre la CR dans snappedZone
        if (GameManager.Instance.gameEventID == 7)
        {
            if (smartphone.GetComponentInChildren<CollisionBox>().ValueHasEnteredPlayer() == true)
            {
                GameManager.Instance.PlayerPassedEventWithSound();
            }
        }           //OK     pour passer GE07 il faut attraper le gsm et le mettre dans sa poche
        if (GameManager.Instance.gameEventID == 8)
        {
            if (porteAOuvrir.transform.GetChild(0).GetComponent<CollisionBox>().ValueHasEnteredPlayer() == true)
            {
                GameManager.Instance.PlayerPassedEventWithSound();
            }
            
        }           //OK     pour passer GE08 il faut passer a travers la porte
        if (GameManager.Instance.gameEventID == 9)
        {
            if (zoneChaiseRoulante.GetComponentInChildren<CollisionBox>().ValueHasEnteredChaiseRoulante() == true)
            {
                GameManager.Instance.PlayerPassedEventWithSound();
            }
        }           //OK     pour passer GE09 il faut amener la CR au bon endroit
        if (GameManager.Instance.gameEventID == 10)
        {  
            if (zoneLitHopital.GetComponentInChildren<CollisionBox>().ValueHasEnteredLitHopital() == true)
            {                
                GameManager.Instance.PlayerPassedEventWithSound();
            }
        }          //OK     pour passer GE10 il faut amener le Lit au bon endroit
        //-------------------------------------------------------------------------------------------------------------------------------




        //-------------------------------------------------------------------------------------------------------------------------------
        //                                                                                             ID Events Scene 1 commencent à 100
        //                                                                                             ----------------------------------

        if (GameManager.Instance.gameEventID == 102)
        {

            bool hasAlreadyEnteredPlayer = collisionObject101.GetComponent<CollisionBox>().ValueHasAlreadyEnteredPlayer();

            if (hasAlreadyEnteredPlayer == true)
            {
                Debug.Log("<b><color=blue>                                              Game Event 102 terminé                                       </color></b>");
                score = score + 25;
                GameManager.Instance.PlayerPassedEventWithSound();
                
            }

        }           //check que le player soit allé dans la chambre 552
        if (GameManager.Instance.gameEventID == 103)
        {
            bool hasEnteredPlayer = collisionObject102.GetComponent<CollisionBox>().ValueHasEnteredPlayer();
            bool hasEnteredNPC = collisionObject102.GetComponent<CollisionBox>().ValueHasEnteredNPC();

            if (hasEnteredPlayer == true)                          //quand le joueur est entré le npc vient dire qu'il voit de la fumée
            {                
                NPC02.SetActive(true);
                smoke.SetActive(true);
            }
            if (hasEnteredNPC == true)
            {
                audioManagerPeutLancerAudio = true;
            }            

            if (boutonAAppuyer.GetComponent<VRTK_PhysicsPusher>().IsResting() == false)
            {
                Debug.Log("<b><color=blue>                                              Game Event 103 terminé                                       </color></b>");
                alarmes.SetActive(true);
                score = score + 25;
                GameManager.Instance.PlayerPassedEventWithSound();
            }
        }           //check que le player soit retourné dans le bureau
        if (GameManager.Instance.gameEventID == 104)
        {

            bool hasEnteredPlayer = collisionObject103.GetComponent<CollisionBox>().ValueHasEnteredPlayer();
            
            if (lancerLeQuestionnaireUneSeuleFois == false)
            {
               
                questionnaire.SetActive(true);
                questionnaireManager.DesactiverControles();
                lancerLeQuestionnaireUneSeuleFois = true;
            }
           


            if (hasEnteredPlayer == true)
            {
                Debug.Log("<b><color=blue>                                              Game Event 104 terminé                                       </color></b>");
                score = score + 25;
                GameManager.Instance.PlayerPassedEventWithSound();
            }
        }
        if (GameManager.Instance.gameEventID == 105)
        {            
            bool hasAlreadyEnteredObject1 = safeZoneTrigger101.GetComponent<CollisionBox>().ValueHasEnteredChaiseRoulante();
            bool hasAlreadyEnteredObject2 = safeZoneTrigger102.GetComponent<CollisionBox>().ValueHasEnteredChaiseRoulante();
            bool hasAlreadyEnteredObject3 = safeZoneTrigger103.GetComponent<CollisionBox>().ValueHasEnteredChaiseRoulante();
            if (hasAlreadyEnteredObject1 == true || hasAlreadyEnteredObject2 == true || hasAlreadyEnteredObject3 == true)
            {
                Debug.Log("<b><color=blue>                                              Game Event 105 terminé                                       </color></b>");
                GameManager.Instance.PlayerPassedEventWithSound();
                            

                score = score + 500;
            }     

        }           //check que la chaise roulante soit dans la zone safe
        if (GameManager.Instance.gameEventID == 106)
        {

            bool hasEnteredPlayer = collisionObject104.GetComponent<CollisionBox>().ValueHasEnteredPlayer();

            if (hasEnteredPlayer == true)
            {
                Debug.Log("<b><color=blue>                                              Game Event 106 terminé                                       </color></b>");
                GameManager.Instance.PlayerPassedEventWithSound();



            }
        }           //check que le player aille dans la chambre 562
        if (GameManager.Instance.gameEventID == 107)
        {
            choixPatientAAider01 = litHopital.GetComponentInChildren<CollisionBox>().ValueHasEnteredPlayer();
            choixPatientAAider02 = NPC04.GetComponentInChildren<CollisionBox>().ValueHasEnteredPlayer();

            if (choixPatientAAider01 == true)
            {
                litHopital.transform.GetChild(1).gameObject.SetActive(false);
                NPC04.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                scene01Canvas06.SetActive(false);
                choixPatientAAider00 = 1;
                GameManager.Instance.PlayerPassedEventWithSound();
            }
            else if (choixPatientAAider02 == true)
            {
                litHopital.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
                NPC04.transform.GetChild(0).gameObject.SetActive(false);
                scene01Canvas06.SetActive(false);
                
                choixPatientAAider00 = 2;
                GameManager.Instance.PlayerPassedEventWithSound();

            }

        }           //choix de la personne a evacuer
        if (GameManager.Instance.gameEventID == 108)
        {
            NPC01.GetComponent<BasicAI>().fireState = NPCsBehaviourManager.FireState.fireIsOn;
                 if (choixPatientAAider00 == 1)
            {
                bool hasAlreadyEnteredObject1 = safeZoneTrigger101.GetComponent<CollisionBox>().ValueHasEnteredLitHopital();
                bool hasAlreadyEnteredObject2 = safeZoneTrigger102.GetComponent<CollisionBox>().ValueHasEnteredLitHopital();
                bool hasAlreadyEnteredObject3 = safeZoneTrigger103.GetComponent<CollisionBox>().ValueHasEnteredLitHopital();               

                if (hasAlreadyEnteredObject1 == true || hasAlreadyEnteredObject2 == true || hasAlreadyEnteredObject3 == true)
                {
                    Debug.Log("<b><color=blue>                                              Game Event 108 - 1 terminé                                       </color></b>");
                    GameManager.Instance.PlayerPassedEventWithSound();
                    score = score + 500;
                }//quand on a mis le lit en sécurité, PPE

                if (NPC04.GetComponentInChildren<CollisionBox>().hasEnteredNPC == true)
                {
                    NPC04.GetComponentInChildren<FollowerScript>().enabled = true;
                    NPC04.GetComponentInChildren<FollowerScript>().target = NPC01;
                    NPC04.GetComponentInChildren<BasicAI>().stateAfter = BasicAI.StateAfter.FOLLOW;
                    NPC04.GetComponentInChildren<BasicAI>().fireState = NPCsBehaviourManager.FireState.fireIsOn;
                    NPC01.GetComponentInChildren<BasicAI>().walkTarget = waypoints.transform.GetChild(0).gameObject;        //la target de NPC01 est waypointNPC01
                    //texte d'accord je vous suis
                }// pendant ce temps, NPC01 evacue NPC04. quand il a pénétré le trigger, NPC04 suit NPC01 et NPC01 va vers une ZoneSafe 


            }           //On a choisi d'évacuer le lit
            else if (choixPatientAAider00 == 2)
            {
                //si le collider du NPC est dans la zone safe, alors PlayerPassedEvent
                bool hasAlreadyEnteredObject1 = safeZoneTrigger101.GetComponent<CollisionBox>().ValueHasEnteredNPC();
                bool hasAlreadyEnteredObject2 = safeZoneTrigger102.GetComponent<CollisionBox>().ValueHasEnteredNPC();
                bool hasAlreadyEnteredObject3 = safeZoneTrigger103.GetComponent<CollisionBox>().ValueHasEnteredNPC();

                if (hasAlreadyEnteredObject1 == true || hasAlreadyEnteredObject2 == true || hasAlreadyEnteredObject3 == true)
                {
                    Debug.Log("<b><color=blue>                                              Game Event 108 - 2 terminé                                       </color></b>");
                    GameManager.Instance.PlayerPassedEventWithSound();
                    score = score + 500;
                }

                if (litHopital.GetComponentInChildren<CollisionBox>().hasEnteredNPC == true)
                {
                    
                    StartCoroutine(AttendreAnimTerminee(5f));
                    // on lance une discussion
                    // on met un délai avant de lancer l'anim;
                    
                }
            }           //On a choisi d'évacuer la personne

        }           //check que le lit d'hopital soit dans la Zone Safe       
        if (GameManager.Instance.gameEventID == 109)
        {
            if(gsm.GetComponent<CollisionBox>().hasEnteredPlayer == true)
            {
                aPrisLeGSM = true;
            }                                           //on change la valeur du bool aPrisGsm si on le touche dans ce GameEvent
            if (aPrisLeGSM == false && endSceneTrigger.GetComponent<CollisionBox>().hasEnteredPlayer == true)
            {
                GameManager.Instance.SkipToNextLevel(7);
            }       //on retourne au lobby
            else if (aPrisLeGSM == true && endSceneTrigger.GetComponent<CollisionBox>().hasEnteredPlayer == true)
            {
                GameManager.Instance.SkipToNextLevel(7);
                score = score - 800;
            }   //on retourne au lobby
        }
        if (porteAVerifier.GetComponent<IsTheDoorHot>().PorteVerifiee() == true && porteAEteVerifiee == false)
        {
            porteAEteVerifiee = true;
        }
        else if (porteAVerifier.GetComponent<IsTheDoorHot>().porteBloquee == true && porteAEteBloquee == false)
        {
            porteAEteBloquee = true;
        }
        //-------------------------------------------------------------------------------------------------------------------------------




        //-------------------------------------------------------------------------------------------------------------------------------
        //                                                                                             ID Events Scene 2 commencent à 200
        //                                                                                             ----------------------------------
        if (GameManager.Instance.gameEventID == 202)
        {
           if (trigger201.GetComponent<CollisionBox>().hasEnteredPlayer == true)
            {               
                GameManager.Instance.PlayerPassedEvent();
            }
        }           //Si on va dans le couloir, PPE
        if (GameManager.Instance.gameEventID == 203)
        {
            if (trigger202.GetComponent<CollisionBox>().hasEnteredPlayer == true)
            {                
                GameManager.Instance.PlayerPassedEvent();
            }
        }           //Si on s'approche des chaises, PPE
        if (GameManager.Instance.gameEventID == 204)
        {
            if (chaiseADeplacer[0].GetComponent<ComptagePoints>().aEteCompte == true && chaiseADeplacer[1].GetComponent<ComptagePoints>().aEteCompte == true)
            {                
                GameManager.Instance.PlayerPassedEventWithSound();
            }
        }           // Si on a rangé les 2 chaises, PPE
        if (GameManager.Instance.gameEventID == 205)
        {
            if (collisionObject102.GetComponent<CollisionBox>().hasEnteredPlayer == true)
            {
                GameManager.Instance.SkipToNextLevel(4);
            }
        }           //Quand on arrive dans le bureau, on passe a la scene 01
        //-------------------------------------------------------------------------------------------------------------------------------



    }           //Sert à faire les check pour pouvoir passer au Game Event suivant : FONCTION PRINCIPALE AVEC GAMEEVENTUPDATED


    private void ObjectSnappedToDropZone(object sender, SnapDropZoneEventArgs e)
    {
        
        if(e.snappedObject.GetComponent<ComptagePoints>().aEteCompte == false)
        {
            Debug.Log("Vous avez désencombré une issue de secours et gagné 100 points");
            score = score + 100;
            GameManager.Instance.Sound1();
            e.snappedObject.GetComponentInChildren<ComptagePoints>().aEteCompte = true;
            nombreObjetsRangés++;
        }
        else if (e.snappedObject.GetComponent<ComptagePoints>().aEteCompte == true)
        {
            Debug.Log("<b><color=#0000FF> Vous avez déjà posé cet objet à son bon emplacement !!  </color></b>");
            GameManager.Instance.Sound2();
            score = score - 1;
        }


    }//sert à envoyer l'info qu'un objet a été snapdropped
    IEnumerator AttendreAnimTerminee(float temps1)
    {
        yield return new WaitForSeconds(temps1);        
        litHopitalAnim.SetActive(true);
        NPC01.SetActive(false);
        litHopital.SetActive(false);
        isFinishedPlaying = true; 
    }// sert à décaler certaines animations dans le temps;
    
}