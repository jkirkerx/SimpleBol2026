using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HoursOfOperationModel = SimpleBol.Models.MongoDb.HoursOfOperation;
using DailyHoursModel = SimpleBol.Models.MongoDb.DailyHours;

namespace SimpleBol.WinForms.Dialogs
{
    public partial class HoursOfOperation : Form
    {
        public HoursOfOperationModel? Schedule { get; set; }

        public HoursOfOperation()
        {
            InitializeComponent();
            SetDefaultTimes();
        }

        private void SetDefaultTimes()
        {
            var open = DateTime.Today.AddHours(8);
            var close = DateTime.Today.AddHours(17);

            dateTimePickerSundayOpen.Value = open;
            dateTimePickerSundayClose.Value = close;
            dateTimePickerMondayOpen.Value = open;
            dateTimePickerMondayClose.Value = close;
            dateTimePickerTuesdayOpen.Value = open;
            dateTimePickerTuesdayClose.Value = close;
            dateTimePickerWednesdayOpen.Value = open;
            dateTimePickerWednesdayClose.Value = close;
            dateTimePickerThursdayOpen.Value = open;
            dateTimePickerThursdayClose.Value = close;
            dateTimePickerFridayOpen.Value = open;
            dateTimePickerFridayClose.Value = close;
            dateTimePickerSaturdayOpen.Value = open;
            dateTimePickerSaturdayClose.Value = close;
        }

        private void HoursOfOperation_Load(object sender, EventArgs e)
        {
            var workingArea = Screen.FromControl(Owner ?? this).WorkingArea;

            Location = new Point(
                workingArea.Right - Width,
                workingArea.Top + ((workingArea.Height - Height) / 2));

            LoadSchedule();
        }

        private void LoadSchedule()
        {
            if (Schedule?.Days == null)
            {
                return;
            }

            SetPickerValues(DayOfWeek.Sunday, dateTimePickerSundayOpen, dateTimePickerSundayClose, checkBoxSundayClosed);
            SetPickerValues(DayOfWeek.Monday, dateTimePickerMondayOpen, dateTimePickerMondayClose, checkBoxMondayClosed);
            SetPickerValues(DayOfWeek.Tuesday, dateTimePickerTuesdayOpen, dateTimePickerTuesdayClose, checkBoxTuesdayClosed);
            SetPickerValues(DayOfWeek.Wednesday, dateTimePickerWednesdayOpen, dateTimePickerWednesdayClose, checkBoxWednesdayClosed);
            SetPickerValues(DayOfWeek.Thursday, dateTimePickerThursdayOpen, dateTimePickerThursdayClose, checkBoxThursdayClosed);
            SetPickerValues(DayOfWeek.Friday, dateTimePickerFridayOpen, dateTimePickerFridayClose, checkBoxFridayClosed);
            SetPickerValues(DayOfWeek.Saturday, dateTimePickerSaturdayOpen, dateTimePickerSaturdayClose, checkBoxSaturdayClosed);
        }

        private void SetPickerValues(
            DayOfWeek day,
            DateTimePicker openPicker,
            DateTimePicker closePicker,
            CheckBox closedCheckBox)
        {
            var dailyHours = Schedule!.Days.FirstOrDefault(hours => hours.Day == day);
            if (dailyHours == null)
            {
                return;
            }

            closedCheckBox.Checked = dailyHours.IsClosed;

            if (TimeOnly.TryParseExact(dailyHours.Open, "HH:mm", out var open))
            {
                openPicker.Value = DateTime.Today.Add(open.ToTimeSpan());
            }

            if (TimeOnly.TryParseExact(dailyHours.Close, "HH:mm", out var close))
            {
                closePicker.Value = DateTime.Today.Add(close.ToTimeSpan());
            }
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            Schedule = new HoursOfOperationModel
            {
                Days =
                [
                    CreateDailyHours(DayOfWeek.Sunday, dateTimePickerSundayOpen, dateTimePickerSundayClose, checkBoxSundayClosed),
                    CreateDailyHours(DayOfWeek.Monday, dateTimePickerMondayOpen, dateTimePickerMondayClose, checkBoxMondayClosed),
                    CreateDailyHours(DayOfWeek.Tuesday, dateTimePickerTuesdayOpen, dateTimePickerTuesdayClose, checkBoxTuesdayClosed),
                    CreateDailyHours(DayOfWeek.Wednesday, dateTimePickerWednesdayOpen, dateTimePickerWednesdayClose, checkBoxWednesdayClosed),
                    CreateDailyHours(DayOfWeek.Thursday, dateTimePickerThursdayOpen, dateTimePickerThursdayClose, checkBoxThursdayClosed),
                    CreateDailyHours(DayOfWeek.Friday, dateTimePickerFridayOpen, dateTimePickerFridayClose, checkBoxFridayClosed),
                    CreateDailyHours(DayOfWeek.Saturday, dateTimePickerSaturdayOpen, dateTimePickerSaturdayClose, checkBoxSaturdayClosed)
                ]
            };
        }

        private static DailyHoursModel CreateDailyHours(
            DayOfWeek day,
            DateTimePicker openPicker,
            DateTimePicker closePicker,
            CheckBox closedCheckBox)
        {
            return new DailyHoursModel
            {
                Day = day,
                IsClosed = closedCheckBox.Checked,
                Open = closedCheckBox.Checked ? null : openPicker.Value.ToString("HH:mm"),
                Close = closedCheckBox.Checked ? null : closePicker.Value.ToString("HH:mm")
            };
        }

        private void Closed_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkBoxSundayClosed)
                SetClosedState(checkBoxSundayClosed, dateTimePickerSundayOpen, dateTimePickerSundayClose);
            else if (sender == checkBoxMondayClosed)
                SetClosedState(checkBoxMondayClosed, dateTimePickerMondayOpen, dateTimePickerMondayClose);
            else if (sender == checkBoxTuesdayClosed)
                SetClosedState(checkBoxTuesdayClosed, dateTimePickerTuesdayOpen, dateTimePickerTuesdayClose);
            else if (sender == checkBoxWednesdayClosed)
                SetClosedState(checkBoxWednesdayClosed, dateTimePickerWednesdayOpen, dateTimePickerWednesdayClose);
            else if (sender == checkBoxThursdayClosed)
                SetClosedState(checkBoxThursdayClosed, dateTimePickerThursdayOpen, dateTimePickerThursdayClose);
            else if (sender == checkBoxFridayClosed)
                SetClosedState(checkBoxFridayClosed, dateTimePickerFridayOpen, dateTimePickerFridayClose);
            else if (sender == checkBoxSaturdayClosed)
                SetClosedState(checkBoxSaturdayClosed, dateTimePickerSaturdayOpen, dateTimePickerSaturdayClose);
        }

        private static void SetClosedState(
            CheckBox closedCheckBox,
            DateTimePicker openPicker,
            DateTimePicker closePicker)
        {
            openPicker.Enabled = !closedCheckBox.Checked;
            closePicker.Enabled = !closedCheckBox.Checked;
        }
    }
}
