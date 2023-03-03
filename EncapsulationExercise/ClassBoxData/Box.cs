using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassBoxData
    {
    public class Box
        {
        private const string Message = "{0} cannot be zero or negative.";
        //set the body of a repeating message and modify just the bit that you need later instead of writing the message every time
        //easier to maintain if the message has to be changed later 
        private double length;
        private double width;
        private double height;

        public Box(double lenght, double widrh, double height)
            {
            Length = lenght;
            Width = widrh;
            Height = height;
            }
        public double Length
            {
            get { return length; }
            set
                {
                if (value <= 0)
                    {
                    throw new ArgumentException(string.Format(Message, nameof(Length)));
                    }
                length = value;
                }
            }

        public double Width
            {
            get { return width; }
            set
                {
                if (value <= 0)
                    {
                    throw new ArgumentException(string.Format(Message, nameof(Width)));
                    }
                width = value;
                }
            }


        public double Height
            {
            get { return height; }
            set
                {
                if (value <= 0)
                    {
                    throw new ArgumentException(string.Format(Message, nameof(Height)));
                    }
                height = value;
                }
            }

        public double SurfaceArea() => 2 * Length * Width + LateralSurfaceArea();
        public double LateralSurfaceArea() => 2 * Length * Height + 2 * Width * Height;
        public double Volume() => Length * Width * Height;
        }
    }
