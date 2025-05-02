using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextScroll : MonoBehaviour
{
    [SerializeField]
    private RectTransform textRectTransform;
    [SerializeField]
    private TMP_Text TextMeshPro;

    [SerializeField]
    private PoetrySO poetrySO;

    [SerializeField]
    private float scrollSpeed = 100f;

    private void OnValidate()
    {
        if (textRectTransform == null)
        {
            textRectTransform = GetComponent<RectTransform>();
        }
        if (TextMeshPro == null)
        {
            TextMeshPro = GetComponent<TMP_Text>();
        }
    }
    private void Awake()
    {
        if (textRectTransform == null)
        {
            textRectTransform = GetComponent<RectTransform>();
        }
        if (TextMeshPro == null)
        {
            TextMeshPro = GetComponent<TextMeshPro>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set the text to the first line of the poetry
        int poetry = Random.Range(0,poetrySO.lines.Length + 1);
        TextMeshPro.text = poetrySO.lines[1];
        //// Set the size of the RectTransform to fit the text
        textRectTransform.sizeDelta = new Vector2(textRectTransform.sizeDelta.x, TextMeshPro.preferredHeight);
        //// Set the position of the RectTransform to be at the bottom of the screen
        textRectTransform.anchoredPosition = new Vector2(textRectTransform.anchoredPosition.x, -textRectTransform.sizeDelta.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        textRectTransform.anchoredPosition += new Vector2(0, Time.deltaTime * scrollSpeed);
    }
}
