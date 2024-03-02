using System.Collections;
using System.Collections.Generic;
using SaveLoadSystem;
using UnityEngine;

public class SaveGameRunner : MonoBehaviour
{
    public void SaveGame()
    {
        SaveGameManager.CurrentSaveData.index = 10;
        SaveGameManager.SaveGame();
    }

    public void LoadGame()
    {
        SaveGameManager.CurrentSaveData.index = 20;
        SaveGameManager.LoadGame();
        Debug.Log(SaveGameManager.CurrentSaveData.index);
    }
}
