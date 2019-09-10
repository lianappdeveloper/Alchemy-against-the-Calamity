using System.Collections;
using UnityEngine;

public class StunningBullet : projectile
{
    public override void HitTarget()
    {
        if (target != null)
        {
            target.gameObject.GetComponent<EnemyStats>().GetStunnedBitch();
            Destroy(gameObject);
        }
    }
}