using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class SuperManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    [SerializeField] ImagesManager imagesManager;
    [SerializeField] AudioManagerDiabloEdition audioManager;
    [SerializeField] TimeManager timeManager;

    [SerializeField] int movesMin;
    [SerializeField] int movesMax;


    public static Action GameStarted;
    public static bool IsEventRunning { get; private set; }

    private void Start()
    {
        GameStarted += GameProcessStarted; // подписываем метод начала игры на событие
    }

    private void OnDisable()
    {
        GameStarted -= GameProcessStarted; // отписка от события чтоб предотвратить утечки памяти
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsEventRunning)
        {
            StartGameProcess();
        }
    }

    public void StartGameProcess()
    {
        IsEventRunning = true;
        GameStarted?.Invoke();
    }

    public void GameProcessStarted()
    {
        imagesManager.canvasBackgroundImage.color = imagesManager.defaultCanvasBackgroundImageColor; // скидываем цвет фонового изображения на дефолтный
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

        // Запускаем сиквенс, отвечающий за движение по карте
        var movesSequence = imagesManager.MakeMovesOnBoard(timeManager.intervalPreEveryMove, timeManager.intervalChangeWithEveryIteration, RandomsVariations.SimpleRandomMinMax(movesMin, movesMax));
        mySequence.Append(movesSequence);

        mySequence.Append(imagesManager.PreAnimateChosenCard(imagesManager.currentImg, timeManager.durationPreAnimateChosenCardTick)); // преанимация выбранной карты, скейлы туда-сюда
        mySequence.Append(imagesManager.background.transform.DOScale(0, timeManager.durationBgScaleToZeroWhenCardChosen)) // скейлим бэкграунд в 0
                       .Join(imagesManager.canvasBackgroundImage.DOColor(imagesManager.darkenCanvasBackgroundColor, timeManager.durationBgScaleToZeroWhenCardChosen / 4)); // параллельно затемняем фон

        // Создаём параллельный сиквенс, чтоб анимацию выбранной карты и проигрываемое аудио запустить одновременно
        var parallelSequence = DOTween.Sequence();
        parallelSequence.AppendCallback(() => {
            audioManager.PlayAudioWhenCardChosen(imagesManager.currentImg.GetComponent<ICardData>()); // Запускаем аудио, выбираемое в зависимости от картсета и карты
            imagesManager.AnimateChosenCard(timeManager.durationChosenCardScaleAnimation); // Запускаем параллельно анимацию выбранной карты
        });

        mySequence.Append(parallelSequence);

        // После того, как аудио доиграет, меняем текст основной кнопки, делаем ее активной
        mySequence.OnComplete(() => {
            DOTween.Sequence()
                .AppendInterval(audioManager.ClipToPlayWhenCardChosen.length)
                .AppendCallback(() => {                    
                    IsEventRunning = false;
                    uiManager.BlinkingTextControl();
                });
        });
    }
}
