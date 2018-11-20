using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void EndDialogWhenLeaving() 
    {
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("NPC is present!");
            var player = collision.gameObject;

            player.SendMessage("SetNPCPresence", true);
            player.SendMessage("SetNPCDialogue", dialogue);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("NPC is gone ...");
            var player = collision.gameObject;

            EndDialogWhenLeaving();
            player.SendMessage("SetNPCPresence", false);
        }
    }
}
