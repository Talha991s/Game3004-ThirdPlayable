using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    [SerializeField] private GameObject QuestPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAcceptPressed()
    {
        FindObjectOfType<SoundManager>().Play("click");
        QuestPanel.SetActive(false);
    }
}
