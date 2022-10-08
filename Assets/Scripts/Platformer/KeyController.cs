using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class KeyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.PlayerGotKey();
            Destroy(gameObject); 
        }
    }
}
