using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool isDoor = false;
    public GameObject doorIndicator;
    [SerializeField] private Vector2 doorLocation;
    public Transform playerTransform;


    void Update()
    {
        if (isDoor && Input.GetKeyDown(KeyCode.E))
        {
           playerTransform.position = doorLocation;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDoor = true;
            doorIndicator.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isDoor = false;
            doorIndicator.SetActive(false);
        }
    }
}
