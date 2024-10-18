using UnityEngine;

[RequireComponent(typeof(Wallet))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Jumper))]
[RequireComponent(typeof(InputService))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    public readonly int Damage = 15;

    private PlayerMovement _movementController;
    private Jumper _jumpController;
    private InputService _inputService;
    private Wallet _wallet;
    private Health _health;
    private float _horizontal;

    private void Awake()
    {
        _movementController = GetComponent<PlayerMovement>();
        _jumpController = GetComponent<Jumper>();
        _inputService = GetComponent<InputService>();
        _wallet = GetComponent<Wallet>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Dead += Die;
    }

    private void OnDisable()
    {
        _health.Dead -= Die;
    }

    private void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            TakeCoin(coin);
        else if (collision.TryGetComponent(out HealthBox healthbox))
            TakeHealthBox(healthbox);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
            TakeHit(enemy);
    }

    private void Move()
    {
        _horizontal = _inputService.GetHorizontal();
        _movementController.Move(_horizontal);
    }

    private void TakeHit(Enemy enemy)
    {
        _jumpController.Jump();
        _health.ChangeHealth(-enemy.Damage);
    }

    private void Flip()
    {
        if (_horizontal != 0)
            _movementController.FlipSprite(_horizontal);
    }

    private void TakeHealthBox(HealthBox healthBox)
    {
        _health.ChangeHealth(healthBox.HealthRecovery);
        healthBox.Remove();
    }

    private void TakeCoin(Coin coin)
    {
        _wallet.AddCoin(coin.Price);
        coin.Remove();
    }

    private void Jump()
    {
        if (_inputService.IsPushJump() == true)
            _jumpController.Jump();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}