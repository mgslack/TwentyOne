using System;
using System.Windows.Forms;
using PlayingCards;

/*
 * Defines the partial class of the options dialog for the TwentyOne game.
 * 
 * Author:  M. G. Slack
 * Written: 2021-11-08
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: yyyy-mm-dd - xxxxx.
 * 
 */
namespace TwentyOne
{
    public partial class OptionsDlg : Form
    {
        #region Properties
        private CardBacks _cardBack = CardBacks.Spheres;
        public CardBacks CardBack { get { return _cardBack; } set { _cardBack = value; } }

        private NumberOfDecks _numDecks = NumberOfDecks.One_Deck;
        public NumberOfDecks NumDecks { get { return _numDecks; } set { _numDecks = value; } }

        private int _minBet = 1;
        public int MinBet { get { return _minBet; } set { _minBet = value; } }

        private int _maxBet = 5;
        public int MaxBet { get { return _maxBet; } set { _maxBet = value; } }

        private int _initBank = 100;
        public int InitBank { get { return _initBank; } set { _initBank = value; } }

        private bool _betMax = false;
        public bool BetMax { get { return _betMax; } set { _betMax = value; } }

        private PlayingCardImage _images = null;
        public PlayingCardImage Images { set { _images = value; } }
        #endregion

        // --------------------------------------------------------------------

        #region Private Methods
        private void LoadAndDisplayCardBacks()
        {
            int idx = 0;

            foreach (string name in Enum.GetNames(typeof(CardBacks)))
                cbImage.Items.Add(name);
            foreach (int val in Enum.GetValues(typeof(CardBacks)))
                if (val == (int)_cardBack) idx = (int)_cardBack - (int)CardBacks.Spheres;
            cbImage.SelectedIndex = idx;

            if (_images != null) pbCardBack.Image = _images.GetCardBackImage(_cardBack);
        }

        private void LoadAndDisplayNumOfDecks()
        {
            foreach (string name in Enum.GetNames(typeof(NumberOfDecks)))
                cbNumDecks.Items.Add(name);
            foreach (int val in Enum.GetValues(typeof(NumberOfDecks)))
                if (val == (int)_numDecks) cbNumDecks.SelectedIndex = val;
        }
        #endregion

        // --------------------------------------------------------------------

        public OptionsDlg()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        #region Event Handlers
        private void OptionsDlg_Load(object sender, EventArgs e)
        {
            LoadAndDisplayCardBacks();
            LoadAndDisplayNumOfDecks();
            nudMin.Maximum = MainWin.MAX_BET_AMOUNT; nudMin.Value = _minBet;
            nudMax.Maximum = MainWin.MAX_BET_AMOUNT; nudMax.Value = _maxBet;
            nudBank.Maximum = MainWin.MAX_IBANK_AMOUNT; nudBank.Value = _initBank;
            cbBetMax.Checked = _betMax;
        }

        private void CbNumDecks_SelectedIndexChanged(object sender, EventArgs e)
        {
            _numDecks = (NumberOfDecks)cbNumDecks.SelectedIndex;
            if (_numDecks == NumberOfDecks.Default) _numDecks = NumberOfDecks.One_Deck;
        }

        private void CbBetMax_CheckedChanged(object sender, EventArgs e)
        {
            _betMax = cbBetMax.Checked;
        }

        private void CbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _cardBack = (CardBacks)(cbImage.SelectedIndex + (int)CardBacks.Spheres);
            if (_images != null) pbCardBack.Image = _images.GetCardBackImage(_cardBack);
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            int min = (int)nudMin.Value;
            int max = (int)nudMax.Value;
            int bank = (int)nudBank.Value;
            bool fOk = true;

            if (max < min)
            {
                fOk = false;
                MessageBox.Show(this, "Maximum Bet must be greater or equal to minimum bet.", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudMax.Focus();
            }
            if (fOk && bank < min)
            {
                fOk = false;
                MessageBox.Show(this, "Initial bank has to be greater or equal to minimum bet.", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudBank.Focus();
            }
            if (fOk)
            {
                _minBet = min; _maxBet = max; _initBank = bank;
                DialogResult = DialogResult.OK;
            }
        }
        #endregion
    }
}
