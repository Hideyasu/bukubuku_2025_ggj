using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        // ���łɃC���X�^���X�����݂��A�����ꂪ�������g�łȂ��ꍇ�͎�����j��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // ���̃I�u�W�F�N�g���C���X�^���X�Ƃ��ēo�^
        Instance = this;

        // �V�[���؂�ւ��ł��j������Ȃ��悤�ɂ���
        DontDestroyOnLoad(gameObject);

        // �����I�u�W�F�N�g�ɂ���AudioSource���擾
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// �C�ӂ�SE(AudioClip)���Đ�
    /// </summary>
    public void PlaySE(AudioClip seClip)
    {
        if (audioSource == null || seClip == null) return;
        audioSource.PlayOneShot(seClip);
    }
}
