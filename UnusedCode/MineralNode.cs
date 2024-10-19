using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralNode : FPSInteractable
{
    public float timeInteract = 10;
    bool isInteracting;

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
        AudioManager.Instance.Play(BoreholeAudioType.Drill);
        isInteracting = true;
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
        isInteracting = false;
        Destroy(this.gameObject);
        AudioManager.Instance.Stop(BoreholeAudioType.Drill);
    }

    public override bool IsInteractable()
    {
        return true;
    }
}

public abstract class FPSInteractable : MonoBehaviour
{
    public float minDistance;

    public abstract bool IsInteractable();
    public abstract void Interact();
    public abstract void StopInteract();
}