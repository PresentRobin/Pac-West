using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chase.Models
{
    public class Camera
    {
        public Matrix Transform { get; set; }

        public void Follow(Vector2 target)
        {
            var position = Matrix.CreateTranslation(
                -target.X,
                -target.Y,
                0);

            var offset = Matrix.CreateTranslation(400, 200, 0);

            Transform = position * offset;
        }
    }
}
