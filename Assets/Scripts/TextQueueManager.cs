using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TextQueueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float displayTime = 2.0f; // Time each text message is displayed
    public List<string> textQueue = new List<string>(); // Queue of text messages

    private IEnumerator currentDisplayCoroutine;

    private void Start()
    {
        if (textDisplay == null)
        {
            Debug.LogError("TextMeshProUGUI component not assigned!");
        }
        else
        {
            currentDisplayCoroutine = DisplayTextQueue();
            StartCoroutine(currentDisplayCoroutine);
        }
    }

    private IEnumerator DisplayTextQueue()
    {
        while (textQueue.Count > 0)
        {
            string message = textQueue[0];
            textQueue.RemoveAt(0);

            textDisplay.text = message;

            yield return new WaitForSeconds(displayTime);
        }
    }

    public void QueueText(string message)
    {
        textQueue.Add(message);

        // If there is no coroutine running, start displaying the text queue
        if (currentDisplayCoroutine == null)
        {
            currentDisplayCoroutine = DisplayTextQueue();
            StartCoroutine(currentDisplayCoroutine);
        }
    }
}
