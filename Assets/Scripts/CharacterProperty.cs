using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperty : MonoBehaviour
{
    Renderer _renderer = null;
    protected Renderer myRender
    {
        get
        {
            if (_renderer == null)
            {
                _renderer = GetComponent<Renderer>();
                if (_renderer == null)
                {
                    _renderer = GetComponentInChildren<Renderer>();
                }
            }
            return _renderer;
        }
    }

    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    Rigidbody _rigid = null;
    protected Rigidbody myRigid
    {
        get
        {
            if (_rigid == null)
            {
                _rigid = GetComponent<Rigidbody>();
                if (_rigid == null)
                {
                    _rigid = GetComponentInChildren<Rigidbody>();
                }
            }
            return _rigid;
        }
    }

    Collider _collider = null;
    protected Collider myCollider
    {
        get
        {
            if (_collider == null)
            {
                _collider = GetComponent<Collider>();
                if (_collider == null)
                {
                    _collider = GetComponentInChildren<Collider>();
                }
            }
            return _collider;
        }
    }
}
