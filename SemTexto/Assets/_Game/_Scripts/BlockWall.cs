using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockWall : MonoBehaviour
{
    [SerializeField]
    private Item.type typeItem = default;
    [SerializeField]
    private ParticleSystem explosionParticle = default;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            bool usedItem = BagManager.instance.UseItem(typeItem);
            if (usedItem)
            {
                MainCamera.instance.PlayExplosion();
                explosionParticle.Play();
                Destroy(gameObject);
            }
        }
    }
}
