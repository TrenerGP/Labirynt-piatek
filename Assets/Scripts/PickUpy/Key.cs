using UnityEngine;

public enum KeyColor
{
    Red,
    Green,
    Blue
}

public class Key : PickUp
{
    public KeyColor color;
    public override void Picked()
    {
        base.Picked();
        GameManager.gameManager.AddKey(color);
    }
}
