using NUnit.Framework.Internal.Filters;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPanel : MonoBehaviour
{
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
    private void Start()
    {
        lvlUpAnimator = GetComponent<Animator>();   
    }
    public void SetUp()
    {
        confirmGj.SetActive(false);
        LoadMemberIcons();
        LoadSkillIcons();
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
    void CallNextWave()
    {
        nextWaveAnimator.Play("afterLvlUpBg", 0, 0);
    }
}
