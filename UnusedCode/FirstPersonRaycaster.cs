using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonRaycaster : MonoBehaviour
{
    [SerializeField]
    private FPSInteractable current;
    public static Action<bool> OnHoveredObjectHovered;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        int layerMask = LayerMask.GetMask("FPSInteractable");
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))
        {
            var interactable = hitInfo.collider.gameObject.GetComponent<FPSInteractable>();
            if(hitInfo.distance <= interactable.minDistance)
            {
                if (interactable.IsInteractable())
                {
                    current = interactable;
                    OnHoveredObjectHovered?.Invoke(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        interactable.Interact();
                    }
                }
            }
            return;
        }

        if(current != null)
        {
            current.StopInteract();
            OnHoveredObjectHovered?.Invoke(false);
        }
    }
}
