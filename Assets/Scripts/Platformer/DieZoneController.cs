using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DieZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {        
        var player = col.gameObject.GetComponent<PlayerController>();

        if (player != null)
            player.Die();
    }
}
