using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
[RequireComponent (typeof(Animator))]
public class BulletAnimatorController : MonoBehaviour
{
    private DamageDealer _damageDealer;   
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damageDealer = GetComponent<DamageDealer>();
    }

    private void Update()
    {
        _animator.SetBool("Hit", _damageDealer.Hit);
    }
}
