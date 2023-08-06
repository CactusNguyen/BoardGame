using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Activation", fileName = "Activation")]
public class Activation : ScriptableObject
{
    public void DrawProgrammerCard()
    {
        GameMaster.Ins.DrawCard(CardType.Programmer);
    }
    
    public void DrawArtistCard()
    {
        GameMaster.Ins.DrawCard(CardType.Artist);
    }
    
    public void DrawDesignerCard()
    {
        GameMaster.Ins.DrawCard(CardType.Designer);
    }
    
    public void DrawMarketingCard()
    {
        GameMaster.Ins.DrawCard(CardType.Marketing);
    }
    
    public void DrawProjectCard()
    {
        GameMaster.Ins.DrawCard(CardType.Project);
    }
    
    public void DrawChallengeCard()
    {
        GameMaster.Ins.DrawCard(CardType.Challenge);
    }

    public void DrawOutSourceCard()
    {
        GameMaster.Ins.DrawCard(CardType.OutSource);
    }

    public void Move(int stepCount)
    {
        GameMaster.Ins.CurrentPlayer.Move(stepCount);
    }

    public void ChangeFinance(int amount)
    {
        GameMaster.Ins.CurrentPlayer.ChangeFinance(amount);
    }

    public void EndTurn()
    {
        GameMaster.Ins.EndTurn();
    }
}

public enum CardType
{
    Programmer,
    Artist,
    Designer,
    Marketing,
    Project,
    Challenge,
    OutSource
}