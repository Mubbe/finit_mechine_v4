using Godot;
using System;


public partial class Player : CharacterBody3D
{

    public int MaxHealth = 100;
    public int CurrentHealth = 100;

    public event EventHandler DamageTaken;
    public event EventHandler death;



    public override void _Ready()
    {
       
      

    }
    public void TakeDamage(int damage)
	{
        CurrentHealth -= damage;
        GD.Print("Player took damage! Health now: ", CurrentHealth);
		if (CurrentHealth < 0)
		{
			CurrentHealth = 0;
		}

        DamageTaken?.Invoke(this, EventArgs.Empty);
    }

    public void death_state()
    {

        DamageTaken?.Invoke(this, EventArgs.Empty);
    }





    public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}


		Velocity = velocity;
		MoveAndSlide();
	}
}
