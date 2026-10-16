using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
#endif

public class UIHandler : MonoBehaviour
{
    public TMP_Text bestScore;
    [SerializeField]
    private TMP_InputField nameInputField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Persistence.Instance.LoadScore();
        bestScore.text= "Best Score : " + Persistence.Instance.bestNamePlayer + " : " + Persistence.Instance.bestPoints;

    }

    public void StartGame()
    {
        if (nameInputField != null && !string.IsNullOrEmpty(nameInputField.text))
        {
            // 2. Passa o nome digitado para o Persistence
            Persistence.Instance.namePlayer = nameInputField.text;
            
            // 3. Salva imediatamente para que o nome fique registrado no JSON
            //Persistence.Instance.SaveScore();
        }
        else
        {
            // Opcional: define um nome padrão caso o jogador não digite nada
            Persistence.Instance.namePlayer = "Player";
        }

        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Persistence.Instance.SaveScore();
#if UNITY_EDITOR
    EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
