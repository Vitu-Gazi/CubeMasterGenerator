using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Randomize
{
    private static List<Level> arrays = new List<Level>();
    public static void RandomizeGenerator(ref List<GameObject> cubes, Vector3 scale)
    {
        List<GameObject> newCubes = new List<GameObject>();

        if (scale == Vector3.zero || scale.x <= 3 || scale.y <= 3 || scale.z <= 3)
        {
            while (cubes.Count > 0)
            {
                SetPositions(cubes, newCubes);
            }

            return;
        }

        List<GameObject> levelCubes = new List<GameObject>();
        List<GameObject> levelCubesTwo = new List<GameObject>();
        List<Level> newLevels = new List<Level>();

        for (int z = 0; z < scale.z; z++)
        {
            arrays.Add(new Level());

            for (int i = 0; i < scale.x * scale.y; i++)
            {
                arrays[arrays.Count - 1].cubes.Add(cubes[0]);
                cubes.RemoveAt(0);
            }
        }
        while (arrays.Count > 0)
        {
            if (arrays.Count >= 2)
            {
                levelCubes = arrays[0].cubes;
                levelCubesTwo = arrays[arrays.Count - 1].cubes;
                levelCubes = levelCubes.Union(levelCubesTwo).ToList();
                arrays.Remove(arrays[arrays.Count - 1]);
            }
            else
            {
                levelCubes = arrays[arrays.Count - 1].cubes;
            }

            AddNewLevel(levelCubes, newLevels);

            arrays.Remove(arrays[0]);
        }
        foreach (Level level in newLevels)
        {
            int i = 0;

            while (level.cubes.Count > 0)
            {
                SetPositions(level.cubes, newCubes);

                i++;
            }
        }

        void SetPositions(List<GameObject> list, List<GameObject> newCubes)
        {
            GameObject obj = list[Random.Range(0, list.Count)];

            if (newCubes.Count > 0)
            {
                Vector3 secondPos = obj.transform.position;
                obj.transform.position = newCubes[0].transform.position;
                newCubes[0].transform.position = secondPos;
            }

            list.Remove(obj);
            newCubes.Add(obj);
        }

        void AddNewLevel(List<GameObject> levelCubes, List<Level> newLevels)
        {
            Level l = new Level();
            l.cubes = new List<GameObject>(levelCubes);
            newLevels.Add(l);
        }
    }

    public static void Shaffle(List<CubeController> list)
    {
        List<CubeController> newCubes = new List<CubeController>();

        while (list.Count > 0)
        {
            CubeController obj = list[Random.Range(0, list.Count)];

            if (newCubes.Count > 0)
            {
                Vector3 secondPos = obj.transform.position;
                obj.transform.position = newCubes[0].transform.position;
                newCubes[0].transform.position = secondPos;
            }

            list.Remove(obj);
            newCubes.Add(obj);
        }
    }
}

