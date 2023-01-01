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
    [SerializeField] private CubeController prefabCube;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Vector3> scales;
    [SerializeField] private Vector3 scale;

    private List<GameObject> cubes = new List<GameObject>();
    private List<GameObject> xCubes = new List<GameObject>();

    private int number = 0;
    private int cubeNumber = 0;

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
            CubeController cube = Instantiate(prefabCube);

            if (number >= sprites.Count)
            {
                number = 0;
            }

            cube.SetSprite(sprites[number]);

            cubeNumber++;

            if (cubeNumber == 3)
            {
                number++;
                cubeNumber = 0;
            }

            cube.transform.SetParent(parent);
            cube.transform.localPosition = new Vector3(x, y, z);
            xCubes.Add(cube.gameObject);

            SetOffset(x, y + (yNumber * 0.05f), z + (zNumber * 0.05f), cube.gameObject);

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

            cubes.Add(cube.gameObject);
            FigureController.Instance.AddCube(cube);
        }

        Randomize.RandomizeGenerator(ref cubes, scale);
    }

    private float GetFloat (float scale, float prefabScale)
    {
        return 0 - (scale / 2f) * prefabScale + prefabScale / 2;
    }
    private void SetOffset(float x, float y, float z, GameObject cube)
    {
        Vector3 offset = new Vector3();
        offset.x = x + (xCubes.Count - 1) * (prefabCube.transform.localScale.x + 0.05f);
        offset.y = y;
        offset.z = z;
        cube.transform.localPosition = offset;
    }
}
