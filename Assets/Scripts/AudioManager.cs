using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip gameStartedClip;
    public AudioClip chosenClipToPlay { get; internal set; }

    // словари с набором карта+звук
    public Dictionary<string, AudioClip> cardSoundsCommon;
    public Dictionary<string, AudioClip> cardSoundsUncommon;
    public Dictionary<string, AudioClip> cardSoundsRare;
    public Dictionary<string, AudioClip> cardSoundsMythical;

    // аудиоклипы под common-карты
    [SerializeField] AudioClip countessCommonCardSound;
    [SerializeField] AudioClip andarielCommonCardSound;
    [SerializeField] AudioClip mephistoCommonCardSound;
    [SerializeField] AudioClip diabloCommonCardSound;
    [SerializeField] AudioClip baalCommonCardSound;
    [SerializeField] AudioClip nihlaCommonCardSound;
    [SerializeField] AudioClip summonerCommonCardSound;

    // аудиоклипы под uncommon-карты
    [SerializeField] AudioClip noneUncommonCardSound;

    // аудиоклипы под rare-карты
    [SerializeField] AudioClip andarielRareCardSound;
    [SerializeField] AudioClip countessRareCardSound;
    [SerializeField] AudioClip baalRareCardSound;

    // аудиоклипы под mythical-карты
    [SerializeField] AudioClip diabloMythicalCardSound;

    // Start is called before the first frame update
    void Start()
    {
        // создаём словарь с зависимостью аудиоклипов от выбранной карты для категории common
        cardSoundsCommon = new Dictionary<string, AudioClip>()
        {
            { "countess", countessCommonCardSound },
            { "andariel", andarielCommonCardSound },
            { "mephisto", mephistoCommonCardSound },
            { "diablo", diabloCommonCardSound },
            { "baal", baalCommonCardSound },
            { "nihla", nihlaCommonCardSound },
            { "summoner", summonerCommonCardSound },
        };

        // создаём словарь с зависимостью аудиоклипов от выбранной карты для категории uncommon
        cardSoundsUncommon = new Dictionary<string, AudioClip>()
        {
            { "none", noneUncommonCardSound },

        };

        // создаём словарь с зависимостью аудиоклипов от выбранной карты для категории rare
        cardSoundsRare = new Dictionary<string, AudioClip>()
        {
            { "andariel", andarielRareCardSound },
            { "countess", countessRareCardSound },
            { "baal", baalRareCardSound },

        };

        // создаём словарь с зависимостью аудиоклипов от выбранной карты для категории mythical
        cardSoundsMythical = new Dictionary<string, AudioClip>()
        {
            { "diablo", diabloMythicalCardSound },

        };
    }



    public void SetAuidoClipToPlay(int cardClass, string cardDescription)
    {
        switch(cardClass)
        {
            case 0:
                chosenClipToPlay = cardSoundsCommon[cardDescription];
                break;
            case 1:
                chosenClipToPlay = cardSoundsUncommon[cardDescription];
                break;
            case 2:
                chosenClipToPlay = cardSoundsRare[cardDescription];
                break;
            case 3:
                chosenClipToPlay = cardSoundsMythical[cardDescription];
                break;
        }
    }

    public void PlayAudioWhenGameStarted()
    {
        audioSource.PlayOneShot(gameStartedClip);
    }

    public float PlayAudioWhenCardChosen(int cardClass, string cardDescription)
    {
        SetAuidoClipToPlay(cardClass, cardDescription);
        audioSource.clip = chosenClipToPlay;
        audioSource.Play();
        return chosenClipToPlay.length;
    }
}
