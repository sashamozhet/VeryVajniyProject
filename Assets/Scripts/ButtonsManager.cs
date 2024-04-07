using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections.Generic;


public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] Button startRandomingButton;
    [SerializeField] Button settingsButton;
    [SerializeField] private Color newButtonColor;
    public static Action onStartRandomingButtonClickedAction;
    public static Action onSettingsButtonClickedAction;

    private void Start()
    {
        startRandomingButton.onClick.AddListener(OnStartRandomingButtonClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);


    }

    public static void OnStartRandomingButtonClicked()
    {
        onStartRandomingButtonClickedAction?.Invoke();
    }

    public static void OnSettingsButtonClicked()
    {
        onSettingsButtonClickedAction?.Invoke();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DisableButtons();
        }
    }

    public void DisableButtons()
    {
        foreach (Button button in buttons)
        {
           // button.interactable = false; // Делаем кнопку неактивной
            Image buttonImage = button.GetComponent<Image>(); //Это нужно, потомучто если у кнопки
                                                              //нет этого компонента, то этот метод закрашит программу
            if (buttonImage != null)
            {
                buttonImage.color = newButtonColor; // Изменяем цвет кнопки
            }
        }
    }
    /* что нужно сделать:
    
    нужен метод, который делает все кнопки на экране неактивными.
    на момент у нас их всего 2, гого рандомить и настройки, но кнопки могут в будущем и добавиться
    реализовано по идее это должно быть не костылем, т.е. вариант в котором мы отключение каждой кнопки руками прописываем отдельно
    нам явно не подходит
    офк не забывай про комменты, стейдж, коммит, пуш

    */

}
