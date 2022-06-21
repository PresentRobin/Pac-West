using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace test.Content.Management
{
   
    class GameContent 
    { 
        public Texture2D imgPixel { get; set; }
        //load images
        public GameContent (ContentManager Content)
        {
            imgPixel = Content.Load<Texture2D>("Background/pixel");
        }
        
    }
}
