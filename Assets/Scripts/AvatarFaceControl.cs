using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarFaceControl : MonoBehaviour
{
    [SerializeField] Material _avatarFace;         // Material "Face"
    [SerializeField] Texture _defaultTexture;

    bool _crRunning = false;
    IEnumerator _coroutine;

    void Start()
    {
        _avatarFace.SetTexture("_MainTex", _defaultTexture);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            ShowFace(QuickSlotManager.s_quickSlots[0].fid);
        if (Input.GetKey(KeyCode.Alpha2))
            ShowFace(QuickSlotManager.s_quickSlots[1].fid);
        if (Input.GetKey(KeyCode.Alpha3))
            ShowFace(QuickSlotManager.s_quickSlots[2].fid);
        if (Input.GetKey(KeyCode.Alpha4))
            ShowFace(QuickSlotManager.s_quickSlots[3].fid);
    }

    public void ChangeFace(int faceIndex)
    {
        _avatarFace.SetTexture("_MainTex", QuickSlotManager.s_faceTextures[faceIndex]);
    }

    void ShowFace(int index)
    {

        if (_crRunning && _coroutine != null)
        {
            //StopCoroutine(_coroutine);
            return;
        }

        _coroutine = ShowFaceCoroutine(index);
        StartCoroutine(_coroutine);
    }

    IEnumerator ShowFaceCoroutine(int index)
    {
        _crRunning = true;
        // 표정 변경
        ChangeFace(index);

        // 10초 대기
        yield return new WaitForSeconds(10f);

        // 표정을 다시 default로 바꾸기 (Start에서 fid를 받아오는 방법도 가능)
        _avatarFace.SetTexture("_MainTex", _defaultTexture);
        _crRunning = false;

    }
}
