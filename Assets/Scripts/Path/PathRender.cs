using UnityEngine;

public class PathRender : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    public void SetPositions(Transform startPosition, Transform endPosition)
    {
        _lineRenderer.SetPositions(new Vector3[] { startPosition.position, endPosition.position });
    }
    public void SetLineWidth(float width)
    {
        _lineRenderer.startWidth = width;
        _lineRenderer.endWidth = width;
    }
}
