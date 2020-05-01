using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneBlockTetris
{
    public partial class Form1 : Form
    {
        private Color[] _blockEmptyColor;

        private Timer _timer;

        private int _tetrisPiecePosition;

        private bool[] _blockFilled;

        public Form1()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.Interval = 100;
            _timer.Enabled = true;
            _timer.Tick += new System.EventHandler(TimerTickEvent);

            _blockEmptyColor = new Color[2];

            _blockFilled = new bool[40];

            _tetrisPiecePosition = -3;

            _blockEmptyColor[0] = Color.LimeGreen;

            _blockEmptyColor[1] = Color.DarkSlateBlue;

            foreach (Label label in BlockLabels)
            {
                label.BackColor = _blockEmptyColor[0];
            }

            _timer.Start();
        }

        private void TimerTickEvent(object sender, EventArgs e)
        {
            _tetrisPiecePosition += 5;

            if (_tetrisPiecePosition < 35)
            {
                if (BlockLabels[_tetrisPiecePosition].BackColor == _blockEmptyColor[1])
                {
                    _tetrisPiecePosition = 2;
                }
            }

            if (_tetrisPiecePosition == 2)
            {
                int amount = 0;
                foreach (Label block in BlockLabels)
                {
                    if (block.BackColor == _blockEmptyColor[1])
                    {
                        _blockFilled[amount] = true;
                    }
                    amount++;
                }
            }

            if (_tetrisPiecePosition > 40)
            {
                _tetrisPiecePosition = 2;
            }

            if (_tetrisPiecePosition < 40)
            {
                if (BlockLabels[_tetrisPiecePosition].BackColor == _blockEmptyColor[0])
                {
                    BlockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                    if (_tetrisPiecePosition > 4)       //changed* (_tetrisPiecePosition > 5)   first line last block index is 4
                    {
                        BlockLabels[_tetrisPiecePosition - 5].BackColor = _blockEmptyColor[0];
                    }
                }
            }

            if (BlockLabels[35].BackColor == _blockEmptyColor[1] &&
                BlockLabels[36].BackColor == _blockEmptyColor[1] &&
                BlockLabels[37].BackColor == _blockEmptyColor[1] &&
                BlockLabels[38].BackColor == _blockEmptyColor[1] &&
                BlockLabels[39].BackColor == _blockEmptyColor[1])
            {
                //Free the filled Line
                BlockLabels[35].BackColor = _blockEmptyColor[0];
                BlockLabels[36].BackColor = _blockEmptyColor[0];
                BlockLabels[37].BackColor = _blockEmptyColor[0];
                BlockLabels[38].BackColor = _blockEmptyColor[0];
                BlockLabels[39].BackColor = _blockEmptyColor[0];

                int amountBlocks = 0;

                //make all _blocksFilled false
                _blockFilled = new bool[40];

                //sets ones that are blue to true
                foreach (Label block in BlockLabels)
                {
                    if (block.BackColor == _blockEmptyColor[1])
                    {
                        _blockFilled[amountBlocks + 5] = true;
                    }
                    amountBlocks++;
                }
                //Make all pieces Empty
                foreach (Label block in blockLabels)
                {
                    block.BackColor = _blockEmptyColor[0];
                }
                //Make the pieces fall down after the line is freed
                int amountBlue = 0;
                foreach (bool blockFilled in _blockFilled)
                {
                    if (blockFilled == true)
                    {
                        BlockLabels[amountBlue].BackColor = _blockEmptyColor[1];
                    }
                    amountBlue++;
                }
                if (_tetrisPiecePosition < 40)
                {
                    if (BlockLabels[_tetrisPiecePosition].BackColor == _blockEmptyColor[0])
                    {
                        BlockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                        if (_tetrisPiecePosition > 5)
                        {
                            BlockLabels[_tetrisPiecePosition - 5].BackColor = _blockEmptyColor[0];
                        }
                    }
                }
            }

            //When filled blocks reach the top
            if (BlockLabels[2].BackColor == _blockEmptyColor[1] && BlockLabels[2 + 5].BackColor == _blockEmptyColor[1])
            {
                _timer.Stop();
                DialogResult quitOrContinue = MessageBox.Show("You lost. Do you want to restart the Game?", "Fail", MessageBoxButtons.YesNo);

                if (quitOrContinue == DialogResult.Yes)
                {
                    foreach (Label block in BlockLabels)
                    {
                        block.BackColor = _blockEmptyColor[0];
                    }
                    _blockFilled = new bool[40];
                    _tetrisPiecePosition = -3;
                    _timer.Start();
                }
                else
                {
                    Environment.Exit(0);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                if (_tetrisPiecePosition < 39 && _blockFilled[_tetrisPiecePosition] == false)
                {
                    if (_tetrisPiecePosition != 0 && _tetrisPiecePosition != 5
                        && _tetrisPiecePosition != 10 && _tetrisPiecePosition != 15
                         && _tetrisPiecePosition != 20 && _tetrisPiecePosition != 25
                          && _tetrisPiecePosition != 30 && _tetrisPiecePosition != 35
                           && BlockLabels[_tetrisPiecePosition - 1].BackColor != _blockEmptyColor[1])
                    {
                        _tetrisPiecePosition--;
                        BlockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                        BlockLabels[_tetrisPiecePosition + 1].BackColor = _blockEmptyColor[0];
                        return true;
                    }
                }
            }

            if (keyData == Keys.Right)
            {
                if (_tetrisPiecePosition < 39 && _blockFilled[_tetrisPiecePosition] == false)
                {
                    if (/*_tetrisPiecePosition != 0 &&*/ _tetrisPiecePosition != 4         //changed*  not needed condition
                        && _tetrisPiecePosition != 9 && _tetrisPiecePosition != 14
                         && _tetrisPiecePosition != 19 && _tetrisPiecePosition != 24
                          && _tetrisPiecePosition != 29 && _tetrisPiecePosition != 34
                           && BlockLabels[_tetrisPiecePosition + 1].BackColor != _blockEmptyColor[1])       //changed* (_tetrisPiecePosition - 1)  right block of piece
                    {
                        _tetrisPiecePosition++;
                        BlockLabels[_tetrisPiecePosition].BackColor = _blockEmptyColor[1];
                        BlockLabels[_tetrisPiecePosition - 1].BackColor = _blockEmptyColor[0];
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
