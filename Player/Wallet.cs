using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _count;

    public void AddCoin(int value)
    {
        _count += value;
    }
}
