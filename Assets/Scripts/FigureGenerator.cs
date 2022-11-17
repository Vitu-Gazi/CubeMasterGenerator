using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Level
{
    public List<GameObject> cubes = new List<GameObject>();
}

public class FigureGenerator : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject prefabCube;
    [SerializeField] private List<Vector3> scales;
    [SerializeField] private Vector3 scale;

    private List<GameObject> cubes = new List<GameObject>();
    private List<GameObject> xCubes = new List<GameObject>();

    private int number = 0;
    private int cubeNumber = 0;

    public List<GameObject> AllCubes;

    private void Awake()
    {
        if (scale == Vector3.zero)
        {
            scale = scales[Random.Range(0, scales.Count)];
        }
        if ((scale.x * scale.y * scale.z) % 3 != 0)
        {
            Debug.Log("Количество Кубов в фигуре не может быть поделено на 3");
            return;
        }

        GenerateFigur();
    }

    public void GenerateFigur()
    {
        number = 0;
        cubeNumber = 0;
        parent.position = Vector3.zero;

        float x = GetFloat(scale.x, prefabCube.transform.localScale.x);
        float y = GetFloat(scale.y, prefabCube.transform.localScale.y);
        float z = GetFloat(scale.z, prefabCube.transform.localScale.z);
        float s = scale.x * scale.y * scale.z;

        int yNumber = 0;
        int zNumber = 0;

        for (int i = 0; i < s; i++)
        {
            GameObject cube = Instantiate(prefabCube);

            foreach (Text t in cube.GetComponentsInChildren<Text>())
            {
                t.text = number.ToString();
            }

            cubeNumber++;

            if (cubeNumber == 3)
            {
                number++;
                cubeNumber = 0;
            }

            cube.transform.SetParent(parent);
            cube.transform.localPosition = new Vector3(x, y, z);
            xCubes.Add(cube);

            SetOffset(x, y, z, cube);

            if (xCubes.Count == scale.x)
            {
                y += prefabCube.transform.localScale.y;
                yNumber++;

                if (yNumber == scale.y)
                {
                    y = GetFloat(scale.y, prefabCube.transform.localScale.y);
                    yNumber = 0;

                    if (scale.z > 1)
                    {
                        z += prefabCube.transform.localScale.z;
                        zNumber++;
                    }
                }

                xCubes.Clear();
            }

            cubes.Add(cube);
            AllCubes.Add(cube);
        }

        Randomize.RandomizeGenerator(ref cubes, scale);
    }

    private float GetFloat (float scale, float prefabScale)
    {
        return 0 - (scale / 2f) * prefabScale + prefabScale / 2;
    }
    private void SetOffset(float x, float y, float z, GameObject cube)
    {
        Vector3 offset = xCubes[xCubes.Count - 1].transform.localPosition + xCubes[xCubes.Count - 1].transform.localScale;
        offset.x = x + (xCubes.Count - 1) * prefabCube.transform.localScale.x;
        offset.y = y;
        offset.z = z;
        cube.transform.localPosition = offset;
    }
}
