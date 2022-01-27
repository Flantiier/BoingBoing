using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    #region Variables
    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float turnSmoothValue;
    [SerializeField] private float speedLerp;

    private Vector3 _movement;
    private Vector3 _direction;
    private float _moveX;
    private float _moveY;
    private float _verticalSpeed;

    private float _currentVelocity;
    private Vector3 _refSmooth;
    private Vector3 _originalPosition;

    private CharacterController _cc;
    #endregion

    #region Properties
    public Vector3   OriginalPosition
    {
        get { return _originalPosition; }
        set { _originalPosition = value; }
    }
    #endregion

    #region BuildIn Methods
    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _originalPosition = transform.position;
    }

    void Update()
    {
        MovePlayer();
    }
    #endregion

    #region Customs Methods
    private void MovePlayer()
    {

        _moveX = Input.GetAxis("Horizontal");
        _moveY = Input.GetAxis("Vertical");

        Vector3 inputs = new Vector3(_moveX, 0f, _moveY);

        Vector3 dir = Vector3.SmoothDamp(_direction, inputs, ref _refSmooth, speedLerp);

        Vector3 moveDir = new Vector3();

        if (dir.normalized.magnitude != 0)
        {
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, angle, ref _currentVelocity, turnSmoothValue);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            //on fait aller le joueur vers la direction qu'il regarde
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        _movement = moveDir.normalized * (speed * Time.deltaTime);

        if (_cc.isGrounded)
        {
            _verticalSpeed = -gravity * 0.25f;

            _verticalSpeed = jumpHeight;
        }
        else
        {
            _verticalSpeed -= gravity * Time.deltaTime;
        }

        _movement += _verticalSpeed * Vector3.up * Time.deltaTime;

        _cc.Move(_movement);
    }
    #endregion
}
