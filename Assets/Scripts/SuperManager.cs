using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UIElements;


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
        //buttonsManager.startRandomingButton.onClick.AddListener(StartGameProcess); // добавляем функционал кнопке начала игры
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
            mySequence.Append(imagesManager.backgroundAlwaysSorted.transform.DOScale(1, timeManager.durationSortedBgScaleTime)).AppendInterval(timeManager.intervalAfterSortedBgScaled); // скейлим фейковый бекграунд в 1, показывая его юзеру
            mySequence.Append(imagesManager.BoardStateReturner()); // возвращаем состояние настоящего бейкграунда                      
            mySequence.AppendCallback(() => { imagesManager.background.SetActive(true); }); // включаем настоящий бекграунд
            mySequence.AppendCallback(() => { imagesManager.backgroundAlwaysSorted.SetActive(false); }); // выключаем фейковый бекграунд
        }
        
        mySequence.Append(imagesManager.ShuffleCardsSequence()); // добавляем в очередь перемешивание карточек
        mySequence.Append(imagesManager.background.transform.DOScale(1.03f, timeManager.durationBgPreGameTickAnimation).SetLoops(4, LoopType.Yoyo)); // скейлим бэкграунд туда-сюда
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenGameStarted(); });  // воспроизводим звук типа поехали                                                        
        mySequence.AppendInterval(timeManager.intervalAfterCardsShuffle); // добавляем задержку перед следующим шагом
        mySequence.Append(imagesManager.MakeMovesOnBoard(timeManager.intervalPreEveryMove, timeManager.intervalChangeWithEveryIteration, RandomsVariations.SimpleRandomMinMax(movesMin, movesMax))); // добавляем в очередь метод, пробегающийся по карточкам в списке
       
        mySequence.Append(imagesManager.background.transform.DOScale(0, timeManager.durationBgScaleToZeroWhenCardChosen)); // скейлим бэкграунд в 0, прежде, чем выдать итоговую карточку в центр списка
        mySequence.AppendInterval(timeManager.intervalPreChosenCardShown); // добавляем задержку перед следующим шагом
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenCardChosen(); }); // воспроизводим звук типа бах карта выбрана
        mySequence.Append(imagesManager.AnimateChosenCard(timeManager.durationChosenCardScaleAnimation)); // выводим карточку в центр экрана
        mySequence.Append(buttonsManager.EnableOrDisableButtonsSequence(true)); // включаем кнопки
    }
}

