using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardFigureGenerator : MonoBehaviour
{
    [SerializeField] private int floors;
    [SerializeField] private Transform parent;
    [SerializeField] private CubeController prefabCube;
    [SerializeField] private List<Sprite> sprites;

    private int currentSprite = 0;
    private int spriteSteps = 0;

    private void Awake()
    {
        GeneratePyramid();
    }

    private void GeneratePyramid ()
    {
        int res = 1;
        int lastFloor = 1;

        for (int i = 1; i < floors; i++)
        {
            lastFloor += 2;
            res += lastFloor * lastFloor;
        }

        if (res % 3 != 0)
        {
            return;
        }

        GenerateMethod();
        List<CubeController> list = new List<CubeController>(FigureController.Instance.Cubes);
        Randomize.Shaffle(list);
    }

    private void GenerateMethod ()
    {
        int y = 0;

        for (int i = 1; i < floors + 1; i++)
        {
            CreateFloor(i + (i - 1), y);

            y--;
        }
    }

    private void CreateFloor (int stepNumber, int y)
    {
        for (int x = 0; x < stepNumber; x++)
        {
            for (int z = 0; z < stepNumber; z++)
            {
                float xF = x + GetFloat(stepNumber, prefabCube.transform.localScale.x);
                float zF = z + GetFloat(stepNumber, prefabCube.transform.localScale.x);

                CubeController cube = Instantiate(prefabCube);

                cube.SetSprite(sprites[currentSprite]);
                cube.transform.SetParent(parent);
                cube.transform.localPosition = new Vector3(xF, y, zF);

                FigureController.Instance.AddCube(cube);

                spriteSteps++;

                if (spriteSteps % 3 != 0)
                {
                    currentSprite++;

                    if (currentSprite >= sprites.Count)
                    {
                        currentSprite = 0;
                    }
                }
            }
        }
    }

    private float GetFloat(float scale, float prefabScale)
    {
        return 0 - (scale / 2f) * prefabScale + prefabScale / 2;
    }
}
