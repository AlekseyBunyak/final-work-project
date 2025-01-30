using UnityEngine;
using System;
using System.Collections;

public class Shooter : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bullet;

    [Header("Vars")]
    [SerializeField] private float _fireForce;
    [SerializeField] private float _verticalDeviation;
    [SerializeField] private int _ammo;
    [SerializeField] private float _reloadTimer;
    [SerializeField] private float _cooldown = 0.2f;

    private bool _attackAnimated = false;
    private bool _noShot = false;

    private float _currentReload;
    private int _currentAmmo;

    public bool NoShot { get { return _noShot; } set { _noShot = value; } }
    public bool AttackAnimated => _attackAnimated;  
    public int Ammo => _ammo;
    public float ReloadTimer => _reloadTimer;

    /// <summary>
    /// transmits the number of bullets.
    /// </summary>
    public static Action<int> OnShoot;

    private void Start()
    {
        _currentAmmo = _ammo;
        _currentReload = _reloadTimer;
    }

    private void Update()
    {
        Reloading();
    }

    #region(public methods)
    /// <summary>
    /// Creates a shot
    /// </summary>
    /// <param name="scaleOrientation">Transform.scale.x: 1 or -1</param>
    public void Shoot(float scaleOrientation) 
    {
        if (!_attackAnimated && _ammo > 0 && !NoShot) 
        {
            _attackAnimated = true;

            if (scaleOrientation > 0)
            {
                _bullet.gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                _bullet.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            GameObject bullet = Instantiate(_bullet, _firePoint.transform.position, Quaternion.identity);
            Rigidbody2D bulletVelocity = bullet.GetComponent<Rigidbody2D>();

            if (scaleOrientation > 0)
            {
                bulletVelocity.velocity = new Vector2(_fireForce, bulletVelocity.velocity.y + _verticalDeviation);
            }
            else
            {
                bulletVelocity.velocity = new Vector2(-_fireForce, bulletVelocity.velocity.y + _verticalDeviation);
            }

            _ammo--;

            OnShoot?.Invoke(_ammo);

            StartCoroutine(ShotCooldown());
        }        
    }

    /// <summary>
    /// disable or enable shot
    /// </summary>
    public void SetNoShoot() 
    {
        _noShot = !_noShot;
    }

    #endregion

    #region(private methods)

    private void Reloading()
    {
        if (_ammo <= 0)
        {
            if (_reloadTimer > 0)
            {
                _reloadTimer -= Time.deltaTime;
            }
            else
            {
                _reloadTimer = _currentReload;
                _ammo = _currentAmmo;

                OnShoot?.Invoke(_ammo);
            }
        }
    }

    #endregion

    IEnumerator ShotCooldown()
    {
        yield return new WaitForSeconds(_cooldown);

        _attackAnimated = false;
    }
}


