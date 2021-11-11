using System;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using PlayingCards;
using GameStatistics;

/*
 * Primary class defines the partial class for the main window of the game
 * of TwentyOne (or BlackJack).
 * 
 * Game was originally written for OS/2 using Speedsoft Sybil on 2001-05-18.
 * Later rewritten in Java (applet) on 2002-06-09.  Converted to .net C# for
 * Windows from Sybil source and completed on 2021-11-08.
 * 
 * Author: Michael G. Slack
 * Written: 2021-11-06
 * Version: 1.0.0.0
 * 
 * ----------------------------------------------------------------------------
 * 
 * Updated: yyyy-mm-dd - xxxxx.
 * 
 */
namespace TwentyOne
{
    public partial class MainWin : Form
    {
        #region Constants
        private const string HTML_HELP_FILE = "TwentyOne_help.html";
        private const string FIRST_MSG = "Scoring first hand of split.";
        private const string SECOND_MSG = "Scoring second hand of split.";

        private const string REG_NAME = @"HKEY_CURRENT_USER\Software\Slack and Associates\Games\TwentyOne";
        private const string REG_KEY1 = "PosX";
        private const string REG_KEY2 = "PosY";
        private const string REG_KEY3 = "CardBack";
        private const string REG_KEY4 = "NumberOfDecks";
        private const string REG_KEY5 = "MinimumBet";
        private const string REG_KEY6 = "MaximumBet";
        private const string REG_KEY7 = "InitialBank";
        private const string REG_KEY8 = "BetMaxAmount";

        private const int START_MAX_BET_AMOUNT = 5;
        private const int START_IBANK = 100;
        private const int MAX_DRAW_CARDS = 5;
        public const int MIN_BET_AMOUNT = 1;
        public const int MAX_BET_AMOUNT = 10000;
        public const int MAX_IBANK_AMOUNT = 100000;

        private const int CARD_TWO = CardHand.FIRST + 1;
        private const int CARD_THREE = CARD_TWO + 1;
        private const int CARD_FOUR = CARD_THREE + 1;
        private const int CARD_FIVE = CARD_FOUR + 1;
        private const int TWENTY_ONE = 21;
        #endregion

        #region Private Variables
        private PlayingCardImage images = new PlayingCardImage();
        private CardDeck cards = null;
        private CardBacks cardBack = CardBacks.Spheres;
        private NumberOfDecks NumOfDecks = NumberOfDecks.One_Deck;
        private int MinimumBet = MIN_BET_AMOUNT, MaximumBet = START_MAX_BET_AMOUNT,
            InitialBank = START_IBANK, AmountWon = 0, Bet = 1;
        private bool Bet_Max = false, DownShown = false, DoubleHand = false, SplitHand = false;
        private PictureBox[] DealerCards = new PictureBox[MAX_DRAW_CARDS];
        private PictureBox[] PlayerCards = new PictureBox[MAX_DRAW_CARDS];
        private CardHand DealerC = new CardHand(false);
        private CardHand PlayerC = new CardHand(false);
        private CardHand PSplitC = new CardHand(2, false);
        private Statistics stats = new Statistics(REG_NAME);
        #endregion

        // --------------------------------------------------------------------

