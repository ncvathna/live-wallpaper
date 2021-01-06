using System;
using System.Windows;
using System.IO;

namespace Live_Wallpaper
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private string videoPath = (string) Properties.Settings.Default["VideoPath"];
        public Settings()
        {
            InitializeComponent();
            slVolume.Value = (double)Properties.Settings.Default["VideoVolume"];
            txtPath.Text = Path.GetFileName(videoPath);

            btnChoose.Click += (o, e) =>
            {
                System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
                if (string.IsNullOrEmpty(videoPath)) fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                else fileDialog.InitialDirectory = Path.GetDirectoryName(videoPath);
                fileDialog.Filter = "MP4 videos | *.mp4";
                fileDialog.Multiselect = false;
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    videoPath = fileDialog.FileName;
                    txtPath.Text = Path.GetFileName(videoPath);
                }
            };
            btnCancel.Click += (o, e) =>
            {
                this.DialogResult = false;
                this.Close();
            };
            btnSave.Click += (o, e) =>
            {
                if (string.IsNullOrEmpty(videoPath))
                {
                    MessageBox.Show("Please set a video path.");
                    return;
                }
                Properties.Settings.Default["VideoPath"] = videoPath;
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
