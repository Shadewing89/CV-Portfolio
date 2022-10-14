using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

// Added custom namespace since the 'GameManager' is a special word in Unity and changes the default icon of the script by default
namespace BubblePopper
{
    public class BubblePopperManager : MonoBehaviour
    {
        public static BubblePopperManager Instance { get; private set; }

        [SerializeField]
        private int startBallAmount;

        [SerializeField]
        private TextMeshProUGUI touchCountText;
        [SerializeField]
        private TextMeshProUGUI ballCountText;
        [SerializeField]
        private TextMeshProUGUI levelText;
        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private List<GameObject> ballPrefabs;
        [SerializeField]
        private GameObject ballParent;

        private int ballIndex;
        private int currentScore;
        private int currentLevel;

        private bool doNotSpamMoreBalls;
        
        private void Awake()
        {
            GetComponent<ChainHandler>().ChainExecutionComplete += OnChainExecutionComplete;
            
            if(Instance != null && Instance != this)
            {
                Destroy(Instance);
            }
            else
            {
                Instance = this;
            }
        }
        
        private void OnChainExecutionComplete(int ballAmount)
        {
            AddScore(ballAmount);
            if (ballParent.transform.childCount < 75 && !doNotSpamMoreBalls)
            {
                StartCoroutine(WaitAndCreateMoreBalls());
            }
        }

        private IEnumerator WaitAndCreateMoreBalls()
        {
            doNotSpamMoreBalls = true;
            yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
            CreateBalls(Random.Range(60, 90));
            doNotSpamMoreBalls = false;
        }

        private void Start()
        {
            CreateBalls(startBallAmount);
            currentLevel = 1;
            SetLevelText();
            SetScoreText();
        }

        private void CreateBalls(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                GameObject ball = Instantiate(
                    ballPrefabs[i % ballPrefabs.Count], 
                    new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(8.0f, 18.0f), 0), 
                    Quaternion.identity);
                ball.transform.parent = ballParent.transform;
            }
        }

        private void Update()
        {
            touchCountText.text = "Touch count:" + Input.touchCount;

            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if(touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));
                    GameObject nearestBallToTouchPosition = FindNearestSphereToTouchPosition(touchPosition);

                    if (nearestBallToTouchPosition != null)
                    {
                        StartChaining(nearestBallToTouchPosition.GetComponent<GameBall>());
                    }
                }
            }

            ballCountText.text = "Spheres: " + ballParent.transform.childCount;
        }

        private void StartChaining(GameBall gameBall)
        {
            GetComponent<ChainHandler>().StartChaining(gameBall);
        }

        private GameObject FindNearestSphereToTouchPosition(Vector3 touchPosition)
        {
            GameObject nearest = null;
            float distance = float.MaxValue;

            for (int i = 0; i < ballParent.transform.childCount; i++)
            {
                Transform ball = ballParent.transform.GetChild(i);
                float candidateDistance = Vector3.Distance(touchPosition, ball.position);
                if (candidateDistance < distance)
                {
                    nearest = ball.gameObject;
                    distance = candidateDistance;
                }
            }

            return nearest;
        }

        private void AddScore(int ballAmount)
        {
            for (int i = 0; i < ballAmount; i++)
            {
                currentScore += 100;

                if(currentScore >= 3000)
                {
                    AddLevelAndResetScore();
                }
            }
            
            SetLevelText();
            SetScoreText();
        }

        private void SetLevelText()
        {
            levelText.text = "Level \n" + currentLevel;
        }

        private void SetScoreText()
        {
            scoreText.text = "Score \n" + currentScore;
        }

        private void AddLevelAndResetScore()
        {
            currentLevel += 1;
            currentScore = 0;
        }
    }
}
