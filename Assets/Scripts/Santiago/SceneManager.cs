using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;

    public Texture2D cursorTexture;
    private Vector2 hotSpot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        //Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width, cursorTexture.height), cursorMode);
    }

    public void LoadScene(int sceneIndex)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }

        // Si quieres cambiar el cursor en ciertas escenas
        if (sceneIndex == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            //Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width, cursorTexture.height), cursorMode);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
