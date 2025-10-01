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

    private void Awake()
    {
        instance = this;
        Debug.Log("The game begins!");
        membersTurnOrder = new List<Member>();
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
            foreach (Member member in members) {
                member.EffectsTime();
                
            }
            Debug.Log("New Round");
            DetermineTurnOrder();
        }
        Member memberToPlay = membersTurnOrder[0];
        membersTurnOrder.RemoveAt(0);
        memberToPlay.YourTurn();
    }
}
