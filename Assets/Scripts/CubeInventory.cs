using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInventory : MonoBehaviour
{
    [SerializeField] private Transform[] positions;

    private List<CubeController> cubes = new List<CubeController>();
    private List<CubeController> similarCubes = new List<CubeController>();

    public static CubeInventory Instance;

    private void Start()
    {
        Instance = this;
        similarCubes.Clear();
    }
    public void AddCube (CubeController cube)
    {
        similarCubes.Clear();
        similarCubes.Add(cube);
        if (cubes.Count < 7)
        {
            foreach (CubeController c in cubes)
            {
                if (c.CubeNumber == cube.CubeNumber)
                {
                    similarCubes.Add(c);
                    if (similarCubes.Count >= 3)
                    {
                        foreach (CubeController sim in similarCubes)
                        {
                            cubes.Remove(sim);
                            Destroy(sim.gameObject);
                        }
                        similarCubes.Clear();
                        return;
                    }
                }
            }
            cubes.Add(cube);
            cube.transform.SetParent(null);
            cube.transform.position = positions[cubes.Count - 1].position;
            cube.transform.localScale /= 2;
        }
        else
        {
            Debug.Log("Нет места, вы проиграли");
        }
    }
}
