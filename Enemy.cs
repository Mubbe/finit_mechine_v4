using Godot;
using System;


public partial class Enemy : Node3D
{
    [Export] public Player player;
    [Export] public MeshInstance3D Head;
    [Export] public Area3D Area_head;
    [Export] public Area3D Area_body;
    



    bool dead = false;


    // Called when the node enters the scene tree for the first time.
    enum Npcstate
    {
        idle,
        chasing,
        waiting,
        death
    }

    public override void _Ready()
    {
           
            Area_body.BodyEntered += BodyEntered;
            Area_body.BodyExited += BodyExited;

            Area_head.BodyEntered += Area_Head_Entered;
            Area_head.BodyExited += Area_Head_Exited;

    }

    float time=3f;
    float deathtime = 9999999f;
    private Npcstate currentstate = Npcstate.idle;
    public override void _PhysicsProcess(double delta)
    {
        


        switch (currentstate)
        {
            case Npcstate.idle:
                {
                    Vector3 targetPos = player.GlobalPosition;
                    targetPos.Y = GlobalPosition.Y;



                    float dis = (player.GlobalPosition - GlobalPosition).Length();
                    if (dis <= 10f)
                    {
                        currentstate = Npcstate.chasing;
                        GD.Print("Chasing player...");
                    }



                    break;
                    
                }
            case Npcstate.chasing:
                {

                    // Move towards the player but keep the same Y position:
                    Vector3 targetPos = player.GlobalPosition;
                    targetPos.Y = GlobalPosition.Y;

                    Vector3 dir = (targetPos - GlobalPosition).Normalized();
                    float speed = 2f;
                    GlobalTranslate(dir * speed * (float)delta);


                    float dis = (player.GlobalPosition - GlobalPosition).Length();
                    if (dis >= 10f)
                    {
                        currentstate= Npcstate.idle;
                        GD.Print("idle player...");
                    }
                    
                    
                    break;
                }
            case Npcstate.waiting:
                {
                    time -= (float)delta;

                    if (time <= 0f)
                    {
                        time = 3f;

                        float dis = (player.GlobalPosition - GlobalPosition).Length();

                        if (dis <= 10f)
                        {
                            currentstate = Npcstate.chasing;
                        }
                        else
                        {
                            currentstate = Npcstate.idle;
                        }

                        GD.Print("Waiting finished.");
                    }

                    break;
                }
                case Npcstate.death:
                {

                    /* deathtime-= (float)delta;
                     if (deathtime <= 0f)
                     {
                         deathtime = 9999999f;


                     }*/
                    Head.Scale = new Vector3(1f, 0.5f, 1f);
                    GD.Print("Enemy is dead!");
                    //Area_body.GlobalPosition = new Vector3(0, -100, 0);
                    //Area_head.GlobalPosition = new Vector3(0, -100, 0);
                    Area_head.Monitoring = false;
                    Area_body.Monitoring = false;

                    break;

                }


        }
    }

    void BodyEntered(Node3D body)
    {
       
        if (body == player && !dead)
        {

                GD.Print("BodyEntered called with: ", body.Name);
                player.TakeDamage(10);
                currentstate = Npcstate.waiting;
                GD.Print("Player entered, dealing damage and waiting...");
            
        }

    }

    void BodyExited(Node3D body)
    {
        if (body == player)
        {


        }
    }

    void Area_Head_Entered(Node body)
    {
        
        if (body == player)
        {          
            dead=true;
            Head.Scale = new Vector3(1f, 0.5f, 1f);
            GD.Print("Player entered head area, scaling head down!");
            
            Area_body.Monitoring = false;
          



            currentstate = Npcstate.death;

            player.death_state();

        }
    }

    void Area_Head_Exited(Node body)
    {
        if (body == player)
        {



            GD.Print("Player exited");

        }
    }


}
