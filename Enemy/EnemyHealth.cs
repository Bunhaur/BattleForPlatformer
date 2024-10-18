using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _minHealth = 0;
    private int _health = 100;
    private bool _canDamaged = true;
    private float _timeout = .5f;
    private Coroutine _timeoutDamageWork;

    public event Action Dead;

    public void Hit(int value)
    {
        if (_canDamaged)
            _health = Mathf.Clamp(_health -= value, _minHealth, _maxHealth);

        _timeoutDamageWork = StartCoroutine(TimeoutDamage());

        if (_health == 0)
            Dead?.Invoke();
    }

    private IEnumerator TimeoutDamage()
    {
        _canDamaged = false;

        yield return new WaitForSeconds(_timeout);

        _canDamaged = true;
    }
}
