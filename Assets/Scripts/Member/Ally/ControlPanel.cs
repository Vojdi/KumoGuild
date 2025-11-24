
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text memberNameText;
    [SerializeField] Image memberIcon;
    [SerializeField] Transform skillsParent;
    [SerializeField] Transform speedupTransform;
    static ControlPanel instance;
    public static ControlPanel Instance => instance;
    AllyMember memberOnTurn;
    Skill selectedSkill;

    int selectedButtonIndex;

    Color32 normalColor = new Color32(255, 255, 255, 255);
    Color32 highlightedColor = new Color32(225, 225, 225, 255);
    Color32 pressedColor = new Color32(200, 200, 200, 255);

    bool spedUp;
   

    private void Awake()
    {
        instance = this;
        selectedButtonIndex = -1;
        spedUp = false;
    }
    public void AllyTurn(AllyMember member)
    {
        memberOnTurn = member;
        StartCoroutine(EnableControls(true));
    }
    public void SkillClicked(int index)
    {
        if(selectedButtonIndex != index)
        {
            ResetButtonColors();

            selectedButtonIndex = index;
            var btn = skillsParent.GetChild(index).GetComponent<Button>();
            ColorBlock buttonColors = btn.colors;
            buttonColors.normalColor = pressedColor;
            buttonColors.highlightedColor = pressedColor;
            btn.colors = buttonColors;
        }
        selectedSkill = memberOnTurn.Skills[index];
        if (selectedSkill.SelfOnly)
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (selectedSkill.SelfMember.Position ==  m.Position)
                {
                    m.Targetable = true;
                }
                else
                {
                    m.Targetable = false;
                }
            }
        }
        else
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (m.Effects.OfType<TauntEffect>().Any() && m is EnemyMember)
                {
                    if (selectedSkill.ReachablePositions.Contains(m.Position))
                    {
                        Untarget();
                        m.Targetable = true;
                        break;
                    }
                }
                if (selectedSkill.ReachablePositions.Contains(m.Position))
                {
                    m.Targetable = true;
                }
                else
                {
                    m.Targetable = false;
                }
            }
        }
        VisualEffectManager.Instance.TargetArrows();
    }
    public void SkillPositionSelected(int pos)
    {
        Debug.Log($"{memberOnTurn} used {selectedSkill.SkillName}");//////
        VisualEffectManager.Instance.PlayEffectAnimation(selectedSkill, pos);
        StartCoroutine(EnableControls(false));
        Untarget();
    }
    void Untarget()
    {
        foreach (var m in GameManager.Instance.Members)
        {
            m.Targetable = false;
        }
        VisualEffectManager.Instance.TargetArrows();
    }
    IEnumerator EnableControls(bool parameter)
    {
        ResetButtonColors();   
        if (parameter == true)
        {
            memberNameText.text = memberOnTurn.MemberName;
            memberIcon.sprite = ImageManager.Instance.GetSprite(memberOnTurn.IconId);

            for (int i = 0; i < memberOnTurn.Skills.Count; i++) {
                skillsParent.GetChild(i).GetComponent<Image>().sprite = ImageManager.Instance.GetSprite(memberOnTurn.Skills[i].IconId);
                Debug.Log(skillsParent.GetChild(i).GetComponent<Image>());
            }
        }
        foreach (Transform child in transform)
        {
            if (child != speedupTransform) {
                CanvasGroup cg = child.gameObject.AddComponent<CanvasGroup>();
                cg.alpha = 0;
                child.gameObject.SetActive(parameter);
            }
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(memberNameText.rectTransform);
        yield return null;
        foreach (Transform child in transform)
        {
            var cg = child.GetComponent<CanvasGroup>();
            if(cg!= null)
            {
                Destroy(cg);
            }
        }
    }
    public void SpeedupClicked(Button b)
    {
        if (spedUp)
        {
            ColorBlock c = b.colors;
            c.normalColor = normalColor;
            c.highlightedColor = normalColor;
            c.pressedColor = pressedColor;
            b.colors = c;
            spedUp = false;
            Time.timeScale = 1;
            StartCoroutine(SpeedUpButtonHighlightDelay(b, 1.5f));
        }
        else
        {
            ColorBlock c = b.colors;
            c.normalColor = pressedColor;
            c.highlightedColor = pressedColor;
            c.pressedColor = normalColor;
            b.colors = c;   
            spedUp = true;
            Time.timeScale = 2;
            StartCoroutine(SpeedUpButtonHighlightDelay(b, 3f));

        }
    }
    void ResetButtonColors()
    {
        foreach (Transform child in skillsParent)
        {
            var b = child.GetComponent<Button>();
            ColorBlock c = b.colors;
            c.normalColor = normalColor;
            c.highlightedColor = highlightedColor;
            b.colors = c;
        }
    }
    IEnumerator SpeedUpButtonHighlightDelay(Button b, float time)
    {
        yield return new WaitForSeconds(time);
        ColorBlock c = b.colors;
        c.highlightedColor = highlightedColor;
        b.colors = c; 
    }
}
