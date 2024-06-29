using System;

namespace ZooManager
{
    public class Dog : Animal
    {
        public bool dogmoveUp = false;
        public bool dogmoveDown = false;
        public bool dogmoveLeft = false;
        public bool dogmoveRight = false;
        public bool doghasMoved = false;
        public bool dogisSitting = false;
        private int dogSittingTurns = 0;
        public bool dogStuck = false;

        public Dog(string name)
        {
            emoji = "🐶";
            species = "dog";
            this.name = name;
            reactionTime = 1;
            pointDirection = "";
            DogStartDirection();
        }

        private void DogStartDirection()
        {
            Random random = new Random();
            int dogstartDirection = random.Next(1, 5);
            switch (dogstartDirection)
            {
                case 1:
                    dogmoveUp = true;
                    pointDirection = "↑";
                    break;
                case 2:
                    dogmoveDown = true;
                    pointDirection = "↓";
                    break;
                case 3:
                    dogmoveLeft = true;
                    pointDirection = "←";
                    break;
                case 4:
                    dogmoveRight = true;
                    pointDirection = "→";
                    break;
            }
        }

        public override void Activate()
        {
            if (!doghasMoved)
            {
                base.Activate();
                Console.WriteLine("I am a dog. Woof.");



                if (DogEatMeat())
                {
                    doghasMoved = true;
                    return;
                }

                if (!DogHuntMouse() && !DogSmellMeat() && !DogHuntCat() && !DogEatCheese() && !DogPushRock() && !DogStuckHole())
                {
                    DogMove();
                }
                //ensures only one movement
                doghasMoved = true;
            }
        }

        public void DogMove()
        {
            //Console.WriteLine($"MoveLeft: {moveLeft}, MoveUp: {moveUp}, MoveRight: {moveRight}, MoveDown: {moveDown}");
            if (dogmoveLeft && Game.DogMoving(this, Direction.left))
            {
                return;
            }

            else if (dogmoveUp && Game.DogMoving(this, Direction.up))
            {
                return;
            }

            else if (dogmoveRight && Game.DogMoving(this, Direction.right))
            {
                return;
            }

            else if (dogmoveDown && Game.DogMoving(this, Direction.down))
            {
                return;
            }

            else
            {
                SwitchDirection();
            }

            return;
        }

        public void SwitchDirection()
        {
            if (dogmoveLeft)
            {
                dogmoveLeft = false;
                dogmoveRight = true;
                pointDirection = "→";
                Game.DogMoving(this, Direction.right);
                return;
            }
            else if (dogmoveRight)
            {
                dogmoveRight = false;
                dogmoveLeft = true;
                pointDirection = "←";
                Game.DogMoving(this, Direction.left);
                return;
            }
            else if (dogmoveUp)
            {
                dogmoveUp = false;
                dogmoveDown = true;
                pointDirection = "↓";
                Game.DogMoving(this, Direction.down);
                return;
            }
            else if (dogmoveDown)
            {
                dogmoveDown = false;
                dogmoveUp = true;
                pointDirection = "↑";
                Game.DogMoving(this, Direction.up);
                return;
            }
        }

