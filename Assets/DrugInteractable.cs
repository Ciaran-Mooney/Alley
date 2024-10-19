using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugInteractable : FPSInteractable
{
    // how long you take for before you wake up 
    public float timeInteract;
    public bool isInteracting;

   public override void Interact()
    {

        // dont want to take the drug more than once per time
        if (isInteracting)
        {
            return;
        }
        isInteracting = true;

        // fade to black

        // reset mission type

        // hear syringe noise

        //AudioManager.Instance.Play(BoreholeAudioType.Drill);
        //isInteracting = true;
        CanvasElements.Instance.m_interactBar.StartInteracting(new InteractBar.InteractParameters(OnFinishInteracted, timeInteract));

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

        // tp to bed

        // wake up

        isInteracting = false;
        Destroy(this.gameObject);
    }

    public override bool IsInteractable()
    {
        return true;
    }
}
