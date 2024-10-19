using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasElements : MonoBehaviour
{
    public InteractBar m_interactBar;
    public Crosshair m_crosshair;

    public static CanvasElements Instance;

    private void Awake()
    {
        Instance = this;
    }
}
