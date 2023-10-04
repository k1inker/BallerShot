using System;
using UnityEngine;

public class ByPathMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isMove;
    [SerializeField] private Vector3 _offsetForActionPlay;

    private Vector3 _targetPosition;
    public Action OnReachPoint;
    private Vector3 _positionInvokeAction;
    private void OnEnable()
    {
        PlayerHandler.Instance.OnFinishGame += (bool x) => _isMove = false;
    }
    private void OnDisable()
    {
        PlayerHandler.Instance.OnFinishGame -= (bool x) => _isMove = false;
    }
    private void Awake()
    {
        _targetPosition = new Vector3(PathHandler.Instance.EndPosition.position.x, transform.position.y, PathHandler.Instance.EndPosition.position.z);
        _positionInvokeAction = _targetPosition - _offsetForActionPlay;
    }
    private void FixedUpdate()
    {
        if (!_isMove)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);

        if(_positionInvokeAction.x <= transform.position.x && _positionInvokeAction.z <= transform.position.z)
        {
            OnReachPoint?.Invoke();
        }
    }
    public void StartMove()
    {
        _isMove = true;
    }
}
