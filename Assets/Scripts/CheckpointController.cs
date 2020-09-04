using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public string checkpointName;

    public void Start()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_checkpoint"))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + "_checkpoint") == checkpointName)
            {
                PlayerController.instance.transform.position = transform.position;
                Debug.Log("Player starting at " + checkpointName);
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_checkpoint", null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + "_checkpoint", checkpointName);
            Debug.Log("Player hit " + checkpointName);
        }
    }
}
