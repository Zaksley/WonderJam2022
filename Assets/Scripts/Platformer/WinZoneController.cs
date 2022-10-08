using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WinZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<PlayerController>();

        if (player != null)
            Debug.Log("WINNER");
    }
}