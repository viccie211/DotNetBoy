using System;
using Eto.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoy.FrontEnd.Wpf
{
	class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			new Application(Eto.Platforms.Wpf).Run(new MainForm(new ServiceCollection()));
			
		}
	}
}
