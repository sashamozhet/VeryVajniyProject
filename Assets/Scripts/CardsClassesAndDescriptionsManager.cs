using System.Collections.Generic;
using UnityEngine;

public class CardsClassesAndDescriptionsManager : MonoBehaviour
{
    [SerializeField] int CardClass;
    [SerializeField] string CardDescription;



    public int GetCardClass()
    {
        return CardClass;
    }

    public string GetCardDescription()
    {
        return CardDescription;
    }
}
