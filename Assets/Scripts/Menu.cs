using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Iniciar()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}