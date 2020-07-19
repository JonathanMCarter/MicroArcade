using CarterGames.Crushing.Saving;
using UnityEngine;
using UnityEngine.UI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.Crushing
{
    public class MethodPass : MonoBehaviour
    {
        private Animator anim;
        [SerializeField] private Text amountText;
        private GameManager gameManager;

        private void Start()
        {
            anim = GetComponent<Animator>();
            amountText = GetComponentInChildren<Text>();
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }


        public void ShowStars()
        {
            amountText.text = gameManager.saveData.starsCollected[1].ToString();
        }


        public void ResetTrig()
        {
            anim.ResetTrigger("collectStar");
        }
    }
}