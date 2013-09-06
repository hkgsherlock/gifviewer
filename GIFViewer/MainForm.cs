/*
 * Created by SharpDevelop.
 * User: Charlie Poon
 * Date: 19/5/2013
 * Time: 1:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace GIFView
{
	public partial class MainForm : Form
	{
		private Video _video = null;
		private int _width = new Int32();
		private int _height = new Int32();
		
		public MainForm(string filepath, int width, int height)
		{
			InitializeComponent();
			
			_width = width;
			_height = height;
			this.Text = System.IO.Path.GetFileName(filepath);
			
			// dispose of the old video to clean up resources
			if (_video != null)	
			{
				_video.Dispose();
			}
			// open a new video
			_video = new Video(filepath);
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			// assign the win form control that will contain the video
			_video.Owner = videoPanel;
			// resize to fit in the panel
			if (_width + this.Width - videoPanel.Width > Screen.PrimaryScreen.Bounds.Width) {
				this.Width = Screen.PrimaryScreen.Bounds.Width;
				this.Height = (int) (Screen.PrimaryScreen.Bounds.Width - this.Width - videoPanel.Width) * _height / _width;
			} else if (_height + this.Height - videoPanel.Height > Screen.PrimaryScreen.Bounds.Height) {
				this.Height = Screen.PrimaryScreen.Bounds.Height;
				this.Width = (int) (Screen.PrimaryScreen.Bounds.Height - this.Height - videoPanel.Height) * _width / _height;
			} else {
				this.Width = _width + this.Width - videoPanel.Width;
				this.Height = _height + this.Height - videoPanel.Height;
			}
			// play the first frame of the video so we can identify it
			_video.Play();
		}
	}
}
