/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using UnityEngine;

namespace CarterGames.Starshine
{
    public class ParticlesColoursPass : MonoBehaviour
    {

        private ParticleSystem particles;
        private Gradient grad;
        private Color col;

        void Start()
        {
            col = GetComponentInParent<SpriteRenderer>().color;

            // gets the partlce system
            particles = GetComponent<ParticleSystem>();

            // does stuff with it to update the colours xD
            var trail = particles.trails;

            grad.SetKeys(
                new GradientColorKey[] { new GradientColorKey(col, 0f), new GradientColorKey(col, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
                );

            trail.colorOverTrail = grad;
        }
    }
}