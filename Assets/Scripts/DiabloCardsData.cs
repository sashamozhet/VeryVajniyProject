using System.Collections.Generic;
using UnityEngine;

public class DiabloCardsData : MonoBehaviour, ICardData
{
    [SerializeField] int CardCategory;
    [SerializeField] string CardDescription;



    public int GetCardCategory()
    {
        return CardCategory;
    }

    public string GetCardDescription()
    {
        return CardDescription;
    }
}
