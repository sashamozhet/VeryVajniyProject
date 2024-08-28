using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{

    [SerializeField] TimeManager timeManager;
    [SerializeField] ColorManager colorManager;
    [SerializeField] internal GameObject[] buttons;
    [SerializeField] internal GameObject settingsButtonObject;
    public TextMeshProUGUI pressSpaceToPlayText;
    internal Button settingsButton;

    [SerializeField] SizeManager sizeManager;


    private void Start()
    {   
        SuperManager.GameStarted += BlinkingTextControl;
        BlinkingTextControl();
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


    public void BlinkingTextControl()
    {
        if (ImagesManager.CardAnimatedMovedToDisplay)
        {
            pressSpaceToPlayText.transform.position = new Vector2(Screen.width / 2, Screen.height - sizeManager.pixelsFromTopPressCardTextChangedState);
            pressSpaceToPlayText.transform.localScale = new Vector2(sizeManager.sizePressCardTextChangedState, sizeManager.sizePressCardTextChangedState);
        }

        if (!SuperManager.IsEventRunning)
        {   
            pressSpaceToPlayText.gameObject.SetActive(true);
            pressSpaceToPlayText.alpha = colorManager.minOpacityPressSpaceText;
            pressSpaceToPlayText.DOFade(colorManager.maxOpacityPressSpaceText, timeManager.durationBlinkingTextLoop).SetLoops(-1, LoopType.Yoyo).SetId("pressspacetoplay");
        }
        else
        {
            pressSpaceToPlayText.gameObject.SetActive(false);
            DOTween.Kill("pressspacetoplay");
        }
        
    }
}
