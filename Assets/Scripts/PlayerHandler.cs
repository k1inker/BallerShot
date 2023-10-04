using System;
using System.Collections;
using UnityEngine;

public class PlayerHandler : Singelton<PlayerHandler>
{
    public Action<bool> OnFinishGame;

    [Header("Setup projectile")]
    [SerializeField] private float _scaleBySecond;
    [SerializeField] private float _criticalRadius;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private Transform _spawnPoint;

    [Header("General")]
    [SerializeField] private ByPathMovement _movement;
    [SerializeField] private Transform _modelPlayer;

    private bool _isCanShoot = true;
    private GameObject _currentProjectile;
    private Coroutine _increaseSizeCorutine;
    private void OnEnable()
    {
        UIManager.Instance.OnPointDown += ChargingFire;
        UIManager.Instance.OnPointUp += Shoot;
        _movement.OnReachPoint += () => DoorHandler.Instance.OpenDoor();
        _movement.OnReachPoint += () => StartCoroutine(FinishGameWithDelay());
    }
    private void OnDisable()
    {
        UIManager.Instance.OnPointDown -= ChargingFire;
        UIManager.Instance.OnPointUp -= Shoot;
        _movement.OnReachPoint -= () => DoorHandler.Instance.OpenDoor();
        _movement.OnReachPoint -= () => StartCoroutine(FinishGameWithDelay());
    }
    public void OnProjectileDestroy()
    {
        _isCanShoot = true;
        CheckPath();
    }
    private void ChargingFire()
    {
        if (!_isCanShoot)
            return;

        _currentProjectile = Instantiate(_spawnPrefab.gameObject, _spawnPoint.transform.position, Quaternion.identity);
        _increaseSizeCorutine = StartCoroutine(IncreaseSize());
        _isCanShoot = false;
    }
    private void Shoot()
    {
        if (_increaseSizeCorutine == null)
            return;

        _currentProjectile.GetComponent<ByPathMovement>().StartMove();
        StopCoroutine(_increaseSizeCorutine);
        _increaseSizeCorutine = null;
    }
    private IEnumerator IncreaseSize()
    {
        while (true)
        {
            yield return new WaitForSeconds(.1f);

            Vector3 currentProjScale = _currentProjectile.gameObject.transform.localScale;
            Vector3 currentScalePlayer = _modelPlayer.transform.localScale;

            currentProjScale = Vector3.Lerp(currentProjScale, currentProjScale + Vector3.one * _scaleBySecond, Time.deltaTime);
            currentScalePlayer = Vector3.Lerp(currentScalePlayer, currentScalePlayer - Vector3.one * _scaleBySecond, Time.deltaTime);

            _currentProjectile.gameObject.transform.localScale = currentProjScale;
            _modelPlayer.transform.localScale = currentScalePlayer;

            PathHandler.Instance.PathRender.SetLineWidth(currentScalePlayer.x);

            if (_criticalRadius >= currentScalePlayer.x)
            {
                Shoot();
            }
        }
    }
    private void CheckPath()
    {
        Vector3 startPosition = PathHandler.Instance.StartPosition.position;
        Vector3 endPosition = PathHandler.Instance.EndPosition.position;
        // Создаем луч от начальной точки до конечной точки линии
        Ray ray = new Ray(startPosition, endPosition - startPosition);

        // Получаем все объекты, которые пересекают луч
        RaycastHit[] hits = Physics.SphereCastAll(ray, _modelPlayer.transform.localScale.x / 2f, Vector3.Distance(startPosition, endPosition), _enemyMask);

        if (hits.Length == 0)
        {
            _isCanShoot = false;
            GetComponent<ByPathMovement>().StartMove();
        }
        else if(_modelPlayer.transform.localScale.x < _criticalRadius)
        {
            OnFinishGame?.Invoke(false);
        }
    }
    private IEnumerator FinishGameWithDelay()
    {
        yield return new WaitForSeconds(1f);
        OnFinishGame?.Invoke(true);
    }
}
