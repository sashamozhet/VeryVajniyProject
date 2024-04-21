using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;


public class ButtonsManager : MonoBehaviour
{
    [SerializeField] internal Button[] buttons;
    [SerializeField] internal Button startRandomingButton;
    [SerializeField] internal Button settingsButton;
    [SerializeField] private Color newButtonColor;


    private void Start()
    {
    }




    public void EnableOrDisableButtons(bool isEnable)
    {
        foreach (Button button in buttons)
        {
           button.interactable = isEnable; // Делаем кнопку активной или неактивной
            Image buttonImage = button.GetComponent<Image>(); //Это нужно, потомучто если у кнопки
                                                              //нет этого компонента, то этот метод закрашит программу
            if (buttonImage != null)
            {
                buttonImage.color = isEnable? Color.white : Color.red; // Изменяем цвет кнопки, если отключаем кнопки = новый цвет, если возвращаем = дефолтный
            }
        }
    }

    public Sequence EnableOrDisableButtonsSequence(bool isEnable)
    {
        return DOTween.Sequence().AppendCallback(() => { EnableOrDisableButtons(isEnable); });
    }

}
