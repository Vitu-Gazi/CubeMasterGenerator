using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DarkPanelController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float speed;

    private IDisposable panel;

    public void HidePanel (Action callback)
    {
        gameObject.SetActive(true);

        panel?.Dispose();

        panel = Observable.EveryUpdate().TakeUntilDisable(gameObject).TakeWhile(x => image.color.a > 0).Finally(() =>
        {
            gameObject.SetActive(false);
            callback?.Invoke();

        }).Subscribe(_ => 
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (speed * Time.deltaTime));
        });
    }
    public void ShowPanel(Action callback)
    {
        gameObject.SetActive(true);

        panel?.Dispose();

        panel = Observable.EveryUpdate().TakeUntilDisable(gameObject).TakeWhile(x => image.color.a < 1).Finally(() => 
        {
            callback?.Invoke();

        }).Subscribe(_ =>
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (speed * Time.deltaTime));
        });
    }
}
