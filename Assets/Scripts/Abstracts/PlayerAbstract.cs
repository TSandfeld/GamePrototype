using UnityEngine;
using System.Collections;

public abstract class PlayerAbstract : MonoBehaviour
{
    static int playerScore = 0;

    int totalRecipes = 22;

    protected float initHP = 100;
    protected static float HP = 100;

    protected bool isNPCPresent = false;
    protected Dialogue NPCDialogue;

	// Use this for initialization
    protected virtual void Start()
	{
        
    }

    protected virtual void TakeDamage(int damage) 
    {
        HP -= damage;
    }

    protected void ResetHP() 
    {
        HP = 100;
    }

    protected void CollectItem() 
    {
        playerScore++;

        if (playerScore.Equals(totalRecipes))
        {
            // Win condition met
            new ChangeScene().SwitchScenes("Win Scene");
        }
    }


    protected string GetScoreText() 
    {
        return playerScore.ToString() + "/" + totalRecipes.ToString();
    }

    protected float GetHP() 
    {
        return HP;
    }
}
