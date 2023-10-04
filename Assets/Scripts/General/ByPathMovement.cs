using UnityEngine;

public class ByPathMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _isMove;

    private Vector3 _targetPosition;
    private void Awake()
    {
        _targetPosition = new Vector3(PathHandler.Instance.EndPosition.position.x, transform.position.y, PathHandler.Instance.EndPosition.position.z);
    }
    private void FixedUpdate()
    {
        if (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
    }
    public void StartMove()
    {
        _isMove = true;
    }
}
