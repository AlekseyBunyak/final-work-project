using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] _layers;
    [SerializeField] private float[] _coeff;

    private int _layersCount;

    private void Start()
    {
        _layersCount = _layers.Length;
    }


    private void Update()
    {
        for (int i = 0; i < _layersCount; i++)
        {
            _layers[i].position = new Vector2(transform.position.x * _coeff[i], _layers[i].position.y);
        }
    }
}
