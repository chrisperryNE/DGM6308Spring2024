using System;
namespace ZooManager
{
    public class Mouse : Animal
    {
        
        public bool mousemoveUp = false;
        public bool mousemoveDown = false;
        public bool mousemoveLeft = false;
        public bool mousemoveRight = false;
        public bool mouseMovedOneSpace = false;
        public bool mouseHasMoved = false;




        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name; 
            reactionTime = 1;
            pointDirection = "";
            MouseStartDirection();
        }

        private void MouseStartDirection()
        {
            Random random = new Random();
            int mousestartDirection = random.Next(1, 5);
            switch (mousestartDirection)
            {
                case 1:
                    mousemoveUp = true;
                    pointDirection = "↑";
                    break;
                case 2:
                    mousemoveDown = true;
                    pointDirection = "↓";
                    break;
                case 3:
                    mousemoveLeft = true;
                    pointDirection = "←";
                    break;
                case 4:
                    mousemoveRight = true;
                    pointDirection = "→";
                    break;

            }
            
        }


        public override void Activate()
        {
            if (!mouseMovedOneSpace)
            {
                base.Activate();
                Console.WriteLine("I am a mouse. Squeak.");
                

                if (!EatCheese() && !ReachExit() && !MousePushMeat() && !FleeDog()) 
                {
                    MouseMove();
                    
                }
                //ensures only one movement
                mouseMovedOneSpace = true;
                
            }
        }

        


