using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gold : MonoBehaviour
{
    //public static
    private QuestGoal CollectedQuestGold;
    // public QuestGoal totalQuestGoal;
    [SerializeField] private TMP_Text goldtext;
   // [SerializeField] private TMP_Text Totalgoldtext;

    public Quest quest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectGoal();
        }
    }

    public void CollectGoal()
    {
        if (quest.IsActive)
        {
            var goldCollected = CollectedQuestGold.CollectedGold;
            goldCollected++;
            goldtext.text = goldCollected.ToString();
            FindObjectOfType<SoundManager>().Play("collect");
            Destroy(gameObject);

            if (quest.goal.IsReached())
            {
                quest.Complete();
            }
        }
    }

    public void OnAcceptPressed()
    {
        //FindObjectOfType<SoundManager>().Play("click");
        // QuestScreen.SetActive(false);
        quest.IsActive = true;
        //quuuest.quest.IsActive = true;
        //player.quest = quest;
    }
}
