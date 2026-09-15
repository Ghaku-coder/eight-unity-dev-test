using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Transform GroundCheckOrigin;
    [SerializeField] private LayerMask GroundMask;

    [SerializeField] private Vector3 boxHalfExtents = new Vector3(0.3f, 0.1f, 0.3f);
    [SerializeField] private float checkOffset = 0.1f; // hạ tâm box xuống một chút để trùm qua vùng chân-đất

    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        Vector3 center = GroundCheckOrigin.position + Vector3.down * checkOffset;

        IsGrounded = Physics.CheckBox(
            center,
            boxHalfExtents,
            Quaternion.identity,
            GroundMask
        );
    }

    private void OnDrawGizmosSelected()
    {
        if (GroundCheckOrigin == null) return;

        Vector3 center = GroundCheckOrigin.position + Vector3.down * checkOffset;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(center, boxHalfExtents * 2f);
    }
}