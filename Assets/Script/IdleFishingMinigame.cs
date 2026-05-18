using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleFishingMinigame : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private List<GameObject> notesSpawnLocation;
    [SerializeField] private GameObject notes;

    [Header ("Notes Setting")]
    [SerializeField] private float minNoteSpawnTime = 0.5f;
    [SerializeField] private float maxNoteSpawnTime = 1f;
    [SerializeField] private float hitJudgement = 0.15f; 

    private Queue<IdleFishingMinigameRing> currentNotes = new Queue<IdleFishingMinigameRing>();

    public FishingRodScript fishingRod;
    
    void Update()
    {
        HandleInputs(); 
        HandleNoteSpawns();
        Pop(false);

        // Debug
        // if(currentNotes.Count > 0)
        // {
        //     float notesPosition = currentNotes.Peek().GetComponent<Notes>().notesTransform.position.x;
        //     float deltaPosition = Math.Abs(notesPosition - judgmentWindow.position.x);
        //
        //     Debug.Log(deltaPosition);
        // }
    }

    private IdleFishingMinigameRing top = null;
    void Pop(bool forcePop)
    {
        if(currentNotes.Count == 0) return;

        if(top == null) top = currentNotes.Peek().GetComponent<IdleFishingMinigameRing>();
        if(!forcePop)
        {
            if(top.GetJudgement() < -hitJudgement)
            {
                currentNotes.Dequeue();
                top.Popped();
            }
            top = null;
        }else
        {
            currentNotes.Dequeue();
            top.Popped();
            top = null;
        }

        if(top == null && currentNotes.Count > 0) top = currentNotes.Peek().GetComponent<IdleFishingMinigameRing>();
    }

    private float timeToSpawn = 0f;
    void HandleNoteSpawns()
    {
        timeToSpawn -= Time.deltaTime;
        if(timeToSpawn > 0f) return;
        timeToSpawn = UnityEngine.Random.Range(minNoteSpawnTime, maxNoteSpawnTime);
        int typesRandom = UnityEngine.Random.Range(0, 2);

        GameObject notesToSpawn = Instantiate(notes, notesSpawnLocation[typesRandom].transform);
        if(typesRandom == 0) notesToSpawn.GetComponent<IdleFishingMinigameRing>().SetAttributes(KeyCode.Mouse0);
        else notesToSpawn.GetComponent<IdleFishingMinigameRing>().SetAttributes(KeyCode.Mouse1);

        currentNotes.Enqueue(notesToSpawn.GetComponent<IdleFishingMinigameRing>());
    }

    void HandleJudgment(KeyCode buttonPressed)
    {
        if(currentNotes.Count == 0) return;

        KeyCode currentTopType = currentNotes.Peek().GetComponent<IdleFishingMinigameRing>().GetNoteType();
        if(currentTopType == buttonPressed)
        {
            if(top != null && top.GetJudgement() >= -hitJudgement && top.GetJudgement() <= hitJudgement)
            {
                Debug.Log("hit");
                fishingRod.duration -= 0.2f;
            }
            Pop(true);
        }
    }

    void HandleInputs()
    {
        if(currentNotes.Count > 0)
        {
            // for handling inputs
            if(Input.GetKeyDown(KeyCode.Mouse0)) HandleJudgment(KeyCode.Mouse0);  
            if(Input.GetKeyDown(KeyCode.Mouse1)) HandleJudgment(KeyCode.Mouse1);  
        }
    }
}
