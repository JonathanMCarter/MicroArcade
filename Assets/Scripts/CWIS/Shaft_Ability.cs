using CarterGames.Assets.AudioManager;
using System.Collections;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.CWIS
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(ParticleSystem))]
    public class Shaft_Ability : MonoBehaviour
    {
        public GameManager.Ranks currentRank;
        [SerializeField] private FlickerButton flicker;
        private int timesUsed = 0;

        private bool isCoR = false;
        private ParticleSystem ps;
        private CapsuleCollider cc;
        [SerializeField] private CWIS_Controller.Controller thisControl;
        private CWIS_Controller control;

        private int[] rankUpRequirements;

        public float duration = 1.5f;
        public float size = 1;

        private bool useAbility = false;
        public bool canUseAbilty = true;

        public float cooldown = 10;
        public float cooldownStart = 10;

        private GameManager gm;
        private AudioManager am;


        private void Start()
        {
            rankUpRequirements = new int[6]
            {
            Random.Range(1, 2),
            Random.Range(3, 4),
            Random.Range(5, 7),
            Random.Range(9, 11),
            Random.Range(13, 16),
            Random.Range(20, 25)
            };

            ps = GetComponent<ParticleSystem>();
            cc = GetComponent<CapsuleCollider>();
            gm = FindObjectOfType<GameManager>();
            control = FindObjectOfType<CWIS_Controller>();
            var em = ps.emission;
            em.enabled = false;

            am = FindObjectOfType<AudioManager>();
        }


        private void Update()
        {
            if ((Input.GetMouseButton(0)) && canUseAbilty)
            {
                if (control.activeTurret == thisControl)
                {
                    useAbility = true;
                }
            }

            if (useAbility && !isCoR && canUseAbilty)
            {
                var main = ps.main;
                main.duration = duration;
                var em = ps.emission;
                em.enabled = true;
                StartCoroutine(DelayShafts());
            }

            if ((!canUseAbilty) & (!isCoR))
            {
                cooldown -= Time.deltaTime / 2;

                if (cooldown < 0)
                {
                    cooldown = 0;
                    canUseAbilty = true;
                }
                
            }
        }


        private IEnumerator DelayShafts()
        {
            timesUsed++;
            CheckForNewRank();
            isCoR = true;
            cc.enabled = true;
            cooldown = cooldownStart;
            canUseAbilty = false;
            ps.Play();
            am.PlayFromTime("chaft", 4.5f, .75f);
            yield return new WaitForSeconds(duration);
            ps.Stop();
            cc.enabled = false;
            isCoR = false;
            var em = ps.emission;
            em.enabled = false;
            useAbility = false;
        }


        private void CheckForNewRank()
        {

            if (timesUsed == rankUpRequirements[0])
            {
                currentRank = gm.Rankup(GameManager.Ranks.None);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                duration += .5f;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[1])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Chev1);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                duration += .5f;
                cooldownStart = 9;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[2])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Chev2);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                duration += .5f;
                cooldownStart = 8;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[3])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Chev3);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                duration += .5f;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[4])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Star1);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                duration += .5f;
                cooldownStart = 7;
                //flicker.shouldFlicker = true;
            }

            if (timesUsed == rankUpRequirements[5])
            {
                currentRank = gm.Rankup(GameManager.Ranks.Star2);
                am.Play("levelup", .35f, Random.Range(.85f, 1.15f));
                cooldownStart = 6;
                //flicker.shouldFlicker = true;
            }
        }
    }
}