using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public static bool IsQuestWindowOpen;
   public PlayerHealth player;
    //public GameObject player;
    public GameObject QuestScreen;
    public GameObject QuestTrigger;

    [Header("Text")]
    public TMP_Text TitleText;
    public TMP_Text descriptionText;
    // public TMP_Text coinText;
    //private Gold quuuest;
    //public QuestGoal CollectedQuestGold;
    //// public QuestGoal totalQuestGoal;
    //[SerializeField] private TMP_Text goldtext;

    public void QuestWindowOpen()
    {
        IsQuestWindowOpen = true;
        QuestScreen.SetActive(true);
        TitleText.text = quest.title;
        descriptionText.text = quest.Description;
       // coinText.text = quest.GoldenCoinCollected.ToString();
    }

    public void OnAcceptPressed()
    {
        FindObjectOfType<SoundManager>().Play("click");
        QuestScreen.SetActive(false);
        quest.IsActive = true;
        //quuuest.quest.IsActive = true;
        player.quest = quest;
    }

    //public void CollectGoal()
    //{
    //    if (quest.IsActive)
    //    {
    //        var goldCollected = CollectedQuestGold.CollectedGold;
    //        goldCollected++;
    //        goldtext.text = goldCollected.ToString();
    //        FindObjectOfType<SoundManager>().Play("collect");
    //       // Destroy(gameObject);

    //        if (goldCollected >=10)
    //        {
    //            quest.Complete();
    //            quest.IsActive = false;
    //            Debug.Log("Done");
    //        }
    //    }
    //}
}
