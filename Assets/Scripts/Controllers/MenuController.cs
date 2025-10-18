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

    private void Update()
    {
        if (InputManager.MenuWasPressed)
        {
            ActivateMenu();
        }
    }

    public void ActivateMenu()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }
}