        #region Private Methods
        private void LoadRegistryValues()
        {
            int winX = -1, winY = -1, chk, cardB = (int)CardBacks.Spheres;

            try
            {
                winX = (int)Registry.GetValue(REG_NAME, REG_KEY1, winX);
                winY = (int)Registry.GetValue(REG_NAME, REG_KEY2, winY);
                cardB = (int)Registry.GetValue(REG_NAME, REG_KEY3, cardB);
                if (Enum.IsDefined(typeof(CardBacks), cardB)) cardBack = (CardBacks)cardB;
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY4, 1);
                if (chk < (int)NumberOfDecks.One_Deck || chk > (int)NumberOfDecks.Seven_Deck)
                    NumOfDecks = NumberOfDecks.One_Deck;
                else
                    NumOfDecks = (NumberOfDecks)chk;
                MinimumBet = (int)Registry.GetValue(REG_NAME, REG_KEY5, MIN_BET_AMOUNT);
                if (MinimumBet < MIN_BET_AMOUNT || MinimumBet > MAX_BET_AMOUNT)
                    MinimumBet = MIN_BET_AMOUNT;
                MaximumBet = (int)Registry.GetValue(REG_NAME, REG_KEY6, START_MAX_BET_AMOUNT);
                if (MaximumBet < MinimumBet || MaximumBet > MAX_BET_AMOUNT)
                    MaximumBet = MinimumBet;
                InitialBank = (int)Registry.GetValue(REG_NAME, REG_KEY7, START_IBANK);
                if (InitialBank < MinimumBet || InitialBank > MAX_IBANK_AMOUNT)
                    InitialBank = MinimumBet * 2;
                chk = (int)Registry.GetValue(REG_NAME, REG_KEY8, 0);  // default = false
                Bet_Max = chk > 0;
            }
            catch (Exception) { /* ignore, go with defaults, but could use MessageBox.Show(e.Message); */ }

            if ((winX != -1) && (winY != -1)) this.SetDesktopLocation(winX, winY);
        }

        private void WriteRegistryValues()
        {
            Registry.SetValue(REG_NAME, REG_KEY3, (int)cardBack, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY4, (int)NumOfDecks, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY5, MinimumBet, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY6, MaximumBet, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY7, InitialBank, RegistryValueKind.DWord);
            Registry.SetValue(REG_NAME, REG_KEY8, Bet_Max, RegistryValueKind.DWord);
        }

        private void SetupContextMenu()
        {
            ContextMenu mnu = new ContextMenu();
            MenuItem mnuStats = new MenuItem("Game Statistics");
            MenuItem sep = new MenuItem("-");
            MenuItem mnuHelp = new MenuItem("Help");
            MenuItem mnuAbout = new MenuItem("About");

            mnuStats.Click += new EventHandler(MnuStats_Click);
            mnuHelp.Click += new EventHandler(MnuHelp_Click);
            mnuAbout.Click += new EventHandler(MnuAbout_Click);
            mnu.MenuItems.AddRange(new MenuItem[] { mnuStats, sep, mnuHelp, mnuAbout });
            this.ContextMenu = mnu;
        }

        private void SetupComponentArrays()
        {
            DealerCards[0] = pbDCard1; DealerCards[1] = pbDCard2; DealerCards[2] = pbDCard3;
            DealerCards[3] = pbDCard4; DealerCards[4] = pbDCard5;
            PlayerCards[0] = pbPCard1; PlayerCards[1] = pbPCard2; PlayerCards[2] = pbPCard3;
            PlayerCards[3] = pbPCard4; PlayerCards[4] = pbPCard5;
        }

        private void ShuffleCards(bool showDlg)
        {
            // check if number of card decks has been changed and reset card deck(s)
            if (cards == null || cards.NumOfDecks != (int)NumOfDecks)
                cards = new CardDeck(NumOfDecks);
            if (showDlg)
            {
                ShuffleDlg dlg = new ShuffleDlg { Cards = cards };
                _ = dlg.ShowDialog(this);
                dlg.Dispose();
            }
            else
            {
                cards.Shuffle();
            }
            lblCardsLeft.Text = "" + cards.CardsLeft;
        }

        private string GetMoneyDisplay(int val)
        {
            return "$" + val + ".00";
        }

        private void DisplayPlayersBank()
        {
            lblBankAmt.Text = GetMoneyDisplay(AmountWon);
        }

        private DialogResult GetBet()
        {
            DialogResult ret;

            // check if have enough money, reset bank?
            if (AmountWon < MinimumBet)
            {
                ret = MessageBox.Show("You don't have enough money for bet, reset bank?",
                    this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ret == DialogResult.Yes)
                {
                    AmountWon += InitialBank;
                    DisplayPlayersBank();
                }
            }

            BetDlg dlg = new BetDlg
            {
                BetMax = Bet_Max,
                Bet = Bet,
                AmountInBank = AmountWon,
                MinBet = MinimumBet,
                MaxBet = MaximumBet
            };

            ret = dlg.ShowDialog(this);
            if (ret == DialogResult.OK) Bet = dlg.Bet;
            dlg.Dispose();

            return ret;
        }

