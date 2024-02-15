using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public DialogueTrigger trigger;
    private bool isTalking = false;
    public GameObject doorIndicator;

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.E))
        {
            trigger.StartDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTalking = true;
            doorIndicator.SetActive(true);
        }
            
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isTalking = false;
            doorIndicator.SetActive(false);
        }
            
    }
}
