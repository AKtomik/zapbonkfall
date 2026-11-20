using UnityEngine;
public enum ClickType { Tap, Hold, Lift, None }

public class ShootingController : MonoBehaviour
{
    [Header("[REFERENCES]")]
    [SerializeField] private ControllerSettings _settings;
    [SerializeField] private Transform _firePoint;

    [Header("[BULLETS TYPES]")]
    [SerializeField] private GameObject _simpleBullet;

    [Header("[TANK SETTINGS]")]
    [SerializeField] private float _rotationSpeed = 6f;
    [Tooltip("General fire rate for all tap touch")]
    [SerializeField] private float _tapFireRate = 0.3f;

    [Header("-> SubMachine Gun")]
    [SerializeField] private float _subMachineGunfireRate = 0.2f;

    [Header("-> Charge Gun")]
    [SerializeField] private float _maxChargeDuration = 1f;

    private ShooterMode _shooterMode;
    private Bullet _currentBullet;
    private float _chargeDuration;
    private float _nextShotTimer;
    private Vector2 _targetDirection;

    void Awake()
    {
        _shooterMode = _settings.ShooterMode;
        _targetDirection = transform.up; /* Initialize position */
    }

    private void Update()
    {
        //Apply Rotation
        transform.up = Vector3.RotateTowards(transform.up, _targetDirection, _rotationSpeed * Time.deltaTime, 0f);
    }

    // --- Public calls (called by Controllers) --- //
    public void SetDirection(Vector2 direction) /* To rotate tank */
    {
        if (direction.sqrMagnitude > 0.0001f) _targetDirection = direction;
    }
    public void ClickHandler(ClickType click)
    {
        switch (click)
        {
            case ClickType.None: break;
            case ClickType.Tap: TapShoot(); break;
            case ClickType.Hold: HoldShoot(); break;
            case ClickType.Lift: LiftShoot(); break;
        }
    }

    // --- Shoots types --- //
    private void TapShoot()
    {
        _chargeDuration = 0f;

        if (_shooterMode == ShooterMode.SubMachineGun)
        {
            Shoot(_tapFireRate);
        }
        else if (_shooterMode == ShooterMode.ChargeGun)
        {
            if (Time.time < _nextShotTimer) return;

            _currentBullet = SpawnBullet();
            _currentBullet.transform.SetParent(_firePoint, true);
            _nextShotTimer = Time.time + _tapFireRate;
        }
    }

    private void HoldShoot()
    {
        if (_shooterMode == ShooterMode.SubMachineGun)
        {
            Shoot(_subMachineGunfireRate);
        }
        else if (_shooterMode == ShooterMode.ChargeGun && _currentBullet != null)
        {
            _chargeDuration += Time.deltaTime;
            _currentBullet.Charge(Mathf.Clamp01(_chargeDuration / _maxChargeDuration));
        }
    }

    private void LiftShoot()
    {
        if (_shooterMode == ShooterMode.ChargeGun && _currentBullet != null)
        {
            _currentBullet.Launch();
            _currentBullet = null;
        }
        _chargeDuration = 0f;
    }

    // --- General methods --- //

    private void Shoot(float fireRate) // Fire rate guard
    {
        if (Time.time < _nextShotTimer) return;

        SpawnBullet().Launch();
        _nextShotTimer = Time.time + fireRate;
    }
    private Bullet SpawnBullet()
    {
        GameObject go = Instantiate(_simpleBullet, _firePoint.position, _firePoint.rotation);
        return go.GetComponent<Bullet>();
    }
}