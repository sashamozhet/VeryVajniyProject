using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class RuneCounterButton : MonoBehaviour, IPointerClickHandler
{
    private int runeCount = 0;
    private TMP_Text runeCountTextToDisplay;
    private AudioSource audioSource;

    private void Awake()
    {
        runeCountTextToDisplay = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        // Получаем ссылку на AudioSource из менеджера
        audioSource = AudioManager.instance;
        RuneCounterTextControl();
    }

    public void RuneCounterTextControl()
    {   
        runeCountTextToDisplay.gameObject.SetActive(runeCount != 0);
        runeCountTextToDisplay.text = "x" + runeCount.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Проверяем какая кнопка мыши была нажата
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            runeCount++;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (runeCount > 0) { runeCount--; }
        }
        RuneCounterTextControl();
    }

    public void PlaySound()
    {   
        if(audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }        
    }
}
