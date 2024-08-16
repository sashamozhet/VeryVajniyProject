using UnityEngine;
using DG.Tweening;
using System;

public class SuperManager : MonoBehaviour
{
    [SerializeField] ButtonsManager buttonsManager;
    [SerializeField] ImagesManager imagesManager;
    [SerializeField] AudioManager audioManager;
    [SerializeField] TimeManager timeManager;

    [SerializeField] int movesMin;
    [SerializeField] int movesMax;

    public static Action GameStarted;

    private void Start()
    {
        GameStarted += GameProcessStarted; // подписываем метод начала игры на событие
    }

    public void StartGameProcess()
    {
        GameStarted?.Invoke();
    }

    public void GameProcessStarted()
    {
        buttonsManager.EnableOrDisableButtons(false); // отключаем кнопки
        var mySequence = DOTween.Sequence(); // создаём очередь выполнения твинов

        if (imagesManager.CardAnimatedMovedToDisplay)
        {
            mySequence.AppendCallback(() => { imagesManager.background.SetActive(false); }); // выключаем настоящий бекграунд
            mySequence.Append(imagesManager.backgroundAlwaysSorted.transform.DOScale(0, 0.000001f)); // скейлим в 0 неактивный фейковый бекграунд
            mySequence.AppendCallback(() => { imagesManager.backgroundAlwaysSorted.SetActive(true); }); // включаем фейковый бекграунд
            mySequence.Append(imagesManager.backgroundAlwaysSorted.transform.DOScale(1, timeManager.durationSortedBgScaleTime))
                .AppendInterval(timeManager.intervalAfterSortedBgScaled); // скейлим фейковый бекграунд в 1, показывая его юзеру
            mySequence.Append(imagesManager.BoardStateReturner()); // возвращаем состояние настоящего бейкграунда                      
            mySequence.AppendCallback(() => { imagesManager.background.SetActive(true); }); // включаем настоящий бекграунд
            mySequence.AppendCallback(() => { imagesManager.backgroundAlwaysSorted.SetActive(false); }); // выключаем фейковый бекграунд
        }

        mySequence.Append(imagesManager.ShuffleCardsSequence()); // добавляем в очередь перемешивание карточек
        mySequence.Append(imagesManager.background.transform.DOScale(1.03f, timeManager.durationBgPreGameTickAnimation).SetLoops(4, LoopType.Yoyo)); // скейлим бэкграунд туда-сюда
        mySequence.AppendInterval(timeManager.intervalAfterCardsShuffle);

        // Start the sequence for moves on the board
        var movesSequence = imagesManager.MakeMovesOnBoard(timeManager.intervalPreEveryMove, timeManager.intervalChangeWithEveryIteration, RandomsVariations.SimpleRandomMinMax(movesMin, movesMax));
        mySequence.Append(movesSequence);

        // Pre-animate the chosen card and scale background to zero
        mySequence.Append(imagesManager.PreAnimateChosenCard(imagesManager.currentImg, timeManager.durationPreAnimateChosenCardTick)); // преанимация выбранной карты, скейлы туда-сюда
        mySequence.Append(imagesManager.background.transform.DOScale(0, timeManager.durationBgScaleToZeroWhenCardChosen)); // скейлим бэкграунд в 0

        // Create a parallel sequence for the animation and audio
        var parallelSequence = DOTween.Sequence();

        parallelSequence.AppendCallback(() => {
            audioManager.chosenCardClass = imagesManager.currentImg.GetComponent<CardsClassesAndDescriptionsManager>().GetCardClass();
            audioManager.chosenCardDescription = imagesManager.currentImg.GetComponent<CardsClassesAndDescriptionsManager>().GetCardDescription();
        });

        parallelSequence.AppendCallback(() => {
            // Start audio and animation simultaneously
            float audioDuration = audioManager.PlayAudioWhenCardChosen(); // Start the audio
            imagesManager.AnimateChosenCard(timeManager.durationChosenCardScaleAnimation).Play(); // Start the animation
        });

        mySequence.Append(parallelSequence);

        // Use an OnComplete callback to ensure buttons are enabled after audio ends
        mySequence.OnComplete(() => {
            // Ensure we enable buttons only after the audio ends
            // If `PlayAudioWhenCardChosen` returns the duration of the audio clip, use it here
            float audioDuration = audioManager.PlayAudioWhenCardChosen(); // Retrieve the duration
            DOTween.Sequence()
                .AppendInterval(audioDuration)
                .AppendCallback(() => {
                    buttonsManager.ChangeObjectTextSequence(buttonsManager.startRandomingButtonObject, "pick another"); // Меняем текст главной кнопки
                    buttonsManager.EnableOrDisableButtonsSequence(true); // Включаем кнопки
                });
        });
    }
}
