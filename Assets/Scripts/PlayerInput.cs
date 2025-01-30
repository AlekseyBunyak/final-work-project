using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;

    private float _horizontalDirection;
    private float _verticalDirection;

    private bool _jump;
    private bool _attackOne;

    private void Update()
    {
        _horizontalDirection = Input.GetAxis(GlobalStringVars.HorizontalAxis);
        _verticalDirection = Input.GetAxis(GlobalStringVars.VerticalAxis);
        _jump = Input.GetButtonDown(GlobalStringVars.Jump);
        _attackOne = Input.GetButtonDown(GlobalStringVars.Fire1);

        _playerController.Movement(_horizontalDirection, _verticalDirection, _jump);

        _playerController.Attack(_attackOne);
    }
}
