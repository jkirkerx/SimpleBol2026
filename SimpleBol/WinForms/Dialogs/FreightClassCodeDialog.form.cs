using SimpleBol.Models.MongoDb;
using SimpleBol.Repository.MongoDb;
using System.Globalization;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class FreightClassCodeDialog : Form
    {

        public string FreightClassCodeId { get; set; } = null!;
        public FREIGHTCLASSCODES FreightClassCode { get; set; } = null!;

        private readonly IServiceScopeFactory serviceProvider;
        private readonly IFreightClassCodesRepository? freightClassCodesRepository;

        public FreightClassCodeDialog(
            IServiceScopeFactory serviceProvider,
            IFreightClassCodesRepository freightClassCodesRepository)
        {
            InitializeComponent();
            this.serviceProvider = serviceProvider;
            this.freightClassCodesRepository = freightClassCodesRepository;

        }

        #region Dialog

        protected void FreightClassCodeDialog_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

        }

        protected void FreightClassCodeDialog_Shown(object sender, EventArgs e)
        {
            if (this.FreightClassCodeId != "ADD")
            {
                LoadFreightClassCode(this.FreightClassCodeId);
            }
            else
            {
                this.FreightClassCode = new FREIGHTCLASSCODES();
            }

            this.textBoxName.Focus();

            Cursor = Cursors.Default;
        }

        #endregion
        #region LoadSave

        private async void LoadFreightClassCode(string freightClassCode)
        {
            if (freightClassCodesRepository != null)
            {
                var getFreightClassCode = await freightClassCodesRepository.GetOneFreightClassCodeAsync(freightClassCode);
                if (getFreightClassCode != null)
                {
                    this.FreightClassCode = getFreightClassCode;
                    textBoxName.Text = getFreightClassCode.Name;
                    textBoxDescription.Text = getFreightClassCode.Description;
                    textBoxCodeNumber.Text = getFreightClassCode.CodeNumber.ToString();
                    textBoxComments.Text = getFreightClassCode.Comment;
                    comboBoxWeightType.SelectedIndex = comboBoxWeightType.FindString(getFreightClassCode.WeightType);
                    textBoxMinimumWeightPerFoot.Text = getFreightClassCode.MinWeightPerfoot.ToString();
                    textBoxMaximumWeightPerFoot.Text = getFreightClassCode.MaxWeightPerFoot.ToString();
                    checkBoxEnabled.Checked = getFreightClassCode.Enabled == true ? true : false;
                    checkBoxMarkedAsDeleted.Checked = getFreightClassCode.IsDeleted == true ? true : false;

                }

            }

        }

        private async Task<bool> SaveFreightClassCode()
        {
            bool vFlag = true;

            if (this.FreightClassCode == null) { this.FreightClassCode = new FREIGHTCLASSCODES(); }

            // Freight Class Code Name
            if (textBoxName.Text.Length == 0)
            {
                vFlag = false;
                textBoxName.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxName.BackColor = System.Drawing.SystemColors.Window;
            }

            // Freight Class Code Description
            if (textBoxDescription.Text.Length == 0)
            {
                vFlag = false;
                textBoxDescription.BackColor = Color.LightSalmon;
            }
            else
            {
                textBoxDescription.BackColor = System.Drawing.SystemColors.Window;
            }

            // Freight Class Code Number
            if (textBoxCodeNumber.Text.Length == 0)
            {
                vFlag = false;
                textBoxCodeNumber.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = double.TryParse(textBoxCodeNumber.Text, out double value);
                if (result)
                {
                    textBoxCodeNumber.BackColor = System.Drawing.SystemColors.Window;
                    this.FreightClassCode.CodeNumber = value;
                }
                else
                {
                    vFlag = false;
                    textBoxCodeNumber.BackColor = Color.LightSalmon;
                }
            }

            // Freight Class Unit of Measurement Weight
            if (comboBoxWeightType.SelectedIndex < 0)
            {
                vFlag = false;
                comboBoxWeightType.BackColor = Color.LightSalmon;
            }
            else
            {
                comboBoxWeightType.BackColor = System.Drawing.SystemColors.Window;
            }

            // Freight Class Code Minimum Linear Lbs per square foot
            if (textBoxMinimumWeightPerFoot.Text.Length == 0)
            {
                vFlag = false;
                textBoxMinimumWeightPerFoot.BackColor = Color.LightSalmon;
            }
            else
            {
                // Check if number
                bool result = double.TryParse(textBoxMinimumWeightPerFoot.Text.Trim(), out double value);
                if (result)
                {
                    textBoxMinimumWeightPerFoot.BackColor = System.Drawing.SystemColors.Window;
                    this.FreightClassCode.MinWeightPerfoot = value;
                }
                else
                {
                    vFlag = false;
                    textBoxMinimumWeightPerFoot.BackColor = Color.LightSalmon;
                }
            }

            // Freight Class Code Maximum Linear Lbs per square foot
            if (textBoxMaximumWeightPerFoot.Text.Length == 0)
            {
                // The highest density class has no upper limit.
                this.FreightClassCode.MaxWeightPerFoot = null;
                textBoxMaximumWeightPerFoot.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                // Check if number
                bool result = double.TryParse(textBoxMaximumWeightPerFoot.Text.Trim(), out double value);
                if (result)
                {
                    textBoxMaximumWeightPerFoot.BackColor = System.Drawing.SystemColors.Window;
                    this.FreightClassCode.MaxWeightPerFoot = value;
                }
                else
                {
                    vFlag = false;
                    textBoxMaximumWeightPerFoot.BackColor = Color.LightSalmon;
                }
            }

            if (vFlag)
            {
                this.FreightClassCode.Name = textBoxName.Text.Trim();
                this.FreightClassCode.Description = textBoxDescription.Text.Trim();

                // Convert the Code Number to a double
                bool result1 = double.TryParse(textBoxCodeNumber.Text, out double codeNumber);
                if (result1) { this.FreightClassCode.CodeNumber = codeNumber; }

                // Unit of Minimum measurement
                bool result2 = double.TryParse(textBoxMinimumWeightPerFoot.Text, out double minNumber);
                if (result2) { this.FreightClassCode.MinWeightPerfoot = minNumber; }

                // Unit of Maximum measurement
                bool result3 = double.TryParse(textBoxMaximumWeightPerFoot.Text, out double maxNumber);
                this.FreightClassCode.MaxWeightPerFoot = result3 ? maxNumber : null;

                this.FreightClassCode.WeightType = comboBoxWeightType.Text;
                this.FreightClassCode.Comment = textBoxComments.Text.Trim();

                // Checkboxes
                this.FreightClassCode.Enabled = checkBoxEnabled.Checked == true ? true : false;
                this.FreightClassCode.IsDeleted = checkBoxMarkedAsDeleted.Checked == true ? true : false;

                this.FreightClassCode.UpdatedOnUtc = DateTime.UtcNow;

                if (freightClassCodesRepository != null)
                {
                    if (FreightClassCodeId == "ADD")
                    {

                        this.FreightClassCode.CreatedOnUtc = DateTime.UtcNow;
                        await freightClassCodesRepository.AddFreightClassCodeAsync(this.FreightClassCode);

                    }
                    else
                    {
                        await freightClassCodesRepository.UpdateFreightClassCodeAsync(this.FreightClassCode, this.FreightClassCodeId);
                    }
                }

            }

            return vFlag;
        }


        #endregion
        #region Buttons

        private async void OK_Button_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var validate = await SaveFreightClassCode();
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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
        #region ComboBoxes

        private void SetComboBoxesToWindowsSystemRegion()
        {

            string countryName = RegionInfo.CurrentRegion.DisplayName;
            if (countryName == "United States")
            {
                comboBoxWeightType.SelectedIndex = comboBoxWeightType.FindString("LBS");
            }
            else
            {
                comboBoxWeightType.SelectedIndex = comboBoxWeightType.FindString("KG");
            }

        }

        #endregion
        #region TextBoxes

        private void TextBoxCodeNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxMinimumWeightPerFoot_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!char.IsNumber(ch) && ch != 8 && ch != 46)  //8 is Backspace key; 46 is Delete key. This statement accepts dot key. 
            //if (!char.IsLetterOrDigit(ch) && !char.IsLetter(ch) && ch != 8 && ch != 46)   //This statement accepts dot key. 
            {
                e.Handled = true;
                MessageBox.Show("Only numbers are accepted.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void TextBoxMaximumWeightPerFoot_KeyPress(object sender, KeyPressEventArgs e)
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
        #region Checkboxes

        private void CheckBoxEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void CheckBoxMarkedAsDeleted_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
