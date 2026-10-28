using UnityEngine;
public enum ClickType { Tap, Hold, Lift, None }
public enum ShooterMode { SubMachineGun, ChargeGun, None }

public class ShootingController : MonoBehaviour
{
    [Header("[SHOOTER SETTINGS]")]
    [SerializeField] ShooterMode _shooterMode = ShooterMode.SubMachineGun;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _rotationSpeed = 6f;

    [Header("-> SubMachine Gun")]
    [SerializeField] private float _fireRate = 0.2f;

    [Header("-> Charge Gun")]
    [SerializeField] private float _maxChargeDuration = 1f;

    [Header("[BULLETS TYPES]")]
    [SerializeField] private GameObject _simpleBullet;

    private Camera _camera;
    private Bullet _currentBullet;

    private float _chargeDuration;
    private float _nextShotTimer;
    private Vector2 _direction;

    void Awake() 
    { 
        _camera = Camera.main; 
    }

    private void Update()
    {
        ShootType(DetectClickType());
        transform.up = Vector3.RotateTowards(transform.up, _direction, _rotationSpeed * Time.deltaTime, 0f);
    }

    private ClickType DetectClickType()
    {
        foreach (Touch touch in InputManager.GetClickTaps()) { CanonRotation(touch.position); }

        if (InputManager.GetClickTaps().Length > 0) return ClickType.Tap;
        if (InputManager.GetClickHolds().Length > 0) return ClickType.Hold;
        if (InputManager.GetClickLifts().Length > 0) return ClickType.Lift;


        return ClickType.None;
    }

    private void ShootType(ClickType clickType)
    {
        switch (clickType)
        {
            case ClickType.None:
                break;
            case ClickType.Tap:
                TapShoot();
                break;
            case ClickType.Hold:
                HoldShoot();
                break;
            case ClickType.Lift:
                LiftShoot();
                break;
        }
    }

    private void CanonRotation(Vector2 screenPos)
    {
        _direction = _camera.ScreenToWorldPoint(screenPos) - transform.position;
    }

    private void TapShoot()
    {
        if (Time.time >= _nextShotTimer + _fireRate)
        {
            if (_shooterMode == ShooterMode.SubMachineGun)
            {
                SpawnBullet().Launch();
                _nextShotTimer = Time.time + _fireRate;
            }
            else if (_shooterMode == ShooterMode.ChargeGun)
            {
                _currentBullet = SpawnBullet();
                _currentBullet.transform.SetParent(_firePoint, true);
                _nextShotTimer = Time.time + _fireRate;
            }
        }

        _chargeDuration = 0f;
    }

    private void HoldShoot()
    {
        _chargeDuration += Time.deltaTime;

        if (_shooterMode == ShooterMode.SubMachineGun)
        {
            if (Time.time >= _nextShotTimer)
            {
                SpawnBullet().Launch();
                _nextShotTimer = Time.time + _fireRate;
            }
        }
        else if (_shooterMode == ShooterMode.ChargeGun && _currentBullet != null)
        {
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

    private Bullet SpawnBullet()
    {
        GameObject go = Instantiate(_simpleBullet, _firePoint.position, _firePoint.rotation);
        return go.GetComponent<Bullet>();
    }
}