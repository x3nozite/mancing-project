using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartFishing : MonoBehaviour
{
    public Slider forceUI;
    private float sliderDirection;
    public float force;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        castingRod();
    }

    void castingRod()
    {
        if (force > 1f) sliderDirection = -1f;
        else if (force < 0f) sliderDirection = 1f;
        force += 2f * Time.deltaTime * sliderDirection;
        slider();


        if (Input.GetKey(KeyCode.Space))
        {
            castRod();
            StartCoroutine(Wait());
            sliderDirection = 0f;
        }
    }

    void castRod()
    {
        
    }

    void slider()
    {
        forceUI.value = force;
    }

    void resetGauge()
    {
        force = 0;
        forceUI.value = 0;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        resetGauge();
    }
}
