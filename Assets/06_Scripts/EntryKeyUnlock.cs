using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class EntryKeyUnlock : MonoBehaviour {
    Memory memory;
    Block block;
    private void Start() {
        memory = Camera.main.GetComponent<Memory>();
        block = GetComponentInParent<Block>();
    }
    public void LockKeys() {
        memory.isKeyLocked = false;
        block.isMoveLocked = false;
    }
}
