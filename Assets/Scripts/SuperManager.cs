using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Unity.VisualScripting;

public class SuperManager : MonoBehaviour
{
    [SerializeField] ButtonsManager buttonsManager;
    [SerializeField] ImagesManager imagesManager;
    [SerializeField] AudioManager audioManager;

    public static Action GameStarted;


    private void Start()
    {
        buttonsManager.startRandomingButton.onClick.AddListener(Test);
        GameStarted += GameProcessStarted;
        
    }

    public void Test()
    {
        GameStarted?.Invoke();
    }

    public void GameProcessStarted()
    {
        buttonsManager.EnableOrDisableButtons(false);
        var mySequence = DOTween.Sequence();
        mySequence.AppendCallback(() => {imagesManager.ShuffleCardsOnBoard(); });
    }
}

