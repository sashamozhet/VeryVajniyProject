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
        GameStarted += GameProcessStarted; // ����������� ����� ������ ���� �� �������
        //buttonsManager.startRandomingButton.onClick.AddListener(StartGameProcess); // ��������� ���������� ������ ������ ����
    }

    public void StartGameProcess()
    {
        GameStarted?.Invoke();
    }

    public void GameProcessStarted()
    {   
        buttonsManager.EnableOrDisableButtons(false); // ��������� ������
        var mySequence = DOTween.Sequence(); // ������ ������� ���������� ������

        if (imagesManager.CardAnimatedMovedToDisplay)
        {
            mySequence.AppendCallback(() => { imagesManager.background.SetActive(false); }); // ��������� ��������� ���������
            mySequence.Append(imagesManager.backgroundAlwaysSorted.transform.DOScale(0, 0.000001f)); // ������� � 0 ���������� �������� ���������
            mySequence.AppendCallback(() => { imagesManager.backgroundAlwaysSorted.SetActive(true); }); // �������� �������� ���������
            mySequence.Append(imagesManager.backgroundAlwaysSorted.transform.DOScale(1, timeManager.durationSortedBgScaleTime)).AppendInterval(timeManager.intervalAfterSortedBgScaled); // ������� �������� ��������� � 1, ��������� ��� �����
            mySequence.Append(imagesManager.BoardStateReturner()); // ���������� ��������� ���������� �����������                      
            mySequence.AppendCallback(() => { imagesManager.background.SetActive(true); }); // �������� ��������� ���������
            mySequence.AppendCallback(() => { imagesManager.backgroundAlwaysSorted.SetActive(false); }); // ��������� �������� ���������
        }
        
        mySequence.Append(imagesManager.ShuffleCardsSequence()); // ��������� � ������� ������������� ��������
        mySequence.Append(imagesManager.background.transform.DOScale(1.03f, timeManager.durationBgPreGameTickAnimation).SetLoops(4, LoopType.Yoyo)); // ������� ��������� ����-����
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenGameStarted(); });  // ������������� ���� ���� �������                                                        
        mySequence.AppendInterval(timeManager.intervalAfterCardsShuffle); // ��������� �������� ����� ��������� �����
        mySequence.Append(imagesManager.MakeMovesOnBoard(timeManager.intervalPreEveryMove, timeManager.intervalChangeWithEveryIteration, RandomsVariations.SimpleRandomMinMax(movesMin, movesMax))); // ��������� � ������� �����, ������������� �� ��������� � ������
       
        mySequence.Append(imagesManager.background.transform.DOScale(0, timeManager.durationBgScaleToZeroWhenCardChosen)); // ������� ��������� � 0, ������, ��� ������ �������� �������� � ����� ������
        mySequence.AppendInterval(timeManager.intervalPreChosenCardShown); // ��������� �������� ����� ��������� �����
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenCardChosen(); }); // ������������� ���� ���� ��� ����� �������
        mySequence.Append(imagesManager.AnimateChosenCard(timeManager.durationChosenCardScaleAnimation)); // ������� �������� � ����� ������
        mySequence.Append(buttonsManager.EnableOrDisableButtonsSequence(true)); // �������� ������
    }
}

