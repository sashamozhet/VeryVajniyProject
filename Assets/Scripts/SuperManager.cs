using UnityEngine;
using DG.Tweening;
using System;


public class SuperManager : MonoBehaviour
{
    [SerializeField] ButtonsManager buttonsManager;
    [SerializeField] ImagesManager imagesManager;
    [SerializeField] AudioManager audioManager;

    [SerializeField] float defaultIntervaBetweenMoves;
    [SerializeField] float intervalChangeWithEveryIteration;

    [SerializeField] int movesMin;
    [SerializeField] int movesMax;

    public static Action GameStarted;


    private void Start()
    {
        GameStarted += GameProcessStarted;
        //GameStarted += audioManager.PlayAudioWhenGameStarted;
        buttonsManager.startRandomingButton.onClick.AddListener(StartGameProcess);
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
            mySequence.Append(imagesManager.BoardStateReturner());
        }
        mySequence.Append(imagesManager.ShuffleCardsSequence()); // добавляем в очередь перемешивание карточек
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenGameStarted(); });  // воспроизводим звук типа поехали                                                       // 
        mySequence.AppendInterval(0.5f); // добавляем 0.5сек задержки перед следующим шагом
        mySequence.Append(imagesManager.MakeMovesOnBoard(defaultIntervaBetweenMoves, intervalChangeWithEveryIteration, RandomsVariations.SimpleRandomMinMax(movesMin, movesMax))); // добавляем в очередь метод, пробегающийся по карточкам в списке
        mySequence.Append(imagesManager.GameObjectScalerSequence(imagesManager.background, 0, 1f)); // скейлим бэкграунд в 0, прежде, чем выдать итоговую карточку в центр списка
        mySequence.AppendInterval(0.5f); // добавляем 0.5сек задержки перед следующим шагом
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenCardChosen(); }); // воспроизводим звук типа бах карта выбрана
        mySequence.Append(imagesManager.AnimateChosenCard()); // выводим карточку в центр экрана
        mySequence.Append(buttonsManager.EnableOrDisableButtonsSequence(true)); // включаем кнопки
    }
}

