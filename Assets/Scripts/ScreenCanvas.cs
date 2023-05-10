using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class ScreenCanvas : Singleton<ScreenCanvas>
{

    private Camera mainCamera;
    public Transform iconTransform;
    public TextMeshProUGUI moneyText;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    public Vector3 GetIconPos(Vector3 target)
    {
        Vector3 uiPos = iconTransform.position;
        uiPos.z = (target - mainCamera.transform.position).z;
        Vector3 result = mainCamera.ScreenToWorldPoint(uiPos);
        return result;
    }
    public void ImageScale()
    {

        iconTransform.DOScale(new Vector3(2.4f, 2.4f, 2.4f), 0.1f).OnComplete(() => iconTransform.DOScale(new Vector3(2f, 2f, 2f), 0.1f));

    }
    public void ImageAndTextScale()
    {
        iconTransform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 1, 5, 1);
        moneyText.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 1, 5, 1).OnComplete(() => moneyText.transform.DOScale(new Vector3(1, 1, 1), 0.2f));
        // iconTransform.DOScale(new Vector3(2.8f, 2.8f, 2.8f), 0.2f).OnComplete(() => iconTransform.DOScale(new Vector3(2f, 2f, 2f), 0.2f));
        // moneyText.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f).OnComplete(() => moneyText.transform.DOScale(new Vector3(1, 1, 1), 0.2f));
    }
}

