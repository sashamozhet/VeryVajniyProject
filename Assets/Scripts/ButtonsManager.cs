using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;


public class ButtonsManager : MonoBehaviour
{
    [SerializeField] Button startRandomingButton;
    [SerializeField] Button settingsButton;
    //[SerializeField] List<Button> buttons;
    public static Action OnImageResizeRequest;
    private void Start()
    {
      //  startRandomingButton.onClick = null;
        startRandomingButton.onClick.AddListener(OnStartRandomingButton);

    }
    public void OnStartRandomingButton()
    {
        OnImageResizeRequest?.Invoke();
    }

    /* что нужно сделать:
    
    нужен метод, который делает все кнопки на экране неактивными.
    на момент у нас их всего 2, гого рандомить и настройки, но кнопки могут в будущем и добавиться
    реализовано по идее это должно быть не костылем, т.е. вариант в котором мы отключение каждой кнопки руками прописываем отдельно
    нам явно не подходит
    офк не забывай про комменты, стейдж, коммит, пуш

    */

}
