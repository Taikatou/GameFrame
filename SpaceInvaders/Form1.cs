using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders
{
    public partial class Form1 : Form
    {
        public int Frame;
        // The form keeps a reference to a single Game object
        private Game _game;

        private bool _gameOver;

        private readonly List<Keys> _keysPressed = new List<Keys>();
        private readonly Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            Frame = 0;
            _game = new Game(_random, FormArea);
            _gameOver = false;
            _game.GameOver += game_GameOver;
            animationTimer.Start();
        }

        public Rectangle FormArea
        {
            get { return ClientRectangle; }
        }

        private void animationTimer_Tick(object sender, EventArgs e)
        {
            if (Frame < 3)
                Frame++;
            else
                Frame = 0;
            _game.Twinkle();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            _game.Draw(graphics, Frame, _gameOver);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();
            if (e.KeyCode == Keys.S)
            {
                // code to reset the game
                _gameOver = false;
                _game = new Game(_random, FormArea);
                _game.GameOver += game_GameOver;
                gameTimer.Start();
                return;
            }
            if (e.KeyCode == Keys.Space)
                _game.FireShot();
            if (_keysPressed.Contains(e.KeyCode))
                _keysPressed.Remove(e.KeyCode);
            _keysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_keysPressed.Contains(e.KeyCode))
                _keysPressed.Remove(e.KeyCode);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            _game.Go();
            foreach (var key in _keysPressed)
                if (key == Keys.Left)
                {
                    _game.MovePlayer(Direction.Left, _gameOver);
                    return;
                }
                else if (key == Keys.Right)
                {
                    _game.MovePlayer(Direction.Right, _gameOver);
                    return;
                }
        }

        private void game_GameOver(object sender, EventArgs e)
        {
            gameTimer.Stop();
            _gameOver = true;
            Invalidate();
        }
    }
}