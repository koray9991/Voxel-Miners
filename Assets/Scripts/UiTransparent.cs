using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class UiTransparent : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI text;
    public bool imageBool, textBool;

    private void Start()
    {
        if (imageBool)
        {
            image = GetComponent<Image>();
            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0), 2);
        }
        if (textBool)
        {
            text = GetComponent<TextMeshProUGUI>();
            text.DOColor(new Color(text.color.r, text.color.g, text.color.b, 0), 3);
        }

    }
}
