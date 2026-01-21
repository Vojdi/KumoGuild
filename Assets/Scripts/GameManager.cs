using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] List<Member> members;
    [SerializeField] Texture2D cursorTexture;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] GameObject enemiesGJ;
    Vector3[] enemyVector3 = new Vector3[] {new Vector3(0,0,0), new Vector3(2.75f, 2.5f, 0), new Vector3(5.5f,0, 0)};
    public List<Member> Members => members;
    List<Member> membersTurnOrder;
    Member memberToPlay;
    int roundCount;

    private void Awake()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(cursorTexture.width / 2, cursorTexture.width / 2), CursorMode.Auto);
        instance = this;
        Debug.Log("The game begins!");
        membersTurnOrder = new List<Member>();
        for(int i  = 0; i < members.Count; i++)
        {
            Members[i].Position = i;
        }
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
        NextTurn();
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
        if (membersTurnOrder.Count == 0)
        {
            roundCount++;
            TimeEffects();
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
        CheckForBattleEnd();
    }
    void CheckForBattleEnd()
    {
        if (!Members.OfType<EnemyMember>().Any())
        {
            Debug.Log("win");
            /*foreach(Transform child in enemiesGJ.transform)
            {
                Destroy (child.gameObject);
            }
            for (int i = 0; i < 3; i++)
            {
                List<EnemyMember> listPossibleEnemies = new List<EnemyMember>();
                foreach (GameObject gj in enemyPrefabs) {
                    Debug.Log(gj.GetComponent<EnemyMember>().DesiredPositions.Count + "dpc");
                    if (gj.GetComponent<EnemyMember>().DesiredPositions.Contains(i + 3)){
                        listPossibleEnemies.Add(gj.GetComponent<EnemyMember>());
                    }
                }
                Debug.Log(listPossibleEnemies.Count + " List Pos Enemies");
                int chosenEnemy = Random.Range(0, listPossibleEnemies.Count);
                Debug.Log(chosenEnemy + " Chosen Enemy");
                var enemy = Instantiate(listPossibleEnemies[chosenEnemy], enemyVector3[i], Quaternion.identity, enemiesGJ.transform);
                enemy.transform.localPosition = enemyVector3[i];    
                Debug.Log(enemy + " Enemy");
                while (members.Count <= i + 3)
                {
                    members.Add(null);
                }
                members[i + 3] = enemy.GetComponent<Member>();
            }
            membersTurnOrder.Clear();
            NextTurn();*/
            Time.timeScale = 0;
        }
        if (!Members.OfType<AllyMember>().Any())
        {
            Debug.Log("noob");
            Time.timeScale = 0;
        }
    } 
    void TimeEffects()
    {
        foreach (Member member in members.ToList())
        {
            member.EffectsTime();
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
