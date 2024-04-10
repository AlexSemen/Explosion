using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDivisible : MonoBehaviour
{
    [SerializeField] private CubeDivisible _prefab;
    [SerializeField] private float _radiusExplode;
    [SerializeField] private float _forceExplode;

    private int _chanceDivisible;
    private int _maxChanceDivisible;
    private int _modifierChanceDivisible;
    private int _modifierScale;
    private int _maxDivisible;
    private int _mixDivisible;

    private void Awake()
    {
        _mixDivisible = 2;
        _maxDivisible = 4;
        _maxChanceDivisible = 100;
        _chanceDivisible = _maxChanceDivisible;
        _modifierScale = 2;
        _modifierChanceDivisible = 2;
    }

    public void OnClick()
    {
        Destroy(gameObject);

        if(Random.Range(0, _maxChanceDivisible) <= _chanceDivisible) 
        {
            int divisible = Random.Range(_mixDivisible, _maxDivisible + 1);

            List<Rigidbody> newCubs = new List<Rigidbody>();

            for (int i = 0; i < divisible; i++)
            {
                CubeDivisible newCub = Instantiate(_prefab, transform.position, Quaternion.identity);
                newCub.Init(transform.localScale, _chanceDivisible);
                newCubs.Add(newCub.GetComponent<Rigidbody>());
            }

            Explode(newCubs);
        }
    }

    public void Init(Vector3 scale, int chanceDivisible)
    {
        transform.localScale = scale / _modifierScale;
        _chanceDivisible = chanceDivisible / _modifierChanceDivisible;
        gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
    }

    private void Explode(List<Rigidbody> Cubs)
    {
        foreach(Rigidbody Cub in Cubs)
        {
            Cub.AddExplosionForce(_forceExplode, transform.position, _radiusExplode);
        }
    }
}
