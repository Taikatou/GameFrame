﻿//------------------------------------------------------
// 
// Copyright - (c) - 2016 - Mille Boström 
//
// Youtube channel - https://www.speedcoding.net
//------------------------------------------------------
using System;
using LetsCreatePokemon.EventArg;

namespace LetsCreatePokemon.Inputs
{
    internal abstract class Input
    {
        public static bool LockInput { get; set; }
        private event EventHandler<NewInputEventArgs> newInput; 
        private double counter;
        private double cooldown; 

        public event EventHandler<NewInputEventArgs> NewInput
        {
            add { newInput += value; }
            remove { newInput -= value;  }
        }

        public bool ThrottleInput { get; set; }

        protected Input()
        {
            counter = 0;
            cooldown = 0; 
        }

        public void Update(double gameTime)
        {
            if (LockInput)
                return; 
            if (cooldown > 0)
            {
                counter += gameTime;
                if (counter > gameTime)
                {
                    counter = 0;
                    cooldown = 0;
                }
                else
                {
                    return; 
                }
            }
            CheckInput(gameTime);
        }

        protected abstract void CheckInput(double gameTime);

        protected void SendNewInput(Common.Inputs inputs)
        {
            newInput?.Invoke(this, new NewInputEventArgs(inputs));
        }
    }
}
