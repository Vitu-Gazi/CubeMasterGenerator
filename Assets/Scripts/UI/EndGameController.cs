using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : Singleton<EndGameController>
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private DarkPanelController darkPanelController;
    [SerializeField] private Button resumeButton;

    [DllImport("__Internal")] private static extern void ResumeExtern();
    [DllImport("__Internal")] private static extern void LevelAdExtern();

    private bool lookAd = false;
    private bool oneLose = false;

    private void Start()
    {
        darkPanelController.HidePanel(null);
    }

    public void EndGame (bool value)
    {
        if (value)
        {
            winPanel.SetActive(true);

            LevelAdExtern();
        }
        else
        {
            if (!oneLose)
            {
                oneLose = true;
            }
            else
            {
                resumeButton.interactable = false;
            }

            losePanel.SetActive(true);
        }
    }

    public void Restart ()
    {
        darkPanelController.ShowPanel(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
    public void LoadMenu(int value)
    {
        darkPanelController.ShowPanel(() => SceneManager.LoadScene(value));
    }

    // ÆÑ

    public void Resume ()
    {
        ResumeExtern();
    }

    public void AfterResume ()
    {
        if (lookAd)
        {
            FigureController.Instance.ClearingFigure();
            CubeInventory.Instance.ClearingInventory();
            FigureController.Instance.UpdateCubesList();
            losePanel.SetActive(false);
        }
        else
        {
            LoadMenu(0);
        }
    }

    public void EnableLookAd ()
    {
        lookAd = true;
    }
}
