using System;
using System.Windows;

namespace Live_Wallpaper
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            slVolume.Value = (double)Properties.Settings.Default["VideoVolume"];
            txtPath.Text = (string)Properties.Settings.Default["VideoPath"];

            btnChoose.Click += (o, e) =>
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                if (string.IsNullOrEmpty(txtPath.Text)) fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                else fileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(txtPath.Text);
                fileDialog.Filter = "MP4 videos | *.mp4";
                fileDialog.Multiselect = false;
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) txtPath.Text = fileDialog.FileName;
            };
            btnCancel.Click += (o, e) =>
            {
                this.DialogResult = false;
                this.Close();
            };
            btnSave.Click += (o, e) =>
            {
                if (txtPath.Text == "")
                {
                    MessageBox.Show("Please set a video path.");
                    return;
                }
                Properties.Settings.Default["VideoPath"] = txtPath.Text;
                Properties.Settings.Default["VideoVolume"] = slVolume.Value;
                Properties.Settings.Default.Save();
                this.DialogResult = true;
                this.Close();
            };
            this.Closing += (o, e) =>
            {
                if ((string)Properties.Settings.Default["VideoPath"] == "")
                {
                    MessageBox.Show("Please set a video path and save.");
                    Application.Current.Shutdown();
                }
            };
        }
    }
}
