using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    [SerializeField] private float _timeAllive = 1f;
    [SerializeField] private Collider _collider;
    
    private Renderer _material;
    private void Awake()
    {
        _material = GetComponent<Renderer>();
    }
    public void TakeGamage(Material materialInfection)
    {
        _material.sharedMaterial = materialInfection;
        _collider.enabled = false;
        Destroy(gameObject, _timeAllive);
    }
}
