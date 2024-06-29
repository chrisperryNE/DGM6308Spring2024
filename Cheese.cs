using System;
namespace ZooManager
{
    public class Cheese : Occupant
    {
        public bool cheeseTryMove = false;

        public Cheese()
        {
            this.emoji = "🧀";
            this.species = "cheese";
        }

        public override void OccupantActivate()
        {
            if (!cheeseTryMove)
            {
                base.OccupantActivate();
                Console.WriteLine("I am a rock.");
                NextToHole();
                //PushedByMeat();
                cheeseTryMove = true;
            }


        }

        public void NextToHole()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "hole"))
            {
                this.emoji = null;
                Game.cheeseScore++;
                Game.cheeseScore++;
                Game.cheeseScore++;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "hole"))
            {
                this.emoji = null;
                Game.cheeseScore++;
                Game.cheeseScore++;
                Game.cheeseScore++;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "hole"))
            {
                this.emoji = null;
                Game.cheeseScore++;
                Game.cheeseScore++;
                Game.cheeseScore++;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "hole"))
            {
                this.emoji = null;
                Game.cheeseScore++;
                Game.cheeseScore++;
                Game.cheeseScore++;
            }
        }



    }
	
}

