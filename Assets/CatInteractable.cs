using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CatInteractable : FPSInteractable
{
   public override void Interact()
    {


        // if (isInteracting)
        // {
        //     return;
        // }

        if(isHeld)
        {
            // play cat noise
            return;
        }
        else
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            isHeld = true;
            GameState.heldObject = this;

            this.transform.parent = player.transform;
            this.transform.localPosition = new Vector3(0.13f, 0.777f, 1.263f);
        }

        //AudioManager.Instance.Play(BoreholeAudioType.Drill);
        //isInteracting = true;

        
        // below code is necessary but badly done
        CanvasElements.Instance.m_interactBar.StartInteracting(new InteractBar.InteractParameters(OnFinishInteracted, 0));

    }

    public override void StopInteract()
    {
        // if (!isInteracting)
        // {
        //     return;
        // }
        // isInteracting = false;
        // CanvasElements.Instance.m_interactBar.StopInteracting();
    }

    private void OnFinishInteracted()
    {
        //isInteracting = false;
        //Destroy(this.gameObject);
        //AudioManager.Instance.Stop(BoreholeAudioType.Drill);
        GameState.missionState = MissionState.Completed;
    }

    public override bool IsInteractable()
    {
        return GameState.mission == Mission.Cat && GameState.missionState == MissionState.Active;
    }
}
