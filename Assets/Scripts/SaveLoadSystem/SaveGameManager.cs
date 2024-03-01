using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    public static class SaveGameManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string Directory = "/SaveData/";
        public const string FileName = "SaveGame.sav";

        public static bool Save()
        {
            var dir = Application.persistentDataPath + Directory;

            return true;
        }
    }
}