        public bool DogEatMeat()
        {
            if (dogSittingTurns > 0)
            {
                Game.DogMoving(this, Direction.none);
                dogSittingTurns--;
                return true;
            }


            if (Game.Seek(location.x, location.y, Direction.up, "meat"))
            {
                Game.Attack(this, Direction.up);
                dogmoveUp = true;
                dogmoveDown = false;
                dogmoveLeft = false;
                dogmoveRight = false;
                dogSittingTurns = 1;
                dogStuck = false;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "meat"))
            {
                Game.Attack(this, Direction.down);
                dogmoveUp = false;
                dogmoveDown = true;
                dogmoveLeft = false;
                dogmoveRight = false;
                dogStuck = false;
                dogSittingTurns = 1;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "meat"))
            {
                Game.Attack(this, Direction.left);
                dogmoveUp = false;
                dogmoveDown = false;
                dogmoveLeft = true;
                dogmoveRight = false;
                dogStuck = false;
                dogSittingTurns = 1;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "meat"))
            {
                Game.Attack(this, Direction.right);
                dogmoveUp = false;
                dogmoveDown = false;
                dogmoveLeft = false;
                dogmoveRight = true;
                dogStuck = false;
                dogSittingTurns = 1;
                return true;
            }
            return false;
        }


        public bool DogHuntCat()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "cat") && dogmoveUp == true)
            {
                Game.Attack(this, Direction.up);

                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.down, "cat") && dogmoveDown == true)
            {
                Game.Attack(this, Direction.down);

                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "cat") && dogmoveLeft == true)
            {
                Game.Attack(this, Direction.left);

                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "cat") && dogmoveRight == true)
            {
                Game.Attack(this, Direction.right);

                return true;
            }
            return false;
        }

        public bool DogHuntMouse()
        {
            if (Game.Seek(location.x, location.y, Direction.up, "mouse") && dogmoveUp == true)
            {
                Game.Attack(this, Direction.up);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.down, "mouse") && dogmoveDown == true)
            {
                Game.Attack(this, Direction.down);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "mouse") && dogmoveLeft == true)
            {
                Game.Attack(this, Direction.left);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "mouse") && dogmoveRight == true)
            {
                Game.Attack(this, Direction.right);
                Game.gameStatus = "Game Over";
                Game.isGameOver = true;
                return true;
            }
            return false;
        }

        public bool DogSmellMeat()
        {

            if (Game.Seek(location.x, location.y - 1, Direction.up, "meat"))
            {
                dogmoveUp = true;
                dogmoveDown = false;
                dogmoveLeft = false;
                dogmoveRight = false;
                pointDirection = "↑";
                dogStuck = false;
                Game.DogMoving(this, Direction.up);
                //DogMove();
                return true;
            }
            if (Game.Seek(location.x, location.y - 2, Direction.up, "meat"))
            {
                dogmoveUp = true;
                dogmoveDown = false;
                dogmoveLeft = false;
                dogmoveRight = false;
                pointDirection = "↑";
                dogStuck = false;
                Game.DogMoving(this, Direction.up);
                //DogMove();
                return true;
            }

            if (Game.Seek(location.x, location.y + 1, Direction.down, "meat"))
            {
                dogmoveUp = false;
                dogmoveDown = true;
                dogmoveLeft = false;
                dogmoveRight = false;
                pointDirection = "↓";
                dogStuck = false;
                Game.DogMoving(this, Direction.down);
                //DogMove();
                return true;
            }
            if (Game.Seek(location.x, location.y + 2, Direction.down, "meat"))
            {
                dogmoveUp = false;
                dogmoveDown = true;
                dogmoveLeft = false;
                dogmoveRight = false;
                pointDirection = "↓";
                dogStuck = false;
                Game.DogMoving(this, Direction.down);
                //DogMove();
                return true;
            }

           
            if (Game.Seek(location.x - 1, location.y, Direction.left, "meat"))
            {
                dogmoveUp = false;
                dogmoveDown = false;
                dogmoveLeft = true;
                dogmoveRight = false;
                pointDirection = "←";
                dogStuck = false;
                Game.DogMoving(this, Direction.left);
                //DogMove();
                return true;
            }
            if (Game.Seek(location.x - 2, location.y, Direction.left, "meat"))
            {
                dogmoveUp = false;
                dogmoveDown = false;
                dogmoveLeft = true;
                dogmoveRight = false;
                pointDirection = "←";
                dogStuck = false;
                Game.DogMoving(this, Direction.left);
                //DogMove();
                return true;
            }

            if (Game.Seek(location.x + 1, location.y, Direction.right, "meat"))
            {
                dogmoveUp = false;
                dogmoveDown = false;
                dogmoveLeft = false;
                dogmoveRight = true;
                pointDirection = "→";
                dogStuck = false;
                Game.DogMoving(this, Direction.right);
                //DogMove();
                return true;
            }
            if (Game.Seek(location.x + 2, location.y, Direction.right, "meat"))
            {
                dogmoveUp = false;
                dogmoveDown = false;
                dogmoveLeft = false;
                dogmoveRight = true;
                pointDirection = "→";
                dogStuck = false;
                Game.DogMoving(this, Direction.right);
                //DogMove();
                return true;
            }
            return false;
        }


        public bool DogEatCheese()
        {
            if (Game.Seek(location.x, location.y, Direction.left, "cheese") && (dogmoveLeft == true))
            {
                Game.Attack(this, Direction.left);
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.up, "cheese") && (dogmoveUp == true))
            {
                
                Game.Attack(this, Direction.up);
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "cheese") && (dogmoveRight == true))
            {
                Game.Attack(this, Direction.right);
                return true;
            }

            else if (Game.Seek(location.x, location.y, Direction.down, "cheese") && (dogmoveDown == true))
            {
                Game.Attack(this, Direction.down);
                return true;
            }

            return false;
        }


        public bool DogPushRock()
        {
            if (Game.Seek(location.x, location.y, Direction.left, "rock") && (dogmoveLeft == true))
            {
                SwitchDirection();

                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.up, "rock") && (dogmoveUp == true))
            {
                SwitchDirection();

                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "rock") && (dogmoveRight == true))
            {
                SwitchDirection();
                return true;
            }

            else if (Game.Seek(location.x, location.y, Direction.down, "rock") && (dogmoveDown == true))
            {
                SwitchDirection();
                return true;
            }

            return false;
        }

   

        public bool DogStuckHole()
        {

            if (Game.Seek(location.x, location.y, Direction.left, "hole") ||
              Game.Seek(location.x, location.y, Direction.up, "hole") ||
              Game.Seek(location.x, location.y, Direction.right, "hole") ||
              Game.Seek(location.x, location.y, Direction.down, "hole"))
            {
                dogStuck = true;
                Game.DogMoving(this, Direction.none);
            
                return true;
            }
            return false;
        }
    }
}
