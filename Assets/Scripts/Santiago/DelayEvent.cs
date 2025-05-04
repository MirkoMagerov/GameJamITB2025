using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayEvent : MonoBehaviour
{
    public float delayTime = 2f; // Tiempo de retraso en segundos
    public GameObject objectToActivate; // Objeto a activar después del retraso
    void Start()
    {
        // Inicia la corrutina para activar el objeto después del retraso
        StartCoroutine(ActivateObjectAfterDelay());
    }
    private IEnumerator ActivateObjectAfterDelay()
    {
        // Espera el tiempo de retraso
        yield return new WaitForSeconds(delayTime);
        // Activa el objeto
        objectToActivate.SetActive(true);
        PauseGame();
    }

    private void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
