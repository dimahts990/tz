using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Effect : MonoBehaviour
{
    private bool isFirstShake = true;
    private bool isFirstBounce = true;
    private Vector3 scale;
    private Vector3 pos;

    public void Bounce()
    {
        if (isFirstBounce)
        {
            scale = transform.localScale;
            isFirstBounce = false;
        }

        var Seq = DOTween.Sequence();
        Seq.Append(transform.DOScale(scale + new Vector3(0.8f, 0.8f, 0.8f), 0.2f))
            .Append(transform.DOScale(scale + new Vector3(-1f, -1f, -1f), 0.1f))
            .Append(transform.DOScale(scale + new Vector3(0.4f, 0.4f, 0.4f), 0.1f))
            .Append(transform.DOScale(scale, 0.1f));
    }

    public void Shake()
    {
        if (isFirstShake)
        {
            pos = transform.localPosition;
            isFirstShake = false;
        }

        var Seq = DOTween.Sequence();
        Seq.Append(transform.DOLocalMove(pos + new Vector3(3f, 0f, 0), 0.1f))
            .Append(transform.DOLocalMove(pos + new Vector3(-2.5f, 0f, 0), 0.1f))
            .Append(transform.DOLocalMove(pos + new Vector3(1f, 0f, 0), 0.1f))
            .Append(transform.DOLocalMove(pos, 0.2f));
    }

    public void FadeIn(bool isText=false)
    {
        if (isText)
        {
            Text text;
            text = GetComponent<Text>();
            text.DOColor(new Color(0, 0, 0, 1), 1f);
        }
        else
        {
            Image image;
            image = GetComponent<Image>();
            image.DOColor(new Color(0, 0, 0, 1), 1f);
        }
    }
    
    public void FadeOut(bool isText=false)
    {
        if (isText)
        {
            Text text;
            text = GetComponent<Text>();
            text.DOColor(new Color(0, 0, 0, 0), 1f);
        }
        else
        {
            Image image;
            image = GetComponent<Image>();
            image.DOColor(new Color(0, 0, 0, 0), 1f);
        }
    }
}
