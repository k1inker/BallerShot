using UnityEngine;

public class DoorHandler : Singelton<DoorHandler> 
{
    [SerializeField] private Animator _animator;
    public void OpenDoor()
    {
        _animator.Play("DoorOpen");
    }
}
