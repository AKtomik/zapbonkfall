using UnityEngine;
public enum ClickType { Tap, Hold, Lift, None }
public enum ShooterMode { SubMachineGun, ChargeImpact, None}

public class ShootingController : MonoBehaviour
{
    [Header("[SHOOTER SETTINGS]")]
    [SerializeField] ShooterMode _shooterMode = ShooterMode.SubMachineGun;
    [SerializeField] private Transform _firePoint;

    [Header("-> Hold")]
    [SerializeField] private float _holdFireRate = 0.2f;
    [SerializeField] private float _maxHoldTimer = 1f;

    [Header("[BULLETS TYPES]")]
    [SerializeField] private GameObject _simpleBullet;

    private float _holdDuration;
    private float _holdShotTimer;


    private Bullet _currentBullet;

    bool onlyOne = false;


    private void Update()
    {
        ShootType(DetectClickType());
    }

    private ClickType DetectClickType()
    {
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
    private void TapShoot()
    {
        if (Time.time >= _holdShotTimer + _holdFireRate)
        {
            if (_shooterMode == ShooterMode.SubMachineGun)
            {
                SpawnBullet().Launch();
                _holdShotTimer = Time.time + _holdFireRate;
            }
            else if (_shooterMode == ShooterMode.ChargeImpact)
            {
                _currentBullet = SpawnBullet();
                _currentBullet.transform.SetParent(_firePoint, true);
                _holdShotTimer = Time.time + _holdFireRate;
            }
        }

        _holdDuration = 0f;
    }

    private void HoldShoot()
    {
        _holdDuration += Time.deltaTime;

        if (_shooterMode == ShooterMode.SubMachineGun)
        {
            if (Time.time >= _holdShotTimer)
            {
                SpawnBullet().Launch();
                _holdShotTimer = Time.time + _holdFireRate;
            }
        }
        else if (_shooterMode == ShooterMode.ChargeImpact && _currentBullet != null)
        {
            _currentBullet.Charge(Mathf.Clamp01(_holdDuration / _maxHoldTimer));
        }
    }

    private void LiftShoot()
    {
        if (_shooterMode == ShooterMode.ChargeImpact && _currentBullet != null)
        {
            _currentBullet.Launch();
            _currentBullet = null;
        }
        _holdDuration = 0f;
    }

    private Bullet SpawnBullet()
    {
        GameObject go = Instantiate(_simpleBullet, _firePoint.position, _firePoint.rotation);
        return go.GetComponent<Bullet>();
    }
}