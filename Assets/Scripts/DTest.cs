using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class DTest : MonoBehaviour
{
    [SerializeField]
    RectTransform window;
    [SerializeField]
    CanvasGroup canvasGroup;
    [SerializeField]
    TMP_Text textMesh;
    float time;
    bool timerFlag;
    float speed = 10f;
    [SerializeField]
    Button openButton, closeButton;
    // Start is called before the first frame update
    void Start()
    {
        openButton.onClick.AddListener(() =>
        {
            OpenWindow();
            ButtonAnimation(openButton);
        });
        closeButton.onClick.AddListener(() =>
        {
            CloseWindow();
            ButtonAnimation(closeButton);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (timerFlag)
        {
            time += Time.deltaTime;
            textMesh.maxVisibleCharacters = (int)(time * speed);
        }
    }

    public void OpenWindow()
    {
        Sequence seq = DOTween.Sequence()
            .AppendCallback(() =>
            {
                textMesh.maxVisibleCharacters = 0;
                /*
                window.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
                canvasGroup.DOFade(1, 0.5f);
                window.DORotate(new Vector3(0, 0, -360), 0.5f).SetRelative();
                */
                window.GetComponent<Image>().material.DOFloat(1f, "_Ratio", 2f);
            })
            .AppendInterval(0.5f)
            .AppendCallback(() =>
            {
                timerFlag = true;
                time = 0;
            })
            ;
    }

    public void CloseWindow()
    {
        /*
        window.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutCubic);
        canvasGroup.DOFade(0, 0.5f);
        window.DORotate(new Vector3(0, 0, 360), 0.5f).SetRelative();
        */
        //window.DOSizeDelta(new Vector2(0, 0), 1f).SetEase(Ease.OutBack);
        window.GetComponent<Image>().material.DOFloat(0f, "_Ratio", 2f);
        timerFlag = false;
    }

    void ButtonAnimation(Button button)
    {
        button.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f).SetRelative();
    }
}
