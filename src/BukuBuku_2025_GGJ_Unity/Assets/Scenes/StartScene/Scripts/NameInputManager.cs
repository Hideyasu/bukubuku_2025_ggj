using UnityEngine;
// using UnityEngine.UI;  // ������ InputField �p
using TMPro;        // TMP_InputField ���g�p����ꍇ

public class NameInputManager : MonoBehaviour
{
    // public InputField nameInputField;
    public TMP_InputField nameInputField; // TextMeshPro �ł��g���Ȃ炱����

    // ���O���͊m�莞�ɌĂ΂�郁�\�b�h�i�{�^�������ȂǂɂЂ��t����j
    public void OnNameConfirm()
    {
        // InputField �ɓ��͂��ꂽ��������O���[�o���ϐ��Ɋi�[
        GlobalParam.playerName = nameInputField.text;

        // �f�o�b�O���O�Ŋm�F�iOptional�j
        Debug.Log("���͂��ꂽ���O: " + GlobalParam.playerName);

        // ���̃V�[���ֈړ�����ꍇ�͂����Ŏ��s
        // SceneManager.LoadScene("GameScene");  // ��
    }
}
