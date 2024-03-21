using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomsVariations
{

    // Start is called before the first frame update

    public void Start()
    {

    }
    public int SimpleRandomForCardIndex(int min, int max)
    {
        return Random.Range(min, max);
    }
}
