/*  Author: Joseph Malibiran
 *  Date Created: March 11, 2021
 *  Last Updated: March 11, 2021
 *  Description: 
 */

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameMenuScr : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string savefileName = "Hamstronaut";       //This is the name of the save file. An indexing number will be appended to this name. This is different from the save file header seen in-game.
    private string gameVersion = "0.3";

    //Loads save file data at given save slot index
    private bool LoadSaveFile(int _saveSlotIndex) 
    {
        //This game will have a maximum 8 save slots hardcoded.
        if (_saveSlotIndex < 0 || _saveSlotIndex > 8) 
        { 
            Debug.LogError("[Error] Invalid save slot index! Slot number must be between from 0 to 8.");
            return false;
        }

        if (!File.Exists(Application.persistentDataPath + "/" + savefileName + _saveSlotIndex + ".hamsave")) 
        {
            Debug.LogError("[Error] File does not exist; Cannot load a save file that does not exist.");
            return false;
        }

        SaveData readSaveData = SaveFileReaderWriter.ReadFromSaveFile(Application.persistentDataPath + "/" + savefileName + _saveSlotIndex + ".hamsave");

        if (this.gameVersion != readSaveData.gameVersion) 
        {
            Debug.LogWarning("[Warning] Cannot load save file; incompatible version. ");
            return false;
        }

        LoadedSaveFile.loadedSaveData = readSaveData;

        return true;
    }

    public void LoadGameFromSave(int _saveSlotIndex) 
    {

        //Load Save File
        if (!LoadSaveFile(_saveSlotIndex)) 
        {
            return;
        }

        //A Game scene will load a game state from save file on Awake() if this value is set to true
        LoadedSaveFile.loadLevelBasedOnSaveFile = true;

        //Load appropriate scene
        switch (LoadedSaveFile.loadedSaveData.currentLevel) 
        {
            case 0:
                //Don't load anything, but stats are loaded anyway because they're on LoadedSaveFile.loadedSaveData
                break;
            case 1:
                SceneManager.LoadScene(1);
                break;
            default:
                //Don't load anything, but stats are loaded anyway because they're on LoadedSaveFile.loadedSaveData
                break;
        }

    }


}
