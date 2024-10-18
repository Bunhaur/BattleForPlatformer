using UnityEngine;

public class PlayerHealth : Health
{
    public void AddHealth(int value)
    {
        _health = Mathf.Clamp(_health += value, _minHealth, _maxHealth);
    }
}