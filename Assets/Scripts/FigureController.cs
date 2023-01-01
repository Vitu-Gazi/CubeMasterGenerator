using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FigureController : Singleton<FigureController>
{
    [SerializeField] private List<CubeController> cubes = new List<CubeController>();

    public List<CubeController> Cubes => cubes;

    public void AddCube (CubeController cube)
    {
        cubes.Add(cube);
    }

    public void RemoveCube(CubeController cube)
    {
        cubes.Remove(cube);

        if (cubes.Count <= 0)
        {
            EndGameController.Instance.EndGame(true);
        }
    }

    public void ClearingFigure ()
    {
        foreach (var cube in cubes)
        {
            foreach (var cub in CubeInventory.Instance.Cubes)
            {
                if (cube.CubeSprite == cub.CubeSprite)
                {
                    cube.transform.SetParent(null);
                    cube.ActivateDestroy();
                }
            }
        }
    }

    public void UpdateCubesList()
    {
        cubes.Clear();
        cubes = GetComponentsInChildren<CubeController>().ToList();

        

        if (cubes.Count <= 0)
        {
            EndGameController.Instance.EndGame(true);
        }
    }
}
