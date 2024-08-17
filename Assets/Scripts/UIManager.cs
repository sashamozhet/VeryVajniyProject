using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    /// <summary>
    /// здесь мы изначально используем GameObject а не Button потому, что хотим полностью скрывать кнопки во время процесса рандома
    /// с GameObject одним методом мы полностью скрываем кнопку
    /// с Button придётся отдельно убирать изображение, отдельно кликабельность
    /// а если я неправ и это не так, переделаем обратно
    /// </summary>
    [SerializeField] TimeManager timeManager;
    [SerializeField] internal GameObject[] buttons;
    [SerializeField] internal GameObject settingsButtonObject;
    public TextMeshProUGUI pressSpaceToPlayText;
    internal Button settingsButton;


    private void Start()
    {
        BlinkingTextControl(true);
    }

    public void ChangeObjectText(GameObject obj, string newText)
    {
        var txt = obj.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = newText;
    }

    public Sequence ChangeObjectTextSequence(GameObject obj, string newText)
    {
        return DOTween.Sequence().AppendCallback(() => { ChangeObjectText(obj, newText); });
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

    public void BlinkingTextControl(bool setTrueOrFalse)
    {
        // Останавливаем текущую анимацию
        DOTween.Kill("BlinkingText");

        pressSpaceToPlayText.gameObject.SetActive(setTrueOrFalse);
        if (setTrueOrFalse)
        {
            // Устанавливаем текст на немного затемнённый перед запуском анимации
            pressSpaceToPlayText.alpha = 0.4f;

            // Запускаем анимацию мигания
            pressSpaceToPlayText.DOFade(1f, timeManager.durationBlinkingTextLoop).SetLoops(-1, LoopType.Yoyo).SetId("BlinkingText");
        }
        else
        {
            // Сбрасываем прозрачность текста к 1 при его отключении
            pressSpaceToPlayText.alpha = 1f;
        }
    }
}
