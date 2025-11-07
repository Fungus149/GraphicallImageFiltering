using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryKeyUnlock : MonoBehaviour
{
    Memory memory;

    private void Start()
    {
        memory = Camera.main.GetComponent<Memory>();
    }
    public void UnlockKeys()
    {
        memory.key_lock = false;
    }
}
