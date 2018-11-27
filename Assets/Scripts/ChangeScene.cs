using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    [SerializeField]
    private string newLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Is fired");
        if(collision.CompareTag("Player"))
        {
            SwitchScenes(newLevel);
        }
    }

    public void SwitchScenes(string sceneName) {
        SceneManager.LoadScene(newLevel);
    }
}
