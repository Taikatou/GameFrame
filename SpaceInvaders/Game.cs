using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceInvaders
{
    internal class Game
    {
        private const int invaderXSpacing = 60;
        private const int invaderYSpacing = 60;
        private int currentGameFrame = 1;

        private Rectangle formArea;
        private int framesSkipped = 6;

        private Direction invaderDirection;
        private readonly List<Invader> invaders; 
        private readonly List<Shot> invaderShots;
        private int livesLeft = 5;
        private readonly PointF livesLocation;

        private readonly Font messageFont = new Font(FontFamily.GenericMonospace, 50, FontStyle.Bold);

        private readonly PlayerShip playerShip;
        private readonly List<Shot> playerShots;
        private readonly Random random;

        private int score;

        private readonly PointF scoreLocation;
        private readonly Stars stars;
        private readonly Font statsFont = new Font(FontFamily.GenericMonospace, 15);
        private int wave;
        private readonly PointF waveLocation;

        public Game(Random random, Rectangle formArea)
        {
            this.formArea = formArea;
            this.random = random;
            stars = new Stars(random, formArea);
            scoreLocation = new PointF(formArea.Left + 5.0F, formArea.Top + 5.0F);
            livesLocation = new PointF(formArea.Right - 120.0F, formArea.Top + 5.0F);
            waveLocation = new PointF(formArea.Left + 5.0F, formArea.Top + 25.0F);
            playerShip = new PlayerShip(formArea,
                new Point(formArea.Width/2, formArea.Height - 50));
            playerShots = new List<Shot>();
            invaderShots = new List<Shot>();
            invaders = new List<Invader>();


            nextWave();
        }

        // Draw is fired with each paint event of the main form
        public void Draw(Graphics graphics, int frame, bool gameOver)
        {
            graphics.FillRectangle(Brushes.Black, formArea);

            stars.Draw(graphics);
            foreach (var invader in invaders)
                invader.Draw(graphics, frame);
            playerShip.Draw(graphics);
            foreach (Shot shot in playerShots)
                shot.Draw(graphics);
            foreach (Shot shot in invaderShots)
                shot.Draw(graphics);

            graphics.DrawString("Score: " + score,
                statsFont, Brushes.Yellow, scoreLocation);
            graphics.DrawString("Lives: " + livesLeft,
                statsFont, Brushes.Yellow, livesLocation);
            graphics.DrawString("Wave: " + wave,
                statsFont, Brushes.Yellow, waveLocation);
            if (gameOver)
                graphics.DrawString("GAME OVER", messageFont, Brushes.Red,
                    formArea.Width/4, formArea.Height/3);
        }

        // Twinkle (animates stars) is called from the form animation timer
        public void Twinkle()
        {
            stars.Twinkle(random);
        }

        public void MovePlayer(Direction direction, bool gameOver)
        {
            if (!gameOver)
                playerShip.Move(direction);
        }

        public void FireShot()
        {
            if (playerShots.Count < 2)
            {
                Shot newShot = new Shot(
                    new Point(playerShip.Location.X + playerShip.image.Width/2
                        , playerShip.Location.Y),
                    Direction.Up, formArea);
                playerShots.Add(newShot);
            }
        }

        public void Go()
        {
            if (playerShip.Alive)
            {
                // Check to see if any shots are off screen, to be removed
                List<Shot> deadPlayerShots = new List<Shot>();
                foreach (Shot shot in playerShots)
                    if (!shot.Move())
                        deadPlayerShots.Add(shot);
                foreach (Shot shot in deadPlayerShots)
                    playerShots.Remove(shot);

                List<Shot> deadInvaderShots = new List<Shot>();
                foreach (Shot shot in invaderShots)
                    if (!shot.Move())
                        deadInvaderShots.Add(shot);
                foreach (Shot shot in deadInvaderShots)
                    invaderShots.Remove(shot);

                moveInvaders();
                returnFire();
                checkForCollisions();
                if (invaders.Count < 1)
                    nextWave();
            }
        }

        private void moveInvaders()
        {
            // if the frame is skipped invaders do not move
            if (currentGameFrame > framesSkipped)
            {
                // Check to see if invaders are at edge of screen, 
                // if so change direction
                if (invaderDirection == Direction.Right)
                {
                    var edgeInvaders =
                        from invader in invaders
                        where invader.Location.X > formArea.Width - 100
                        select invader;
                    if (edgeInvaders.Count() > 0)
                    {
                        invaderDirection = Direction.Left;
                        foreach (var invader in invaders)
                            invader.Move(Direction.Down);
                    }
                    else
                    {
                        foreach (var invader in invaders)
                            invader.Move(Direction.Right);
                    }
                }

                if (invaderDirection == Direction.Left)
                {
                    var edgeInvaders =
                        from invader in invaders
                        where invader.Location.X < 100
                        select invader;
                    if (edgeInvaders.Count() > 0)
                    {
                        invaderDirection = Direction.Right;
                        foreach (var invader in invaders)
                            invader.Move(Direction.Down);
                    }
                    else
                    {
                        foreach (var invader in invaders)
                            invader.Move(Direction.Left);
                    }
                }

                // Check to see if invaders have made it to the bottom
                var endInvaders =
                    from invader in invaders
                    where invader.Location.Y > playerShip.Location.Y
                    select invader;
                if (endInvaders.Count() > 0)
                    GameOver(this, null);

                foreach (var invader in invaders)
                    invader.Move(invaderDirection);
            }
            currentGameFrame++;
            if (currentGameFrame > 6)
                currentGameFrame = 1;
        }

        private void returnFire()
        {
            //// invaders check their location and fire at the player
            if (invaderShots.Count == wave)
                return;
            if (random.Next(10) < 10 - wave)
                return;

            var invaderColumns =
                from invader in invaders
                group invader by invader.Location.X
                into columns
                select columns;

            var randomColumnNumber = random.Next(invaderColumns.Count());
            var randomColumn = invaderColumns.ElementAt(randomColumnNumber);

            var invaderRow =
                from invader in randomColumn
                orderby invader.Location.Y descending
                select invader;

            var shooter = invaderRow.First();
            var newShotLocation = new Point
            (shooter.Location.X + shooter.Area.Width/2,
                shooter.Location.Y + shooter.Area.Height);

            Shot newShot = new Shot(newShotLocation, Direction.Down,
                formArea);
            invaderShots.Add(newShot);
        }


        private void checkForCollisions()
        {
            // Created seperate lists of dead shots since items can't be
            // removed from a list while enumerating through it
            List<Shot> deadPlayerShots = new List<Shot>();
            List<Shot> deadInvaderShots = new List<Shot>();

            foreach (Shot shot in invaderShots)
                if (playerShip.Area.Contains(shot.Location))
                {
                    deadInvaderShots.Add(shot);
                    livesLeft--;
                    playerShip.Alive = false;
                    if (livesLeft == 0)
                        GameOver(this, null);
                    // worth checking for gameOver state here too?
                }

            foreach (Shot shot in playerShots)
            {
                var deadInvaders = new List<Invader>();
                foreach (var invader in invaders)
                    if (invader.Area.Contains(shot.Location))
                    {
                        deadInvaders.Add(invader);
                        deadInvaderShots.Add(shot);
                        // Score multiplier based on wave
                        score = score + 1*wave;
                    }
                foreach (var invader in deadInvaders)
                    invaders.Remove(invader);
            }
            foreach (Shot shot in deadPlayerShots)
                playerShots.Remove(shot);
            foreach (Shot shot in deadInvaderShots)
                invaderShots.Remove(shot);
        }

        private void nextWave()
        {
            wave++;
            invaderDirection = Direction.Right;
            // if the wave is under 7, set frames skipped to 6 - current wave number
            if (wave < 7)
                framesSkipped = 6 - wave;
            else
                framesSkipped = 0;

            var currentInvaderYSpace = 0;
            for (var x = 0; x < 5; x++)
            {
                var currentInvaderType = (ShipType) x;
                currentInvaderYSpace += invaderYSpacing;
                var currentInvaderXSpace = 0;
                for (var y = 0; y < 5; y++)
                {
                    currentInvaderXSpace += invaderXSpacing;
                    var newInvaderPoint =
                        new Point(currentInvaderXSpace, currentInvaderYSpace);
                    var newInvader =
                        new Invader(currentInvaderType, newInvaderPoint, 10);
                    invaders.Add(newInvader);
                }
            }
        }

        public event EventHandler GameOver;
    }
}