﻿using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DottweenExample
{
    public sealed class AddDamage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textPrefab;
        [SerializeField] private TweenParams _tweenParams;
        [SerializeField] private float _height;
        [SerializeField] private Transform _root;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ApplyDamage();
            }
        }

        private void ApplyDamage()
        {
            var mosePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            var text = Instantiate(_textPrefab, mosePosition, Quaternion.identity, _root);
            var value = Random.Range(5, 100);

            text.text = $"-{value}";
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);

            var rectTransform = text.GetComponent<RectTransform>();
            rectTransform.localScale = new Vector3(0.5f,0.5f,0.5f);
            Sequence sequence = DOTween.Sequence();

            var newPositionY = rectTransform.anchoredPosition.y + _height;

            sequence.Append(rectTransform.DOAnchorPosY(newPositionY, _tweenParams.Duration));
            sequence.Insert(0.0f, rectTransform.DOScale(Vector3.one, _tweenParams.Duration));
            sequence.Insert(0.0f, text.DOFade(1.0f, _tweenParams.Duration / 2.0f));
            sequence.Append(rectTransform.DOScale(Vector3.zero, _tweenParams.Duration / 2.0f));
            sequence.SetEase(_tweenParams.Ease);
            sequence.OnComplete(() =>
            {
                sequence = null;
                Destroy(text.gameObject);
            });
            
            Sequence sequenceTimer = DOTween.Sequence();
            sequenceTimer.AppendInterval(2.0f);
            sequenceTimer.OnComplete(() =>
            {
                Debug.LogWarning("Hi!");
            });
        }
    }
}
