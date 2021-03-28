using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool IsActive;

    public string title;
    public string Description;
    public int GoldenCoinCollected;

    public QuestGoal goal;

    public void Complete()
    {
        IsActive = false;
        Debug.Log(title + " was completed! ");
    }
}
