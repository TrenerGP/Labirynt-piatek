using UnityEngine;

public class PickUp : MonoBehaviour
{
    protected float y_pos = 0;
    protected bool up = true;

    // Enum do wyboru osi obrotu
    public enum RotationAxis { X, Y, Z }
    public RotationAxis rotationAxis = RotationAxis.Y;

    // Prêdkoœæ obrotu (mo¿na te¿ zmieniæ w Inspectorze)
    public float rotationSpeed = 50f;

    public virtual void Picked()
    {
        Debug.Log("Pickup collected!");
        Destroy(this.gameObject);
    }

    protected void Rotation()
    {
        // P³ywaj¹cy ruch góra/dó³
        if (transform.position.y < 1.45f && up)
            y_pos = 0.3f;
        else if (transform.position.y > 0.9f && !up)
            y_pos = -0.3f;
        else
            up = !up;

        // Obrót w zale¿noœci od wybranej osi
        Vector3 rotationVector = Vector3.zero;
        switch (rotationAxis)
        {
            case RotationAxis.X:
                rotationVector = new Vector3(rotationSpeed * Time.deltaTime, 0, 0);
                break;
            case RotationAxis.Y:
                rotationVector = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                break;
            case RotationAxis.Z:
                rotationVector = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
                break;
        }

        transform.Rotate(rotationVector);

        // P³ywanie w górê i w dó³
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y + y_pos * Time.deltaTime,
            transform.position.z
        );
    }

    protected void Update()
    {
        Rotation();
    }
}
