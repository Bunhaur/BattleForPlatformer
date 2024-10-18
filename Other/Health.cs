using System;
using System.Collections;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int _maxHealth = 100;
    [SerializeField] protected int _health = 85;

    protected int _minHealth;
    protected bool _canBeDamaged = true;
    protected float _timeout = .5f;
    protected Coroutine _timeoutDamageWork;

    private WaitForSeconds _wait;

    public event Action Dead;

    private void Awake()
    {
        _wait = new WaitForSeconds(_timeout);
    }

    public void DicreaseHealth(int value)
    {
        if (_canBeDamaged)
        {
            _health = Mathf.Clamp(_health -= value, _minHealth, _maxHealth);
            _timeoutDamageWork = StartCoroutine(TimeoutDamage());

            if (_health == _minHealth)
            {
                Dead?.Invoke();
            }
        }
    }

    private IEnumerator TimeoutDamage()
    {
        _canBeDamaged = false;

        yield return _wait;

        _canBeDamaged = true;
    }
}