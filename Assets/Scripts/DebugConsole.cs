using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugConsole : MonoBehaviour
{
    public Canvas canvas;

    private GameObject m_last_text_box;

    Dictionary<string, GameObject> console = new Dictionary<string, GameObject>();

    void Start()
    {
        m_last_text_box = canvas.transform.GetChild(0).gameObject;
    }

    public void Log(object message, string prefix)
    {
        Text _text;
        GameObject _gameObject;

        if (!console.ContainsKey(prefix))
        {
            _gameObject = Instantiate(m_last_text_box);
            _gameObject.transform.SetParent(canvas.transform);
            _gameObject.transform.position = m_last_text_box.transform.position;
            _gameObject.transform.position += Vector3.down * 14;

            console.Add(prefix, _gameObject);
            m_last_text_box = _gameObject;
        }
        _text = console[prefix].GetComponent<Text>();
        _text.text = prefix + ": " + message.ToString();
    }
}
