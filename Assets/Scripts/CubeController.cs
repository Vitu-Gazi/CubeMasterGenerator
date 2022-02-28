using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    [SerializeField] private Text[] text;

    public int CubeNumber => System.Convert.ToInt32(text[0].text);

    public static CubeController choosesCube;

    private void OnMouseDown()
    {
        choosesCube = this;
    }

    private void OnMouseUpAsButton()
    {
        if (choosesCube == this)
        {
            CubeInventory.Instance.AddCube(this);
            choosesCube = null;
        }
        else
        {
            choosesCube = null;
        }
    }
}