        /* Updated to check if hunt was successful */
        public bool EatCheese()
        {
            if (Game.Seek(location.x, location.y, Direction.left, "cheese") && (mousemoveLeft == true))
            {
                Game.cheeseScore++;
                Game.Attack(this, Direction.left);
                //mouseHasMoved = true;
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.up, "cheese") && (mousemoveUp == true))
            {
                Game.cheeseScore++;
                Game.Attack(this, Direction.up);
                //mouseHasMoved = true;
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "cheese") && (mousemoveRight == true))
            {
                Game.cheeseScore++;
                Game.Attack(this, Direction.right);
                //mouseHasMoved = true;
                return true;
            }

            else if (Game.Seek(location.x, location.y, Direction.down, "cheese") && (mousemoveDown == true))
            {
                Game.cheeseScore++;
                Game.Attack(this, Direction.down);
                //mouseHasMoved = true;
                return true;
            }

            return false;
        }

        public bool MousePushMeat()
        {
            if (Game.Seek(location.x, location.y, Direction.left, "meat") && (Game.SeekTwoSpaces(location.x, location.y, Direction.left, "")) && (mousemoveLeft == true))
            {
                Game.MouseMoving(this, Direction.left);
                
                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.up, "meat") && (Game.SeekTwoSpaces(location.x, location.y, Direction.up, "")) && (mousemoveUp == true))
            {
                Game.MouseMoving(this, Direction.up);

                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "meat") && (Game.SeekTwoSpaces(location.x, location.y, Direction.right, "")) && (mousemoveRight == true))
            {
                Game.MouseMoving(this, Direction.right);
                return true;
            }

            else if (Game.Seek(location.x, location.y, Direction.down, "meat") && (Game.SeekTwoSpaces(location.x, location.y, Direction.down, "")) && (mousemoveDown == true))
            {
                Game.MouseMoving(this, Direction.down);
                return true;
            }

            return false;
        }

        /* Updated to check if hunt was successful */
        public bool ReachExit()
        {
            if (Game.Seek(location.x, location.y, Direction.left, "hole") && (mousemoveLeft == true))
            {
                
                Game.Attack(this, Direction.left);
                Game.isGameOver = true;

                if (Game.cheeseScore >= 3)
                {
                    Game.gameStatus = "You Win!";
                }
                else
                {
                    Game.gameStatus = "You Lose!";
                }

                return true;

            }
            else if (Game.Seek(location.x, location.y, Direction.up, "hole") && (mousemoveUp == true))
            {
                
                Game.Attack(this, Direction.up);
                Game.isGameOver = true;
                if (Game.cheeseScore >= 3)
                {
                    Game.gameStatus = "You Win!";
                }
                else
                {
                    Game.gameStatus = "You Lose!";
                }
                return true;
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "hole") && (mousemoveRight == true))
            {
                
                Game.Attack(this, Direction.right);
                Game.isGameOver = true;
                if (Game.cheeseScore >= 3)
                {
                    Game.gameStatus = "You Win!";
                }
                else
                {
                    Game.gameStatus = "You Lose!";
                }
                return true;
            }

            else if (Game.Seek(location.x, location.y, Direction.down, "hole") && (mousemoveDown == true))
            {
                
                Game.Attack(this, Direction.down);
                Game.isGameOver = true;
                if (Game.cheeseScore >= 3)
                {
                    Game.gameStatus = "You Win!";
                }
                else
                {
                    Game.gameStatus = "You Lose!";
                }
                return true;
            }

            return false;
        }



        /* Updated to work like Elephant Flee */
        public bool FleeDog()
        {
            //look for dog
            if (mousemoveUp && Game.Seek(location.x, location.y, Direction.up, "dog"))
            {
                Game.MouseRunning(this, Direction.down);
                SwitchDirection();
                return true;
            }
            if (mousemoveUp && Game.Seek(location.x, location.y - 1, Direction.up, "dog"))
            {
                Game.MouseRunning(this, Direction.down);
                SwitchDirection();
                return true;
            }
            if (mousemoveUp && Game.Seek(location.x, location.y - 2, Direction.up, "dog"))
            {
                Game.MouseRunning(this, Direction.down);
                SwitchDirection();
                return true;
            }

            if (mousemoveDown && Game.Seek(location.x, location.y, Direction.down, "dog"))
            {
                Game.MouseRunning(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (mousemoveDown && Game.Seek(location.x, location.y + 1, Direction.down, "dog"))
            {
                Game.MouseRunning(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (mousemoveDown && Game.Seek(location.x, location.y + 2, Direction.down, "dog"))
            {
                Game.MouseRunning(this, Direction.up);
                SwitchDirection();
                return true;
            }


            if (mousemoveLeft && Game.Seek(location.x, location.y, Direction.left, "dog"))
            {
                Game.MouseRunning(this, Direction.right);
                SwitchDirection();
                return true;
            }

            if (mousemoveLeft && Game.Seek(location.x - 1, location.y, Direction.left, "dog"))
            {
                Game.MouseRunning(this, Direction.right);
                SwitchDirection();
                return true;
            }
            if (mousemoveLeft && Game.Seek(location.x - 2, location.y, Direction.left, "dog"))
            {
                Game.MouseRunning(this, Direction.right);
                SwitchDirection();
                return true;
            }


            if (mousemoveRight && Game.Seek(location.x, location.y, Direction.right, "dog"))
            {
                Game.MouseRunning(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (mousemoveRight && Game.Seek(location.x + 1, location.y, Direction.right, "dog"))
            {
                Game.MouseRunning(this, Direction.up);
                SwitchDirection();
                return true;
            }
            if (mousemoveRight && Game.Seek(location.x + 2, location.y, Direction.right, "dog"))
            {
                Game.MouseRunning(this, Direction.up);
                SwitchDirection();
                return true;
            }

            return false;
        }

        public void MouseMove()
        {
            
            if (mousemoveLeft && Game.MouseMoving(this, Direction.left))
            {
                return;
            }
            else if (mousemoveUp && Game.MouseMoving(this, Direction.up))
            {
                return ;
            }
            else if (mousemoveRight && Game.MouseMoving(this, Direction.right))
            {
                return ;
            }
            else if (mousemoveDown && Game.MouseMoving(this, Direction.down))
            {
                return ;
            }
            else
            {
                SwitchDirection();
            }
            return;
        }

        public void SwitchDirection()
        {
            if (mousemoveLeft)
            {
                mousemoveLeft = false;
                mousemoveUp = true;
                pointDirection = "↑";
                Game.MouseMoving(this, Direction.up);
                return;
            }

            else if (mousemoveUp)
            {
                mousemoveUp = false;
                mousemoveRight = true;
                pointDirection = "→";
                Game.MouseMoving(this, Direction.right);
                return;
            }

            else if (mousemoveRight)
            {
                mousemoveRight = false;
                mousemoveDown = true;
                pointDirection = "↓";
                Game.MouseMoving(this, Direction.down);
                return;
            }

            else if (mousemoveDown)
            {
                mousemoveDown = false;
                mousemoveLeft = true;
                pointDirection = "←";
                Game.MouseMoving(this, Direction.left);
                return;
            }


        }

        //public bool IsTrapped()
        //{

        //    if (mousemoveLeft && !Game.MouseMoving(this, Direction.left))
        //    {
        //        return;
        //    }
        //    else if (mousemoveUp && Game.MouseMoving(this, Direction.up))
        //    {
        //        return;
        //    }
        //    else if (mousemoveRight && Game.MouseMoving(this, Direction.right))
        //    {
        //        return;
        //    }
        //    else if (mousemoveDown && Game.MouseMoving(this, Direction.down))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        SwitchDirection();
        //    }
        //    return;
        //}

    }
}

