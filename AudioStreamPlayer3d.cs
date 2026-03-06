using Godot;
using System;

public partial class AudioStreamPlayer3d : AudioStreamPlayer3D
{
	[Export] Player player;
    // Called when the node enters the scene tree for the first time.

    bool dead=false;
    public override void _Ready()
	{
        player.DamageTaken += OnPlayerDamageTaken;
        player.death += OnPlayerDeath;

        Stop();

    }
    private void OnPlayerDamageTaken(object sender, EventArgs e)
    {  if(!dead)
        { 
            Play(); 
        }

          

    }
    private void OnPlayerDeath(object sender, EventArgs e)
    {
        dead = true;
        Stop();

    }


}
