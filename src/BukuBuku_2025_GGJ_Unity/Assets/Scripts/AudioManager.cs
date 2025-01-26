using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;   // シングルトン用インスタンス
    private AudioSource audioSource;      // BGM再生用

    private void Awake()
    {
        // すでに別のインスタンスがあるなら自分を破棄（1つだけ存在させる）
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // インスタンスとして登録
        instance = this;

        // シーンをまたいでも破棄しない
        DontDestroyOnLoad(gameObject);

        // このオブジェクトにアタッチされているAudioSourceを取得
        audioSource = GetComponent<AudioSource>();

        // BGMをループ再生したい場合
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        // シーンが切り替わったときに呼ばれるイベントを登録
        SceneManager.activeSceneChanged += OnSceneChanged;

        // 起動時にGameSceneでなければBGMを再生開始
        if (!IsGameScene(SceneManager.GetActiveScene().name))
        {
            audioSource?.Play();
        }
    }

    /// <summary>
    /// シーンが切り替わったときに呼び出されるコールバック
    /// </summary>
    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (audioSource == null) return;

        // 新しいシーンがGameSceneならBGM停止、それ以外ならBGM再生
        if (IsGameScene(newScene.name))
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
        else
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
    }

    /// <summary>
    /// シーン名が"GameScene"かどうかを判定
    /// </summary>
    private bool IsGameScene(string sceneName)
    {
        return sceneName == "GameScene";
    }
}
