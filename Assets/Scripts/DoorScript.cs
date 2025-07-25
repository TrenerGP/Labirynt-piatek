using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public Transform closePosition;
    public Transform openPosition;
    public Transform door;
    public bool open = false;
    public float speed = 1.0f;

    private void Start()
    {
        door.position = closePosition.position;
    }

    private void Update()
    {
        if (open)
        {
            door.position = Vector3.MoveTowards(
                door.position, openPosition.position, speed * Time.deltaTime);
        }
    }

    public void Open()
    {
        open = true;
    }
}
