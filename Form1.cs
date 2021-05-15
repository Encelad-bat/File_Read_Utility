using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Read_Utility
{
    public partial class Form1 : Form
    {
        Lazy<Label> input_path_of_file_Label = new Lazy<Label>();
        Lazy<TextBox> input_path_of_file_Textbox = new Lazy<TextBox>();
        Lazy<Label> progress_Label = new Lazy<Label>();
        Lazy<ProgressBar> progress_ProgressBar = new Lazy<ProgressBar>();
        Lazy<TextBox> file_text_TextBox = new Lazy<TextBox>();

        public Form1()
        {
            InitializeComponent();
        }

        private void File_Read_Utility_Load(object sender, System.EventArgs e)
        {
            this.input_path_of_file_Label.Value.Text = "Input path of file:";
            this.input_path_of_file_Label.Value.Location = new Point(0, 0);
            this.input_path_of_file_Label.Value.Font = new Font("Arial", 10);
            this.input_path_of_file_Label.Value.Width = 120;
            this.Controls.Add(this.input_path_of_file_Label.Value);

            this.input_path_of_file_Textbox.Value.Location = new Point(this.input_path_of_file_Label.Value.Width, 0);
            this.input_path_of_file_Textbox.Value.Size = new Size(this.Width - (this.input_path_of_file_Label.Value.Width + 30),this.input_path_of_file_Label.Value.Height);
            this.input_path_of_file_Textbox.Value.KeyPress += input_path_of_file_Textbox__Enter_KeyPress;
            this.Controls.Add(this.input_path_of_file_Textbox.Value);

            this.progress_Label.Value.Text = "Progress:";
            this.progress_Label.Value.Location = new Point(0, this.input_path_of_file_Label.Value.Height+20);
            this.progress_Label.Value.Font = new Font("Arial", 10);
            this.progress_Label.Value.Width = 120;
            this.Controls.Add(this.progress_Label.Value);

            this.progress_ProgressBar.Value.Location = new Point(this.progress_Label.Value.Width, this.progress_Label.Value.Location.Y);
            this.progress_ProgressBar.Value.Size = new Size(this.Width - (this.progress_Label.Value.Width + 30), this.progress_Label.Value.Height);
            this.Controls.Add(this.progress_ProgressBar.Value);

            this.file_text_TextBox.Value.Location = new Point(10,this.progress_Label.Value.Location.Y + this.progress_Label.Value.Height + 10);
            this.file_text_TextBox.Value.Size = new Size(this.Width - 40, this.Height - (this.file_text_TextBox.Value.Location.Y + 40));
            this.file_text_TextBox.Value.Multiline = true;
            this.file_text_TextBox.Value.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(this.file_text_TextBox.Value);
        }

        private void input_path_of_file_Textbox__Enter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                string path = (sender as TextBox).Text.Replace("/", "//");
                if (File.Exists(path))
                {
                    MessageBox.Show("Reading File...");
                    this.file_text_TextBox.Value.Text = File.ReadAllText(path);
                    this.file_text_TextBox.Value.Click += file_text_TextBox__Click;
                    this.progress_ProgressBar.Value.Maximum = this.file_text_TextBox.Value.GetLineFromCharIndex(this.file_text_TextBox.Value.Text.Length-1);
                }
                else
                {
                    MessageBox.Show("File does not exists!Check file path!", "Wrong path!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void file_text_TextBox__Click(object sender, EventArgs e)
        {
            this.progress_ProgressBar.Value.Value = (sender as TextBox).GetLineFromCharIndex((sender as TextBox).GetFirstCharIndexOfCurrentLine());
        }
    }
}
