using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("[BULLET SETTINGS]")]
    [SerializeField] private float _speed = 0.3f;
    [SerializeField] private float _timeBeforeDestruction = 1.2f;
    [SerializeField] private float _maxScaleMultiplier = 3f;

    private bool _isLaunched;
    private Vector3 _baseScale;

    private void Awake()
    {
        _baseScale = transform.localScale;
    }

    private void Update()
    {
        if (_isLaunched)
            transform.position += transform.up * _speed * Time.deltaTime;
    }

    // t01 : 0 = pas chargée, 1 = charge maximale
    public void Charge(float t01)
    {
        transform.localScale = _baseScale * Mathf.Lerp(1f, _maxScaleMultiplier, t01);
    }

    public void Launch()
    {
        transform.SetParent(null);
        _isLaunched = true;
        Destroy(gameObject, _timeBeforeDestruction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Bullet collide with {collision.collider.gameObject.name}");
    }
}