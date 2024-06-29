using System;
namespace ZooManager
{
	public class Rock : Occupant
	{
        public bool rockTryMove = false;

        public Rock()
		{
			this.emoji = "🪨";
			this.species = "rock";

        }

        public override void OccupantActivate()
        {
            if (!rockTryMove)
            {
                base.OccupantActivate();
                Console.WriteLine("I am a rock.");
                //PushedByDog();
                HuntForHole();
                rockTryMove = true;
            }
        }

        public void HuntForHole()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "hole"))
            {
                this.emoji = null;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "hole"))
            {
                this.emoji = null;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "hole"))
            {
                this.emoji = null;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "hole"))
            {
                this.emoji = null;
            }
        }

        //public void PushedByDog()
        //{

        //    if (Game.Seek(location.x, location.y, Direction.up, "dog") && (Game.dogisMovingDown))
        //    {
        //        Game.RockPushed(this, Direction.down);
        //    }
        //    if (Game.Seek(location.x, location.y, Direction.down, "dog") && (Game.dogisMovingUp))
        //    {
        //        Game.RockPushed(this, Direction.up);
        //    }
        //    if (Game.Seek(location.x, location.y, Direction.left, "dog") && (Game.dogisMovingRight))
        //    {
        //        Game.RockPushed(this, Direction.right);
        //    }
        //    if (Game.Seek(location.x, location.y, Direction.right, "dog") && (Game.dogisMovingLeft))
        //    {
        //        Game.RockPushed(this, Direction.left);
        //    }
            
        //}
    }
}

