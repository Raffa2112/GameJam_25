using UnityEngine;

public class LookAtCentre : MonoBehaviour
{
    private void Update()
    {
        Vector3 centreAlignedWithY = new Vector3(
            CentreOfUniverse.Position.X,
            transform.position.y,
            CentreOfUniverse.Position.Z
        );
        transform.LookAt(centreAlignedWithY);
    }
}