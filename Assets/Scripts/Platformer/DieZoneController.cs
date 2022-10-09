using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DieZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {        
        var player = col.gameObject.GetComponent<PlayerController>();
        var explodable = col.gameObject.GetComponent<RespawnController>();

        if (player != null)
            player.Die();
        
        if (explodable != null)
            explodable.Explode();
    }
}
