using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    public bool isVisible;
    // Used as an offset
    public float initialBlinkTime;
    // Used as speed for blinking after offset
    public float finalBlinkTime;

    Vector3 startingScale;

    // Start is called before the first frame update
    void Start()
    {
        startingScale = transform.localScale;
        StartCoroutine(Countdown());
    }

    // Update is called once per frame
    void Update()
    {
        Blink();
    }

    IEnumerator Countdown()
    {
        while(true)
        {
            yield return new WaitForSeconds(initialBlinkTime);
            initialBlinkTime = finalBlinkTime;
            isVisible = !isVisible;
        }
    }

    void Blink()
    {
        transform.localScale = isVisible ? startingScale : new Vector3(0, 0, 0);
    }
}
