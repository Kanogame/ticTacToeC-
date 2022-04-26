using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiсTacToe
{
    public partial class MainForm : Form
    {
        private Game game;
        public Graphics g;

        public MainForm()
        {
            InitializeComponent();
            game = new Game();
            game.RepaintRequired += RepaintRequired;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //game.Initialize(this.ClientSize);
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            game.MouseDown(e.Location, ClientSize);
            Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            game.Display(this.ClientSize, g);
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            RepaintRequired();
        }

        public void RepaintRequired()
        {
            Invalidate();
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            RepaintRequired();
            game.GetMouseLocation(e.Location);
        } 
    }
}
