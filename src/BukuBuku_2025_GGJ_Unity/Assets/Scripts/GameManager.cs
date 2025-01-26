using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject scoreManager;
    public GameObject randomSpeech;
    int pastScore;

	void Start()
	{
		//scoreManager.GetComponent<ScoreManager>().Score;
  //      randomSpeech.GetComponent<RandomSpeech>();
	}

	void Update()
    {
        if (randomSpeech.GetComponent<RandomSpeech>().IsWatching() == true)
        {
            if (pastScore != scoreManager.GetComponent<ScoreManager>().Score)
			{
				scoreManager.GetComponent<ScoreManager>().Score = 0;
            }
        }
        pastScore = scoreManager.GetComponent<ScoreManager>().Score;
    }
}
