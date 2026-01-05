using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] List<Member> members;
    public List<Member> Members => members;
    List<Member> membersTurnOrder;
    Member memberToPlay;
    int roundCount;

    private void Awake()
    {
        instance = this;
        Debug.Log("The game begins!");
        membersTurnOrder = new List<Member>();
        for(int i  = 0; i < members.Count; i++)
        {
            Members[i].Position = i;
        }
        roundCount = 0;
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
        if (!Members.OfType<EnemyMember>().Any() || !Members.OfType<AllyMember>().Any())
        {
            Debug.Log("The Battle Ended");
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
}
