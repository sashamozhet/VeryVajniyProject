using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public int chosenCardClass;
    public string chosenCardDescription;

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



    public void SetAuidoClipToPlay()
    {
        switch(chosenCardClass)
        {
            case 0:
                chosenClipToPlay = cardSoundsCommon[chosenCardDescription];
                break;
            case 1:
                chosenClipToPlay = cardSoundsUncommon[chosenCardDescription];
                break;
            case 2:
                chosenClipToPlay = cardSoundsRare[chosenCardDescription];
                break;
            case 3:
                chosenClipToPlay = cardSoundsMythical[chosenCardDescription];
                break;
        }
    }

    public void PlayAudioWhenGameStarted()
    {
        audioSource.PlayOneShot(gameStartedClip);
    }

    public float PlayAudioWhenCardChosen()
    {
        SetAuidoClipToPlay();
        if (chosenClipToPlay == null)
        {
            Debug.LogError("Chosen card audio clip is null. Ensure it is properly assigned.");
            return 0f;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource is null. Ensure it is properly assigned.");
            return 0f;
        }

        audioSource.clip = chosenClipToPlay;
        audioSource.Play();

        Debug.Log("Playing audio clip: " + chosenClipToPlay.name + ", duration: " + chosenClipToPlay.length + " seconds");

        return chosenClipToPlay.length;
    }
}
