using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

            int infiniteLoopIndex = 0;
            while (toBeChecked.Count > 0 && infiniteLoopIndex < 200)
            {
                for (int i = 0; i < toBeChecked.Count; i++)
                {
                    GameObject currentCheckedBall = toBeChecked[i];

                    if (currentCheckedBall == null)
                    {
                        toBeChecked.Remove(currentCheckedBall);
                        continue;
                    }
                    
                    if (!objectsInTheChain.Contains(currentCheckedBall))
                    {
                        objectsInTheChain.Add(currentCheckedBall);
                    }

                    GameBall currentGameBall = currentCheckedBall.GetComponent<GameBall>();
                    foreach (GameObject nextOne in currentGameBall.touchingObjects)
                    {
                        if (nextOne != null && !toBeChecked.Contains(nextOne) && !nextOne.GetComponent<GameBall>().Checked)
                        {
                            toBeChecked.Add(nextOne);
                            nextOne.GetComponent<GameBall>().Checked = true;
                        }
                    }
                    toBeChecked.Remove(currentCheckedBall);
                }
                infiniteLoopIndex++;
            }

            if(infiniteLoopIndex >= 200)
            {
                Debug.Log("Stopped possible infinite loop while chaining");
            }
            
            DestroyEveryBallInTheChain(gameBall.transform.position);
        }

        private void DestroyEveryBallInTheChain(Vector3 punchDirection)
        {
            int ballCount = objectsInTheChain.Count;
            float animationTime = 0.2f;
            
            CameraPunch.Instance.PunchCamera(punchDirection, ballCount);
            
            foreach (GameObject ballToBeRemoved in objectsInTheChain)
            {
                ballToBeRemoved.transform.DOScale(0.8f, animationTime);
                StartCoroutine(WaitAndDestroy(ballToBeRemoved, animationTime));
            }
            objectsInTheChain.Clear();
            ChainExecutionComplete?.Invoke(ballCount);
        }

        private IEnumerator WaitAndDestroy(GameObject ballToBeRemoved, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            Destroy(ballToBeRemoved);
        }
    }
}