using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float waitAfterDying = 2f;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void PlayerDied()
    {
        StartCoroutine(PlayerDiedCoroutine());
    }

    public IEnumerator PlayerDiedCoroutine()
    {
        yield return new WaitForSeconds(waitAfterDying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
