using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    private void Start()
    {
        menuCanvas.SetActive(false);
    }

    public void ActivateMenu(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
