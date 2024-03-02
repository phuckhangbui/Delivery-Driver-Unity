using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class SaveData
    {
        public int index = 1;
        [SerializeField] private float myFloat = 5.7f;

        public bool ourBool = true;
        public Vector2 ourVector = new Vector2(0, 0);

    }
}