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
        FadeToBlack.instance.Fade();

        //AudioManager.Instance.Play(BoreholeAudioType.Drill);
        //isInteracting = true;
        CanvasElements.Instance.m_interactBar.StartInteracting(new InteractBar.InteractParameters(OnFinishInteracted, timeInteract));

        this.gameObject.SetActive(false);
    }

    public override void StopInteract()
    {
        if (!isInteracting)
        {
            return;
        }
        CanvasElements.Instance.m_interactBar.StopInteracting();
    }

    private void OnFinishInteracted()
    {
        Destroy(this.gameObject);
    }

    public override bool IsInteractable()
    {
        return true;
    }
}
