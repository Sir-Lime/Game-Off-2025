using NUnit.Framework;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   [SerializeField] private float followSpeed = 0.125f;
   [SerializeField] private Vector3 offset = new Vector3(10.5f, 1.5f, 0.0f);
   private bool isCollected = false;
   private Transform entityTransform;
   public bool IsCollected { get { return isCollected; } }

    void LateUpdate()
    {
        if(isCollected)
        {
            FollowPlayer();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.gameObject.CompareTag("Player"))
      {
         entityTransform = collision.gameObject.transform;
         isCollected = true;
      }
   }

   public void FollowPlayer()
   {
      Vector3 targetPosition = entityTransform.position + offset;
      Vector3 smoothPosition = Vector3.Lerp(entityTransform.position, targetPosition, followSpeed);
      transform.position = smoothPosition;
   }
}
