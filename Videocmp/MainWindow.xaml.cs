using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Windows.Input;

namespace Videocmp
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (sender, e) => this.DragMove();
            this.ResizeMode = ResizeMode.CanMinimize;
            this.KeyDown += (sender, e) =>
            {
                if (e.Key != Key.Enter) { return; }
                var direction = Keyboard.Modifiers == ModifierKeys.Shift ? FocusNavigationDirection.Previous : FocusNavigationDirection.Next;
                (FocusManager.GetFocusedElement(this) as FrameworkElement)?.MoveFocus(new TraversalRequest(direction));
            };

            //Videocmp.exeのパス
            Assembly myAssembly = Assembly.GetEntryAssembly();
            var videocmpPath = System.IO.Path.GetDirectoryName(myAssembly.Location);

            //Videocmp.exeと同じ場所にffmpeg.exeがない場合終了
            if (!System.IO.File.Exists(videocmpPath + @"\ffmpeg.exe"))
            {
                System.Windows.Forms.MessageBox.Show("必要なファイルが見つかりません。ツールを終了します。");
                Close();
            }

        }

        //参照ボタン押下
        private void Reference(object sender, RoutedEventArgs e)
        {
            //ファイル選択ダイアログ表示
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.Title = "ファイルを開く";
            dialog.Filter = "全てのファイル(*.*)|*.*";

            //対象ファイル欄と出力ファイル欄に選択したファイルを元に情報表示
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.input.Text = dialog.FileName;
                this.output.Text = System.IO.Path.GetDirectoryName(this.input.Text) + "\\" + System.IO.Path.GetFileNameWithoutExtension(this.input.Text) + ".mp4";
            }
        }

        private Storyboard _convertNow;
        private string outputFile = "";

        //変換ボタン押下
        public void Conversion(object sender, RoutedEventArgs e)
        {
            //対象ファイル存在チェック
            if (!System.IO.File.Exists(this.input.Text))
            {
                System.Windows.Forms.MessageBox.Show("対象ファイルが見つかりません。", "実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //.mp4からの変換の場合上書きになってしまうので、出力ファイル名を変更させる
            if (System.IO.Path.GetExtension(this.input.Text) == ".mp4" && this.input.Text == this.output.Text)
            {
                System.Windows.Forms.MessageBox.Show("対象ファイル名と出力ファイル名が同じです。\r\n出力ファイル名を変更してください。", "実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.output.Focus();
                return;
            }

            //出力ファイル存在チェック
            if (System.IO.File.Exists(this.output.Text))
            {
                System.Windows.Forms.MessageBox.Show("出力ファイルが既に存在します。\r\n出力ファイル名を変更してください。", "実行エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.output.Focus();
                return;
            }

            _convertNow = (Storyboard)this.Resources["convAnimeKey"];
            _convertNow.Begin();
            EnableCheck en = new EnableCheck();
            var regProcessor = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", false);

            try
            {
                //変換処理
                var process = new Process();

                process.StartInfo = new ProcessStartInfo
                {
                    Arguments = $"-i \"{this.input.Text}\" -vcodec libx264 -s vga -y \"{this.output.Text}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                };

                //レジストリキー確認、WinVer（64bit？32bit?)
                if ((string)regProcessor.GetValue("PROCESSOR_ARCHITECTURE") == "x86")
                {
                    process.StartInfo.FileName = @"C:\WM_BWK\ffmpeg_32.exe";
                }
                else
                {
                    process.StartInfo.FileName = @"C:\WM_BWK\ffmpeg.exe";
                }

                process.EnableRaisingEvents = true;
                process.Exited += new EventHandler(Process_Exited);
                process.Start();
                en.Enable = false;
                this.DataContext = en;
                outputFile = this.output.Text;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("変換処理に失敗しました");
                _convertNow.Stop();
                en.Enable = true;
                this.DataContext = en;
            }
        }

        //非同期で変換処理が終わるのを監視
        private void Process_Exited(object sender, EventArgs e)
        {
            _convertNow.Stop();
            Dispatcher.Invoke(new Action(() =>
            {
                EnableCheck en = new EnableCheck();
                en.Enable = true;
                this.DataContext = en;
            }));
            if (System.IO.File.Exists(outputFile))
            {
                System.Windows.Forms.MessageBox.Show("変換処理が完了しました。", "変換完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("変換処理に失敗しました。\r\n出力ファイル名が不正です。", "変換失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Dispatcher.Invoke(new Action(() =>
                {
                    this.output.Focus();
                }));
            }
        }

        //クリアボタン押下
        private void Clear(object sender, RoutedEventArgs e)
        {
            this.input.Text = "";
            this.output.Text = "";
        }

        //閉じるボタン押下
        private void Close(object sender, RoutedEventArgs e)
        {
            Process[] ps = Process.GetProcessesByName("ffmpeg");

            //変換途中だとffmpegのプロセスが残るので殺す
            if (ps.Length != 0)
            {
                foreach (Process p in ps)
                {
                    p.Kill();
                }
            }
            Close();
        }

        //最小化ボタン押下
        private void Minimize(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        //ファイルドラッグ時の見た目
        private void input_PreviewDragOver(object sender, System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop, true))
            {
                e.Effects = System.Windows.DragDropEffects.Copy;
            }
            else
            {
                e.Effects = System.Windows.DragDropEffects.None;
            }
            e.Handled = true;
        }

        //ファイルドラッグ時の動作
        private void input_Drop(object sender, System.Windows.DragEventArgs e)
        {
            var dropFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];
            if (dropFiles == null) return;
            this.input.Text = dropFiles[0];
            this.output.Text = System.IO.Path.GetDirectoryName(this.input.Text) + "\\" + System.IO.Path.GetFileNameWithoutExtension(this.input.Text) + ".mp4";
        }

        private void EnablePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Enable") return;

            var en = (EnableCheck)sender;

        }
    }

    //変換ボタン非活性化用コンバータ
    public class CBoolNegativeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0].ToString() == "" || values[1].ToString() == "" || values[2].ToString() == "False") { return false; }
            else { return true; }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => null;
    }

    //変換実施時isEnabled = falseにする用プロパティ定義
    public class EnableCheck : INotifyPropertyChanged
    {
        private bool _Enable;
        public bool Enable
        {
            get { return _Enable; }
            set
            {
                _Enable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Enabled"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
