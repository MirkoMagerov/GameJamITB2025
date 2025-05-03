using UnityEngine;

public class PoetryTrigger : MonoBehaviour
{
    public PoetryGenerator poetryGenerator;
    public LevelWordsSO specialWords;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TriggerPoem();
        }
    }
    private void Start()
    {
        if (poetryGenerator == null)
            {
            poetryGenerator = GetComponent<PoetryGenerator>();
        }
    }
    public void TriggerPoem()
    {
        poetryGenerator.specialWords = specialWords;
        poetryGenerator.StartPoem();
    }
}