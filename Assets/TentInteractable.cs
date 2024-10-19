using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentInteractable : FPSInteractable
{
    float timeInteract = 5;
    bool isInteracting = false;
    public override void Interact()
    {

        if (isInteracting)
        {
            return;
        }
        //AudioManager.Instance.Play(BoreholeAudioType.Drill);
        isInteracting = true;
        CanvasElements.Instance.m_interactBar.StartInteracting(new InteractBar.InteractParameters(OnFinishInteracted, timeInteract));

    }

    public override void StopInteract()
    {
    }

    private void OnFinishInteracted()
    {
        isInteracting = false;
        GameState.missionState = MissionState.Completed;
        //AudioManager.Instance.Stop(BoreholeAudioType.Drill);
    }

    public override bool IsInteractable()
    {
        return GameState.mission == Mission.Piss;
    }
}
