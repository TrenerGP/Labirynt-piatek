using UnityEngine;

public class PickUp : MonoBehaviour
{
    protected float y_pos = 0;
    protected bool up = true;

    public virtual void Picked()
    {
        Debug.Log("Pickup collected!");
        Destroy(this.gameObject);
    }

    protected void Rotation()
    {
        if (transform.position.y < 1.45f && up)
            y_pos = 0.3f;
        else if (transform.position.y > 0.9f && !up)
            y_pos = -0.3f;
        else
            up = !up;

            transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y+y_pos*Time.deltaTime,
            transform.position.z
            ); 
    }

    protected void Update()
    {
        Rotation();
    }
}
