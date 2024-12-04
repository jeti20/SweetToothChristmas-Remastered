using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _confirmationPrompt = null; //po naciœnieci apply okienko potiwerdzenia


    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ControlButton()
    {
        Application.Quit();
    }

    public void LoadSite()
    {
        Application.OpenURL("https://github.com/jeti20");
    }

    public IEnumerator ConfirmationBox()
    {
        _confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        _confirmationPrompt.SetActive(false);   
    }
}
