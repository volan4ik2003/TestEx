using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Factory3 : MonoBehaviour
{

    [SerializeField] private GameObject _resource;
    [SerializeField] private float _produceTime = 4f;
    [SerializeField] private int _produceSize = 15;
    [SerializeField] private Text _resText, _resNeedAText, _resNeedBText;
    private int countSize = 0;
    private int countResNeedA = 0;
    private int countResNeedB = 0;

    public int CountSize
    {
        get
        {
            return countSize;
        }

        set
        {
            countSize = value;
        }
    }
    public int CountResNeedA
    {
        get
        {
            return countResNeedA;
        }

        set
        {
            countResNeedA = value;
        }

    }
    public int CountResNeedB
    {
        get
        {
            return countResNeedB;
        }

        set
        {
            countResNeedB = value;
        }

    }
    private void Start()
    {
        StartCoroutine(ProduceRes());

    }

    private void Update()
    {
        _resText.text = countSize.ToString();
        _resNeedAText.text = countResNeedA.ToString();
        _resNeedBText.text = countResNeedB.ToString();
    }
    IEnumerator ProduceRes()
    {
        yield return new WaitForSeconds(_produceTime);
        if (countSize < _produceSize && countResNeedA > 0 && countResNeedB > 0)
        {
            Instantiate(_resource, this.transform.position, Quaternion.identity).transform.DOJump(new Vector3(transform.position.x, 0, transform.position.z - 4), 2, 1, 0.2f);
            countSize++;
            countResNeedA--;
            countResNeedB--;
        }
        StartCoroutine(ProduceRes());
    }
}
