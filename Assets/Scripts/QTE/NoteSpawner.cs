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

    [SerializeField] private ButtonController QKeyController;
    [SerializeField] private ButtonController WKeyController;
    [SerializeField] private ButtonController EKeyController;

    [SerializeField] private List<int> Keys = new List<int>();

    public float beatTempo;

    public bool playerOneTurn;

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
            if (playerOneTurn)
            {
                yield return new WaitForSeconds(beatTempo);

                int randomKey = Random.Range(0, 3);

                switch (randomKey)
                {
                    case 0:
                        GameObject QKey = Instantiate(keyPrefab, QKeyPosition.position, Quaternion.identity);
                        QKey.GetComponent<NoteObject>().keyToPress = KeyCode.Q;
                        QKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                        QKey.GetComponent<NoteObject>().buttonController = QKeyController;
                        QKey.GetComponent<NoteObject>().fadeInDuration = beatTempo / 2;
                        break;
                    case 1:
                        GameObject WKey = Instantiate(keyPrefab, WKeyPosition.position, Quaternion.identity);
                        WKey.GetComponent<NoteObject>().keyToPress = KeyCode.W;
                        WKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                        WKey.GetComponent<NoteObject>().buttonController = WKeyController;
                        WKey.GetComponent<NoteObject>().fadeInDuration = beatTempo / 2;
                        break;
                    case 2:
                        GameObject EKey = Instantiate(keyPrefab, EKeyPosition.position, Quaternion.identity);
                        EKey.GetComponent<NoteObject>().keyToPress = KeyCode.E;
                        EKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                        EKey.GetComponent<NoteObject>().buttonController = EKeyController;
                        EKey.GetComponent<NoteObject>().fadeInDuration = beatTempo / 2;
                        break;
                }
            }
            else
            {

                yield return new WaitForSeconds(beatTempo);

                int randomKey = Random.Range(0, 3);

                switch (randomKey)
                {
                    case 0:
                        GameObject  ArrowLeftKey = Instantiate(keyPrefab, QKeyPosition.position, Quaternion.identity);
                        ArrowLeftKey.GetComponent<NoteObject>().keyToPress = KeyCode.LeftArrow;
                        ArrowLeftKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                        ArrowLeftKey.GetComponent<NoteObject>().buttonController = QKeyController;
                        ArrowLeftKey.GetComponent<NoteObject>().fadeInDuration = beatTempo / 2;
                        break;
                    case 1:
                        GameObject ArrowDownKey = Instantiate(keyPrefab, WKeyPosition.position, Quaternion.identity);
                        ArrowDownKey.GetComponent<NoteObject>().keyToPress = KeyCode.DownArrow;
                        ArrowDownKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                        ArrowDownKey.GetComponent<NoteObject>().buttonController = WKeyController;
                        ArrowDownKey.GetComponent<NoteObject>().fadeInDuration = beatTempo / 2;
                        break;
                    case 2:
                        GameObject ArrowRightKey = Instantiate(keyPrefab, EKeyPosition.position, Quaternion.identity);
                        ArrowRightKey.GetComponent<NoteObject>().keyToPress = KeyCode.RightArrow;
                        ArrowRightKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                        ArrowRightKey.GetComponent<NoteObject>().buttonController = EKeyController;
                        ArrowRightKey.GetComponent<NoteObject>().fadeInDuration = beatTempo / 2;
                        break;
                }

            }
        }

    }
}
