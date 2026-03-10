using NUnit.Framework.Internal.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelUpPanel : MonoBehaviour
{
    static LevelUpPanel instance;
    public static LevelUpPanel Instance => instance;
    [SerializeField] GameObject [] memberGjs;
    [SerializeField] GameObject [] skillsGjs;
    [SerializeField] Sprite missingMemberIconSprite;
    [SerializeField] GameObject confirmGj;
    [SerializeField] Animator nextWaveAnimator;
    Animator lvlUpAnimator;

    Color32 maxColor = new Color32(99,215,255,255);
    Color32 selectedColor = new Color32(255,253,0,255);

    GameObject selectedGj;
    Skill selectedSkill;

    Skill hoveredOverSkill;
    [SerializeField] Canvas canvas;
    [SerializeField] TMPro.TMP_Text infoBoxName;
    [SerializeField] TMPro.TMP_Text infoBoxSkillType;
    [SerializeField] List<GameObject> infoBoxPositionPanels;
    [SerializeField] List<TMPro.TMP_Text> infoBoxStatTexts;
    [SerializeField] RectTransform infoPanelRectTransform;
    Member memberHoveredOver;
    private void Start()
    {
        lvlUpAnimator = GetComponent<Animator>();
        instance = this;
    }
    public void SetUp()
    {
        confirmGj.SetActive(false);
        LoadMemberIcons();
        LoadSkillIcons();
    }
    public bool CheckIfPossibleToUpgrades()
    {
        foreach(Member member in GameManager.Instance.Members.Where(m => m is AllyMember))
        {
            foreach (Skill skill in member.Skills)
            {
                if(skill.Level == 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void LoadMemberIcons()
    {
        foreach (GameObject gj in memberGjs)
        {
            var sprite = gj.GetComponent<Image>().sprite = missingMemberIconSprite;
            var fitter = gj.gameObject.GetComponent<AspectRatioFitter>();
            float w = sprite.rect.width;
            float h = sprite.rect.height;
            float aspect = w / h;
            fitter.aspectRatio = aspect;
            gj.GetComponent<Image>().sprite = sprite;
        }
        foreach (Member member in GameManager.Instance.Members.Where(m => m is AllyMember))
        {
            if (memberGjs[member.Position] != null && member is AllyMember allyMember)
            {
                var sprite = ImageManager.Instance.GetSprite(allyMember.IconId);
                var fitter = memberGjs[allyMember.Position].gameObject.GetComponent<AspectRatioFitter>();
                float w = sprite.rect.width;
                float h = sprite.rect.height;
                float aspect = w / h;
                fitter.aspectRatio = aspect;
                memberGjs[allyMember.Position].GetComponent<Image>().sprite = sprite;
            }
        }
    }
    void LoadSkillIcons()
    {
        selectedGj = null;
        foreach (GameObject skillG in skillsGjs)
        {
            skillG.SetActive(false);
            GameObject child1gj = skillG.transform.GetChild(1).gameObject;
            child1gj.GetComponent<OnImageHover>().enabled = true;
            child1gj.GetComponent<Button>().enabled = true;
            
            GameObject child0gj = skillG.transform.GetChild(0).gameObject;
            var img = child0gj.gameObject.GetComponent<Image>();
            Color32 color = img.color;
            img.color = new Color32(color.r, color.g, color.b, 0);
        }
        foreach (Member member in GameManager.Instance.Members.Where(m => m is AllyMember))
        {
            if (memberGjs[member.Position] != null && member is AllyMember allyMember)
            {
                for (int i = 0; i < allyMember.Skills.Count; i++)
                {
                    var gj = skillsGjs[allyMember.Position * 2 + i];
                    gj.SetActive(true);
                    gj.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = ImageManager.Instance.GetSprite(allyMember.Skills[i].IconId);

                    if (allyMember.Skills[i].Level == 1)
                    {
                        MaxLevelSkill(gj);
                    }
                    
                }
            }
        }
    }
    void MaxLevelSkill(GameObject skillG)
    {
        var childImg = skillG.transform.GetChild(0).GetComponent<Image>();
        childImg.color = maxColor;

        GameObject child1gj = skillG.transform.GetChild(1).gameObject;
        child1gj.GetComponent<OnImageHover>().enabled = false;
        child1gj.GetComponent<Button>().enabled = false;
    }
    public void SkillClicked(int buttonId)
    {
        confirmGj.SetActive(true);
        var childImg = skillsGjs[buttonId].transform.GetChild(0).GetComponent<Image>();
        
        if (selectedGj != null) {
            Image pastChildImg = selectedGj.transform.GetChild(0).gameObject.GetComponent<Image>();
            Color32 c1 = pastChildImg.color;
            pastChildImg.color = new Color32(c1.r, c1.g, c1.b, 0);
        }

        childImg.color = selectedColor;

        selectedGj = skillsGjs[buttonId];
        int positionIndex = Array.IndexOf(skillsGjs, selectedGj) / 2;
        int memberSkillIndex;
        if(Array.IndexOf(skillsGjs, selectedGj) % 2 == 0)
        {
            memberSkillIndex = 0;
        }
        else
        {
            memberSkillIndex = 1;   
        }
        selectedSkill = GameManager.Instance.Members.FirstOrDefault(m => m.Position == positionIndex).Skills[memberSkillIndex];

    }
    public void ConfirmClicked()
    {
        selectedSkill.Level = 1;
        lvlUpAnimator.Play("dapp", 0, 0);   
    }
    public void CallNextWave()
    {
        nextWaveAnimator.Play("afterLvlUpBg", 0, 0);
    }
    public IEnumerator SkillHoveredOver(int index)
    {
        GameObject gj = skillsGjs[index];
        int positionIndex = Array.IndexOf(skillsGjs, gj) / 2;
        int memberSkillIndex;
        if (Array.IndexOf(skillsGjs, selectedGj) % 2 == 0)
        {
            memberSkillIndex = 0;
        }
        else
        {
            memberSkillIndex = 1;
        }
        memberHoveredOver = GameManager.Instance.Members.FirstOrDefault(m => m.Position == positionIndex);
        hoveredOverSkill = GameManager.Instance.Members.FirstOrDefault(m => m.Position == positionIndex).Skills[memberSkillIndex];



        InfoBoxSetup();
        var btn = skillsGjs[index];
        var cg = infoPanelRectTransform.gameObject.AddComponent<CanvasGroup>();
        cg.alpha = 0;
        infoPanelRectTransform.gameObject.SetActive(true);
        infoPanelRectTransform.position = btn.transform.position + new Vector3(0, 50 * canvas.scaleFactor);
        yield return null;
        Destroy(cg);

    }
    public void SkillHoveredOut()
    {
        infoPanelRectTransform.gameObject.SetActive(false);
    }
    void InfoBoxSetup()
    {
        infoBoxSkillType.gameObject.SetActive(true);
        infoBoxName.text = Skill.GetInfoForInfoBox(hoveredOverSkill, "name")[0];
        List<string> arr = Skill.GetInfoForInfoBox(hoveredOverSkill, "skillType");
        infoBoxSkillType.text = $"{arr[0]}/{arr[1]}";
        if (arr[0] == "attack")
        {
            infoBoxSkillType.color = new Color32(255, 9, 71, 255);
        }
        else
        {
            infoBoxSkillType.color = new Color32(0, 255, 128, 255);
        }

        arr = Skill.GetInfoForInfoBox(hoveredOverSkill, "positions");
        if (arr[0] == "self")
        {
            foreach (var panel in infoBoxPositionPanels)
            {
                panel.gameObject.SetActive(true);
            }
            infoBoxPositionPanels[memberHoveredOver.Position].SetActive(false);
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

        arr = Skill.GetInfoForInfoBox(hoveredOverSkill, "stats2");
        foreach (var statText in infoBoxStatTexts)
        {
            statText.gameObject.SetActive(false);
        }
        for (int i = 0; i < arr.Count; i++)
        {
            infoBoxStatTexts[i].text = arr[i];
            infoBoxStatTexts[i].gameObject.SetActive(true);
        }
    }
}
