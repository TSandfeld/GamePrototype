using UnityEngine;
using System.Collections;

public abstract class PlayerAbstract : MonoBehaviour
{
    static int playerScore = 0;

    int totalRecipes = 50;

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

    protected void CollectItem() 
    {
        playerScore++;
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
