using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Player _player;
    [Space]
    [SerializeField] private TMP_Text _hp;

    private void OnEnable()
    {
        _player.HPChanged += UpdateHP;
    }

    private void OnDisable()
    {
        _player.HPChanged -= UpdateHP;
    }

    private void UpdateHP(float amount)
    {
        var hp = amount.ToString("##");
        _hp.text = $"HP: {hp}";
    }
}
