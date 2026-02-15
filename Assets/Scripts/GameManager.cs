using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] List<Member> members;
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] GameObject enemiesGJ;
    [SerializeField] Animator nextWaveAnimator;
    Vector3[] enemyVector3 = new Vector3[] {new Vector3(0,0,0), new Vector3(2.75f, 2.5f, 0), new Vector3(5.5f,0, 0)};
    Vector3[] allyVector3 = new Vector3[] { new Vector3(-5.0f, 0, 0), new Vector3(-2.5f, 2.5f, 0), new Vector3(0, 0, 0) };
    public List<Member> Members => members;
    List<Member> membersTurnOrder;
    Member memberToPlay;
    int roundCount;

    [SerializeField] AllyMember[] allyMemberTypes;
    [SerializeField] GameObject allyMembersParent;
    [SerializeField] LevelUpPanel levelUpPanel;




    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width / 2, cursorTexture.width / 2), CursorMode.Auto);
        instance = this;
        Debug.Log("The game begins!");
        membersTurnOrder = new List<Member>();
        roundCount = 0;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.R)) { 
            Restart();
        }
    }
    private void Start()
    {
        members[0].Position = 3;
        members[1].Position = 4;
        members[2].Position = 5;
        LoadBuild();
        NextTurn();
    }

    private void LoadBuild()
    {
        Dictionary<string, AllyMember> allyPrefabDict = new();
        foreach (var prefab in allyMemberTypes)
        {
            allyPrefabDict[prefab.MemberName] = prefab;
        }

        string build = PlayerPrefs.GetString("build");
        string[] memberEntries = build.Split('|');

        foreach (string entry in memberEntries)
        {
            if (string.IsNullOrEmpty(entry))
                continue;

            string[] parts = entry.Split(':');

            string memberName = parts[0];
            int position = int.Parse(parts[1]);
            string[] skillNames = parts.Length > 2 && !string.IsNullOrEmpty(parts[2])
                ? parts[2].Split(',')
                : new string[0];

            if (!allyPrefabDict.TryGetValue(memberName, out AllyMember prefab))
                continue;

            AllyMember instance = Instantiate(prefab, Vector3.zero, Quaternion.identity, allyMembersParent.transform);
            instance.Position = position;
            instance.transform.localPosition = allyVector3[instance.Position];
            members.Add(instance);

            SetupSkills(instance, skillNames);
        }
    }
    private void SetupSkills(AllyMember member, string[] allowedSkillNames)
    {
        for (int i = member.Skills.Count - 1; i >= 0; i--)
        {
            string skillName = member.Skills[i].SkillName;
            bool keep = false;

            foreach (string allowed in allowedSkillNames)
            {
                if (skillName == allowed)
                {
                    keep = true;
                    break;
                }
            }

            if (!keep)
                member.Skills.RemoveAt(i);
        }
        Debug.Log(member.Skills.Count);
    }
    private void DetermineTurnOrder()
    {
        Debug.Log("making turn order...");
        List<Member> membersTemp = members.ToList();
        List<Member> membersMaxSpeed = new List<Member>();
        List<Member> membersToRemove = new List<Member>();
        while (membersTemp.Count > 0)
        {
            int maxSpeed = 0;
            foreach (Member member in membersTemp)
            {
                if (member.Speed > maxSpeed)
                {
                    maxSpeed = member.Speed;
                }
            }
            foreach (Member member in membersTemp)
            {
                if (member.Speed == maxSpeed)
                {
                    membersMaxSpeed.Add(member);
                    membersToRemove.Add(member);
                }
            }
            foreach (Member member in membersToRemove)
            {
                membersTemp.Remove(member);
            }
            membersToRemove.Clear();
            ShuffleSameSpeedMembers(membersMaxSpeed);
        }
    }
    private void ShuffleSameSpeedMembers(List<Member> membersMaxSpeed)
    {
        while (membersMaxSpeed.Count > 0)
        {
            int RandomNumber = Random.Range(0, membersMaxSpeed.Count);
            membersTurnOrder.Add(membersMaxSpeed[RandomNumber]);
            membersMaxSpeed.Remove(membersMaxSpeed[RandomNumber]);
        }
    }

    public void NextTurn()
    {
        if(CheckForBattleEnd())
        {
            return;
        }
        if (membersTurnOrder.Count == 0)
        {
            roundCount++;
            TimeEffects();
            if (CheckForBattleEnd())
            {
                return;
            }
            VisualEffectManager.Instance.ActionQueue.Enqueue(() =>VisualEffectManager.Instance.NewRound(roundCount));
            DetermineTurnOrder();
        }
        ControlPanel.Instance.AbleToCheckEffects = true;
        memberToPlay = membersTurnOrder[0];
        membersTurnOrder.RemoveAt(0);
        VisualEffectManager.Instance.ActionQueue.Enqueue(() => VisualEffectManager.Instance.NewTurnEffect(memberToPlay));
        VisualEffectManager.Instance.ActionQueue.Dequeue()?.Invoke();
    }
    public void MemberToPlayTurn()
    {
        memberToPlay.YourTurn();
    }
    public void MemberDied(Member member)
    {
        Members.Remove(member);
        if (membersTurnOrder.Contains(member))
        {
            membersTurnOrder.Remove(member);
        }
        member.Die();
    }
    bool CheckForBattleEnd()
    {
        if (!Members.OfType<EnemyMember>().Any())
        {
            nextWaveAnimator.Play("nextWaveBg", 0, 0);
            ControlPanel.Instance.SetActiveSpeedtup(false);
            return true;
        }
        else if (!Members.OfType<AllyMember>().Any())
        {
            Debug.Log("noob");
            Time.timeScale = 0;
            return true;
        }
        else
        {
            return false;
        }
    } 
    public void NextWaveSpawn()
    {
        foreach (Transform child in enemiesGJ.transform)
        {
            EnemyMember em = child.GetComponent<EnemyMember>();
            if (em != null)
            {
                members.Remove(em);
            }
            Destroy(child.gameObject);
        }
        int allyCount = members.OfType<AllyMember>().Count();
        for (int i = 0; i < 3; i++)
        {
            List<EnemyMember> listPossibleEnemies = new List<EnemyMember>();
            foreach (GameObject gj in enemyPrefabs)
            {
                Debug.Log(gj.GetComponent<EnemyMember>().DesiredPositions.Count + "dpc");
                if (gj.GetComponent<EnemyMember>().DesiredPositions.Contains(i + 3))
                {
                    listPossibleEnemies.Add(gj.GetComponent<EnemyMember>());
                }
            }
            Debug.Log(listPossibleEnemies.Count + " List Pos Enemies");
            int chosenEnemy = Random.Range(0, listPossibleEnemies.Count);
            Debug.Log(chosenEnemy + " Chosen Enemy");
            var enemy = Instantiate(listPossibleEnemies[chosenEnemy], enemyVector3[i], Quaternion.identity, enemiesGJ.transform);
            enemy.transform.localPosition = enemyVector3[i];
            enemy.Position = i + 3;
            Debug.Log(enemy + " Enemy");
            while (members.Count <= i + allyCount)
            {
                members.Add(null);
            }
            members[i + allyCount] = enemy.GetComponent<Member>();
        }
        membersTurnOrder.Clear();
        roundCount = 0;

        levelUpPanel.gameObject.SetActive(true);
        levelUpPanel.SetUp();
        levelUpPanel.GetComponent<Animator>().Play("app",0,0);

    }
    public void NextWaveReady()
    {
        levelUpPanel.gameObject.SetActive(false);
        NextTurn();
        ControlPanel.Instance.SetActiveSpeedtup(true);
    }
    void TimeEffects()
    {
        foreach (Member member in members.ToList())
        {
            Debug.Log(member);
        }
        foreach (Member member in members.ToList())
        {
            Debug.Log(member);
            member.EffectsTime();
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
