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
            int divisible = Random.Range(_mixDivisible - 1, _maxDivisible);

            for (int i = 0; i <= divisible; i++)
            {
                CubeDivisible newCub = Instantiate(_prefab, transform.position, Quaternion.identity);
                newCub.transform.localScale = transform.localScale / _modifierScale;
                newCub.SetChanceDivisible(_chanceDivisible / _modifierChanceDivisible);
                newCub.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }

            Explode();
        }
    }

    public void SetChanceDivisible(int chanceDivisible)
    {
        _chanceDivisible = chanceDivisible;
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusExplode);
        Rigidbody rigidbody;

        for (int i = 0;i < colliders.Length; i++)
        {
            rigidbody = colliders[i].GetComponent<Rigidbody>();

            if(rigidbody != null)
            {
                rigidbody.AddExplosionForce(_forceExplode, transform.position, _radiusExplode);
            }
        }
    }
}
