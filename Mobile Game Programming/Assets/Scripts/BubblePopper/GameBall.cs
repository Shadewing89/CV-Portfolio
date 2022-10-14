using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace BubblePopper
{
    public class GameBall : MonoBehaviour
    {
        private BallType ballType;
        private BallType BallType => ballType;
        private Dictionary<string, BallType> ballNameToType;
        public bool Checked;
        
        public List<GameObject> touchingObjects;
        
        private void Awake()
        {
            touchingObjects = new List<GameObject>();
            
            ballNameToType = new Dictionary<string, BallType>
            {
                { "Blue", BallType.Blue }, { "Cyan", BallType.Cyan }, { "Purple", BallType.Purple }, { "Yellow", BallType.Yellow }
            };
        }

        void Start()
        {
            foreach(string color in ballNameToType.Keys)
            {
                if(gameObject.name.StartsWith(color))
                {
                    ballType = ballNameToType[color];
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            GameBall collisionGameBall = collision.gameObject.GetComponent<GameBall>();

            if (collisionGameBall != null && collisionGameBall.BallType == ballType)
            {
                touchingObjects.Add(collision.gameObject);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            GameBall collisionGameBall = other.gameObject.GetComponent<GameBall>();

            if (collisionGameBall != null && touchingObjects.Contains(other.gameObject))
            {
                touchingObjects.Remove(other.gameObject);
            }
        }
    }
}