/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

using System.Collections;
using UnityEngine;

namespace CarterGames.Crushing
{
    public interface ISceneChanger
    {
        Animator transitionsAnim { get; set; }
        bool isCrusherTransition { get; set; }
        IEnumerator ChangeScene(string sceneName);
    }
}