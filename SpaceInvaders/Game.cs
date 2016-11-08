using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using GameFrame.CollisionTest;

namespace SpaceInvaders
{
    internal class Game
    {
        private const int InvaderXSpacing = 60;
        private const int InvaderYSpacing = 60;
        private int _currentGameFrame = 1;

        private Rectangle _formArea;
        private int _framesSkipped = 6;

        private List<BBObject> _enemyShotsPlayer;
        private List<BBObject> _playerShotsEnemies;

        private Direction _invaderDirection;
        private readonly List<Invader> _invaders; 
        private List<Shot> _invaderShots;
        private int _livesLeft = 5;
        private readonly PointF _livesLocation;

        private readonly Font _messageFont = new Font(FontFamily.GenericMonospace, 50, FontStyle.Bold);

        private readonly PlayerShip _playerShip;
        private readonly List<Shot> _playerShots;
        private readonly Random _random;

        private int _score;

        private readonly PointF _scoreLocation;
        private readonly Stars _stars;
        private readonly Font _statsFont = new Font(FontFamily.GenericMonospace, 15);
        private int _wave;
        private readonly PointF _waveLocation;

        public Game(Random random, Rectangle formArea)
        {
            this._formArea = formArea;
            this._random = random;
            _stars = new Stars(random, formArea);
            _scoreLocation = new PointF(formArea.Left + 5.0F, formArea.Top + 5.0F);
            _livesLocation = new PointF(formArea.Right - 120.0F, formArea.Top + 5.0F);
            _waveLocation = new PointF(formArea.Left + 5.0F, formArea.Top + 25.0F);
            _playerShip = new PlayerShip(formArea,
                new Point(formArea.Width/2, formArea.Height - 50));
            _playerShots = new List<Shot>();
            _invaderShots = new List<Shot>();
            _invaders = new List<Invader>();


            NextWave();
        }

        // Draw is fired with each paint event of the main form
        public void Draw(Graphics graphics, int frame, bool gameOver)
        {
            graphics.FillRectangle(Brushes.Black, _formArea);

            _stars.Draw(graphics);
            foreach (var invader in _invaders)
                invader.Draw(graphics, frame);
            _playerShip.Draw(graphics);
            foreach (Shot shot in _playerShots)
                shot.Draw(graphics);
            foreach (Shot shot in _invaderShots)
                shot.Draw(graphics);

            graphics.DrawString("Score: " + _score,
                _statsFont, Brushes.Yellow, _scoreLocation);
            graphics.DrawString("Lives: " + _livesLeft,
                _statsFont, Brushes.Yellow, _livesLocation);
            graphics.DrawString("Wave: " + _wave,
                _statsFont, Brushes.Yellow, _waveLocation);
            if (gameOver)
                graphics.DrawString("GAME OVER", _messageFont, Brushes.Red,_formArea.Width/4, _formArea.Height/3);
        }

        // Twinkle (animates stars) is called from the form animation timer
        public void Twinkle()
        {
            _stars.Twinkle(_random);
        }

        public void MovePlayer(Direction direction, bool gameOver)
        {
            if (!gameOver)
                _playerShip.Move(direction);
        }

        public void FireShot()
        {
            if (_playerShots.Count < 4)
            {
                Shot newShot = new Shot(
                    new Point(_playerShip.Location.X + _playerShip.Image.Width/2
                        , _playerShip.Location.Y),
                    Direction.Up, _formArea);
                _playerShots.Add(newShot);
            }
        }

        public void Go()
        {
            if (_playerShip.Alive)
            {
                // Check to see if any shots are off screen, to be removed
                List<Shot> deadPlayerShots = new List<Shot>();
                foreach (Shot shot in _playerShots)
                    if (!shot.Move())
                        deadPlayerShots.Add(shot);
                foreach (Shot shot in deadPlayerShots)
                    _playerShots.Remove(shot);

                List<Shot> deadInvaderShots = new List<Shot>();
                foreach (Shot shot in _invaderShots)
                    if (!shot.Move())
                        deadInvaderShots.Add(shot);
                foreach (Shot shot in deadInvaderShots)
                    _invaderShots.Remove(shot);

                MoveInvaders();
                ReturnFire();
                CheckForCollisions();
                if (_invaders.Count < 1)
                    NextWave();
            }
        }

