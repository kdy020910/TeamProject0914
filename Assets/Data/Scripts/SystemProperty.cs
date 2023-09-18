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
    MountingSlot _mountingSlot = null;
    protected MountingSlot mountingSlot
    {
        get
        {
            _mountingSlot = FindObjectOfType<MountingSlot>();
            if (_mountingSlot == null)
            {
                _mountingSlot = GetComponent<MountingSlot>();
                if (_mountingSlot == null)
                {
                    _mountingSlot = GetComponentInChildren<MountingSlot>();
                }
            }
            return _mountingSlot;
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
    TInventory _tinventory = null;
    protected   TInventory tinventory
    {
        get
        {
            _tinventory = FindObjectOfType<TInventory>();
            if(_tinventory == null)
            {
                _tinventory= GetComponent<TInventory>();
            }
            return _tinventory;
        }
    }
    RecipeData _recipeData = null;
    protected RecipeData recipeData
    {
        get
        {
            _recipeData = FindObjectOfType<RecipeData>();
            if(_recipeData == null)
            {
                _recipeData = GetComponent<RecipeData>();
            }
            return _recipeData;
        }
    }
    Crop _crop = null;
    protected Crop crop
    {
        get
        {
            _crop = FindObjectOfType<Crop>();
            if(_crop == null)
            {
                _crop = GetComponent<Crop>();
            }
            return _crop;
        }
    }
}
