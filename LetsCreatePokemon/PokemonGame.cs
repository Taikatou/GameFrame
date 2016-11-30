﻿//------------------------------------------------------
// 
// Copyright - (c) - 2016 - Mille Boström 
//
// Youtube channel - https://www.speedcoding.net
//------------------------------------------------------

using System.Collections.Generic;
using LetsCreatePokemon.Battle;
using LetsCreatePokemon.Battle.Phases;
using LetsCreatePokemon.Battle.Phases.TrainerPhases;
using LetsCreatePokemon.Inputs;
using LetsCreatePokemon.Screens;
using LetsCreatePokemon.Screens.ScreenTransitionEffects;
using LetsCreatePokemon.Services.Content;
using LetsCreatePokemon.Services.Screens;
using LetsCreatePokemon.Services.Windows;
using LetsCreatePokemon.Services.Windows.Message;
using LetsCreatePokemon.Services.World;
using LetsCreatePokemon.World;
using LetsCreatePokemon.World.Components.Tiles;
using LetsCreatePokemon.World.Pathfindings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LetsCreatePokemon
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class PokemonGame : Game
    {
        RenderTarget2D backBuffer;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        WorldObject entity;
        IContentLoader contentLoader;
        ScreenLoader screenLoader;
        WindowHandler windowHandler;

        public PokemonGame()
        {
            //TEst
            var p = new Pathfinding();
            var path = p.FindPath(new Vector2(0, 0), new Vector2(5, 0), new List<ICollisionComponent>() { new TileCollision(null, 3, 0)});
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 240;
            graphics.PreferredBackBufferHeight = 160;
            Content.RootDirectory = "Content";
            contentLoader = new ContentLoader(Content);
            windowHandler = new WindowHandler(contentLoader);
            screenLoader = new ScreenLoader(new ScreenTransitionEffectFadeOut(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 5),
                new ScreenTransitionEffectFadeIn(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, 3), contentLoader);
            screenLoader.LoadScreen(new ScreenWorld(screenLoader, new TileTestLoader(), new EntityTestLoader(), new EventRunner(contentLoader)));
            //screenLoader.LoadScreen(new ScreenBattle(screenLoader, windowHandler, new TrainerStartPhase(), new BattleData(new TrainerTestLoader().LoadTrainer(1), new TrainerTestLoader().LoadTrainer(1))));
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            backBuffer = new RenderTarget2D(GraphicsDevice, 240, 160);
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            screenLoader.LoadContent();
            //windowHandler.QueueWindow(new WindowMessage(new Vector2(5, 113), 230, 45, "Hey, Im Ash and youre going down! Hey, Im Ash and youre going down! " +
            //                                                                          "Hey, Im Ash and youre going down! Hey, Im Ash and youre going down! " +
            //                                                                          "Hey, Im Ash and youre going down! Hey, Im Ash and youre going down! " +
            //                                                                          "Hey, Im Ash and youre going down! Hey, Im Ash and youre going down!",
            //                                                                          new InputKeyboard()));
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            screenLoader.Update(gameTime.ElapsedGameTime.Milliseconds);
            windowHandler.Update(gameTime.ElapsedGameTime.Milliseconds);
            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            GraphicsDevice.SetRenderTarget(backBuffer);
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            screenLoader.Draw(spriteBatch);
            windowHandler.Draw(spriteBatch);
            spriteBatch.End();

            return base.BeginDraw();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            spriteBatch.Draw(backBuffer, new Rectangle(0, 0, GraphicsDevice.Viewport.Bounds.Width, GraphicsDevice.Viewport.Bounds.Height), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
