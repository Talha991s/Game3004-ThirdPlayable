/*  Author: Joseph Malibiran
 *  Date Created: March 13, 2021
 *  Last Updated: March 13, 2021
 *  Description: 
 */

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveGameScr : MonoBehaviour{

    [Header("References")]
    [SerializeField] private Transform playerCharacterRef;
    [SerializeField] private PlayerHealth playerHealthRef;
    [SerializeField] private PlayerInventory inventoryRef;
    [SerializeField] private Text[] saveSlots = new Text[4];

    [Header("Settings")]
    [SerializeField] private string savefileName = "Hamstronaut";       //This is the name of the save file. An indexing number will be appended to this name. This is different from the save file header seen in-game.
    private string[] saveFileDisplayHeaders;                            //This game will have a maximum 4 save slots hardcoded.
    private string gameVersion = "0.3";

    private void Awake() 
    {
        if (LoadedSaveFile.loadLevelBasedOnSaveFile == true) 
        {
            LoadedSaveFile.loadLevelBasedOnSaveFile = false;
            LoadGameFromSelectedSaveFile();
        }
        else {
            if (playerHealthRef) {
                playerHealthRef.currentHealth = playerHealthRef.maxhealth;
            }
        }
    }

    private void Start() 
    {
        SetUpSaveSlotHeaders();
    }

    private string GetSaveSlotHeader(int _saveSlotIndex) 
    {
        if (_saveSlotIndex < 1 || _saveSlotIndex > 4) 
        { //This game will have a maximum 4 save slots hardcoded.
            Debug.LogError("[Error] Invalid save slot index! Slot number must be between from 1 to 4.");
            return "[Error] Invalid Save slot index!";
        }

        if (saveFileDisplayHeaders == null) 
        {
            saveFileDisplayHeaders = SaveFileReaderWriter.CheckAvailableSaveFiles(Application.persistentDataPath, savefileName);
        }

        if (saveFileDisplayHeaders != null) 
        {
            if (saveFileDisplayHeaders.Length <= 0) 
            {
                Debug.LogError("[Error] availableSaveFiles array not initialized!");
                return "[Error] availableSaveFiles array not initialized!";
            }
        }
        else 
        {
            Debug.LogError("[Error] availableSaveFiles array not initialized!");
            return "[Error] availableSaveFiles array not initialized!";
        }

        return saveFileDisplayHeaders[_saveSlotIndex - 1];
    }

    private void SetUpSaveSlotHeaders() 
    {
        saveSlots[0].text = GetSaveSlotHeader(1);
        saveSlots[1].text = GetSaveSlotHeader(2);
        saveSlots[2].text = GetSaveSlotHeader(3);
        saveSlots[3].text = GetSaveSlotHeader(4);
    }

    private void LoadGameFromSelectedSaveFile() 
    {

        //Check Loaded Save File
        if (LoadedSaveFile.loadedSaveData == null)
        {
            Debug.LogError("[Error] Could not load save file.");
            return;
        }

        //Check if current scene matches save file level
        if (LoadedSaveFile.loadedSaveData.currentLevel == 1) {
            if (SceneManager.GetActiveScene().buildIndex != 1) {
                Debug.LogError("[Error] Save file level data mismatch.");
                return;
            }
        }

        //Set up level if applicable
        StartCoroutine(LoadGameStateRoutine());

    }

    //Saves game data at given save slot index
    public void SaveGame(int _saveSlotIndex) 
    {
        if (_saveSlotIndex <= 0 || _saveSlotIndex > 4) { //This game will have a maximum 4 (1 to 4) save slots hardcoded. 
            Debug.LogError("[Error] Invalid save slot index! Slot number must be between from 1 to 4.");
            return;
        }

        SaveData newSaveData = new SaveData();
        newSaveData.gameVersion = this.gameVersion;

        //Save Player location
        if (playerCharacterRef) {
            newSaveData.playerCoord = new TransformLite(playerCharacterRef.position.x, playerCharacterRef.position.y, playerCharacterRef.position.z, playerCharacterRef.eulerAngles.x, playerCharacterRef.eulerAngles.y, playerCharacterRef.eulerAngles.z);
        }
        else {
            Debug.LogError("[Error] Reference to player character is missing!");
        }

        //Save Player Health
        if (playerHealthRef) {
            newSaveData.healthAmount = playerHealthRef.currentHealth;
        }
        else {
            Debug.LogError("[Error] Reference to player health is missing!");
        }

        //Save Collected Seed amount
        if (inventoryRef) {
            newSaveData.seedsCollected = inventoryRef.GetPlayerSeedAmount();
        }
        else {
            Debug.LogError("[Error] Reference to inventory is missing!");
        }

        //TEMP settings
        
        newSaveData.livesAmount = 3;
        newSaveData.ammoAmount = 100;
        newSaveData.aliensKilled = 0;
        newSaveData.currentLevel = 1; //0 means not in a level
        newSaveData.levelsUnlocked = 1;
        newSaveData.savefileHeader = "[Marco] Health: " + newSaveData.healthAmount + "; Seeds: " + newSaveData.seedsCollected + "; Levels Unlocked: " + newSaveData.levelsUnlocked;

        SaveFileReaderWriter.WriteToSaveFile(Application.persistentDataPath + "/" + savefileName + _saveSlotIndex + ".hamsave", newSaveData);

        //Update save slot button header
        saveSlots[_saveSlotIndex-1].text  = newSaveData.savefileHeader;

        Debug.Log("[Notice] Game Saved.");
    }

    IEnumerator LoadGameStateRoutine() 
    {
        Time.timeScale = 0;

        //Set up player character transform
        if (playerCharacterRef) {
            playerCharacterRef.position = new Vector3(LoadedSaveFile.loadedSaveData.playerCoord.positionX, LoadedSaveFile.loadedSaveData.playerCoord.positionY, LoadedSaveFile.loadedSaveData.playerCoord.positionZ);
            playerCharacterRef.eulerAngles = new Vector3(LoadedSaveFile.loadedSaveData.playerCoord.orientationX, LoadedSaveFile.loadedSaveData.playerCoord.orientationY, LoadedSaveFile.loadedSaveData.playerCoord.orientationZ);
        }
        else {
            Debug.LogError("[Error] Reference to player character missing.");
        }

        //Set player health
        if (playerHealthRef) {
            playerHealthRef.SetHealth(LoadedSaveFile.loadedSaveData.healthAmount);
        }
        else {
            Debug.LogError("[Error] Reference to player health is missing!");
        }

        //Set seed collected amount and inventory (TODO)
        if (inventoryRef) {
            inventoryRef.SetPlayerSeedAmount(LoadedSaveFile.loadedSaveData.seedsCollected);
        }
        else {
            Debug.LogError("[Error] Reference to inventory is missing!");
        }

        yield return new WaitForSeconds(3.0f);
        Time.timeScale = 1;
    }
}
