using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HitTextInfo : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private TestManager _testManager;
    
    private string _currentHitCubeInfo = "";
    private StringBuilder _stringBuilder = new();

    private void Awake()
    {
        _testManager.HitDataUpdatedEvent += DrawHitBoxId;
    }

    private void OnDestroy()
    {
        _testManager.HitDataUpdatedEvent -= DrawHitBoxId;
    }

    private void DrawHitBoxId(RaycastObjectData[] raycastObjectData)
    {
        _stringBuilder.Clear();

        bool prefixWritten = false;
        bool firstHitWritten = false;

        for (int index = 0; index < raycastObjectData.Length; index++)
        {
            var data = raycastObjectData[index];
            if (data.HitStatus)
            {
                if (!prefixWritten)
                {
                    prefixWritten = true;
                    _stringBuilder.Append("Есть попадения в кубы: ");
                }

                if (firstHitWritten)
                {
                    _stringBuilder.Append(", ");
                }
                else
                {
                    firstHitWritten = true;
                }

                _stringBuilder.Append(index); 
            }
        }

        if (!prefixWritten)
        {
            _stringBuilder.Append("Нет попадений");
        }

    
        if (_stringBuilder.Length == _currentHitCubeInfo.Length &&
            _stringBuilder.ToString().AsSpan().SequenceEqual(_currentHitCubeInfo.AsSpan()))
        {
            return;
        }

        _currentHitCubeInfo = _stringBuilder.ToString();  
        _text.text = _currentHitCubeInfo;
    }
}