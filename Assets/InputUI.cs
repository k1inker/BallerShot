using System;
using UnityEngine;

public class InputUI : Singelton<InputUI>
{
    public Action OnPointDown;
    public Action OnPointUp;

    public void PointDown()
    {
        OnPointDown?.Invoke();
    }
    public void PointUp()
    {
        OnPointUp?.Invoke();
    }
}
