using UnityEngine;

public class Freeze : PickUp
{
    public int time = 10;

    public override void Picked()
    {
        base.Picked();
        GameManager.gameManager.FreezeTime(time);
    }
}
