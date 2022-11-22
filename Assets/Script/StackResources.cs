using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditorInternal.Profiling.Memory.Experimental;

public class StackResources : MonoBehaviour
{
    [SerializeField] private Transform HolderParent;
    private bool IsInDropArea;
    private Vector3 dropPos;
    private Stack<Transform> collectedRes = new Stack<Transform>();
    private float pos = 0;
    [SerializeField]private int stackSize = 10;
    Factory _factory;
    Factory2 _factory2;
    Factory3 _factory3;
    GameObject[] _gameObject;

    private void Start()
    {
        _gameObject = new GameObject[3];;
        _gameObject[0] = GameObject.Find("Factory");
        _gameObject[1] = GameObject.Find("Factory (1)");
        _gameObject[2] = GameObject.Find("Factory (2)");
        _factory = _gameObject[0].GetComponent<Factory>();
        _factory2 = _gameObject[1].GetComponent<Factory2>();
        _factory3 = _gameObject[2].GetComponent<Factory3>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DropB"))
        {
            IsInDropArea = true;
            dropPos = other.transform.position;

            StartCoroutine(DropResB());
        }
        else if (other.CompareTag("DropC"))
        {
            IsInDropArea = true;
            dropPos = other.transform.position;

            StartCoroutine(DropResC());
        }
        else
        {
            Item localItem = null;

            other.TryGetComponent(out localItem);

            if (other.CompareTag("ResourceA") && localItem && localItem.IsCollectedProp == false && collectedRes.Count < stackSize)
            {
                collectedRes.TryPeek(out Transform result);
                if (result == null || result.gameObject.CompareTag("ResourceA"))
                {
                    other.transform.DOJump(HolderParent.position, 2, 1, 0.2f).OnComplete
                        (
                        () =>
                        {
                            other.transform.SetParent(HolderParent);
                            other.transform.localPosition = new Vector3(0, pos, 0.1f);
                            other.transform.localRotation = Quaternion.identity;
                            pos += 0.3f;

                        }
                        );
                    collectedRes.Push(other.transform);
                    localItem.IsCollectedProp = true;
                    --_factory.CountSize;
                }
            }
            if (other.CompareTag("ResourceB") && localItem && localItem.IsCollectedProp == false && collectedRes.Count < stackSize)
            {
                collectedRes.TryPeek(out Transform result);
                if (result == null || result.gameObject.CompareTag("ResourceB"))
                {
                    other.transform.DOJump(HolderParent.position, 2, 1, 0.2f).OnComplete
                    (
                    () =>
                    {
                        other.transform.SetParent(HolderParent);
                        other.transform.localPosition = new Vector3(0, pos, 0.1f);
                        other.transform.localRotation = Quaternion.identity;
                        pos += 0.3f;

                    }
                    );
                    collectedRes.Push(other.transform);
                    localItem.IsCollectedProp = true;
                    --_factory2.CountSize;
                }
            }
            if (other.CompareTag("ResourceC") && localItem && localItem.IsCollectedProp == false && collectedRes.Count < stackSize)
            {
                collectedRes.TryPeek(out Transform result);
                if (result == null || result.gameObject.CompareTag("ResourceC"))
                {
                    other.transform.DOJump(HolderParent.position, 2, 1, 0.2f).OnComplete
                    (
                    () =>
                    {
                        other.transform.SetParent(HolderParent);
                        other.transform.localPosition = new Vector3(0, pos, 0.1f);
                        other.transform.localRotation = Quaternion.identity;
                        pos += 0.3f;

                    }
                    );
                    collectedRes.Push(other.transform);
                    localItem.IsCollectedProp = true;
                    --_factory3.CountSize;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DropB") || other.CompareTag("DropC"))
        { 
            IsInDropArea= false;
            pos = 0;
        }
    }

    IEnumerator DropResB(float _delay = 0)
    {
        while (IsInDropArea)
        { 
            yield return new WaitForSeconds(_delay);
            collectedRes.TryPeek(out Transform result);
            if (collectedRes.Count > 0 && result.gameObject.CompareTag("ResourceA"))
            { 
                Transform newItem = collectedRes.Pop();
                newItem.parent = null;
                newItem.DOJump(dropPos, 2, 1, 0.2f).OnComplete(() => newItem.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f).OnComplete(() => Destroy(newItem.gameObject)));
                _factory2.CountResNeed++;
            }
        }
        yield return null;
    }
    IEnumerator DropResC(float _delay = 0)
    {
        while (IsInDropArea)
        {
            yield return new WaitForSeconds(_delay);
            collectedRes.TryPeek(out Transform result);
            if (collectedRes.Count > 0 && result.gameObject.CompareTag("ResourceA"))
            {
                Transform newItem = collectedRes.Pop();
                newItem.parent = null;
                newItem.DOJump(dropPos, 2, 1, 0.2f).OnComplete(() => newItem.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f).OnComplete(() => Destroy(newItem.gameObject)));
                _factory3.CountResNeedA++;
            }
            else if (collectedRes.Count > 0 && result.gameObject.CompareTag("ResourceB"))
            {
                Transform newItem = collectedRes.Pop();
                newItem.parent = null;
                newItem.DOJump(dropPos, 2, 1, 0.2f).OnComplete(() => newItem.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f).OnComplete(() => Destroy(newItem.gameObject)));
                _factory3.CountResNeedB++;
            }
        }
        yield return null;
    }

}
