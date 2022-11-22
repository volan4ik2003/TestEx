using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Factory2 : MonoBehaviour
{

    [SerializeField] private GameObject _resource;
    [SerializeField] private float _produceTime = 4f;
    [SerializeField] private int _produceSize = 15;
    [SerializeField] private Text _resText, _resNeedText;
    private int countSize = 0;
    private int countResNeed = 0;

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
    public int CountResNeed
    {
        get
        {
            return countResNeed;
        }

        set
        {
            countResNeed = value;
        }

    }
    private void Start()
    {
        StartCoroutine(ProduceRes());

    }

    private void Update()
    {
        _resText.text = countSize.ToString();
        _resNeedText.text = countResNeed.ToString();
    }
    IEnumerator ProduceRes()
    {
        yield return new WaitForSeconds(_produceTime);
        if (countSize < _produceSize && countResNeed > 0)
        {
            Instantiate(_resource, this.transform.position, Quaternion.identity).transform.DOJump(new Vector3(transform.position.x, 0, transform.position.z - 4), 2, 1, 0.2f);
            countSize++;
            countResNeed--;
        }
        StartCoroutine(ProduceRes());
    }
}
