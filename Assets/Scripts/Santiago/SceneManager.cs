using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int sceneIndex)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != sceneIndex)
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
