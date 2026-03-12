using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Android.Types;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TeamSelection : MonoBehaviour
{
    static TeamSelection instance;
    public static TeamSelection Instance => instance;
    [SerializeField] GameObject mainMenuGj;
    [SerializeField] GameObject teamSelectionGj;

    [SerializeField] List<GameObject> addMemberGameObjects;
    [SerializeField] List<GameObject> addSkillGameObjects;

    [SerializeField] Sprite[] plusIcons;
    [SerializeField] Sprite[] crossIcons;
    [SerializeField] Sprite[] returnIcons;

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
    [SerializeField] AudioSource source;

    [SerializeField] TMPro.TMP_Text infoBoxName;
    [SerializeField] TMPro.TMP_Text infoBoxSkillType;
    [SerializeField] GameObject squaresParent;
    [SerializeField] List<GameObject> infoBoxPositionPanels;
    [SerializeField] List<TMPro.TMP_Text> infoBoxStatTexts;
    [SerializeField] RectTransform infoPanelRectTransform;
    Skill hoveredOverSkill;
    Member hoveredOverSkillMember;
    [SerializeField] Canvas canvas;



    private void Awake()
    {
        instance = this;
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
    public IEnumerator SkillHoveredOver(int index)
    {
        hoveredOverSkill = availableSkills[index];
        hoveredOverSkillMember = hoveredOverSkill.SelfMember;
        hoveredOverSkillMember.Position = currentCharSlot;
        InfoBoxSetup();
        var btn = skillSlots[index + 1];
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
            infoBoxPositionPanels[hoveredOverSkillMember.Position].SetActive(false);
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
        for (int i = 0; i < arr.Count; i++)
        {
            infoBoxStatTexts[i].text = arr[i];
            infoBoxStatTexts[i].gameObject.SetActive(true);
        }
    }
    public void SkillHoveredOut()
    {
        infoPanelRectTransform.gameObject.SetActive(false);
    }
    public void BackToMainMenu()
    {
        teamSelectionGj.SetActive(false);
        mainMenuGj.SetActive(true);
    }
    public void AddMemberButtonClicked(int id)
    {
        currentCharSlot = id;

        teamSelectionGj.SetActive(false);
        charSelect.SetActive(true);
        ShowCharacterSelectPanel(id);
    }
    void ShowCharacterSelectPanel(int id)
    {
        foreach (GameObject slot in charSlots) {
            slot.SetActive(false);
        }
        charSlots[0].transform.GetComponentInChildren<Image>().sprite = returnIcons[0];
        charSlots[0].SetActive(true);
        charSlots[1].transform.GetComponentInChildren<Image>().sprite = crossIcons[0];
        charSlots[1].SetActive(true);

        for (int i = 0; i < availableAllyMembers.Count; i++)
        {
            charSlots[i + 2].transform.GetComponentInChildren<Image>().sprite = charIcons[allyMemberTypes.IndexOf(availableAllyMembers[i])];
            charSlots[i + 2].SetActive(true);
        }
    }
    public void RemovedFromCharSel() {

        ResetSlots();
        ResetSelMember();
        addMemberGameObjects[currentCharSlot].GetComponent<Image>().sprite = plusIcons[0];
        charSelect.SetActive(false);
        teamSelectionGj.SetActive(true);

        for (int i = 0; i < 2; i++)
        {
            addSkillGameObjects[i + currentCharSlot * 2].SetActive(false);
        }
    }
    public void ReturnFromCharSel()
    {
        charSelect.SetActive(false);
        teamSelectionGj.SetActive(true);
    }
    public void SelectedChar(int id)
    {
        ResetSelMember();
        ResetSlots();
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
    void ResetSelMember()
    {
        if (selectedAllyMembers[currentCharSlot] != null)
        {
            AllyMember allyy = selectedAllyMembers[currentCharSlot];
            selectedAllyMembers[currentCharSlot] = null;
            availableAllyMembers.Add(allyy);
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
        PlayerPrefs.SetFloat("audio", source.volume);
        PlayerPrefs.SetString("build", prefString);
        SceneManager.LoadScene("Fight");
    }
}
