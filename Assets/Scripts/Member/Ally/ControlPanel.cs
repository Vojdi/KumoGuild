
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text memberNameText;
    [SerializeField] Image memberIcon;
    [SerializeField] Sprite[] timeSpeedIcons;
    [SerializeField] Transform skillsParent;
    [SerializeField] Transform[] notTurnedoff;
    [SerializeField] Transform speedupTransform;
    [SerializeField] Transform pauseTransform;
    [SerializeField] RectTransform infoPanelRectTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] Sprite[] timeStopIcons;
    [SerializeField] Transform optionsTransform;

    [SerializeField] TMPro.TMP_Text infoBoxName;
    [SerializeField] TMPro.TMP_Text infoBoxSkillType;
    [SerializeField] GameObject squaresParent;
    [SerializeField] List<GameObject> infoBoxPositionPanels;
    [SerializeField] List<TMPro.TMP_Text> infoBoxStatTexts;
    [SerializeField] GameObject effectPanel;
    public GameObject EffectPanel => effectPanel;


    static ControlPanel instance;
    public static ControlPanel Instance => instance;
    AllyMember memberOnTurn;
    Skill selectedSkill;
    Skill hoveredOverSkill;

    int selectedButtonIndex;
    bool spedUp;
    bool stopped;
    float lastTimeSpeed;
    public bool AbleToCheckEffects;

    private void Awake()
    {
        lastTimeSpeed = 1f;
        instance = this;
        selectedButtonIndex = -1;
        spedUp = false;
        stopped = false;
    }
    public void AllyTurn(AllyMember member)
    {
        memberOnTurn = member;
        StartCoroutine(EnableControls(true));
    }
    public void SkillClicked(int index)
    {
        if (selectedButtonIndex != index)
        {
            selectedButtonIndex = index;
        }
        ResetSkillBorder();
        Color32 c = skillsParent.GetChild(index).GetChild(0).GetComponent<Image>().color;
        skillsParent.GetChild(index).GetChild(0).GetComponent<Image>().color = new Color32(c.r, c.g, c.b, 255);

        selectedSkill = memberOnTurn.Skills[index];
        if (selectedSkill.SelfOnly)
        {
            foreach (Member m in GameManager.Instance.Members)
            {
                if (selectedSkill.SelfMember.Position == m.Position)
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

    private void ResetSkillBorder()
    {
        foreach (Transform o in skillsParent)
        {
            Color32 c1 = o.GetChild(0).gameObject.GetComponent<Image>().color;
            o.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(c1.r, c1.g, c1.b, 0);
        }
    }

    public IEnumerator SkillHoveredOver(int index)
    {
        hoveredOverSkill = memberOnTurn.Skills[index];
        InfoBoxSetup();
        var btn = skillsParent.GetChild(index);
        var cg = infoPanelRectTransform.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0;
        infoPanelRectTransform.gameObject.SetActive(true);
        infoPanelRectTransform.position = btn.transform.position + new Vector3(0, 100f * canvas.scaleFactor);
        yield return null;
        Destroy(cg);
        
    }
    void InfoBoxSetup()
    {
        squaresParent.SetActive(true);
        infoBoxSkillType.gameObject.SetActive(true);
        infoBoxName.text = Skill.GetInfoForInfoBox(hoveredOverSkill, "name")[0];
        List<string> arr = Skill.GetInfoForInfoBox(hoveredOverSkill, "skillType");
        infoBoxSkillType.text = $"{arr[0]}/{arr[1]}";
        if (arr[0] == "attack")
        {
            infoBoxSkillType.color = new Color32(255,9,71,255);
        }
        else
        {
            infoBoxSkillType.color = new Color32(0,255,128,255);
        }

        arr = Skill.GetInfoForInfoBox(hoveredOverSkill, "positions");
        if (arr[0] == "self")
        {
            foreach (var panel in infoBoxPositionPanels)
            {
                panel.gameObject.SetActive(true);
            }
            infoBoxPositionPanels[memberOnTurn.Position].SetActive(false);
        }
        else
        {
            for (int i = 0; i < infoBoxPositionPanels.Count; i++) 
            {
                if (arr[i] == "true")
                {
                    infoBoxPositionPanels[i].gameObject.SetActive(false);
                }
                else
                {
                    infoBoxPositionPanels[i].gameObject.SetActive(true);
                }
            }
        }

        arr = Skill.GetInfoForInfoBox(hoveredOverSkill, "stats");
        foreach (var statText in infoBoxStatTexts)
        {
            statText.gameObject.SetActive(false);
        }
        for(int i = 0;i < arr.Count; i++)
        {
            infoBoxStatTexts[i].text = arr[i];
            infoBoxStatTexts[i].gameObject.SetActive(true);
        }
    }
    public IEnumerator ControlHoveredOver(GameObject btn, string title)
    {
        ControlInfoSetup(title);
        var cg = infoPanelRectTransform.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0;
        infoPanelRectTransform.gameObject.SetActive(true);
        infoPanelRectTransform.position = btn.transform.position + new Vector3(0, 100f * canvas.scaleFactor);
        yield return null;
        Destroy(cg);
    }
    void ControlInfoSetup(string title)
    {
        squaresParent.SetActive(false);
        infoBoxName.text = title;
        foreach (Transform obj in infoPanelRectTransform.transform) { 
            obj.gameObject.SetActive(false);
        }
        infoPanelRectTransform.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void SkillHoveredOut()
    {
        infoPanelRectTransform.gameObject.SetActive(false);
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
        ResetSkillBorder();
        if (parameter == true)
        {
            memberNameText.text = memberOnTurn.MemberName;
            SetMemberIcon();

            int skillCount = memberOnTurn.Skills.Count;
            for (int i = 0; i < skillsParent.childCount; i++)
            {
                Transform skillChild = skillsParent.GetChild(i);

                if (i < skillCount)
                {
                    skillChild.GetChild(1).GetComponent<Image>().sprite = ImageManager.Instance.GetSprite(memberOnTurn.Skills[i].IconId);
                    skillChild.gameObject.SetActive(true);
                }
                else
                {
                    skillChild.gameObject.SetActive(false);
                }
            }
        }
        foreach (Transform child in transform)
        {
            if (!notTurnedoff.Contains(child))
            {
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
            if (cg != null)
            {
                Destroy(cg);
            }
        }
    }
    public void SpeedupClicked(Image i)
    {
        if (spedUp)
        {
            i.sprite = timeSpeedIcons[0];
            spedUp = false;
            Time.timeScale = 1;
        }
        else
        {
            i.sprite = timeSpeedIcons[1];
            spedUp = true;
            Time.timeScale = 2;
        }
    }
    public void SettingsClicked()
    {
        GameManager.Instance.OptionsDecide();
    }

    void SetMemberIcon()
    {
        var sprite = ImageManager.Instance.GetSprite(memberOnTurn.IconId);
        var fitter = memberIcon.gameObject.GetComponent<AspectRatioFitter>();
        float w = sprite.rect.width;
        float h = sprite.rect.height;
        float aspect = w / h;
        fitter.aspectRatio = aspect;
        memberIcon.sprite = sprite;
    }
    public void SetActiveSpeedtup(bool active)
    {
        pauseTransform.gameObject.SetActive(active);
        speedupTransform.gameObject.SetActive(active);
        if (!active)
        {
            Time.timeScale = 1;
        }
        else
        {
            if (spedUp)
            {
                Time.timeScale = 2;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    public void PausePlay()
    {
        if (stopped)
        {
            stopped = false;
            pauseTransform.gameObject.GetComponent<Image>().sprite = timeStopIcons[0];
            speedupTransform.gameObject.GetComponent<Button>().interactable = true;
            Time.timeScale = lastTimeSpeed;
        }
        else
        {
            stopped = true;
            lastTimeSpeed = Time.timeScale;
            Time.timeScale = 0;
            pauseTransform.gameObject.GetComponent<Image>().sprite = timeStopIcons[1];
            speedupTransform.gameObject.GetComponent<Button>().interactable = false;
        }
    }
    public void PassTurn()
    {
        StartCoroutine(EnableControls(false));
        memberOnTurn.PassTurn();

    }
    public void TurnOffSettinsButton()
    {
        optionsTransform.gameObject.SetActive(false);
    }
}

