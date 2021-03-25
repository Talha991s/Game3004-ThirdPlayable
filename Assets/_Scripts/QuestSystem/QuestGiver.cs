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
    public TMP_Text coinText;

    public void QuestWindowOpen()
    {
        IsQuestWindowOpen = true;
        QuestScreen.SetActive(true);
        TitleText.text = quest.title;
        descriptionText.text = quest.Description;
        coinText.text = quest.GoldenCoinCollected.ToString();
    }

    public void OnAcceptPressed()
    {
        FindObjectOfType<SoundManager>().Play("click");
        QuestScreen.SetActive(false);
        quest.IsActive = true;
        player.quest = quest;
    }

}
