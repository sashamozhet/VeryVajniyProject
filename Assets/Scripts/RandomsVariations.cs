using UnityEngine;

public class RandomsVariations
{

    // Start is called before the first frame update

    public void Start()
    {

    }
    public static int SimpleRandomMinMax(int min, int max)
    {
        return Random.Range(min, max);
    }
}
