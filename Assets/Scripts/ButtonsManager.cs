using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;


public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] internal Button startRandomingButton;
    [SerializeField] Button settingsButton;
    [SerializeField] private Color newButtonColor;
    public Action onStartRandomingButtonClickedAction;
    public Action onSettingsButtonClickedAction;

    private void Start()
    {
        //startRandomingButton.onClick.AddListener(OnStartRandomingButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    public void OnStartRandomingButtonClicked()
    {
        //SuperManager.GameStarted?.Invoke();
    }

    public void OnSettingsButtonClicked()
    {
        onSettingsButtonClickedAction?.Invoke();
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

}