        private PlayingCard DealCard()
        {
            if (!cards.HasMoreCards()) ShuffleCards(true);
            lblCardsLeft.Text = "" + (cards.CardsLeft - 1);
            return cards.GetNextCard();
        }

        private void StartHand()
        {
            SplitHand = false; DoubleHand = false; DownShown = false;
            DealerC.RemoveAll(); PlayerC.RemoveAll(); PSplitC.RemoveAll();
            for (int i = 0; i < 2; i++)
            {
                PlayerC.Add(DealCard());
                PlayerCards[i].Image = images.GetCardImage(PlayerC.CardAt(i));
                DealerC.Add(DealCard());
                if (i == CardHand.FIRST)
                    DealerCards[i].Image = images.GetCardBackImage(cardBack);
                else
                    DealerCards[i].Image = images.GetCardImage(DealerC.CardAt(i));
            }
            stats.StartGame(false);
        }

        private int ScoreOfHand(CardHand hand)
        {
            int tt = 0;

            for (int i = CardHand.FIRST; i < hand.CurNumCardsInHand; i++)
                tt += hand.CardAt(i).GetCardPointValueFace10();

            // check for aces, add 10 if less/equal to 21
            for (int i = CardHand.FIRST; i < hand.CurNumCardsInHand; i++)
                if (hand.CardAt(i).GetCardPointValue() == 1 && tt + 10 <= TWENTY_ONE)
                    tt += 10;

            return tt;
        }

