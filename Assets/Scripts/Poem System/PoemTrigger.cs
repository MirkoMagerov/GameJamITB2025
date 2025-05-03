using UnityEngine;

public class PoetryTrigger : MonoBehaviour
{
    public LevelWordsSO specialWords;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerPoem();
        }
    }

    public void TriggerPoem()
    {
        if (PoetryGenerator.Instance != null)
        {
            PoetryGenerator.Instance.specialWords = specialWords;
            PoetryGenerator.Instance.StartPoem();
        }
    }
}