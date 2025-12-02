using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryKeyLock : MonoBehaviour {
    Memory memory;
    Block block;
    private void Start() {
        memory = Camera.main.GetComponent<Memory>();
        block = GetComponentInParent<Block>();
    }
    public void LockKeys() {
        memory.isKeyLocked = true;
        block.isMoveLocked = true;
    }
}
