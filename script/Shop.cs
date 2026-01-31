using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;
public partial class Shop : CanvasLayer
{
	public int currentTogledId;
	public List<ShopStall> Children => GetChildren().OfType<ShopStall>().ToList();

	public void ShopingStallTogler(bool toggle, int id)
	{
		currentTogledId = id;
		Children[id].isToggled = !Children[id].isToggled;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int idCounter = 0;
        foreach (ShopStall child in Children)
        {
            child.id = idCounter;
			idCounter ++;
        }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (ShopStall child in Children)
			{
				if(child.isToggled)
				{
					if(child.id != currentTogledId)
					{
						child.isToggled = !child.isToggled ;
					}
				}
			}
	}
}
