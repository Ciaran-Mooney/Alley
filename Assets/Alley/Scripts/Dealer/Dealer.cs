using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : FPSInteractable
{
    // dialog
    public static string Intro = "First one's on the house.";
    public static string PissOnHomeless = "Piss on a homeless person.";
    public static string FeedMeCat = "Feed me a cat.";
    public static string TakeThis = "Take this.";
    public static string HaveIt = "Have it.";


    DialogueManager dialogueManager; 
    public float timeInteract = 0;
    bool isInteracting;

    void Start()
    {
        dialogueManager = gameObject.GetComponent<DialogueManager>();
    }

    private void Awake()
    {
        minDistance = 1;
    }

    public override void Interact()
    {

        if (isInteracting)
        {
            return;
        }
        isInteracting = true;

        dialogueManager.StartDialogue(Intro);
    }

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
        
        // if the player has not started a mission
        if(GameState.mission == Mission.None)
        {
            SetMission();
        }
        else
        {
            GiveDrugs();
        }
    }

    public override bool IsInteractable()
    {
        return GameState.mission == Mission.None 
        || GameState.missionState == MissionState.Completed;
    }

    private void SetMission()
    {
        switch(GameState.day)
        {
            case 0:
                GameState.mission = Mission.Freebie;
                break;

            case 1:
                GameState.mission = Mission.Piss;
                break;

            case 2:
                GameState.mission = Mission.Cat;
                break;
            
            case 3:
                //Game complete
                break;
        }
    }

    private void GiveDrugs()
    {
        GameState.mission = Mission.None;
        GameState.heldObject = HeldObject.Drugs;
    }
}
