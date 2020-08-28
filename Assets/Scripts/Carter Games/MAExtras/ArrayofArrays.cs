using System;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.MAExtras
{
    [Serializable]
    public abstract class ArrayofArrays<T>
    {
        public T[] array; 
    }

    [Serializable]
    public class IntAoA : ArrayofArrays<int>
    { }

    [Serializable]
    public class FloatAoA : ArrayofArrays<float>
    { }

    [Serializable]
    public class StringAoA : ArrayofArrays<string>
    { }

    [Serializable]
    public class GameObjectAoA : ArrayofArrays<GameObject>
    { }

    [Serializable]
    public class Vector2AoA : ArrayofArrays<Vector2>
    { }

    [Serializable]
    public class Vector3AoA : ArrayofArrays<Vector3>
    { }
}