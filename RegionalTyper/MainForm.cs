using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsInput;

namespace RegionalTyper
{
    public partial class MainForm : Form
    {
        private InputSimulator simulator = new InputSimulator();
        private LowLevelKeyboardHook lkh = new LowLevelKeyboardHook();
        private bool enableRegionalTyping = false;

        public MainForm()
        {
            InitializeComponent();
            lkh.OnKeyPressed += lkh_OnKeyPressed;
        }

        void lkh_OnKeyPressed(object sender, Keys e)
        {
            if (enableRegionalTyping && Char.IsLetter((char)e))
                ParseText(((char)e).ToString());
        }

        private void ParseText(string text)
        {
            string prefix = ":regional_indicator_";
            string regional = $"{prefix}{text.ToLower()}: ";

            lkh.UnHookKeyboard();
            simulator.Keyboard.TextEntry(regional);
            lkh.HookKeyboard();
        }

        private void btnToggle_Click(object sender, EventArgs e)
        {
            enableRegionalTyping = !enableRegionalTyping;

            if (enableRegionalTyping)
            {
                lkh.HookKeyboard();

                lblStatus.Text = "ACTIVE";
                lblStatus.BackColor = Color.Green;
            }
            else
            {
                lkh.UnHookKeyboard();

                lblStatus.Text = "INACTIVE";
                lblStatus.BackColor = Color.Red;
            }
        }
    }
}
