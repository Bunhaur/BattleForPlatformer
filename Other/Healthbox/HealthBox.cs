using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public readonly int HealthRecovery = 10;

    public void Remove()
    {
        Destroy(gameObject);
    }
}