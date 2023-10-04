using UnityEngine;

public class PathHandler : Singelton<PathHandler>
{
    [field: SerializeField] public Transform StartPosition { get; private set; }
    [field: SerializeField] public Transform EndPosition { get; private set; }

    public PathRender PathRender { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        PathRender = GetComponentInChildren<PathRender>();

        Transform StartPositionOnFloor = StartPosition;
        StartPositionOnFloor.position = new Vector3(StartPosition.position.x, 0.1f, StartPosition.position.z);
        Transform EndPositionOnFloor = EndPosition;
        EndPositionOnFloor.position = new Vector3(EndPosition.position.x, 0.1f, EndPosition.position.z);

        PathRender.SetPositions(StartPositionOnFloor, EndPositionOnFloor);
    }
}
