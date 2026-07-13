using SimpleBol.LVSorters;
using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using MongoDB.Bson;
using System.Globalization;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class ShipperContactEditorDialog : Form
    {

        public string ContactId { get; set; } = null!;
        public string ShipperId { get; set; } = null!;
        public SHIPPER Shipper { get; set; } = null!;
        public ShipperContacts ShipperContact { get; set; } = null!;



        public ShipperContactEditorDialog()
        {

            InitializeComponent();

        }

        #region Form

        protected void ShipperContactEditorDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.TopMost = true;

            if (ContactId != null)
            {
                if (ContactId != "ADD")
                {
                    if (ShipperContact != null)
                    {
                        textBoxName.Text = ShipperContact.ContactName;
                        textBoxEmailAddress.Text = ShipperContact.ContactEmailAddress;
                        maskedTextBoxPhoneNumber.Text = ShipperContact.ContactPhone;
                    }
                }
            }

            Cursor = Cursors.Default;

        }

        protected void ShipperContactEditorDialog_Shown(object sender, EventArgs e)
        {

            textBoxName.Select();

        }

        #endregion
        #region TextBoxes

        private void TextBoxName_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                textBoxName.SelectAll();
            });
        }

        private void TextBoxEmailAddress_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                textBoxEmailAddress.SelectAll();
            });
        }

        private void TextBoxPhoneNumber_Enter(object sender, EventArgs e)
        {
            _ = BeginInvoke((Action)delegate
            {
                maskedTextBoxPhoneNumber.SelectAll();
            });
        }

        private void TextBoxPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        #endregion
        #region TextBox Validators

        private void textBoxEmailValidating_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string email = textBox.Text;

            // Perform email address validation using regular expressions
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isValid = Regex.IsMatch(email, pattern);

            // Set error provider or display an error message if the email is invalid
            if (!isValid)
            {
                errorProvider1.SetError(textBox, "Invalid email address format");
                e.Cancel = true; // Prevents the focus from moving to the next control
            }
            else
            {
                errorProvider1.SetError(textBox, ""); // Clear error if valid
            }
        }

        #endregion
        #region Buttons

        private void OK_Button_Click(object sender, EventArgs e)
        {

            Cursor = Cursors.WaitCursor;
            var validate = SaveShipperContactAsync();
            Cursor = Cursors.Default;

            if (validate)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }

        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            this.DialogResult = DialogResult.Cancel;
            this.Close();

            Cursor = Cursors.Default;

        }

        #endregion
        #region LoadSave

        private bool SaveShipperContactAsync()
        {

            bool vFlag = true;

            // Contact Name
            if (textBoxName.Text.Length == 0)
            {
                vFlag = false;
                textBoxName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxName.BackColor = System.Drawing.SystemColors.Window;
            }

            // EmailAddress Address
            if (textBoxEmailAddress.Text.Length == 0)
            {
                vFlag = false;
                textBoxEmailAddress.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxEmailAddress.BackColor = System.Drawing.SystemColors.Window;
            }

            // Phone Number
            if (maskedTextBoxPhoneNumber.Text.Length == 0)
            {
                vFlag = false;
                maskedTextBoxPhoneNumber.BackColor = Color.LightSalmon;
            }
            else
            {
                maskedTextBoxPhoneNumber.BackColor = System.Drawing.SystemColors.Window;
            }

            if (vFlag)
            {
                var shipperContact = new ShipperContacts
                {
                    ShipperId = ContactId,
                    ContactId = MongoDB.Bson.ObjectId.GenerateNewId().ToString(),
                    ContactName = textBoxName.Text,
                    ContactEmailAddress = textBoxEmailAddress.Text.ToLower(),
                    ContactPhone = maskedTextBoxPhoneNumber.Text
                };

                ShipperContact = shipperContact;

            }

            return vFlag;

        }

        #endregion

    }
}
