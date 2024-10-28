using System.Threading.Tasks;
using UnityEngine;

public interface ICommand
{
    Task Execute();
}

public abstract class PlayerCommand : ICommand
{
    protected readonly IComboEntity player;

    protected PlayerCommand(IComboEntity player)
    {
        this.player = player;
    }

    public abstract Task Execute();

    public static T Create<T>(IComboEntity player) where T : PlayerCommand
    {
        return (T) System.Activator.CreateInstance(typeof(T), player);
    }
}


/*
public class AttackCommand : PlayerCommand
{
    public AttackCommand(IComboEntity player) : base(player)
    {
    }


}*/