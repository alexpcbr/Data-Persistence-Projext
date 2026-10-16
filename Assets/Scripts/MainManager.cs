
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScoreText;
    public GameObject GameOverText;
    private string namePlayer = Persistence.Instance.namePlayer;
    private string bestNamePlayer = Persistence.Instance.bestNamePlayer;

    private bool m_Started = false;
    private int m_Points;
    private int b_Points = Persistence.Instance.bestPoints;

    private bool m_GameOver = false;

    // MIGRATED: InputAction replaces Input.GetKeyDown(KeyCode.Space) 
    private InputAction m_LaunchAction; // Tecla espaço
    private InputAction n_LaunchAction; // Tecla escape

    // MIGRATED: bind the Space key as a button action
    void Awake()
    {
        m_LaunchAction = new InputAction("Launch", InputActionType.Button, "<Keyboard>/space");
        n_LaunchAction = new InputAction("Menu", InputActionType.Button, "<Keyboard>/escape");
        
        b_Points = Persistence.Instance.bestPoints;
        bestScoreText.text = $"Best Score : {bestNamePlayer} : {b_Points}";

    }

    // MIGRATED: enable the action while the component is active
    void OnEnable()
    {
        m_LaunchAction.Enable();
        n_LaunchAction.Enable();
    }

    // MIGRATED: disable the action when the component is inactive
    void OnDisable()
    {
        m_LaunchAction.Disable();
        n_LaunchAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (m_LaunchAction.WasPressedThisFrame()) // MIGRATED: was Input.GetKeyDown(KeyCode.Space)
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (m_LaunchAction.WasPressedThisFrame()) // MIGRATED: was Input.GetKeyDown(KeyCode.Space)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (n_LaunchAction.WasPressedThisFrame())
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        if (b_Points < m_Points)
        {
            b_Points = m_Points;
            bestNamePlayer = namePlayer;
            bestScoreText.text = $"Best Score : {bestNamePlayer} : {b_Points}";
            Persistence.Instance.bestPoints = b_Points;
            Persistence.Instance.bestNamePlayer = bestNamePlayer;
            Persistence.Instance.SaveScore();
        }

        GameOverText.SetActive(true);
    }
}
