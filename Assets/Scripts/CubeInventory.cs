using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInventory : Singleton<CubeInventory>
{
    [SerializeField] private Transform[] positions;

    private List<CubeController> cubes = new List<CubeController>();
    private List<CubeController> similarCubes = new List<CubeController>();

    public List<CubeController> Cubes => cubes;

    private void Start()
    {
        similarCubes.Clear();
    }
    public void AddCube (CubeController cube)
    {
        similarCubes.Clear();
        similarCubes.Add(cube);

        foreach (CubeController c in cubes)
        {
            if (c.CubeSprite == cube.CubeSprite)
            {
                similarCubes.Add(c);
                if (similarCubes.Count >= 3)
                {
                    foreach (CubeController sim in similarCubes)
                    {
                        cubes.Remove(sim);
                        sim.ActivateDestroy();
                    }
                    similarCubes.Clear();

                    for (int i = 0; i < cubes.Count; i++)
                    {
                        cubes[i].transform.DOMove(positions[i].position, 0.5f).SetEase(Ease.Linear);
                    }
                    return;
                }
            }
        }

        cubes.Add(cube);
        cube.transform.SetParent(null);
        cube.transform.DOMove(positions[cubes.Count - 1].position, 0.3f);
        cube.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.3f);
        cube.transform.DORotateQuaternion(Quaternion.identity, 0.3f);


        if (cubes.Count >= 7)
        {
            EndGameController.Instance.EndGame(false);
        }
    }

    public void ClearingInventory ()
    {
        foreach (CubeController cube in cubes)
        {
            cube.ActivateDestroy();
        }

        cubes.Clear();
    }
}
