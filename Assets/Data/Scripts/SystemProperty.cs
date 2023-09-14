using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemProPerty : MonoBehaviour
{
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
    Image _img = null;
    protected Image myimage
    {
        get
        {
            if (_img == null)
            {
                _img = GetComponent<Image>();
                if (_img == null)
                {
                    _img = GetComponentInChildren<Image>();
                }
            }
            return _img;
        }
    }
    PlayerController _playerController = null;
    protected PlayerController playerController
    {
        get
        {
            _playerController = FindObjectOfType<PlayerController>();
            if (_playerController == null)
            {
                _playerController = GetComponent<PlayerController>();
                if (playerController == null)
                {
                    _playerController = GetComponentInChildren<PlayerController>();
                }
            }
            return _playerController;
        }
    }
    PlayerTrigger _playerTrigger = null;
    protected PlayerTrigger playerTrigger
    {
        get
        {
            _playerTrigger = FindObjectOfType<PlayerTrigger>();
            if( _playerTrigger == null)
            {
                _playerTrigger= GetComponent<PlayerTrigger>();
                if(playerTrigger == null)
                {
                    _playerTrigger= GetComponentInChildren<PlayerTrigger>();
                }
            }
            return _playerTrigger;
        }
    }
    
    PondField _pondField = null;
    protected PondField pondField
    {
        get
        {
            _pondField = FindObjectOfType<PondField>();
            if (_pondField == null)
            {
                _pondField = GetComponent<PondField>();
                if (_pondField == null)
                {
                    _pondField = GetComponentInChildren<PondField>();
                }
            }
            return _pondField;
        }
    }

    FarmField _farmField = null;
    protected FarmField farmField
    {
        get
        {
            _farmField = FindObjectOfType<FarmField>();
            if (_farmField == null)
            {
                _farmField = GetComponent<FarmField>();
                if (_farmField == null)
                {
                    _farmField = GetComponentInChildren<FarmField>();
                }
            }
            return _farmField;
        }
    }   
    DiyField _diyField = null;
    protected DiyField diyField
    {
        get
        {
            _diyField = FindObjectOfType<DiyField>();
            if (_diyField == null)
            {
                _diyField = GetComponent<DiyField>();
                if (_diyField == null)
                {
                    _diyField = GetComponentInChildren<DiyField>();
                }
            }
            return _diyField;
        }
    }
    Item _item = null;
    protected Item item
    {
        get
        {
            _item = FindObjectOfType<Item>();
            if( _item == null)
            {
                _item = GetComponent<Item>();
            }
            return _item;
        }
    }
}
