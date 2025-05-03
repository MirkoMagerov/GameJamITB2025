using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerV2 : MonoBehaviour
{
    public static GameManagerV2 Instance;

    public Player leftPlayer;
    public Player rightPlayer;
    public bool leftPlayerPoem;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetRoles();
        InitializeUI();
    }

    private void SetRoles()
    {
        int randomValue = Random.Range(0, 2);
        leftPlayerPoem = randomValue == 0;
    }

    private void InitializeUI()
    {
        if (leftPlayerPoem)
        {
            leftPlayer.poemGameObject.SetActive(true);
            rightPlayer.laudGameObject.SetActive(false);
        }
        else
        {
            leftPlayer.laudGameObject.SetActive(true);
            rightPlayer.poemGameObject.SetActive(false);
        }
    }
}
