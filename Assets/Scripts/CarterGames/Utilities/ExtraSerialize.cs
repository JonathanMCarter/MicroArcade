using System;
using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/


namespace CarterGames.Utilities
{
    public static class ExtraSerialize
    {
        public static SerializeSprite SpriteSerialize(Sprite input)
        {
            Texture2D texture = input.texture;
            SerializeSprite sprite = new SerializeSprite(texture.width, texture.height, ImageConversion.EncodeToPNG(texture));
            return sprite;
        }

        public static Sprite SpriteDeSerialize(SerializeSprite input)
        {
            SerializeSprite sprite = input;
            Texture2D texture = new Texture2D(sprite.x, sprite.y);
            ImageConversion.LoadImage(texture, sprite.data);
            return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), Vector2.one);
        }

        public static SerializeVector3 Vector3Serialize(Vector3 input)
        {
            return new SerializeVector3(input);
        }

        public static Vector3 Vector3DeSerialize(SerializeVector3 input)
        {
            return new Vector3(input.x, input.y, input.z);
        }
    }

    [Serializable]
    public class SerializeSprite
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;
        [SerializeField]
        public byte[] data;

        public SerializeSprite() { }

        public SerializeSprite(int inputX, int inputY, byte[] inputData)
        {
            x = inputX;
            y = inputY;
            data = inputData;
        }
    }


    [Serializable]
    public class SerializeVector3
    {
        [SerializeField]
        public float x;
        [SerializeField]
        public float y;
        [SerializeField]
        public float z;

        public SerializeVector3() { }

        public SerializeVector3(Vector3 input)
        {
            x = input.x;
            y = input.y;
            z = input.z;
        }

        public SerializeVector3(float inputX, float inputY, float inputZ)
        {
            x = inputX;
            y = inputY;
            z = inputZ;
        }
    }
}