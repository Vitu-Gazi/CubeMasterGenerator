using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private SpriteRenderer[] sprites;

    private bool canBeChoosed = true;

    public Sprite CubeSprite => sprites[0].sprite;

    public static CubeController choosedCube;

    private void OnMouseDown()
    {
        choosedCube = this;
    }

    private void OnMouseUpAsButton()
    {
        if (choosedCube == this && canBeChoosed)
        {
            canBeChoosed = false;

            CubeInventory.Instance.AddCube(this);

            FigureController.Instance.RemoveCube(this);
        }

        choosedCube = null;
    }

    public void SetSprite (Sprite newSprite)
    {
        foreach (var sprite in sprites)
        {
            sprite.sprite = newSprite;
        }
    }

    public void ActivateDestroy()
    {
        particle.transform.SetParent(null);
        particle.gameObject.SetActive(true);
        Destroy(gameObject);
    }
}
