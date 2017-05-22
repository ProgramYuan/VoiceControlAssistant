using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WpfIATCSharp
{
    public partial class MediaPlayer : Form
    {
        public MediaPlayer()
        {
            InitializeComponent();

            this.WindowState = FormWindowState.Minimized;
        }

        private void MediaPlayer_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer.currentPlaylist = axWindowsMediaPlayer.newPlaylist("aa", "");
            axWindowsMediaPlayer.currentPlaylist.appendItem(axWindowsMediaPlayer.newMedia("http://file.kuyinyun.com/group1/M00/30/6F/rBBGdVPRmxmAAz-SABeCfDMOjvI894.mp3"));
            axWindowsMediaPlayer.currentPlaylist.appendItem(axWindowsMediaPlayer.newMedia("http://file.kuyinyun.com/group1/M00/7D/3F/rBBGdFPRogiAaxX-ABd7vgVh7I0196.mp3"));
            axWindowsMediaPlayer.Ctlcontrols.play();
        }
    }
}
