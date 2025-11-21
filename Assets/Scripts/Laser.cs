using UnityEngine;

public class Laser : MonoBehaviour
{
   [SerializeField] private float defaultDistance = 100f;
   [SerializeField] private Transform laserFirePoint;
   [SerializeField] private LineRenderer lineRender;
   private Level level;

    void Start()
    {
        level = FindFirstObjectByType<Level>();
    }

    void FixedUpdate()
    {
        ShootLaser();
    }

    private void ShootLaser()
    {
        if(Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D hit2D = Physics2D.Raycast(laserFirePoint.position, transform.right);
            Draw2DRay(laserFirePoint.position, hit2D.point);

            if(hit2D.collider != null)
            {
                if(hit2D.collider.gameObject.CompareTag("Player")) 
                    level.KillPlayer();
            }
        }
        else
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defaultDistance);
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRender.SetPosition(0, startPos);
        lineRender.SetPosition(1, endPos);
    }
}
