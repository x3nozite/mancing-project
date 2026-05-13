using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleFishingMinigame : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private RectTransform notesSpawnLocation;
    public List<GameObject> availableNotes;

    [Header ("Judgement Setting")]
    [SerializeField] private int minNotes;
    [SerializeField] private int maxNotes;

    private Queue<GameObject> currentNotes = new Queue<GameObject>();

    public FishingRodScript fishingRod;
    
    private float timeoutMiss = 0f;
    void Update()
    {
        if(timeoutMiss > 0) timeoutMiss -= Time.deltaTime;

        HandleInputs(); 
        if(currentNotes.Count == 0 && timeoutMiss <= 0)
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
        int toSpawn = UnityEngine.Random.Range(minNotes, maxNotes + 1);

        while(currentNotes.Count < toSpawn)
        {
            int chosenNotes = UnityEngine.Random.Range(0, availableNotes.Count);
            GameObject notesToSpawn = Instantiate(availableNotes[chosenNotes], notesSpawnLocation);
            notesToSpawn.transform.localPosition = Vector3.zero;
            notesToSpawn.transform.localScale = Vector3.one;

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
            // TODO adding what happened on hit
            Debug.Log("Hit");
            PopNotes();
            fishingRod.duration -= 0.2f;
        }else
        {
            Debug.Log("Miss");
            // Reset on miss
            while(currentNotes.Count > 0) PopNotes();
            timeoutMiss = 1f;
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
    }
}
