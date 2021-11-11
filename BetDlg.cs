using System;
using System.Windows.Forms;

/*
 * Defines the partial class of the get bet dialog for the TwentyOne game.
 * 
 * Author:  M. G. Slack
 * Written: 2021-11-07
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: yyyy-mm-dd - xxxxx.
 * 
 */
namespace TwentyOne
{
    public partial class BetDlg : Form
    {
        #region Properties
        private bool _betMax = false;
        public bool BetMax { set { _betMax = value; } }

        private int _bet = 1;
        public int Bet { get { return _bet; } set { _bet = value; } }

        private int _amountInBank = 0;
        public int AmountInBank { set { _amountInBank = value; } }

        private int _minBet = 0;
        public int MinBet { set { _minBet = value; } }

        private int _maxBet = 0;
        public int MaxBet { set { _maxBet = value; } }
        #endregion

        public BetDlg()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void BetDlg_Load(object sender, EventArgs e)
        {
            nudBet.Minimum = _minBet;
            nudBet.Maximum = _maxBet;
            if (_betMax)
                nudBet.Value = _maxBet;
            else if (_bet < _minBet)
                nudBet.Value = _minBet;
            else
                nudBet.Value = _bet;
            lblBank.Text = "" + _amountInBank;
            lblMin.Text = "" + _minBet;
            lblMax.Text = "" + _maxBet;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            _bet = (int)nudBet.Value;
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