        private void MoveInvaders()
        {
            // if the frame is skipped invaders do not move
            if (_currentGameFrame > _framesSkipped)
            {
                // Check to see if invaders are at edge of screen, 
                // if so change direction
                if (_invaderDirection == Direction.Right)
                {
                    var edgeInvaders =
                        from invader in _invaders
                        where invader.Location.X > _formArea.Width - 100
                        select invader;
                    if (edgeInvaders.Any())
                    {
                        _invaderDirection = Direction.Left;
                        foreach (var invader in _invaders)
                            invader.Move(Direction.Down);
                    }
                    else
                    {
                        foreach (var invader in _invaders)
                            invader.Move(Direction.Right);
                    }
                }

                if (_invaderDirection == Direction.Left)
                {
                    var edgeInvaders =
                        from invader in _invaders
                        where invader.Location.X < 100
                        select invader;
                    if (edgeInvaders.Any())
                    {
                        _invaderDirection = Direction.Right;
                        foreach (var invader in _invaders)
                            invader.Move(Direction.Down);
                    }
                    else
                    {
                        foreach (var invader in _invaders)
                            invader.Move(Direction.Left);
                    }
                }

                // Check to see if invaders have made it to the bottom
                var endInvaders =
                    from invader in _invaders
                    where invader.Location.Y > _playerShip.Location.Y
                    select invader;
                if (endInvaders.Any())
                    GameOver?.Invoke(this, null);

                foreach (var invader in _invaders)
                    invader.Move(_invaderDirection);
            }
            _currentGameFrame++;
            if (_currentGameFrame > 6)
                _currentGameFrame = 1;
        }

        private void ReturnFire()
        {
            //// invaders check their location and fire at the player
            if (_invaderShots.Count == _wave)
                return;
            if (_random.Next(10) < 10 - _wave)
                return;

            var invaderColumns =
                from invader in _invaders
                group invader by invader.Location.X
                into columns
                select columns;

            var enumerable = invaderColumns as IGrouping<int, Invader>[] ?? invaderColumns.ToArray();
            var randomColumnNumber = _random.Next(enumerable.Count());
            var randomColumn = enumerable.ElementAt(randomColumnNumber);

            var invaderRow =
                from invader in randomColumn
                orderby invader.Location.Y descending
                select invader;

            var shooter = invaderRow.First();
            var newShotLocation = new Point
            (shooter.Location.X + shooter.Area.Width/2,
                shooter.Location.Y + shooter.Area.Height);

            Shot newShot = new Shot(newShotLocation, Direction.Down,
                _formArea);
            _invaderShots.Add(newShot);
        }


        private void CheckForCollisions()
        {
            // Created seperate lists of dead shots since items can't be
            // removed from a list while enumerating through it
            List<Shot> deadPlayerShots = new List<Shot>();
            List<Shot> deadInvaderShots = new List<Shot>();

            foreach (Shot shot in _invaderShots.Reverse<Shot>())
                if (_playerShip.Area.Contains(shot.Location))
                {
                    deadPlayerShots.Add(shot);
                    _livesLeft--;
                    _invaderShots.Clear();
                    _playerShots.Clear();
                    _playerShip.Alive = false;
                    if (_livesLeft == 0)
                        GameOver?.Invoke(this, null);
                }

            foreach (Shot shot in _playerShots.Reverse<Shot>())
            {
                var deadInvaders = new List<Invader>();
                foreach (var invader in _invaders)
                    if (invader.Area.Contains(shot.Location))
                    {
                        deadInvaders.Add(invader);
                        deadInvaderShots.Add(shot);
                        // Score multiplier based on wave
                        _score = _score + 1*_wave;
                        _playerShots.Remove(shot);
                    }
                foreach (var invader in deadInvaders)
                    _invaders.Remove(invader);
            }
            foreach (Shot shot in deadPlayerShots)
                _playerShots.Remove(shot);
            foreach (Shot shot in deadInvaderShots)
                _invaderShots.Remove(shot);
        }

        private void NextWave()
        {
            _wave++;
            _invaderDirection = Direction.Right;
            // if the wave is under 7, set frames skipped to 6 - current wave number
            if (_wave < 7)
                _framesSkipped = 6 - _wave;
            else
                _framesSkipped = 0;

            var currentInvaderYSpace = 0;
            for (var x = 0; x < 5; x++)
            {
                var currentInvaderType = (ShipType) x;
                currentInvaderYSpace += InvaderYSpacing;
                var currentInvaderXSpace = 0;
                for (var y = 0; y < 5; y++)
                {
                    currentInvaderXSpace += InvaderXSpacing;
                    var newInvaderPoint =
                        new Point(currentInvaderXSpace, currentInvaderYSpace);
                    var newInvader =
                        new Invader(currentInvaderType, newInvaderPoint, 10);
                    _invaders.Add(newInvader);
                }
            }
        }

        public event EventHandler GameOver;
    }
}