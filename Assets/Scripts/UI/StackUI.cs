using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StackUI : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _text;
    public void ChangeProgressBar(int current, int max)
    {
        _progressBar.fillAmount = (float)current / max;
        _text.text = current + "/" + max;
    }
}
