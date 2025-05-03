using TMPro;
using UnityEngine;

public class FollowLastCharacter : MonoBehaviour
{
    public TextMeshProUGUI tmpText; // Or TextMeshPro if 3D
    public RectTransform follower;  // Use Transform if 3D
    public float followSpeed = 5f;  // Speed at which the follower moves

    void Update()
    {
        tmpText.ForceMeshUpdate(); // Update text mesh info

        var textInfo = tmpText.textInfo;
        int lastVisibleCharIndex = tmpText.text.Length - 1;

        if (lastVisibleCharIndex < 0 || lastVisibleCharIndex >= textInfo.characterCount)
            return;

        TMP_CharacterInfo charInfo = textInfo.characterInfo[lastVisibleCharIndex];

        if (!charInfo.isVisible)
            return;

        Vector3 charMidBotPos = (charInfo.bottomLeft + charInfo.bottomRight) / 2;
        Vector3 targetWorldPos = tmpText.transform.TransformPoint(charMidBotPos);

        // Smoothly follow the target position
        follower.position = Vector3.Lerp(follower.position, targetWorldPos, followSpeed * Time.deltaTime);
    }
}
