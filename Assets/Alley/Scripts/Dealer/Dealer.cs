using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : FPSInteractable
{

    public List<string> missionIntro = new(){
        "First one's on the house.",
        "Piss on the homeless.",
        "Feed me a cat."
    };

    public static string TakeThis = "Take this.";
    public static string HaveIt = "Have it.";


    DialogueManager dialogueManager; 
    public float timeInteract = 0;
    bool isInteracting;
    bool giveDrug = false;

    public GameObject drugPrefab;

    void Start()
    {
        dialogueManager = gameObject.GetComponent<DialogueManager>();
    }

    private void Awake()
    {
        GameState.missionState = MissionState.Completed;
    }

    public override void Interact()
    {

        if (isInteracting)
        {
            return;
        }
        isInteracting = true;
  
        // Give the drug, thats it
        if(GameState.missionState == MissionState.Completed)
        {

            if(GameState.day == 0){
                dialogueManager.StartDialogue(missionIntro[0]);
                giveDrug = true;
                GameState.day++;
                SetMission();
                CanvasElements.Instance.m_interactBar.StartInteracting(new InteractBar.InteractParameters(OnFinishInteracted, 0));
                return;
            }

            if(GameState.heldObject != null)
            {
                Destroy(GameState.heldObject.gameObject);
            }

            // give him the drug

            dialogueManager.StartDialogue(TakeThis);
            giveDrug = true;
            GameState.day++;
            SetMission();
            CanvasElements.Instance.m_interactBar.StartInteracting(new InteractBar.InteractParameters(OnFinishInteracted, 0));
            return;
            // reset the mission state
            // return;
        }

        // Do the dialog for either the intro to the mission or remind the player of the mission
        dialogueManager.StartDialogue(missionIntro[GameState.day]);

        CanvasElements.Instance.m_interactBar.StartInteracting(new InteractBar.InteractParameters(OnFinishInteracted, 0));
    }

    public void GiveDrug()
    {
        GameObject go = Instantiate(drugPrefab, Vector3.zero, Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform);
        go.transform.localPosition = new Vector3(-0.4f, 1.28f, 0.409f);
        go.transform.localRotation = Quaternion.Euler(0, 121.76f, 0);
    }

    // public void SetMission(){
    //     // Piss on tent
    //     if(missionIndex == 1){
    //         // make tent interactable
    //     }
    //     // Take cat
    //     else if(missionIndex == 2){
    //         // make cat interactable
    //     }
    // }

    public override void StopInteract()
    {
        if (!isInteracting)
        {
            return;
        }
        isInteracting = false;
        CanvasElements.Instance.m_interactBar.StopInteracting();
    }

    private void OnFinishInteracted()
    {
        isInteracting = false;
        if (giveDrug)
        {
            GiveDrug();
            giveDrug = false;
        }

        // if the player has not started a mission
        //if(GameState.mission == Mission.None)
        //{
        //    SetMission();
        //}
        //else
        //{
        //    GiveDrugs();
        //}
    }

    public override bool IsInteractable()
    {
        return true;
    }

    private void SetMission()
    {
        GameState.missionState = MissionState.Active;
        switch(GameState.day)
        {
            case 0:
                GameState.mission = Mission.Freebie;
                break;

            case 1:
                GameState.mission = Mission.Piss;
                // todo: make tent interactable
                break;

            case 2:
                GameState.mission = Mission.Cat;
                // todo: make cat interactable
                break;
            
            case 3:
                //Game complete
                break;
        }
    }

    private void GiveDrugs()
    {
        GameState.mission = Mission.None;
        //GameState.heldObject = HeldObject.Drugs;
    }
}
