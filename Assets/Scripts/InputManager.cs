using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct InputEntry
{
    [SerializeField] private string key;
    [SerializeField] private KeyCode value;

    public string Key => key;
    public KeyCode Value => value;
    
    public InputEntry(string _key, KeyCode _value)
    {
        key = _key;
        value = _value;
    }
}

public class InputManager : SingletonTemplate<InputManager>
{
    #region Fields & Properties
    #region Fields
    [SerializeField] private List<InputEntry> baseInputs;
    [SerializeField] private string inputColor = "#FFFFFF";
    private List<InputEntry> inputs = new();
    #endregion

    #region Properties
    public List<InputEntry> Inputs => inputs;
    public string InputColor => inputColor;
    #endregion
    #endregion
    
    #region Methods
    protected override void Awake()
    {
        base.Awake();
        LoadInput();
    }

    public bool IsAlreadyInUse(KeyCode code)
    {
        foreach (InputEntry _input in inputs)
        {
            if (_input.Value == code)
                return true;
        }

        return false;
    }
    
    public KeyCode GetInput(string _action)
    {
        foreach (InputEntry _input in inputs)
        {
            if (_input.Key == _action)
                return _input.Value;
        }

        return KeyCode.None;
    }

    public void SaveInput()
    {
        foreach (InputEntry _input in inputs)
            PlayerPrefs.SetString(_input.Key, _input.Value.ToString());
    }

    private void LoadInput()
    {
        inputs.Clear();
        
        foreach (InputEntry _input in baseInputs)
        {
            string _keyString = PlayerPrefs.GetString(_input.Key, _input.Value.ToString());
            inputs.Add(Enum.TryParse(_keyString, out KeyCode _keyCode) ? new InputEntry(_input.Key, _keyCode)
                                                                        : new InputEntry(_input.Key, _input.Value));
        }
    }
    #endregion
}
