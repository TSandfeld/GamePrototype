using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    [SerializeField]
    private string newLevel;

    private string curLevel;

    private bool lvl2Done;
    private bool lvl3Done;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Is fired");
        if(collision.CompareTag("Player"))
        {

            var player = collision.gameObject;
            print("Newlvl : " + newLevel);
            print("Curlvl : " + curLevel);
            if (newLevel.Equals("Level 1"))
            {
                if (SceneManager.GetActiveScene().name.Equals("Level 2 - Platform"))
                {
                    player.SendMessage("GetCurrentPlayerScoreLvl2", gameObject);
                    if (lvl2Done) 
                    {                 
                        SwitchScenes(newLevel);
                    }
                } 
                else if (SceneManager.GetActiveScene().name.Equals("Level 3 - platform")) 
                {
                    player.SendMessage("GetCurrentPlayerScoreLvl3", gameObject);
                    if (lvl3Done) 
                    {                  
                        SwitchScenes(newLevel);
                    }
                }
            }
            else if (newLevel.Equals("Level 2 - Platform"))
            {
                player.SendMessage("SetRequiredLvl2Count");
                SwitchScenes(newLevel);
            } 
            else if (newLevel.Equals("Level 3 - platform"))
            {
                player.SendMessage("SetRequiredLvl3Count");
                SwitchScenes(newLevel);
            }
                
        }
    }

    public void SetLvl2Done() 
    {
        lvl2Done = true;
    }

    public void SetLvl3Done()
    {
        lvl3Done = true;
    }

    public void SwitchScenes(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
