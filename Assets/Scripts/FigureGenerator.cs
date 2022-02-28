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
    [SerializeField] private bool random;

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
        float x = 0 - (scale.x / 2f) * prefabCube.transform.localScale.x + prefabCube.transform.localScale.x / 2;
        float y = 0 - (scale.y / 2f) * prefabCube.transform.localScale.y + prefabCube.transform.localScale.y / 2;
        int yNumber = 0;
        float z = 0 - ((scale.z / 2f) * prefabCube.transform.localScale.z) + prefabCube.transform.localScale.z / 2;
        int zNumber = 0;
        float s = scale.x * scale.y * scale.z;
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
            if (xCubes.Count < scale.x)
            {
                Vector3 offset = xCubes[xCubes.Count - 1].transform.localPosition + xCubes[xCubes.Count - 1].transform.localScale;
                offset.x = x + (xCubes.Count - 1) * prefabCube.transform.localScale.x;
                offset.y = y;
                offset.z = z;
                cube.transform.localPosition = offset;
            }
            else if (xCubes.Count == scale.x)
            {
                Vector3 offset = xCubes[xCubes.Count - 1].transform.localPosition + xCubes[xCubes.Count - 1].transform.localScale;
                offset.x = x + (xCubes.Count - 1) * prefabCube.transform.localScale.x;
                offset.y = y;
                offset.z = z;
                cube.transform.localPosition = offset;
                y += prefabCube.transform.localScale.y;
                yNumber++;
                if (yNumber < scale.y)
                {
                    xCubes.Clear();
                }
                else if (yNumber == scale.y)
                {
                    y = 0 - (scale.y / 2f) * prefabCube.transform.localScale.y + prefabCube.transform.localScale.y / 2;
                    yNumber = 0;
                    if (scale.z > 1)
                    {
                        z += prefabCube.transform.localScale.z;
                        zNumber++;
                    }
                    xCubes.Clear();
                }
            }
            cubes.Add(cube);
            AllCubes.Add(cube);
        }

        Randomize.RandomizeGenerator(ref cubes, scale);
    }

    private void ShuffleFigure()
    {
        if (!random)
        {
            return;
        }

        List<GameObject> objes = new List<GameObject>();

        int x = (int)Mathf.Ceil(scale.y / 2);
        int i = Random.Range(1, x);
        float q = Random.Range(1, Mathf.Ceil(scale.y / 2));
        q *= prefabCube.transform.localScale.y;
        foreach (GameObject cube in AllCubes)
        {
            if (Mathf.Ceil(cube.transform.localPosition.y) == i)
            {
                objes.Add(cube);
            }
            if (Mathf.Ceil(cube.transform.localPosition.y) == -i)
            {
                objes.Add(cube);
            }
            if (Mathf.Ceil(cube.transform.localPosition.y) == 0)
            {
                objes.Add(cube);
            }
        }
        foreach (GameObject cube in objes)
        {
            if (cube.transform.localPosition.y == 0)
            {
                cube.transform.position -= new Vector3(q / 2, 0, 0);
            }
            else
            {
                cube.transform.position += new Vector3(q / 2, 0, 0);
            }
        }
    }
}
