using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace AudioLibrary.Visualization
{
    public partial class SoundVisualizer3x3 : UserControl
    {
        private const int DEFAULT_MARGIN = 3;

        private List<List<SoundVisualizer>> soundVisualizerMatrix = null;
        private int itemWidth = 0;
        private int itemHeight = 0;
        private int margin = DEFAULT_MARGIN;

        public event EventHandler<VisualizerIndexEventArgs> ItemClicked = null;
        public event EventHandler<VisualizerIndexEventArgs> ItemDoubleClicked = null;

        public SoundVisualizer3x3()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        private void OnItemClicked(int xIndex, int yIndex)
        {
            if (ItemClicked != null)
            {
                EventHandler<VisualizerIndexEventArgs> handler = ItemClicked;
                VisualizerIndexEventArgs e = new VisualizerIndexEventArgs(xIndex, yIndex);
                handler(this, e);
            }
        }

        private void OnItemDoubleClicked(int xIndex, int yIndex)
        {
            if (ItemDoubleClicked != null)
            {
                EventHandler<VisualizerIndexEventArgs> handler = ItemDoubleClicked;
                VisualizerIndexEventArgs e = new VisualizerIndexEventArgs(xIndex, yIndex);
                handler(this, e);
            }
        }

        private void HandlePanelMouseClick(object sender, MouseEventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer();
            WAVSound sound = ((SoundVisualizer)sender).Sound;
            string tagString = (string)((SoundVisualizer)sender).Tag;
            string[] tagStringSplit = tagString.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
            int xIndex = int.Parse(tagStringSplit[0]);
            int yIndex = int.Parse(tagStringSplit[1]);
            OnItemClicked(xIndex, yIndex);
            sound.GenerateMemoryStream();
            sound.WAVMemoryStream.Position = 0; // Manually rewind stream 
            soundPlayer.Stream = null;
            soundPlayer.Stream = sound.WAVMemoryStream;
            soundPlayer.PlaySync();
        }

        private void HandlePanelMouseDoubleClick(object sender, MouseEventArgs e)
        {
            WAVSound sound = ((SoundVisualizer)sender).Sound;
            string tagString = (string)((SoundVisualizer)sender).Tag;
            string[] tagStringSplit = tagString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int xIndex = int.Parse(tagStringSplit[0]);
            int yIndex = int.Parse(tagStringSplit[1]);
            OnItemDoubleClicked(xIndex, yIndex);  
        }

        private void SetScale()
        {
            if (soundVisualizerMatrix != null)
            {
                for (int iX = 0; iX < soundVisualizerMatrix.Count; iX++)
                {
                    for (int iY = 0; iY < soundVisualizerMatrix[0].Count; iY++)
                    {
                        soundVisualizerMatrix[iX][iY].Left = iX * itemWidth + iX * margin;
                        soundVisualizerMatrix[iX][iY].Top = iY * itemHeight + iY * margin;
                        soundVisualizerMatrix[iX][iY].Width = itemWidth;
                        soundVisualizerMatrix[iX][iY].Height = itemHeight;
                    }
                }
            }
        }

        public void SetSound(WAVSound sound, int x, int y)
        {
            soundVisualizerMatrix[x][y].SetSound(sound);
        }

        public void Initialize()
        {
            this.Controls.Clear();
            soundVisualizerMatrix = new List<List<SoundVisualizer>>();
            soundVisualizerMatrix.Add(new List<SoundVisualizer>() { new SoundVisualizer(), new SoundVisualizer(), new SoundVisualizer() });
            soundVisualizerMatrix.Add(new List<SoundVisualizer>() { new SoundVisualizer(), new SoundVisualizer(), new SoundVisualizer() });
            soundVisualizerMatrix.Add(new List<SoundVisualizer>() { new SoundVisualizer(), new SoundVisualizer(), new SoundVisualizer() });
            for (int iX = 0; iX < soundVisualizerMatrix.Count; iX++)
            {
                for (int iY = 0; iY < soundVisualizerMatrix[0].Count; iY++)
                {
                    soundVisualizerMatrix[iX][iY].Visible = true;
                    soundVisualizerMatrix[iX][iY].Tag = iX.ToString() + " " + iY.ToString();
                    soundVisualizerMatrix[iX][iY].MouseClick += new MouseEventHandler(HandlePanelMouseClick);
                    soundVisualizerMatrix[iX][iY].MouseDoubleClick += new MouseEventHandler(HandlePanelMouseDoubleClick);
                    this.Controls.Add(soundVisualizerMatrix[iX][iY]);
                }
            }
            SetScale();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            itemWidth = (int)Math.Truncate((this.Width-2*margin) / 3.0);
            itemHeight = (int)Math.Truncate((this.Height-2*margin) / 3.0);
            SetScale();
            this.Focus();
        }
    }
}
