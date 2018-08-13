using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomUserControlsLibrary;

namespace ImageProcessingLibrary.Visualization
{
    public partial class ImagePlot : ScrollableZoomControl
    {
        private Bitmap bitmap;

        public ImagePlot()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.Paint += new PaintEventHandler(HandlePaint);
        }

        private void HandlePaint(object sender, PaintEventArgs e)
        {
            if (bitmap == null) { return; }
            e.Graphics.DrawImage(bitmap, new RectangleF(0, 0, this.Width, this.Height),
                new RectangleF((float)xViewMin, bitmap.Height-(float)yViewMax, (float)(xViewMax - xViewMin), 
                    (float)(yViewMax - yViewMin)), GraphicsUnit.Pixel);
        }

        public void SetImage(Bitmap bitmap)
        {
            this.bitmap = bitmap;
            SetRange(0, bitmap.Width, 0, bitmap.Height);
            scrollbarsVisible = false;
            Invalidate();
        }

        public Bitmap Bitmap
        {
            get { return bitmap; }
        }
    }
}
