using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private DarkPanelController darkPanel;

    private void Start ()
    {
        darkPanel.HidePanel(null);
    }

    public void Load(int value)
    {
        darkPanel.ShowPanel(() => SceneManager.LoadScene(value));
    }
}
