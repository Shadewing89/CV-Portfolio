using System.Collections.Generic;
using UnityEngine;

namespace BubblePopper
{
    public class ChainHandler : MonoBehaviour
    {
        private List<GameObject> objectsInTheChain;
        private List<GameObject> toBeChecked;

        private int toBeCheckedIndex;

        public delegate void ChainExecutionCompleteHandler(int ballAmount);
        public event ChainExecutionCompleteHandler ChainExecutionComplete;
        
        private void Awake()
        {
            objectsInTheChain = new List<GameObject>();
            toBeChecked = new List<GameObject>();
        }

        public void StartChaining(GameBall gameBall)
        {
            objectsInTheChain.Add(gameBall.gameObject);
            
            foreach (GameObject touching in gameBall.touchingObjects)
            {
                toBeChecked.Add(touching);
            }

            while (toBeChecked.Count > 0)
            {
                for (int i = 0; i < toBeChecked.Count; i++)
                {
                    GameObject currentCheckedBall = toBeChecked[i];

                    if (currentCheckedBall == null)
                    {
                        continue;
                    }
                    
                    if (!objectsInTheChain.Contains(currentCheckedBall))
                    {
                        objectsInTheChain.Add(currentCheckedBall);
                    }

                    GameBall currentGameBall = currentCheckedBall.GetComponent<GameBall>();
                    foreach (GameObject nextOne in currentGameBall.touchingObjects)
                    {
                        if (!toBeChecked.Contains(nextOne) && !nextOne.GetComponent<GameBall>().Checked)
                        {
                            toBeChecked.Add(nextOne);
                            nextOne.GetComponent<GameBall>().Checked = true;
                        }
                    }

                    toBeChecked.Remove(currentCheckedBall);
                }
            }
            
            DestroyEveryBallInTheChain();
        }

        private void DestroyEveryBallInTheChain()
        {
            int ballCount = objectsInTheChain.Count;
            foreach (GameObject ballsToBeRemoved in objectsInTheChain)
            {
                Destroy(ballsToBeRemoved);
            }
            objectsInTheChain.Clear();
            ChainExecutionComplete?.Invoke(ballCount);
        }
    }
}