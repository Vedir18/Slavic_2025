using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    #region Skills

    [Header("Skills")]
    [SerializeField] private Image _petIcon1;
    [SerializeField] private Image _petIcon2;
    [SerializeField] private Image _petIcon3;
    [SerializeField] private GameObject _p1, _p2, _p3;
    [SerializeField] private GameObject _extend1, _extend2, _extend3;
    [SerializeField] private Image _p1s1, _p1s2;
    [SerializeField] private Image _p2s1, _p2s2;
    [SerializeField] private Image _p3s1, _p3s2;
    [Header("Icons")]
    [SerializeField] private float _highlightedAlpha;
    [SerializeField] private float _notHighlightedAlpha;


    private PlayerManager _playerManager;
    private bool _highlighted1, _highlighted2, _highlighted3;
    public void Initialize()
    {
        _playerManager = GetComponent<PlayerManager>();
        _highlighted1 = false;
        _highlighted2 = false;
        _highlighted3 = false;
    }

    public void UpdateUI()
    {
        uint selectedSkill = _playerManager.SkillManager.SelectedSkill;
        UpdateSelectedSkills(selectedSkill);

    }

    public void HightlightSkill(uint petID, uint skillID)
    {
        switch (petID)
        {
            case 1:
                _highlighted1 = true;
                switch (skillID)
                {
                    case 1:
                        _p1s1.color = SetAlpha(_p1s1.color, _highlightedAlpha);
                        break;
                    case 2:
                        _p1s2.color = SetAlpha(_p1s2.color, _highlightedAlpha);
                        break;
                }
                break;
            case 2:
                _highlighted2 = true;
                switch (skillID)
                {
                    case 1:
                        _p2s1.color = SetAlpha(_p2s1.color, _highlightedAlpha);
                        break;
                    case 2:
                        _p2s2.color = SetAlpha(_p2s2.color, _highlightedAlpha);
                        break;
                }
                break;
            case 3:
                _highlighted3 = true;
                switch (skillID)
                {
                    case 1:
                        _p3s1.color = SetAlpha(_p3s1.color, _highlightedAlpha);
                        break;
                    case 2:
                        _p3s2.color = SetAlpha(_p3s2.color, _highlightedAlpha);
                        break;
                }
                break;
        }
        StartCoroutine(HighlightUsedSkill((petID, skillID)));
    }

    private void UpdateSelectedSkills(uint selectedSkill)
    {
        bool highlight1 = selectedSkill == 1 || _highlighted1;
        bool highlight2 = selectedSkill == 2 || _highlighted2;
        bool highlight3 = selectedSkill == 3 || _highlighted3;
        _extend1.SetActive(highlight1);
        _extend2.SetActive(highlight2);
        _extend3.SetActive(highlight3);
        _petIcon1.CrossFadeAlpha(highlight1 ? _highlightedAlpha : _notHighlightedAlpha, .1f, true);
        _petIcon2.CrossFadeAlpha(highlight2 ? _highlightedAlpha : _notHighlightedAlpha, .1f, true);
        _petIcon3.CrossFadeAlpha(highlight3 ? _highlightedAlpha : _notHighlightedAlpha, .1f, true);
        _p1.SetActive(selectedSkill == 0);
        _p2.SetActive(selectedSkill == 0);
        _p3.SetActive(selectedSkill == 0);
    }

    private IEnumerator HighlightUsedSkill((uint, uint) id)
    {
        Debug.Log($"Starting {id.Item1} {id.Item2}");
        yield return new WaitForSeconds(.2f);
        Debug.Log($"Ending {id.Item1} {id.Item2}");
        switch (id.Item1)
        {
            case 1:
                _highlighted1 = false;
                switch (id.Item2)
                {
                    case 1:
                        _p1s1.color = SetAlpha(_p1s1.color, _notHighlightedAlpha);
                        break;
                    case 2:
                        _p1s2.color = SetAlpha(_p1s2.color, _notHighlightedAlpha);
                        break;
                }
                break;
            case 2:
                _highlighted2 = false;
                switch (id.Item2)
                {
                    case 1:
                        _p2s1.color = SetAlpha(_p2s1.color, _notHighlightedAlpha);
                        break;
                    case 2:
                        _p2s2.color = SetAlpha(_p2s2.color, _notHighlightedAlpha);
                        break;
                }
                break;
            case 3:
                _highlighted1 = false;
                switch (id.Item2)
                {
                    case 1:
                        _p3s1.color = SetAlpha(_p3s1.color, _notHighlightedAlpha);
                        break;
                    case 2:
                        _p3s2.color = SetAlpha(_p3s2.color, _notHighlightedAlpha);
                        break;
                }
                break;
        }
    }

    private Color SetAlpha(Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }
    #endregion

    #region HP
    [Header("Health")]
    [SerializeField] private Image _hp1;
    [SerializeField] private Image _hp2;
    [SerializeField] private Image _hp3;

    public void DisplayHealth(HealthComponent health)
    {
        _hp1.color = SetAlpha(_hp1.color, health.CurrentHP >= 1 ? 1 : 0);
        _hp2.color = SetAlpha(_hp2.color, health.CurrentHP >= 2 ? 1 : 0);
        _hp3.color = SetAlpha(_hp3.color, health.CurrentHP >= 3 ? 1 : 0);
    }
    #endregion
}
