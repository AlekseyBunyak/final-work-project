using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(PlayerController))]
[RequireComponent(typeof(SurfaceChecker))]
[RequireComponent(typeof(Shooter))]
public class CharacterAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private SurfaceChecker _surfaceChecker;
    private Shooter _shooter;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _surfaceChecker = GetComponent<SurfaceChecker>();
        _shooter = GetComponent<Shooter>();
    }

    private void Update()
    {
        _animator.SetBool("IsWalk", _playerController.IsMoving);
        _animator.SetBool("IsGrounde", _surfaceChecker.IsGrounded);
        _animator.SetBool("IsAttack", _shooter.AttackAnimated);
        _animator.SetBool("DownAnimated", _playerController.DownAnimated);
        _animator.SetBool("IsLadder", _playerController.IsLadder);
        _animator.SetFloat("Velo.y", _playerController.VelosityUp);
    }
}
