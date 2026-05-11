using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Reeling : MonoBehaviour
{
    [SerializeField] private Image greenLeftImage;
    [SerializeField] private Image greenRightImage;
    [SerializeField] private RectTransform arrowPivot;
    [SerializeField] [Range(0f, 1f)] private float greenLeftRange;
    [SerializeField] [Range(0f, 1f)] private float greenRightRange;   

    [SerializeField] private float progressGainAndLoss = 2; 
    private float fishProgress = 50;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fishProgress);
        greenLeftImage.fillAmount = greenLeftRange;
        greenRightImage.fillAmount = greenRightRange;
    }

    void FixedUpdate()
    {
        MoveArrow();
        HandleJudgement();
    }

    void HandleJudgement()
    {
        float currentZ = arrowPivot.localEulerAngles.z;
        if (currentZ > 180) currentZ -= 360;

        float arrowValue = Mathf.InverseLerp(90f, -90f, currentZ);

        if(arrowValue >= greenLeftRange && arrowValue <= greenRightRange)
        {
            fishProgress += Time.deltaTime * progressGainAndLoss;
        }
        else
        {
            fishProgress -= Time.deltaTime * progressGainAndLoss;
        }

        Mathf.Clamp(fishProgress, 0f, 100f);
    }

    [SerializeField] private float arrowSensitifity = 5f;
    [SerializeField] private float arrowSpeedLimit = 0;
    private float arrowAccel = 0;
    void MoveArrow()
    {
        // 1. Calculate Acceleration
        if(Input.GetKey(KeyCode.Mouse0))
        {
            arrowAccel -= arrowSensitifity;
        }
        else
        {
            arrowAccel += arrowSensitifity;
        }

        arrowAccel = Mathf.Clamp(arrowAccel, -arrowSpeedLimit, arrowSpeedLimit);
        float currentZ = arrowPivot.localEulerAngles.z;
        if(currentZ > 180) currentZ -= 360;

        float nextZ = currentZ + arrowAccel;
        nextZ = Mathf.Clamp(nextZ, -90f, 90f);
        arrowPivot.localEulerAngles = new Vector3(0, 0, nextZ);
    }
}
