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
    [SerializeField] private float perfectJudgment;
    [SerializeField] private float greatJudgment;
    [SerializeField] private float badJudgment;
    [SerializeField] private float missJudgment;
    [SerializeField] private float minTimeBetweenNotes;
    [SerializeField] private float maxTimeBetweenNotes;
    private float currentTime = 0f;

    private Queue<GameObject> currentNotes = new Queue<GameObject>();

    void Update()
    {
        HandleInputs(); 
        HandleNoteSpawns();

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
        currentTime += Time.deltaTime;
        float randomLimit = UnityEngine.Random.Range(minTimeBetweenNotes, maxTimeBetweenNotes);
        if(randomLimit > currentTime) return;

        // spawn Notes
        currentTime = 0;
        int chosenNotes = UnityEngine.Random.Range(0, availableNotes.Count);
        GameObject notesToSpawn = Instantiate(availableNotes[chosenNotes], notesSpawnLocation.position, Quaternion.identity, mainBorder);

        notesToSpawn.GetComponent<Notes>().SetSpeed(scrollSpeed);
        currentNotes.Enqueue(notesToSpawn);
    }

    void PopNotes()
    {
        GameObject buffer = currentNotes.Peek();
        currentNotes.Dequeue();
        Destroy(buffer);
    }

    void HandleJudgment(KeyCode buttonPressed, float deltaPosition)
    {
        if(currentNotes.Count == 0) return;

        KeyCode currentTopType = currentNotes.Peek().GetComponent<Notes>().notesType;
        if(currentTopType == buttonPressed)
        {
            // Debug.Log(currentTopType);
            if(deltaPosition <= perfectJudgment)
            {
                Debug.Log("Perfect");
                PopNotes();
            }else if(deltaPosition <= greatJudgment)
            {
                Debug.Log("Great");
                PopNotes();
            }else if(deltaPosition <= badJudgment)
            {
                Debug.Log("Bad");
                PopNotes();
            }
        }else if(deltaPosition <= missJudgment)
        {
            Debug.Log("Miss");
            PopNotes();
        }
    }

    void HandleInputs()
    {
        if(currentNotes.Count > 0)
        {
            float notesPosition = currentNotes.Peek().GetComponent<Notes>().notesTransform.position.x;
            float deltaPosition = Math.Abs(notesPosition - judgmentWindow.position.x);

            if(deltaPosition > greatJudgment && notesPosition > judgmentWindow.position.x)
            {
                PopNotes();
            }

            // for handling inputs
            if(Input.GetKeyDown(KeyCode.UpArrow)) HandleJudgment(KeyCode.UpArrow, deltaPosition);  
            if(Input.GetKeyDown(KeyCode.LeftArrow)) HandleJudgment(KeyCode.LeftArrow, deltaPosition);  
            if(Input.GetKeyDown(KeyCode.DownArrow)) HandleJudgment(KeyCode.DownArrow, deltaPosition);  
            if(Input.GetKeyDown(KeyCode.RightArrow)) HandleJudgment(KeyCode.RightArrow, deltaPosition);  
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
