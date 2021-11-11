using System;
using System.Windows.Forms;
using System.Timers;
using PlayingCards;

/*
 * Defines the partial class of the shuffling cards dialog for the
 * TwentyOne game.
 * 
 * Author:  M. G. Slack
 * Written: 2021-11-06
 *
 * ----------------------------------------------------------------------------
 * 
 * Updated: yyyy-mm-dd - xxxxx.
 * 
 */
namespace TwentyOne
{
    public partial class ShuffleDlg : Form
    {
        #region Properties
        private CardDeck _cards = null;
        public CardDeck Cards { set { _cards = value; } }
        #endregion

        #region Timer
        private readonly System.Timers.Timer timer = new System.Timers.Timer(100);
        #endregion

        #region Custom Event Support
        private event EventHandler PauseDlg;

        private void DoEvent(EventHandler handler)
        {
            if (handler != null) handler(this, EventArgs.Empty);
        }
        #endregion

        public ShuffleDlg()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void ShuffleDlg_Load(object sender, EventArgs e)
        {
            if (_cards != null) _cards.Shuffle();
            PauseDlg += CustomPauseDlg;
            timer.Elapsed += Timer_Tick;
            timer.AutoReset = false;
            DoEvent(PauseDlg);
        }

        private void Timer_Tick(object source, ElapsedEventArgs e)
        {
            timer.Stop();
            DialogResult = DialogResult.OK;
        }

        private void CustomPauseDlg(object sender, EventArgs e)
        {
            timer.Start();
        }
        #endregion
    }
}
