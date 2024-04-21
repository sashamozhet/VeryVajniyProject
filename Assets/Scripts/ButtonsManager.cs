using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonsManager : MonoBehaviour
{   
    /// <summary>
    /// здесь мы изначально используем GameObject а не Button потому, что хотим полностью скрывать кнопки во время процесса рандома
    /// с GameObject одним методом мы полностью скрываем кнопку
    /// с Button придётся отдельно убирать изображение, отдельно кликабельность
    /// а если я неправ и это не так, переделаем обратно
    /// </summary>

    [SerializeField] internal GameObject[] buttons;

    [SerializeField] internal GameObject startRandomingButtonObject;
    internal Button startRandomingButton;

    [SerializeField] internal GameObject settingsButtonObject;
    internal Button settingsButton;

    private void Start()
    {
        startRandomingButton = startRandomingButtonObject.GetComponent<Button>();
        settingsButton = settingsButtonObject.GetComponent<Button>();
    }




    public void EnableOrDisableButtons(bool isEnable)
    {
        foreach (GameObject button in buttons)
        {
            button.SetActive(isEnable);       
        }
    }

    public Sequence EnableOrDisableButtonsSequence(bool isEnable)
    {
        return DOTween.Sequence().AppendCallback(() => { EnableOrDisableButtons(isEnable); });
    }

}
