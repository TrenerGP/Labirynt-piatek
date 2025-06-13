using UnityEngine;

public class TimeModifier : PickUp
{
    public int time = 10;

    public override void Picked()
    {
        base.Picked();
        GameManager.gameManager.AddTime(time);
    }
}
