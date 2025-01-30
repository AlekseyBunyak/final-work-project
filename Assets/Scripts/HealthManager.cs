using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float _health;

    private bool _isDead = false;
    public bool IsDead => _isDead;

    /// <summary>
    /// deals damage and takes away health
    /// </summary>
    /// <param name="damage">incoming damage</param>
    public void HitDamage(float damage) 
    {
        _health -= damage;

        CheckDead();
    }

    private void CheckDead() 
    {
        if (_health <= 0) 
        {
            _isDead = true;
        }
    }
}
