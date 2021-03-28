using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal 
{
    public GoalType goaltype;

    public int CollectedGold;
    

    public bool IsReached()
    {
        return (CollectedGold >= 10);
    }

}

public enum GoalType
{
    Kill,
    Gathering
}
