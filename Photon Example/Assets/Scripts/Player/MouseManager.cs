using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LockMouse
{
    LOCK,
    UNLOCK
}

public class MouseManager : MonoBehaviour
{
    [SerializeField] private LockMouse lockMouse;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CursorSettings();
    }

    public void SetCursor(LockMouse lockMouse)
    {
        switch (lockMouse)
        {
            case LockMouse.LOCK:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case LockMouse.UNLOCK:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                break;
        }
    }

    public void CursorSettings()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (lockMouse == LockMouse.LOCK)
            {
                SetCursor(LockMouse.UNLOCK);
            }
            else
            {
                SetCursor(LockMouse.LOCK);
            }
        }
       

    }
    
}
