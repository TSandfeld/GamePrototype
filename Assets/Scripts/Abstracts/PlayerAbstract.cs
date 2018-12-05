using UnityEngine;
using System.Collections;

public abstract class PlayerAbstract : MonoBehaviour
{
    static int playerScore = 0;

    int totalRecipes = 22;
    static int lvl1Recipes = 8;
    static int lvl2Recipes = 8;
    static int lvl3Recipes = 1;

    static int lvl2RequiredRecipes;
    static int lvl3RequiredRecipes;

    protected float initHP = 100;
    protected static float HP = 100;

    protected bool isNPCPresent = false;
    protected Dialogue NPCDialogue;

    protected GameObject pickUps;
    protected static bool[] collected = new bool[lvl1Recipes];

	// Use this for initialization
    protected virtual void Start()
	{
        
    }

    protected void SetLvl2RequiredRecipes(int count) 
    {
        lvl2RequiredRecipes = count;
    }
    protected int GetLvl2RequiredRecipes()
    {
        return lvl2RequiredRecipes;
    }

    protected void SetLvl3RequiredRecipes(int count)
    {
        lvl3RequiredRecipes = count;
    }

    protected int GetLvl3RequiredRecipes()
    {
        return lvl3RequiredRecipes;
    }

    protected int GetPlayerScore() {
        return playerScore;
    }

    protected bool[] GetCollected() {
        return collected;
    }

    protected virtual void TakeDamage(int damage) 
    {
        HP -= damage;
    }

    protected int GetLvl1RecipesCount() 
    {
        return lvl1Recipes;
    }

    protected int GetLvl2RecipesCount()
    {
        return lvl2Recipes;
    }

    protected int GetLvl3RecipesCount()
    {
        return lvl3Recipes;
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
