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

namespace GIFView
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			if (args.Length > 0 && System.IO.File.Exists(args[0])) {
				// first to regconise if GIF98a & check the resolution of image
				// if still image then open it using Windows Photo Gallery instead
				//http://stackoverflow.com/questions/2848689/c-sharp-tell-static-gifs-apart-from-animated-ones
				System.Drawing.Image regconiseImageSize = System.Drawing.Image.FromFile(args[0]);
				
				System.Drawing.Imaging.FrameDimension FrameDimensions = 
			    new System.Drawing.Imaging.FrameDimension(regconiseImageSize.FrameDimensionsList[0]);
				
				int frames = regconiseImageSize.GetFrameCount(FrameDimensions);
				
				if (frames > 1) {
					int width = regconiseImageSize.Size.Width;
					int height = regconiseImageSize.Size.Height;
					regconiseImageSize.Dispose();
					
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new MainForm(args[0], width, height));
				} else {
					regconiseImageSize.Dispose();
					System.Diagnostics.Process.Start("rundll32.exe",  "\"" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) + "\\Windows Photo Viewer\\PhotoViewer.dll\", ImageView_Fullscreen " + args[0]);
					Application.Exit();
				}
			}
			else {
				// Proposed user-friendly file association function
				Application.Exit();
			}
		}
		
	}
}
