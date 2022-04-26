using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiсTacToe
{
    class Game
    {
        int indent = 20;
        Point MousePosition = new Point(0, 0);
        int hodCount = 1;
        public event Action RepaintRequired;
        bool GameOver = false;
        char GameOverChar;
        bool GameButton = false;
        char[,] data = new char[,]
        {
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
            { ' ', ' ', ' ' },
        };
        private Font font;

        public Game()
        {
            font = new Font("Courier New", 40);
        }


        public void MouseDown(Point MousePosition, Size ClientSize)
        {
            int column = MousePosition.X / (ClientSize.Width / 3);
            int row = MousePosition.Y / (ClientSize.Height / 3);
            if (GameOver != true)
            {
                if (column < 0 || row < 0 || column > 2 || row > 2)
                {
                    return;
                }
                if (!(data[row, column] == 'o' || data[row, column] == 'x'))
                {
                    if (hodCount % 2 == 0)
                    {
                        data[row, column] = 'o';
                    }
                    else
                    {
                        data[row, column] = 'x';
                    }
                    hodCount++;
                }
                if (hodCount >= 5)
                {
                    if (data[0, 0] == data[0, 1] && data[0, 1] == data[0, 2] && (data[0, 0] == 'o' || data[0, 0] == 'x'))
                    {
                        GameOverEvent(data[0, 0]);
                    }
                    else if (data[1, 0] == data[1, 1] && data[1, 1] == data[1, 2] && (data[1, 0] == 'o' || data[1, 0] == 'x'))
                    {
                        GameOverEvent(data[1, 0]);
                    }
                    else if (data[2, 0] == data[2, 1] && data[2, 0] == data[2, 2] && (data[2, 0] == 'o' || data[2, 0] == 'x'))
                    {
                        GameOverEvent(data[2, 0]);
                    }
                    else if (data[0, 0] == data[1, 0] && data[1, 0] == data[2, 0] && (data[0, 0] == 'o' || data[0, 0] == 'x'))
                    {
                        GameOverEvent(data[0, 0]);
                    }
                    else if (data[0, 1] == data[1, 1] && data[1, 1] == data[2, 1] && (data[0, 1] == 'o' || data[0, 1] == 'x'))
                    {
                        GameOverEvent(data[0, 1]);
                    }
                    else if (data[0, 2] == data[1, 2] && data[1, 2] == data[2, 2] && (data[0, 2] == 'o' || data[0, 2] == 'x'))
                    {
                        GameOverEvent(data[0, 2]);
                    }
                    else if (data[0, 0] == data[1, 1] && data[1, 1] == data[2, 2] && (data[0, 0] == 'o' || data[0, 0] == 'x'))
                    {
                        GameOverEvent(data[0, 0]);
                    }
                    else if (data[0, 2] == data[1, 1] && data[1, 1] == data[2, 0] && (data[0, 2] == 'o' || data[0, 2] == 'x'))
                    {
                        GameOverEvent(data[0, 2]);
                    }
                    else if (hodCount == 10)
                    {
                        GameOver = true;
                        GameOverChar = '■';
                    }
                }
            }
            else if (GameButton == true && GameOver == true)
            {
                GameRestart();
            }
        }

        private void GameRestart()
        {
            GameButton = false;
            GameOver = false;
            hodCount = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data[i, j] = ' ';
                }
            }
        }

        private void GameOverEvent(char data)
        {
            if (data != null)
            {
                GameOver = true;
                GameOverChar = data;
            }
        }

        public void Display(Size ClientSize, Graphics g)
        {
            var Size = GetXY(ClientSize);
            var format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            var Padding = GetPadding(ClientSize);
            var GameOverRect = new Rectangle(ClientSize.Width / 4, ClientSize.Height / 4, ClientSize.Width / 2, ClientSize.Height / 2);
            var GameOverButtons = new Rectangle(GameOverRect.X + (GameOverRect.X / 2), GameOverRect.Y * 2 + 30, GameOverRect.Width / 2, GameOverRect.Height / 4);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var rect = new RectangleF(Padding.X * i + 1, Padding.Y * j + 1, Size.X - 1, Size.Y - 1);
                    g.DrawRectangle(Pens.Black, new Rectangle(Padding.X* i, Padding.Y * j, Size.X, Size.Y));
                    if (GameOver != true)
                    {
                        if (MousePosition.X < Padding.X * (i + 1) && MousePosition.X > Size.X * i)
                        {
                            if (MousePosition.Y < Padding.Y * (j + 1) && MousePosition.Y > Size.Y * j)
                            {
                                g.FillRectangle(Brushes.LightGray, rect);
                            }
                        }
                        g.DrawString(data[j, i] + "", font, Brushes.Black, rect, format);
                    }
                    else if (GameOver == true)
                    {
                        g.FillRectangle(Brushes.DarkGray, GameOverRect);
                        g.DrawRectangle(Pens.Black, GameOverButtons);
                        g.FillRectangle(Brushes.Gray, new Rectangle(GameOverButtons.X + 1, GameOverButtons.Y + 1, GameOverButtons.Width - 1, GameOverButtons.Height - 1));
                        g.DrawString(GameOverChar != '■' ? $"победил {GameOverChar}" : "ничья", new Font("Courier New", ClientSize.Width / 16), Brushes.Black, GameOverRect, format);
                        if (MousePosition.X < GameOverButtons.X + GameOverButtons.Width && MousePosition.X > GameOverButtons.X)
                        {
                            if (MousePosition.Y < GameOverButtons.Y + GameOverButtons.Height && MousePosition.Y > GameOverButtons.Y)
                            {
                                g.FillRectangle(Brushes.LightGray, new Rectangle(GameOverButtons.X + 1, GameOverButtons.Y + 1, GameOverButtons.Width - 1, GameOverButtons.Height - 1));
                                GameButton = true;
                            }
                        }
                        else
                        {
                            GameButton = false;
                        }
                        g.DrawString("заново", new Font("Courier New", ClientSize.Width / 32), Brushes.Black, GameOverButtons, format);
                    }
                }
            }
        }
        private Point GetPadding(Size ClientSize)
        {
            int PadW = (ClientSize.Width - indent * 2) / 3 + 20;
            int PadH = (ClientSize.Height - indent * 2) / 3 + 20;
            return new Point(PadW, PadH);
        }

        private Point GetXY(Size ClientSize)
        {
            int X = (ClientSize.Width - indent * 2) / 3;
            int Y = (ClientSize.Height - indent * 2) / 3;
            return new Point(X, Y);
        }

        private void InvokeRepaintRequired()
        {
            if (RepaintRequired != null)
            {
                RepaintRequired();
            }
        }

        public void GetMouseLocation(Point MouseLocation)
        {
            MousePosition = MouseLocation;
        }
    }
}
