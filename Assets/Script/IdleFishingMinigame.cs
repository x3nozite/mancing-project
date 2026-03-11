using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleFishingMinigame : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject judgmentWindowCover;
    [SerializeField] private Transform judgmentWindow;
    [SerializeField] private Transform notesSpawnLocation;
    [SerializeField] private Transform mainBorder;
    public List<GameObject> availableNotes;

    [Header ("Player Settings")]
    [SerializeField] private float scrollSpeed = 0.5f;

    [Header ("Judgement Setting")]
    [SerializeField] private int minNotes;
    [SerializeField] private int maxNotes;
    [SerializeField] private float gapBetween;
    private float currentTime = 0f;

    private Queue<GameObject> currentNotes = new Queue<GameObject>();

    void Update()
    {
        HandleInputs(); 

        if(currentNotes.Count == 0)
        {
            HandleNoteSpawns();
        }

        // Debug
        // if(currentNotes.Count > 0)
        // {
        //     float notesPosition = currentNotes.Peek().GetComponent<Notes>().notesTransform.position.x;
        //     float deltaPosition = Math.Abs(notesPosition - judgmentWindow.position.x);
        //
        //     Debug.Log(deltaPosition);
        // }
    }


    void HandleNoteSpawns()
    {
        int toSpawn = UnityEngine.Random.Range(minNotes, maxNotes);

        while(currentNotes.Count < toSpawn)
        {
            int chosenNotes = UnityEngine.Random.Range(0, availableNotes.Count);
            Vector2 spawnLocation = new Vector2(notesSpawnLocation.position.x + (gapBetween * currentNotes.Count), notesSpawnLocation.position.y);
            GameObject notesToSpawn = Instantiate(availableNotes[chosenNotes], spawnLocation, Quaternion.identity, mainBorder);

            currentNotes.Enqueue(notesToSpawn);
        }
    }

    void PopNotes()
    {
        GameObject buffer = currentNotes.Peek();
        currentNotes.Dequeue();
        Destroy(buffer);
    }

    void HandleJudgment(KeyCode buttonPressed)
    {
        if(currentNotes.Count == 0) return;

        KeyCode currentTopType = currentNotes.Peek().GetComponent<Notes>().notesType;
        if(currentTopType == buttonPressed)
        {
            Debug.Log("Hit");
            PopNotes();
        }else
        {
            Debug.Log("Miss");
            PopNotes();
        }
    }

    void HandleInputs()
    {
        if(currentNotes.Count > 0)
        {
            // for handling inputs
            if(Input.GetKeyDown(KeyCode.UpArrow)) HandleJudgment(KeyCode.UpArrow);  
            if(Input.GetKeyDown(KeyCode.LeftArrow)) HandleJudgment(KeyCode.LeftArrow);  
            if(Input.GetKeyDown(KeyCode.DownArrow)) HandleJudgment(KeyCode.DownArrow);  
            if(Input.GetKeyDown(KeyCode.RightArrow)) HandleJudgment(KeyCode.RightArrow);  
        }

        bool hasInput = false;

        // for animation
        if(Input.GetKey(KeyCode.UpArrow)) hasInput = true;  
        if(Input.GetKey(KeyCode.LeftArrow)) hasInput = true;  
        if(Input.GetKey(KeyCode.DownArrow)) hasInput = true;  
        if(Input.GetKey(KeyCode.RightArrow)) hasInput = true;  

        if(hasInput) judgmentWindowCover.SetActive(true);
        else judgmentWindowCover.SetActive(false);

    }
}
