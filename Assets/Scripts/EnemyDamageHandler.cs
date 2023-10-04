using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    [SerializeField] private float _timeAllive = 1f;
    private Renderer _material;
    private Collider _collider;
    private void Awake()
    {
        _material = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }
    public void TakeGamage(Material materialInfection)
    {
        _material.sharedMaterial = materialInfection;
        _collider.enabled = false;
        Destroy(gameObject, _timeAllive);
    }
}
