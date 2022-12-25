using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    // Attributes for single press
    private bool _isLeftPressed = false;
    private bool _isRightPressed = false;
    private bool _isUpPressed = false;
    private bool _isDownPressed = false;

    // Methods for Events
    public void LeftPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isLeftPressed = true;
        }
        else if (context.canceled)
        {
            _isLeftPressed = false;
        }
    }

    public void Rightressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isRightPressed = true;
        }
        else if (context.canceled)
        {
            _isRightPressed = false;
        }
    }

    public void UpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isUpPressed = true;
        }
        else if (context.canceled)
        {
            _isUpPressed = false;
        }
    }

    public void DownPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isDownPressed = true;
        }
        else if (context.canceled)
        {
            _isDownPressed = false;
        }
    }

    // Methods for GetKeyDown (one single press)
    public bool IsLeftPressed()
    {
        bool result = _isLeftPressed;
        _isLeftPressed = false;
        return result;
    }

    public bool IsRightPressed()
    {
        bool result = _isRightPressed;
        _isRightPressed = false;
        return result;
    }

    public bool IsUpPressed()
    {
        bool result = _isUpPressed;
        _isUpPressed = false;
        return result;
    }

    public bool IsDownPressed()
    {
        bool result = _isDownPressed;
        _isDownPressed = false;
        return result;
    }
}
