using System.Collections;
using UnityEngine;

public class FireHandler : Singelton<FireHandler>
{
    [Header("Setup proj")]
    [SerializeField] private float _scaleBySecond;
    [SerializeField] private float _criticalRadius;
    [SerializeField] private LayerMask _enemyMask;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _modelPlayer;
    [SerializeField] private GameObject _spawnPrefab;

    private bool _isCanShoot = true;
    private GameObject _currentProjectile;
    private Coroutine _increaseSizeCorutine;
    private void OnEnable()
    {
        InputUI.Instance.OnPointDown += ChargingFire;
        InputUI.Instance.OnPointUp += Shoot;
    }
    private void OnDisable()
    {
        InputUI.Instance.OnPointDown -= ChargingFire;
        InputUI.Instance.OnPointUp -= Shoot;
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

            if(_criticalRadius >= currentScalePlayer.x)
            {
                Debug.Log("You loose");
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
        RaycastHit[] hits = Physics.SphereCastAll(ray, _modelPlayer.transform.localScale.x / 2f, Vector3.Distance(startPosition, endPosition),_enemyMask);

        if(hits.Length == 0)
        {
            GetComponent<ByPathMovement>().StartMove();
        }
    }
}
