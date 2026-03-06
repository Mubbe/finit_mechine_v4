using Godot;
using System;

public partial class PlayerUi : Node3D
{
    [Export] Player player;
    [Export] ProgressBar healthBar;

    public override void _Ready()
    {
        player.DamageTaken += OnPlayerDamaged;
        player.death += OnPlayerdeath;

        healthBar.MaxValue = player.MaxHealth;
        healthBar.Value = player.CurrentHealth;
    }

    private void OnPlayerDamaged(object sender, EventArgs e)
    {
        healthBar.Value = player.CurrentHealth;
    }


    private void OnPlayerdeath(object sender, EventArgs e)
    {
        healthBar.Value = player.CurrentHealth;
    }
}
