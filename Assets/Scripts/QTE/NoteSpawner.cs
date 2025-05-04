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
    private List<GameObject> activeNotes = new List<GameObject>();

    public float beatTempo;

    public bool playerOneTurn;

    private bool gameEnded = false;
    private Coroutine spawnKeyCoroutine;

    void Awake()
    {
        beatTempo = beatTempo / 60f;
    }

    void OnEnable()
    {
        spawnKeyCoroutine = StartCoroutine(SpawnKey());
        GameManager.SwapTurn += ClearNotes;
        GameManager.OnEndOfGame += ClearNotes;
    }

    void OnDisable()
    {
        if (spawnKeyCoroutine != null)
        {
            StopCoroutine(spawnKeyCoroutine);
            spawnKeyCoroutine = null;
        }
        GameManager.SwapTurn -= ClearNotes;
        GameManager.OnEndOfGame -= ClearNotes;
    }

    private IEnumerator SpawnKey()
    {
        while (!gameEnded)
        {
            if (!GameManager.Instance.leftPlayerPoem)
            {
                yield return new WaitForSeconds(beatTempo);

                int randomKey = Random.Range(0, 3);

                int doubleKeys = Random.Range(0, 4);

                CreateKey(randomKey);

                if (doubleKeys == 1)
                {
                    int randomKey2 = Random.Range(0, 3);
                    while (randomKey2 == randomKey)
                    {
                        randomKey2 = Random.Range(0, 3);
                    }
                    CreateKey(randomKey2);
                }

            }
            else
            {
                yield return new WaitForSeconds(beatTempo);

                int randomKey = Random.Range(0, 3);

                int doubleKeys = Random.Range(0, 4);

                CreateKeyRightPlayer(randomKey);

                if (doubleKeys == 1)
                {
                    int randomKey2 = Random.Range(0, 3);
                    while (randomKey2 == randomKey)
                    {
                        randomKey2 = Random.Range(0, 3);
                    }
                    CreateKeyRightPlayer(randomKey2);
                }
            }
        }
    }

    private void CreateKey(int randomKey)
    {
        switch (randomKey)
        {
            case 0:
                GameObject QKey = Instantiate(keyPrefab, QKeyPosition.position, Quaternion.identity);
                QKey.GetComponent<NoteObject>().keyToPress = KeyCode.Q;
                QKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                QKey.GetComponent<NoteObject>().buttonController = QKeyController;
                QKey.GetComponent<NoteObject>().fadeInDuration = beatTempo;
                activeNotes.Add(QKey);
                break;
            case 1:
                GameObject WKey = Instantiate(keyPrefab, WKeyPosition.position, Quaternion.identity);
                WKey.GetComponent<NoteObject>().keyToPress = KeyCode.W;
                WKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                WKey.GetComponent<NoteObject>().buttonController = WKeyController;
                WKey.GetComponent<NoteObject>().fadeInDuration = beatTempo;
                activeNotes.Add(WKey);
                break;
            case 2:
                GameObject EKey = Instantiate(keyPrefab, EKeyPosition.position, Quaternion.identity);
                EKey.GetComponent<NoteObject>().keyToPress = KeyCode.E;
                EKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                EKey.GetComponent<NoteObject>().buttonController = EKeyController;
                EKey.GetComponent<NoteObject>().fadeInDuration = beatTempo;
                activeNotes.Add(EKey);
                break;
        }
    }

    private void CreateKeyRightPlayer(int randomKey)
    {
        switch (randomKey)
        {
            case 0:
                GameObject ArrowLeftKey = Instantiate(keyPrefab, QKeyPosition.position, Quaternion.identity);
                ArrowLeftKey.GetComponent<NoteObject>().keyToPress = KeyCode.LeftArrow;
                ArrowLeftKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                ArrowLeftKey.GetComponent<NoteObject>().buttonController = QKeyController;
                ArrowLeftKey.GetComponent<NoteObject>().fadeInDuration = beatTempo;
                activeNotes.Add(ArrowLeftKey);
                break;
            case 1:
                GameObject ArrowDownKey = Instantiate(keyPrefab, WKeyPosition.position, Quaternion.identity);
                ArrowDownKey.GetComponent<NoteObject>().keyToPress = KeyCode.DownArrow;
                ArrowDownKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                ArrowDownKey.GetComponent<NoteObject>().buttonController = WKeyController;
                ArrowDownKey.GetComponent<NoteObject>().fadeInDuration = beatTempo;
                activeNotes.Add(ArrowDownKey);
                break;
            case 2:
                GameObject ArrowRightKey = Instantiate(keyPrefab, EKeyPosition.position, Quaternion.identity);
                ArrowRightKey.GetComponent<NoteObject>().keyToPress = KeyCode.RightArrow;
                ArrowRightKey.GetComponent<NoteObject>().beatTempo = beatTempo;
                ArrowRightKey.GetComponent<NoteObject>().buttonController = EKeyController;
                ArrowRightKey.GetComponent<NoteObject>().fadeInDuration = beatTempo;
                activeNotes.Add(ArrowRightKey);
                break;
        }
    }

    public void ClearNotes()
    {
        StopCoroutine(spawnKeyCoroutine);
        foreach (GameObject note in activeNotes)
        {
            if (note != null)
            {
                Destroy(note);
            }
        }
        activeNotes.Clear();
    }
}