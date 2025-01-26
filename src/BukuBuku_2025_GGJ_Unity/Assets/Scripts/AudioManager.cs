using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;   // �V���O���g���p�C���X�^���X
    private AudioSource audioSource;      // BGM�Đ��p

    private void Awake()
    {
        // ���łɕʂ̃C���X�^���X������Ȃ玩����j���i1�������݂�����j
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // �C���X�^���X�Ƃ��ēo�^
        instance = this;

        // �V�[�����܂����ł��j�����Ȃ�
        DontDestroyOnLoad(gameObject);

        // ���̃I�u�W�F�N�g�ɃA�^�b�`����Ă���AudioSource���擾
        audioSource = GetComponent<AudioSource>();

        // BGM�����[�v�Đ��������ꍇ
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
    }

    private void Start()
    {
        // �V�[�����؂�ւ�����Ƃ��ɌĂ΂��C�x���g��o�^
        SceneManager.activeSceneChanged += OnSceneChanged;

        // �N������GameScene�łȂ����BGM���Đ��J�n
        if (!IsGameScene(SceneManager.GetActiveScene().name))
        {
            audioSource?.Play();
        }
    }

    /// <summary>
    /// �V�[�����؂�ւ�����Ƃ��ɌĂяo�����R�[���o�b�N
    /// </summary>
    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (audioSource == null) return;

        // �V�����V�[����GameScene�Ȃ�BGM��~�A����ȊO�Ȃ�BGM�Đ�
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
    /// �V�[������"GameScene"���ǂ����𔻒�
    /// </summary>
    private bool IsGameScene(string sceneName)
    {
        return sceneName == "GameScene";
    }
}
