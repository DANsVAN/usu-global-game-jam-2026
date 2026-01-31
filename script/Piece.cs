using Godot;
using System;

public partial class Piece : Node2D
{
	public int movement = 1;
	public int move_dir = 1;
	public	int damage = 1;
	public 	int HP = 1;
	public bool can_move = true;
	public bool tryed_to_move = true;
	// Called when the node enters the scene tree for the first time.


	public void MoveOneHalfSec(Vector2 targetPos)
	{
		Tween tween = GetTree().CreateTween();
		
		// SetTrans(Linear) ensures there is no "acceleration" or "easing"
		tween.SetTrans(Tween.TransitionType.Linear);
		
		// This will ALWAYS take 1.0 seconds, no matter how far away targetPos is
		tween.TweenProperty(this, "position", targetPos, 0.5f);
	}
	public void SetPos(float positionX, float positionY)
	{
		// 1. Get current position
		Vector2 currentPos = Position;

		// 2. Modify only what you need
		currentPos.X = positionX; 
		currentPos.Y = positionY; 

		// 3. Re-assign it
		MoveOneHalfSec(currentPos);
		// Position = currentPos;
	}
	public override void _Ready()
	{

	}
	public void init_data(int m_dir){
		move_dir = m_dir;
		if (move_dir > 0){
			Modulate = new Color(0.0f, 0.0f, 1.0f);
		}
		if (move_dir < 0){
			Modulate = new Color(1.0f, 0.0f, 0.0f);
		}
	}
	public int[] get_move(){
		int[] next_spot = {1*move_dir, 0};
		return next_spot ;
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
