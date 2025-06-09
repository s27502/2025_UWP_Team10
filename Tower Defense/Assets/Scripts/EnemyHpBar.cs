using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class EnemyHpBar : MonoBehaviour
{
    [SerializeField] private Slider hpBarFill;
    private Camera cam;
    public void Awake()
    {
        UpdateHpBar(1, 1);
    }

    private void Start()
    {
        cam = Transform.FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        if (cam == null) return;
        
        hpBarFill.gameObject.transform.LookAt(
                transform.position + cam.transform.rotation * Vector3.forward,
            cam.transform.rotation * Vector3.up
                );
    }

    public void UpdateHpBar(float current, float max)
    {
        hpBarFill.value = current / max;
    }
}
