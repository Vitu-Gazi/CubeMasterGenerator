using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Randomize
{
    private static List<Level> arrays = new List<Level>();
    public static void RandomizeGenerator(ref List<GameObject> cubes, Vector3 scale)
    {
        Vector3 secondPos = new Vector3();
        List<GameObject> newCubes = new List<GameObject>();
        if (scale.x <= 3 || scale.y <= 3 || scale.z <= 3)
        {
            while (cubes.Count > 0)
            {
                if (newCubes.Count > 0)
                {
                    GameObject obj = cubes[Random.Range(0, cubes.Count)];
                    secondPos = obj.transform.position;
                    obj.transform.position = newCubes[0].transform.position;
                    newCubes[0].transform.position = secondPos;
                    cubes.Remove(obj);
                    newCubes.Add(obj);
                }
                else
                {
                    GameObject obj = cubes[Random.Range(0, cubes.Count)];
                    cubes.Remove(obj);
                    newCubes.Add(obj);
                }
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
                //Это можно укоротить
                levelCubes = arrays[0].cubes;
                levelCubesTwo = arrays[arrays.Count - 1].cubes;
                levelCubes = levelCubes.Union(levelCubesTwo).ToList();
                arrays.Remove(arrays[arrays.Count - 1]);
                arrays.Remove(arrays[0]);
                Level l = new Level();
                l.cubes = new List<GameObject>(levelCubes);
                newLevels.Add(l);
            }
            else
            {
                levelCubes = arrays[arrays.Count - 1].cubes;
                arrays.Remove(arrays[0]);
                Level l = new Level();
                l.cubes = new List<GameObject>(levelCubes);
                newLevels.Add(l);
            }
        }
        foreach (Level level in newLevels)
        {
            int i = 0;
            while (level.cubes.Count > 0)
            {
                if (newCubes.Count > 0)
                {
                    GameObject obj = level.cubes[Random.Range(0, level.cubes.Count)];
                    secondPos = obj.transform.position;
                    obj.transform.position = newCubes[0].transform.position;
                    newCubes[0].transform.position = secondPos;
                    level.cubes.Remove(obj);
                    newCubes.Add(obj);
                    i++;
                }
                else
                {
                    GameObject obj = level.cubes[Random.Range(0, level.cubes.Count)];
                    level.cubes.Remove(obj);
                    newCubes.Add(obj);
                    i++;
                }
            }
        }
    }
}

