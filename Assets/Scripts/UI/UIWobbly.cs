using System;
using TMPro;
using UnityEngine;

public class UIWobbly : MonoBehaviour
{
    #region Fields & Properties
    #region Fields

    [SerializeField] private TMP_Text text;
    [SerializeField] private float fSpeed = 2.0f;
    [SerializeField] private float fAmount = 1.0f;
    #endregion

    #region Properties
    #endregion
    #endregion
    
    #region Methods
    private void Start()
    {
        if (!text)
            text = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        text.ForceMeshUpdate();
        TMP_TextInfo _info = text.textInfo;

        for (int i = 0; i < _info.characterCount; i++)
        {
            if (!_info.characterInfo[i].isVisible)
                continue;

            int _index = _info.characterInfo[i].vertexIndex;
            Vector3[] _vertices = _info.meshInfo[_info.characterInfo[i].materialReferenceIndex].vertices;
            Vector3 _offset = Wobble(Time.time + i);
            _vertices[_index] += _offset;
            _vertices[_index + 1] += _offset;
            _vertices[_index + 2] += _offset;
            _vertices[_index + 3] += _offset;
        }

        for (int i = 0; i < _info.meshInfo.Length; i++)
        {
            _info.meshInfo[i].mesh.vertices = _info.meshInfo[i].vertices;
            text.UpdateGeometry(_info.meshInfo[i].mesh, i);
        }
    }

    private Vector2 Wobble(float _time)
    {
        return new Vector2(Mathf.Sin(_time * fSpeed), Mathf.Cos(_time * fSpeed)) * fAmount;
    }
    #endregion
}
