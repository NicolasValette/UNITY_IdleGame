using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Increment : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _incrementText;
    [SerializeField]
    private TMP_Text _incrementWordText;
    [SerializeField]
    private GameObject _startRootButton;
    [SerializeField]
    private bool _reset = false;
    [SerializeField]
    private Slider _letterSlider;
    [SerializeField]
    private Slider _wordSlider;

    [SerializeField]
    private int _letterForWord = 10;
    [SerializeField]
    private GameObject _wordButton;
    private int _numbWords = 0;
    private float _increment = 0;
    private float _timeSinceLastInc = 0f;
    private bool _isStarted = false;
    private bool _writeLetter = true;
    public float IncrementNumber { get => _increment; }
    // Start is called before the first frame update
    void Start()
    {
        _letterSlider.value = 0f;
        _letterSlider.maxValue = _letterForWord;
        if (_reset)
        {
            _increment = 0;
        }
        _incrementWordText.text = $"Mots : {_numbWords}";
    }

    private void increment ()
    {
        _increment++;
        _timeSinceLastInc = 0f;
        _letterSlider.value = _increment;

        if (_increment >= _letterForWord)
        {
            _wordButton.SetActive(true);
            _writeLetter = false;
        }
    }

    public void WriteWord()
    {
        if (_numbWords >= 0)                    // a changer !!!!!!!!!!!!!!!!!!!!!
        {
            _wordSlider.transform.parent.gameObject.SetActive(true);
        }
        _numbWords++;
        _wordSlider.value = _numbWords;
        _letterSlider.value = 0f;
        _wordButton.SetActive(false);
        _writeLetter = true;
        _increment = 0;
        _timeSinceLastInc = 0f;
        _incrementWordText.text = $"Mots : {_numbWords}";
    }
    // Update is called once per frame
    void Update()
    {
        if (_isStarted && _writeLetter)
        {
            _timeSinceLastInc += Time.deltaTime;
            if (_timeSinceLastInc > 1f)
            {
                increment();
            }
            _incrementText.text = _increment.ToString();
        }
    }
    public void StartIncrement()
    {
        _isStarted = true;
        _startRootButton.SetActive(false);
        _increment = PlayerPrefs.GetFloat("Increment", 0f);
        if (_reset)
        {
            _increment = 0f;
        }

    }


}
