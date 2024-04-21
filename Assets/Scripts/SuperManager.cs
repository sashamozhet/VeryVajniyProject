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
        buttonsManager.EnableOrDisableButtons(false); // ��������� ������
        var mySequence = DOTween.Sequence(); // ������ ������� ���������� ������

        if (imagesManager.CardAnimatedMovedToDisplay)
        {
            mySequence.Append(imagesManager.BoardStateReturner());
        }
        mySequence.Append(imagesManager.ShuffleCardsSequence()); // ��������� � ������� ������������� ��������
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenGameStarted(); });  // ������������� ���� ���� �������                                                       // 
        mySequence.AppendInterval(0.5f); // ��������� 0.5��� �������� ����� ��������� �����
        mySequence.Append(imagesManager.MakeMovesOnBoard(defaultIntervaBetweenMoves, intervalChangeWithEveryIteration, RandomsVariations.SimpleRandomMinMax(movesMin, movesMax))); // ��������� � ������� �����, ������������� �� ��������� � ������
        mySequence.Append(imagesManager.GameObjectScalerSequence(imagesManager.background, 0, 1f)); // ������� ��������� � 0, ������, ��� ������ �������� �������� � ����� ������
        mySequence.AppendInterval(0.5f); // ��������� 0.5��� �������� ����� ��������� �����
        mySequence.AppendCallback(() => { audioManager.PlayAudioWhenCardChosen(); }); // ������������� ���� ���� ��� ����� �������
        mySequence.Append(imagesManager.AnimateChosenCard()); // ������� �������� � ����� ������
        mySequence.Append(buttonsManager.EnableOrDisableButtonsSequence(true)); // �������� ������
    }
}

