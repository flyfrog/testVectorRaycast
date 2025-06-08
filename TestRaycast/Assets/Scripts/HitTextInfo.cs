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
        _stringBuilder.Append("Нет попадений");

        bool prefixWrited = false;
        bool firstHitNumberWrited = false;

        for (var index = 0; index < raycastObjectData.Length; index++)
        {
            var data = raycastObjectData[index];
            if (data.HitStatus)
            {
                if (!prefixWrited)
                {
                    prefixWrited = true;
                    _stringBuilder.Clear();
                    _stringBuilder.Append("Есть попадения в кубы: ");
                }

                if (firstHitNumberWrited)
                {
                    _stringBuilder.Append($", ");
                }
                
                if (!firstHitNumberWrited)
                {
                    firstHitNumberWrited = true;
                }
                
                _stringBuilder.Append($"{index}");
            }
        }

        var result = _stringBuilder.ToString();
        
        if (result==_currentHitCubeInfo)
        {
            return;
        }

        _currentHitCubeInfo = result;
        _text.text = result;
    }
}