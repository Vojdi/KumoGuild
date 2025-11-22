
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text memberNameText;
    [SerializeField] Image memberIcon;
    [SerializeField] Transform skillsParent;
    static ControlPanel instance;
    public static ControlPanel Instance => instance;
    AllyMember memberOnTurn;
    Skill selectedSkill;
    private void Awake()
    {
        instance = this;
    }
    public void AllyTurn(AllyMember member)
    {
        memberOnTurn = member;
        StartCoroutine(EnableControls(true));
    }
    public void SkillClicked(int index)
    {
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
            CanvasGroup cg = child.gameObject.AddComponent<CanvasGroup>();
            cg.alpha = 0;
            child.gameObject.SetActive(parameter);
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
}
