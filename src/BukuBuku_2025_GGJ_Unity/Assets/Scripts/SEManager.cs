using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        // すでにインスタンスが存在し、かつそれが自分自身でない場合は自分を破棄
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // このオブジェクトをインスタンスとして登録
        Instance = this;

        // シーン切り替えでも破棄されないようにする
        DontDestroyOnLoad(gameObject);

        // 同じオブジェクトにあるAudioSourceを取得
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 任意のSE(AudioClip)を再生
    /// </summary>
    public void PlaySE(AudioClip seClip)
    {
        if (audioSource == null || seClip == null) return;
        audioSource.PlayOneShot(seClip);
    }
}
