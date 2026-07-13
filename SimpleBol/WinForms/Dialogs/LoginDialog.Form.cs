using System;
using SimpleBol.Classes.Common;
using SimpleBol.Repository.MongoDb;
using SimpleBol.Services;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class LoginDialog : Form
    {
        private readonly IAccountsRepository accountsRepository;
        private readonly ICurrentUserSession currentUserSession;

        public LoginDialog(
            IAccountsRepository accountsRepository,
            ICurrentUserSession currentUserSession)
        {
            InitializeComponent();
            this.accountsRepository = accountsRepository;
            this.currentUserSession = currentUserSession;
            AcceptButton = buttonSubmit;
        }

        #region Form


        #endregion
        #region Buttons

        private async void ButtonSubmit_Click(object sender, EventArgs e)
        {
            var loginId = textBox1.Text.Trim();
            var password = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(loginId) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Enter your user name and password.", "Sign In",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            buttonSubmit.Enabled = false;
            try
            {
                var account = await accountsRepository.GetAccountByLoginIdAsync(loginId);
                if (account is null ||
                    string.IsNullOrWhiteSpace(account.PasswordHashed) ||
                    account.PasswordSalt is null ||
                    !VerifyPassword(password, account.PasswordHashed, account.PasswordSalt))
                {
                    textBoxPassword.Clear();
                    MessageBox.Show("The user name or password is incorrect.", "Sign In",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxPassword.Focus();
                    return;
                }

                currentUserSession.SignIn(account);
                if (!string.IsNullOrWhiteSpace(account.AccountId))
                {
                    await accountsRepository.RecordSuccessfulLoginAsync(account.AccountId);
                    account.LastLogin = DateTime.UtcNow;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sign in could not be completed.{Environment.NewLine}{ex.Message}",
                    "Sign In", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonSubmit.Enabled = true;
            }
        }

        #endregion
        #region LoadSave

        private static bool VerifyPassword(string password, string hash, byte[] salt)
        {
            try
            {
                return EncryptDecryptAes.VerifyPassword(password, hash, salt);
            }
            catch (FormatException)
            {
                return false;
            }
        }




        #endregion
    }
}
