using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using Unity.Android.Types;
using System.Linq;
using UnityEngine.SceneManagement;

public class TeamSelection : MonoBehaviour
{
    [SerializeField] GameObject mainMenuGj;
    [SerializeField] GameObject teamSelectionGj;

    [SerializeField] List<GameObject> addMemberGameObjects;
    [SerializeField] List<GameObject> addSkillGameObjects;

    [SerializeField] Sprite[] plusIcons;
    [SerializeField] Sprite[] crossIcons;

    [SerializeField] Sprite[] charIcons;
    [SerializeField] List<AllyMember> allyMemberTypes;

    [SerializeField] GameObject charSelect;
    [SerializeField] GameObject[] charSlots;
    [SerializeField] GameObject skillSelect;
    [SerializeField] GameObject[] skillSlots;

    List<AllyMember> availableAllyMembers;
    int currentCharSlot;
    int currentSkillSlot;
    AllyMember[] selectedAllyMembers;

    List<Skill> availableSkills;
    List<List<Skill>> skillTypes;
    Skill[] selectedSkills;

    private void Awake()
    {
        availableAllyMembers = new List<AllyMember>();
        selectedAllyMembers = new AllyMember[3];
        availableSkills = new List<Skill>();
        selectedSkills = new Skill[6];
        skillTypes = new List<List<Skill>>();
    }
    
    private void Start()
    {
        foreach (AllyMember member in allyMemberTypes)
        {
            availableAllyMembers.Add(member);
        }

        for (int i = 0; i < allyMemberTypes.Count; i++)
        {
            List<Skill> skillsForAlly = new List<Skill>();

            for (int j = 0; j < allyMemberTypes[i].Skills.Count; j++)
            {
                skillsForAlly.Add(allyMemberTypes[i].Skills[j]);
            }

            skillTypes.Add(skillsForAlly);
        }
    }
    public void BackToMainMenu()
    {
        teamSelectionGj.SetActive(false);
        mainMenuGj.SetActive(true);
    }
    public void AddMemberButtonClicked(int id)
    {
        currentCharSlot = id;
        ResetSlots(); 
        if (selectedAllyMembers[currentCharSlot] != null)
        {
            AllyMember ally = selectedAllyMembers[currentCharSlot];
            selectedAllyMembers[currentCharSlot] = null;
            availableAllyMembers.Add(ally);
        }
        teamSelectionGj.SetActive(false);
        charSelect.SetActive(true);
        ShowCharacterSelectPanel(id);
    }
    void ShowCharacterSelectPanel(int id)
    {
        foreach (GameObject slot in charSlots) {
            slot.SetActive(false);
        }
        charSlots[0].transform.GetComponentInChildren<Image>().sprite = crossIcons[0];
        charSlots[0].SetActive(true);
        for (int i = 0; i < availableAllyMembers.Count; i++) 
        {
            charSlots[i + 1].transform.GetComponentInChildren<Image>().sprite = charIcons[allyMemberTypes.IndexOf(availableAllyMembers[i])];
            charSlots[i + 1].SetActive(true);   
        }
    }
    public void ExitedFromCharSel() {

        addMemberGameObjects[currentCharSlot].GetComponent<Image>().sprite = plusIcons[0];
        charSelect.SetActive(false);
        teamSelectionGj.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            addSkillGameObjects[i + currentCharSlot * 2].SetActive(false);   
        }
    }
    public void SelectedChar(int id)
    {
        addMemberGameObjects[currentCharSlot].GetComponent<Image>().sprite = charIcons[allyMemberTypes.IndexOf(availableAllyMembers[id])]; 
        AllyMember ally = availableAllyMembers[id];
        availableAllyMembers.RemoveAt(id);
        selectedAllyMembers[currentCharSlot] = ally;
        charSelect.SetActive(false);
        teamSelectionGj.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            addSkillGameObjects[i + currentCharSlot * 2].SetActive(true);
        }
    }
    public void AddSkillButtonClicked(int id)
    {
        currentCharSlot = id / 2;
        currentSkillSlot = id;
        if (selectedSkills[id] != null)
        {
            selectedSkills[id] = null;
        }
        teamSelectionGj.SetActive(false);
        skillSelect.SetActive(true);
        ShowSkillSelectPanel(id);
    }
    void ShowSkillSelectPanel(int id)
    {
        foreach (GameObject slot in skillSlots)
        {
            slot.SetActive(false);
        }
        skillSlots[0].transform.GetComponentInChildren<Image>().sprite = crossIcons[1];
        skillSlots[0].SetActive(true);
        SetAvailableSkills();

        for (int i = 0; i < availableSkills.Count; i++)
        {
            skillSlots[i + 1].transform.GetComponentInChildren<Image>().sprite = ImageManager.Instance.GetSprite(availableSkills[i].IconId);
            skillSlots[i + 1].SetActive(true);
        }
    }
    void SetAvailableSkills()
    {
        availableSkills.Clear();
        Skill[] tempSkills = selectedAllyMembers[currentCharSlot].Skills.ToArray();
        foreach (Skill skill in tempSkills)
        {
            if (skill.UsableFromPositions.Contains(currentCharSlot) && !selectedSkills.Contains(skill))
            {
                availableSkills.Add(skill);
            }
        }
    }
    public void SelectedSkill(int id)
    {
        addSkillGameObjects[currentSkillSlot].GetComponent<Image>().sprite = ImageManager.Instance.GetSprite(availableSkills[id].IconId);
        Skill skill = availableSkills[id];
        selectedSkills[currentSkillSlot] = skill;
        skillSelect.SetActive(false);
        teamSelectionGj.SetActive(true);
    }
    public void ExitedFromSkillSel()
    {
        addSkillGameObjects[currentSkillSlot].GetComponent<Image>().sprite = plusIcons[1];
        skillSelect.SetActive(false);
        teamSelectionGj.SetActive(true);
    }
    public void ResetSlots()
    {
        int index1 = currentCharSlot * 2;
        int index2 = currentCharSlot * 2 + 1;

        addSkillGameObjects[index1].GetComponent<Image>().sprite = plusIcons[1];
        addSkillGameObjects[index2].GetComponent<Image>().sprite = plusIcons[1];

        selectedSkills[index1] = null;
        selectedSkills[index2] = null;
    }
    public void Begin()
    {
        string prefString = "";
        bool firstMemberAdded = false;

        for (int i = 0; i < selectedAllyMembers.Length; i++)
        {
            if (selectedAllyMembers[i] == null)
                continue;

            if (firstMemberAdded)
                prefString += "|";

            firstMemberAdded = true;

            prefString += selectedAllyMembers[i].MemberName;
            prefString += ":" + i;
            prefString += ":";

            int skillStartIndex = i * 2;
            bool firstSkillAdded = false;

            for (int j = skillStartIndex; j < skillStartIndex + 2; j++)
            {
                if (j < 0 || j >= selectedSkills.Length)
                    continue;

                if (selectedSkills[j] == null)
                    continue;

                if (firstSkillAdded)
                    prefString += ",";

                firstSkillAdded = true;
                prefString += selectedSkills[j].SkillName;
            }
        }
        PlayerPrefs.SetString("build", prefString);
        SceneManager.LoadScene("Fight");
    }
}
