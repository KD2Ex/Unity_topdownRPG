using UnityEngine;

[CreateAssetMenu(fileName = "MediaCommand", menuName = "Console/Commands/Media")]
public class MediaCommand : ScriptableConsoleCommand
{
    //public CommandAlias CommandAliases;
    public MediaData Data;

    private CommandsMediaPlayer mediaPlayer => GameManager.instance.CommandsMediaPlayer;
    
    public override void Execute()
    {
        mediaPlayer.Execute(Data);
    }

    public override bool CanExecute()
    {
        return Data.animationClip != null && Data.audioClip != null;
    }
}