using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField]
    Transform attackZone;

    [SerializeField]
    float attackRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        Collider[] hitEnemies =  Physics.OverlapSphere(attackZone.position, attackRadius);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Es Hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackZone == null)
            return;

        Gizmos.DrawWireSphere(attackZone.position, attackRadius);
    }
}