        private void ScoreIt(int pts, bool player, bool push, bool fiveCards, bool resetGame)
        {
            string tt, ww;

            if (!DownShown)
            {
                DealerCards[CardHand.FIRST].Image = images.GetCardImage(DealerC.CardAt(CardHand.FIRST));
                DownShown = true;
            }

            if (push)
            {
                tt = "Push! Nobody wins this hand...";
                AmountWon += Bet;
                if (DoubleHand) AmountWon += Bet;
                stats.GameDone();
            }
            else
            {
                if (player)
                {
                    AmountWon += Bet + Bet; // add bet back, plus winnings
                    if (DoubleHand) AmountWon += Bet + Bet; // double it
                    ww = "Player";
                    stats.GameWon(0);
                }
                else
                {
                    ww = "Dealer";
                    stats.GameLost(0);
                }
                if (fiveCards)
                    tt = string.Format("{0} has drawn 5 cards without going over 21, {1} wins.", ww, ww);
                else
                    tt = string.Format("The {0} has won with {1}.", ww, pts);
            }
            DisplayPlayersBank();
            MessageBox.Show(tt, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (resetGame)
            {
                btnPlay.Enabled = true; btnPlay.Focus();
                btnDouble.Enabled = false; btnInsurance.Enabled = false;
                btnSplit.Enabled = false; btnHit.Enabled = false; btnStay.Enabled = false;
                lblCurrentBet.Text = GetMoneyDisplay(0);
                for (int i = 0; i < MAX_DRAW_CARDS; i++)
                {
                    PlayerCards[i].Image = null;
                    DealerCards[i].Image = null;
                }
            }
        }

        private void CheckHands()
        {
            int dd = ScoreOfHand(DealerC);
            int pp = ScoreOfHand(PlayerC);

            if (dd == TWENTY_ONE && pp == TWENTY_ONE)
            {
                ScoreIt(pp, false, true, false, true);
            }
            else if (pp == TWENTY_ONE)
            {
                ScoreIt(pp, true, false, false, true);
            }
            else if (dd == TWENTY_ONE && DealerC.CardAt(CARD_TWO).GetCardPointValue() != 1)
            {
                ScoreIt(dd, false, false, false, true);
            }
            else
            {
                btnPlay.Enabled = false;
                btnHit.Enabled = true; btnStay.Enabled = true; btnDouble.Enabled = true;
                if (DealerC.CardAt(CARD_TWO).GetCardPointValue() == 1)
                    btnInsurance.Enabled = true;
                if (PlayerC.CardAt(CardHand.FIRST).GetCardPointValue() == PlayerC.CardAt(CARD_TWO).GetCardPointValue())
                    btnSplit.Enabled = true;
                btnStay.Focus();
            }
        }

        private void DoScoringWithPoints(int dd, int pp, bool resetGame)
        {
            if (dd == pp)
                ScoreIt(pp, false, true, false, resetGame);
            else if (dd < pp)
                ScoreIt(pp, true, false, false, resetGame);
            else
                ScoreIt(dd, false, false, false, resetGame);
        }

        private void FinishOffDealer()
        {
            int p2 = 0;

            DealerCards[CardHand.FIRST].Image = images.GetCardImage(DealerC.CardAt(CardHand.FIRST));
            DownShown = true;

            int dd = ScoreOfHand(DealerC);
            int pp = ScoreOfHand(PlayerC);
            if (SplitHand) p2 = ScoreOfHand(PSplitC);

            while (dd <= 16)
            {
                DealerC.Add(DealCard());
                int cIdx = DealerC.CurNumCardsInHand - 1;
                DealerCards[cIdx].Image = images.GetCardImage(DealerC.CardAt(cIdx));
                dd = ScoreOfHand(DealerC);
                Application.DoEvents();
                if (DealerC.CurNumCardsInHand == MAX_DRAW_CARDS && dd <= TWENTY_ONE) dd = TWENTY_ONE;
            }
            if (dd <= TWENTY_ONE)
            {
                if (DealerC.CurNumCardsInHand == MAX_DRAW_CARDS)
                {
                    ScoreIt(dd, false, false, true, true);
                }
                else
                {
                    if (SplitHand) MessageBox.Show(this, FIRST_MSG, this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    DoScoringWithPoints(dd, pp, !SplitHand);
                    if (SplitHand)
                    {
                        MessageBox.Show(this, SECOND_MSG, this.Text, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        DoScoringWithPoints(dd, p2, true);
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Dealer has exceeded 21.", this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                if (SplitHand) MessageBox.Show(this, FIRST_MSG, this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ScoreIt(pp, true, false, false, !SplitHand);
                if (SplitHand)
                {
                    MessageBox.Show(this, SECOND_MSG, this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    ScoreIt(p2, true, false, false, true);
                }
            }
        }
        #endregion

        // --------------------------------------------------------------------

        public MainWin()
        {
            InitializeComponent();
        }

        // --------------------------------------------------------------------

        #region Event Handlers
        private void MainWin_Load(object sender, EventArgs e)
        {
            LoadRegistryValues();
            AmountWon = InitialBank;  // start off with winnings equaling bank
            DisplayPlayersBank();
            SetupContextMenu();
            SetupComponentArrays();
            ShuffleCards(false);
            pbCardShoe.Image = images.GetCardBackImage(cardBack);
            stats.GameName = this.Text;
        }

        private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Registry.SetValue(REG_NAME, REG_KEY1, this.Location.X);
                Registry.SetValue(REG_NAME, REG_KEY2, this.Location.Y);
            }
        }

        private void BtnOptions_Click(object sender, EventArgs e)
        {
            OptionsDlg dlg = new OptionsDlg
            {
                CardBack = cardBack,
                NumDecks = NumOfDecks,
                MinBet = MinimumBet,
                MaxBet = MaximumBet,
                InitBank = InitialBank,
                BetMax = Bet_Max,
                Images = images
            };

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (cardBack != dlg.CardBack)
                {
                    cardBack = dlg.CardBack;
                    pbCardShoe.Image = images.GetCardBackImage(cardBack);
                    if (DealerCards[CardHand.FIRST].Image != null && !DownShown)
                        DealerCards[CardHand.FIRST].Image = images.GetCardBackImage(cardBack);
                }
                NumOfDecks = dlg.NumDecks;
                MinimumBet = dlg.MinBet;
                MaximumBet = dlg.MaxBet;
                InitialBank = dlg.InitBank;
                Bet_Max = dlg.BetMax;
                WriteRegistryValues();
            }
            dlg.Dispose();
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (GetBet() == DialogResult.OK)
            {
                lblCurrentBet.Text = "" + GetMoneyDisplay(Bet);
                AmountWon -= Bet;
                DisplayPlayersBank();
                StartHand();
                CheckHands();
            }
        }

        private void BtnHit_Click(object sender, EventArgs e)
        {
            // don't have these options any more...
            btnDouble.Enabled = false; btnInsurance.Enabled = false; btnSplit.Enabled = false;
            PlayerC.Add(DealCard());
            int cardIdx = PlayerC.CurNumCardsInHand - 1;
            PlayerCards[cardIdx].Image = images.GetCardImage(PlayerC.CardAt(cardIdx));
            int pp = ScoreOfHand(PlayerC);
            if (PlayerC.CurNumCardsInHand == MAX_DRAW_CARDS && pp <= TWENTY_ONE)
            {
                ScoreIt(pp, true, false, true, true);
            }
            else if (pp > TWENTY_ONE)
            {
                MessageBox.Show("You've exceeded 21.", this.Text, MessageBoxButtons.OK,
                    MessageBoxIcon.Asterisk);
                ScoreIt(ScoreOfHand(DealerC), false, false, false, true);
            }
        }

        private void BtnStay_Click(object sender, EventArgs e)
        {
            FinishOffDealer();
        }

        private void BtnDouble_Click(object sender, EventArgs e)
        {
            if (AmountWon < Bet)
            {
                MessageBox.Show(this, "You don't have enough money to double down with.", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AmountWon -= Bet;
                DisplayPlayersBank();
                PlayerC.Add(DealCard());
                PlayerCards[CARD_THREE].Image = images.GetCardImage(PlayerC.CardAt(CARD_THREE));
                if (ScoreOfHand(PlayerC) > TWENTY_ONE)
                {
                    MessageBox.Show(this, "You've exceeded 21.", this.Text, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    ScoreIt(ScoreOfHand(DealerC), false, false, false, true);
                }
                else
                {
                    DoubleHand = true;
                    FinishOffDealer();
                }
            }
        }

        private void BtnSplit_Click(object sender, EventArgs e)
        {
            if (AmountWon < Bet)
            {
                MessageBox.Show(this, "You don't have enough money to split.", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AmountWon -= Bet;
                DisplayPlayersBank();
                PSplitC.Add(PlayerC.Remove(CARD_TWO));
                PlayerC.Add(DealCard());
                PSplitC.Add(DealCard());
                PlayerCards[CARD_TWO].Image = images.GetCardImage(PlayerC.CardAt(CARD_TWO));
                PlayerCards[CARD_FOUR].Image = images.GetCardImage(PSplitC.CardAt(CardHand.FIRST));
                PlayerCards[CARD_FIVE].Image = images.GetCardImage(PSplitC.CardAt(CARD_TWO));
                Application.DoEvents();
                SplitHand = true;
                FinishOffDealer();
            }
        }

        private void BtnInsurance_Click(object sender, EventArgs e)
        {
            int insCost = (int)Math.Round(Bet * 0.25);

            if (insCost > AmountWon)
            {
                MessageBox.Show(this,
                    string.Format("You don't have enough money for insurance (need {0}).", insCost),
                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                AmountWon -= insCost;
                DisplayPlayersBank();
                if (DealerC.CardAt(CardHand.FIRST).GetCardPointValueFace10() == 10)
                { // dealer had 21, only lose insurance
                    AmountWon += Bet;
                    ScoreIt(TWENTY_ONE, false, false, false, true);
                }
                else
                {
                    MessageBox.Show(this, "Dealer does not have 21.", this.Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnStay.Focus();
                    btnInsurance.Enabled = false;
                }
            }
        }

        private void MnuStats_Click(object sender, EventArgs e)
        {
            stats.ShowStatistics(this);
        }

        private void MnuHelp_Click(object sender, EventArgs e)
        {
            var asm = Assembly.GetEntryAssembly();
            var asmLocation = Path.GetDirectoryName(asm.Location);
            var htmlPath = Path.Combine(asmLocation, HTML_HELP_FILE);

            try
            {
                Process.Start(htmlPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Cannot load help: " + ex.Message, this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MnuAbout_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();

            about.ShowDialog(this);
            about.Dispose();
        }
        #endregion
    }
}
