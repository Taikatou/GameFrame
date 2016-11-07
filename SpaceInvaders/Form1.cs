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
        private Game game;

        private bool gameOver;

        private readonly List<Keys> keysPressed = new List<Keys>();
        private readonly Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            Frame = 0;
            game = new Game(random, FormArea);
            gameOver = false;
            game.GameOver += game_GameOver;
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
            game.Twinkle();
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            game.Draw(graphics, Frame, gameOver);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
                Application.Exit();
            if (e.KeyCode == Keys.S)
            {
                // code to reset the game
                gameOver = false;
                game = new Game(random, FormArea);
                game.GameOver += game_GameOver;
                gameTimer.Start();
                return;
            }
            if (e.KeyCode == Keys.Space)
                game.FireShot();
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
            keysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            game.Go();
            foreach (var key in keysPressed)
                if (key == Keys.Left)
                {
                    game.MovePlayer(Direction.Left, gameOver);
                    return;
                }
                else if (key == Keys.Right)
                {
                    game.MovePlayer(Direction.Right, gameOver);
                    return;
                }
        }

        private void game_GameOver(object sender, EventArgs e)
        {
            gameTimer.Stop();
            gameOver = true;
            Invalidate();
        }
    }
}