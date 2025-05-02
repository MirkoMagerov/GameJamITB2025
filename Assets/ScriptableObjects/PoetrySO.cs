using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoetrySO", menuName = "ScriptableObjects/PoetrySO")]
public class PoetrySO : ScriptableObject
{
    public string title;
    public string author;
    [TextArea(3, 10)]
    public string[] lines;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
