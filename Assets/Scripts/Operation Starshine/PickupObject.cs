using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

namespace CarterGames.Starshine
{
    public class PickupObject : MonoBehaviour
    {
        public enum PickupTypes
        {
            Health,
            Shield,
        };

        public PickupTypes Type;
        public int Value;
        BoxCollider2D BC;
        public AudioManager AM;

        private void Start()
        {
            // references the boxcollider on the gameobject
            BC = GetComponent<BoxCollider2D>();
            AM = FindObjectOfType<DialogueScript>().AM;
        }

        private void OnEnable()
        {
            // re-enabled the trigger box so it can be picked up again
            if (BC)
            {
                if (!BC.enabled)
                {
                    BC.enabled = true;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // if it hits a player
            if (collision.gameObject.GetComponent<PlayerController>())
            {
                // Add the value to the type of stat it increments
                switch (Type)
                {
                    case PickupTypes.Health:
                        collision.gameObject.GetComponent<PlayerController>().Health += Value;
                        collision.gameObject.GetComponent<ShipManagement>().PlayerStats.healthGained += Value;
                        AM.Play("Pickup", .5f);
                        BC.enabled = false;
                        break;
                    case PickupTypes.Shield:
                        collision.gameObject.GetComponent<PlayerController>().Shield += Value;
                        collision.gameObject.GetComponent<ShipManagement>().PlayerStats.shieldGained += Value;
                        AM.Play("Pickup", .5f);
                        BC.enabled = false;
                        break;
                    default:
                        break;
                }

                // disables the object after running the above code
                gameObject.SetActive(false);
            }
        }
    }
}