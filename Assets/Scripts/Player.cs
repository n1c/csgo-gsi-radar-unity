using System.Collections;
using System.Collections.Generic;
using Libs;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite _ctTexture = default;
    [SerializeField] private Sprite _tTexture = default;
    [SerializeField] private GameObject _isMain = default;
    private CurrentMap _currentMap = default;

    private float _lerpSpeed = 0.01f;
    private Vector3 _oldPosition = new Vector3();
    private Vector3 _newPosition = new Vector3();
    private Quaternion _oldRotation = Quaternion.identity;
    private Quaternion _newRotation = Quaternion.identity;

    private void Awake()
    {
        _currentMap = GameObject.Find("Map").GetComponent<CurrentMap>();
    }

    private void Update()
    {
        if (_newPosition != transform.position)
        {
            transform.position = Vector3.Lerp(_oldPosition, _newPosition, Time.time * _lerpSpeed);
        }

        if (_newRotation != transform.rotation)
        {
            transform.rotation = Quaternion.Lerp(_oldRotation, _newRotation, Time.time * _lerpSpeed);
        }
    }

    public void SetData(PayloadModels.Player p, bool isMain)
    {
        _oldPosition = _newPosition;

        Vector3 position = Helpers.String2Vector(p.position);
        // @TODO: Before we figure out Nuke etc, just set z to 0
        position.z = 0;

        _newPosition = _currentMap.Dims.ScaleVector(position);

        // @TODO: Check health and draw as dead if required?
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite(p);
        // gameObject.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));

        if (isMain)
        {
            Vector3 rotation = Helpers.String2Vector(p.forward);
            _oldRotation = _newRotation;
            _newRotation = Helpers.Forward2Rotation(rotation);

            _isMain.SetActive(true);
        }
        else
        {
            _isMain.SetActive(false);
        }
    }

    private Sprite Sprite(PayloadModels.Player player)
    {
        return player.team == "CT" ? _ctTexture : _tTexture;
    }
}
