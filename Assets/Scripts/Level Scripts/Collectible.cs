using NUnit.Framework;
using playerController;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   [SerializeField] private float followSpeed = 0.125f;
   [SerializeField] private Vector2 offset = new Vector2(3.5f, 1.5f);
   private Transform entityTransform;
   private PlayerController player;
   private bool isCollected = false;
   public bool IsCollected { get { return isCollected; } }

    void Update()
    {
        if(isCollected)
        {
            FollowPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Player") && !isCollected)
      {
         entityTransform = collision.gameObject.transform;
         player = entityTransform.GetComponent<PlayerController>();
         isCollected = true;
         SFXScript.instance.pickUpSFX();
      }
   }

   public void FollowPlayer()
   {
      Vector3 targetPosition = player.FacingDir == 1 ? entityTransform.position + new Vector3(-offset.x, offset.y) : entityTransform.position + new Vector3(offset.x, offset.y);
      Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
      transform.position = smoothPosition;
   }
}
