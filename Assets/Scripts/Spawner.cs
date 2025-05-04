using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] gameObjects;

    void Start()
    {
        int randomIndex = Random.Range(0, 2);
        if (randomIndex == 0) gameObjects[0].SetActive(false);
        else gameObjects[1].SetActive(false);
    }
}
