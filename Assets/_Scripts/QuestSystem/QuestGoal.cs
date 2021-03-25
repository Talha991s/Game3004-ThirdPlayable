using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goaltype;

    public int CollectedGold;
    public int TotalGold;

    public bool IsReached()
    {
        return (CollectedGold >= TotalGold);
    }


}

public enum GoalType
{
    Kill,
    Gathering
}
