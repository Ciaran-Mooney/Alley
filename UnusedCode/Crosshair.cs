using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public Image m_crosshair;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FirstPersonRaycaster.OnHoveredObjectHovered += OnObjectHovered;
    }

    private void OnObjectHovered(bool isHovering)
    {
        if (isHovering)
        {
            m_crosshair.color = Color.yellow;
        }
        else
        {
            m_crosshair.color = Color.white;
        }
    }
}


