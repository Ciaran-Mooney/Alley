using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractBar : MonoBehaviour
{
    public Image m_bar;
    public bool m_isInteracting;
    public InteractParameters m_interactParameters;

    private void Awake()
    {
        m_bar.gameObject.SetActive(false);
    }

    public void StartInteracting(InteractParameters interactParameters)
    {
        m_interactParameters = interactParameters;
        m_bar.transform.localScale = Vector3.one;
        m_isInteracting = true;
        m_bar.gameObject.SetActive(true);
    }

    public void FinishInteracting()
    {
        m_isInteracting = false;
        m_interactParameters.completedCallback.Invoke();
        m_bar.gameObject.SetActive(false);
    }
    public void StopInteracting()
    {

    }

    private void Update()
    {
        if (!m_isInteracting)
        {
            return;
        }

        Vector3 scale = m_bar.transform.localScale;
        scale.x = m_interactParameters.currentSeconds / m_interactParameters.durationSeconds;
        m_bar.transform.localScale = scale;

        m_interactParameters.currentSeconds += Time.deltaTime;

        if (m_interactParameters.currentSeconds >= m_interactParameters.durationSeconds)
        {
            FinishInteracting();
        }
    }

    public class InteractParameters
    {
        public Action completedCallback;
        public float currentSeconds;
        public float durationSeconds;
        public float step;

        public InteractParameters(Action completedCallback, float durationSeconds)
        {
            this.completedCallback = completedCallback;
            this.durationSeconds = durationSeconds;
            this.currentSeconds = 0;
        }
    }
}
