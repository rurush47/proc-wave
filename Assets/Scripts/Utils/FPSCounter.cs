using TMPro;
using UnityEngine;

namespace Utils
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private float _sampleTime = 1;
        private float _time;
        private int _frameCount;

        void Update()
        {
            _time += Time.unscaledDeltaTime;
            _frameCount++;

            if (_time >= _sampleTime)
            {
                _fpsText.text = "FPS: " + Mathf.RoundToInt(_frameCount / _time) + 
                                " (sampled over " + _sampleTime + "s)";

                _time -= _sampleTime;
                _frameCount = 0;
            }
        }
    }
}