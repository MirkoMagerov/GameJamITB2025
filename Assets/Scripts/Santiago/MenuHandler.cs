using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> targetDelay = new();
    [SerializeField]
    private float delayTime;

    [SerializeField] private GameObject resourcesCanvas;
    [SerializeField] private GameObject scrollingText;

    public void ShowDelay(int targetIndex)
    {
        StartCoroutine(Delay(targetIndex));
    }

    public IEnumerator Delay(int targetIndex)
    {
        yield return new WaitForSeconds(delayTime);
        targetDelay[targetIndex].SetActive(true);
    }

    public void ShowResources()
    {

        if (resourcesCanvas.activeSelf)
        {
            resourcesCanvas.SetActive(false);
            scrollingText.SetActive(true);
            return;
        }
        else
        {
            resourcesCanvas.SetActive(true);
            scrollingText.SetActive(false);
        }

    }
}
