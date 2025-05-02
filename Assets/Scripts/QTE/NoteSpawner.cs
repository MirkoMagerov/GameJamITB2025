using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class NoteSpawner : MonoBehaviour
{

    [SerializeField] private GameObject keyPrefab;

    [SerializeField] private Transform QKeyPosition;
    [SerializeField] private Transform WKeyPosition;
    [SerializeField] private Transform EKeyPosition;
    [SerializeField] private Transform RKeyPosition;

    public float beatTempo;

    private bool gameEnded = false;

    private void Start()
    {
        beatTempo = beatTempo / 60f;

        StartCoroutine(SpawnKey());
    }

    private IEnumerator SpawnKey()
    {
        while (!gameEnded)
        {
            yield return new WaitForSeconds(beatTempo);

            int randomKey = Random.Range(0, 4);

            switch (randomKey)
            {
                case 0:
                    GameObject QKey = Instantiate(keyPrefab, QKeyPosition.position, Quaternion.identity);
                    QKey.GetComponent<NoteObject>().keyToPress = KeyCode.Q;
                    QKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                    break;
                case 1:
                    GameObject WKey = Instantiate(keyPrefab, WKeyPosition.position, Quaternion.identity);
                    WKey.GetComponent<NoteObject>().keyToPress = KeyCode.W;
                    WKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                    break;
                case 2:
                    GameObject EKey = Instantiate(keyPrefab, EKeyPosition.position, Quaternion.identity);
                    EKey.GetComponent<NoteObject>().keyToPress = KeyCode.E;
                    EKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                    break;
                case 3:
                    GameObject RKey = Instantiate(keyPrefab, RKeyPosition.position, Quaternion.identity);
                    RKey.GetComponent<NoteObject>().keyToPress = KeyCode.R;
                    RKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                    break;
            }
        }
    }

}
