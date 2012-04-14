using System;
using System.Windows.Forms;

namespace odbc
{
    public class TimeSpanColumn : DataGridViewColumn
    {
        public TimeSpanColumn()
            : base(new TimeSpanCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(TimeSpanCell)))
                {
                    throw new InvalidCastException("Must be a TimeSpanCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class TimeSpanCell : DataGridViewTextBoxCell
    {

        public TimeSpanCell()
            : base()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            TimeSpanEditingControl ctl = DataGridView.EditingControl as TimeSpanEditingControl;
            ctl.Value = (this.Value.ToString() != "") ? TimeSpan.Parse(Value.ToString()) : TimeSpan.Zero;
        }

        public override Type EditType
        {
            get { return typeof(TimeSpanEditingControl); }
        }

        public override Type ValueType
        {
            get { return typeof(TimeSpan); }
        }

        public override object DefaultNewRowValue
        {
            get { return TimeSpan.Zero; }
        }
    }

    class TimeSpanEditingControl : DomainUpDown, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        TimeSpan step = new TimeSpan(0, 5, 0);
        TimeSpan max = new TimeSpan(23, 55, 0);

        public TimeSpanEditingControl()
        {
            this.ReadOnly = true;
			for (TimeSpan i = max; i >= TimeSpan.Zero; i = i.Add(-step))
                this.Items.Add(i.ToString());
        }

        public TimeSpan Value
        {
            get { return TimeSpan.Parse(SelectedItem.ToString()); }
            set 
            {
                if (value != null && !value.GetType().Equals(typeof(TimeSpan)))
                {
                    throw new InvalidCastException("Must be a TimeSpan");
                }
				this.SelectedIndex = (int)((max.TotalMinutes - value.TotalMinutes) / step.TotalMinutes);
            }
        }

        public object EditingControlFormattedValue
        {
            get { return this.Value.ToString(); }
            set
            {
                if (value is String)
                    this.Value = TimeSpan.Parse((String)value);
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        public int EditingControlRowIndex
        {
            get { return rowIndex; }
            set { rowIndex = value; }
        }

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll) { }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        public DataGridView EditingControlDataGridView
        {
            get { return dataGridView; }
            set { dataGridView = value; }
        }

        public bool EditingControlValueChanged
        {
            get { return valueChanged; }
            set { valueChanged = value; }
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        protected override void OnTextChanged(EventArgs eventargs)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(eventargs);
        }
    }
}