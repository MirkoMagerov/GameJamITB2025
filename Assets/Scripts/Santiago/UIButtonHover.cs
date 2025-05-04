using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private Image buttonImage;
    private TMP_Text buttonText;
    public GameObject adornoText;

    public Color normalColor = Color.white;
    public Color hoverColor = Color.gray;

    void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TMP_Text>();

        if (buttonImage != null)
            buttonImage.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Entered Button: " + gameObject.name);
        if (buttonImage != null)
            buttonImage.color = hoverColor;
        if (buttonText != null)
            buttonText.color = hoverColor;
        if (adornoText != null)
            adornoText.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
            buttonImage.color = normalColor;
        if (buttonText != null)
            buttonText.color = normalColor;
        if (adornoText != null)
            adornoText.gameObject.SetActive(false);
    }
}
