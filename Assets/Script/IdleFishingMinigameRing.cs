using System.Collections.Generic;
using UnityEngine.UI;              
using UnityEngine;
using System;

public class IdleFishingMinigameRing : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private List<Sprite> noteSprites = new List<Sprite>();
    private KeyCode noteType;  

    private float judgement = 1f;

    private Image notesImage;

    void Start()
    {
        notesImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        judgement -= speed * Time.deltaTime;
    
        int getIndex = Mathf.Max(0, (int)(judgement * noteSprites.Count));
        notesImage.sprite = noteSprites[getIndex];
    }

    public void SetAttributes(KeyCode types)
    {
        noteType = types;
    }

    public KeyCode GetNoteType()
    {
        return noteType;
    }

    public float GetJudgement()
    {
        return judgement;
    }

    public void Popped()
    {
        Destroy(gameObject);
    }
}
