using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryKeyLock : MonoBehaviour
{
    Memory memory;

    private void Start()
    {
        memory = Camera.main.GetComponent<Memory>();
    }
    public void LockKeys()
    {
        memory.key_lock = true;
    }
}
